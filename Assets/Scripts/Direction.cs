using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Direction : MonoBehaviour
{
    CapsuleCollider2D col;
    public ContactFilter2D cf;
    RaycastHit2D[] gh = new RaycastHit2D[5];
    RaycastHit2D[] wh = new RaycastHit2D[5];
    RaycastHit2D[] ch = new RaycastHit2D[5];
    Animator a;
    public float groundDistance = 0.05f;
    public float wDistance = 0.2f;
    public float cDistance = 0.05f;
    private Vector2 wcDirection => gameObject.transform.localScale.x > 0 ? Vector2.right : Vector2.left;
    [SerializeField]
    private bool isGrounded;
    [SerializeField]
    private bool isOnWall;
    [SerializeField]
    private bool isOnCeiling;

    public bool IsGrounded
    {
        get
        {
            return isGrounded;
        }
        private set
        {
            isGrounded = value;
            a.SetBool("isGrounded", value);
        }
    }

    public bool IsOnWall
    {
        get
        {
            return isOnWall;
        }
        private set
        {
            isOnWall = value;
            a.SetBool("isOnWall", value);
        }
    }

    public bool IsOnCeiling
    {
        get
        {
            return isOnCeiling;
        }
        private set
        {
            isOnCeiling = value;
            a.SetBool("isOnCeiling", value);
        }
    }

    private void Awake()
    {
        col = GetComponent<CapsuleCollider2D>();
        a = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        IsGrounded = col.Cast(Vector2.down, cf, gh, groundDistance) > 0;
        IsOnWall = col.Cast(wcDirection, cf, wh, wDistance) > 0;
        IsOnCeiling = col.Cast(Vector2.up, cf, ch, cDistance) > 0;
    }

    private void FixedUpdate()
    {
        
    }
}
