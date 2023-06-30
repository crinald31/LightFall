using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D), typeof(Direction), typeof(DamageHealth))]
public class PlayerController : MonoBehaviour
{
    private Vector2 playerMove;
    private Rigidbody2D rb;
    private Animator animator;
    private Direction direction;
    private DamageHealth dmg;
    private Collider2D collider;

    [SerializeField] private bool isMoving = false;
    [SerializeField] private float jumpImpulse = 10f;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float airSpeed = 5f;
    [SerializeField] private bool isFacingRight = true;

    public float CurrentMoveSpeed
    {
        get
        {
            if (dmg.IsAlive && CanMove)
            {
                if (IsMoving && !direction.IsOnWall)
                {
                    if (direction.IsGrounded)
                    {
                        return speed;
                    }
                    else
                    {
                        return airSpeed;
                    }
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                return 0;
            }
        }
    }

    public bool IsMoving
    {
        get { return isMoving; }
        private set
        {
            isMoving = value;
            animator.SetBool("isMoving", value);
        }
    }

    public bool CanMove
    {
        get { return animator.GetBool("canMove"); }
    }

    public bool IsFacingRight
    {
        get { return isFacingRight; }
        private set
        {
            if (isFacingRight != value)
            {
                transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            }
            isFacingRight = value;
        }
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        direction = GetComponent<Direction>();
        dmg = GetComponent<DamageHealth>();
        collider = GetComponent<Collider2D>();
    }

    private void FixedUpdate()
    {
        if (!dmg.Lock)
        {
            rb.velocity = new Vector2(playerMove.x * CurrentMoveSpeed, rb.velocity.y);
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        playerMove = context.ReadValue<Vector2>();
        IsMoving = playerMove != Vector2.zero;

        if (context.started)
        {
            IsMoving = true;
        }
        else if (context.canceled)
        {
            IsMoving = false;
        }

        SetDirection(playerMove);
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started && direction.IsGrounded && CanMove)
        {
            animator.SetTrigger("jump");
            rb.velocity = new Vector2(rb.velocity.x, jumpImpulse);
        }
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            animator.SetTrigger("attack");
        }
    }

    public void OnHit(int damage, Vector2 knockback)
    {
        rb.velocity = new Vector2(knockback.x, rb.velocity.y + knockback.y);
    }

    private void SetDirection(Vector2 playerMove)
    {
        if (playerMove.x > 0 && !IsFacingRight)
        {
            IsFacingRight = true;
        }
        else if (playerMove.x < 0 && IsFacingRight)
        {
            IsFacingRight = false;
        }
    }
}