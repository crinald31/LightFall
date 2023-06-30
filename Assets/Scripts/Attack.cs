using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    private Collider2D attackCollider;
    public int attackDamage = 10;
    public Vector2 knockback = Vector2.zero;

    private void Awake()
    {
        attackCollider = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DamageHealth damageable = collision.GetComponent<DamageHealth>();
        if (damageable != null)
        {
            Vector2 deliveredKnockback = GetDeliveredKnockback();
            damageable.Hit(attackDamage, deliveredKnockback);
            Debug.Log(collision.name + " hit for " + attackDamage);
        }
    }

    private Vector2 GetDeliveredKnockback()
    {
        if (transform.parent.localScale.x > 0)
        {
            return knockback;
        }
        else
        {
            return new Vector2(-knockback.x, knockback.y);
        }
    }
}