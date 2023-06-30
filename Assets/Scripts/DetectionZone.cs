using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DetectionZone : MonoBehaviour
{
    public UnityEvent NoColliders;

    Collider2D col;
    public List<Collider2D> detected = new List<Collider2D>();

    private void Awake()
    {
        col = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        detected.Add(collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        detected.Remove(collision);

        if (detected.Count <= 0 )
        {
            NoColliders.Invoke();
        }
    }
}
