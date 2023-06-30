using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bee : MonoBehaviour
{
    public float speed = 4f;
    public float pointDistance;
    int pointNumber;
    Rigidbody2D rb;
    Direction d;
    Animator a;
    DamageHealth dmg;
    Transform nextPoint;
    public DetectionZone zone;
    public List<Transform> points;
    public bool hasTarget = false;

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

    private void Fly()
    {
        Vector2 directionToWaypoint = (nextPoint.position - transform.position).normalized;
        float d = Vector2.Distance(directionToWaypoint, transform.position);
        rb.velocity = directionToWaypoint * speed;
        if (d <= pointDistance)
        {
            pointNumber++;
            if (pointNumber >= points.Count)
            {
                pointNumber = 0;
            }
            nextPoint = points[pointNumber];
        }
    }

    private void Awake()
    {
        a = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        dmg = GetComponent<DamageHealth>();
    }

    void Start()
    {
        nextPoint = points[pointNumber];
    }

    // Update is called once per frame
    void Update()
    {
        HasTarget = zone.detected.Count > 0;
    }

    private void FixedUpdate()
    {
        if (dmg.IsAlive)
        {
            if (CanMove)
            {
                Fly();
            }
            else{
                rb.velocity = Vector3.zero;
            }
        }
    }
}
