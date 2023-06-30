using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthItem : MonoBehaviour
{
    public int restore = 10;
    DamageHealth dmg;

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D col)
    {
        dmg = col.GetComponent<DamageHealth>();
        if ((dmg.Health < dmg.MaxHealth) )
        {
            dmg.Heal(restore);
            Destroy(gameObject);
            dmg.HealthBar();
        }
    }
}
