using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D),typeof(Direction), typeof(DamageHealth))]
public class Goblin : MonoBehaviour
{
    public float speed = 3f;
    public float stopRate = 0.6f;
    Rigidbody2D rb;
    Direction d;
    Animator a;
    DamageHealth dmg;
    public enum WalkableDirection { Right, Left }
    private WalkableDirection walkDirection;
    private Vector2 walkDirectionVector;
    public DetectionZone zone;
    public DetectionZone groundDetection;
    public bool hasTarget = false;

    public WalkableDirection WalkDirection
    {
        get
        {
            return walkDirection;
        }
        private set
        {
            if (walkDirection != value)
            {
                gameObject.transform.localScale = new Vector2(gameObject.transform.localScale.x * -1, gameObject.transform.localScale.y);
                if (value == WalkableDirection.Right)
                {
                    walkDirectionVector = Vector2.right;
                }
                else if (value == WalkableDirection.Left)
                {
                    walkDirectionVector = Vector2.left;
                }
            }
            walkDirection = value;
        }
    }

    private void Flip()
    {
        if (WalkDirection == WalkableDirection.Right)
        {
            WalkDirection = WalkableDirection.Left;
        }
        else if (WalkDirection == WalkableDirection.Left)
        {
            WalkDirection = WalkableDirection.Right;
        }
        else
        {
            Debug.LogError("Walkable direction is neither left or right");
        }
    }

    public bool HasTarget
    {
        get
        {
            return hasTarget;
        }
        private set
        {
            hasTarget = value;
            a.SetBool("hasTarget", value);
        }
    }

    public bool CanMove
    {
        get
        {
            return a.GetBool("canMove");
        }
    }

    public void OnHit (int damage, Vector2 knockback)
    {
        rb.velocity = new Vector2(knockback.x, rb.velocity.y + knockback.y);
    }

    public float AttackCooldown
    {
        get
        {
            return a.GetFloat("attackCooldown");
        }
        private set
        {
            a.SetFloat("attackCooldown", Mathf.Max(value, 0));
        }
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        d = GetComponent<Direction>();
        a = GetComponent<Animator>();
        dmg = GetComponent<DamageHealth>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HasTarget = zone.detected.Count > 0;
        if (AttackCooldown > 0)
        {
            AttackCooldown -= Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        if (d.IsOnWall && d.IsGrounded || groundDetection.detected.Count == 0)
        {
            Flip();
        }
        if (!dmg.Lock)
        {
            if (CanMove)
            {
                rb.velocity = new Vector2(speed * walkDirectionVector.x, rb.velocity.y);
            }
            else
            {
                rb.velocity = new Vector2(Mathf.Lerp(rb.velocity.x, 0, stopRate), rb.velocity.y);
            }
        }
    }
}
