using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    public Camera cam;
    public Transform followTarget;
    Vector2 startingPosition;
    float startingZ;
    Vector2 camMovement => (Vector2) cam.transform.position - startingPosition;
    float parallax => Mathf.Abs(distanceFromTarget) / clippingPlane;
    float distanceFromTarget => transform.position.z - followTarget.transform.position.z;
    float clippingPlane => (cam.transform.position.z + (distanceFromTarget > 0 ? cam.farClipPlane : cam.nearClipPlane));
    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
        startingZ = transform.position.z;
    }

    void FixedUpdate()
    {
        Vector2 newPosition = startingPosition + camMovement * parallax;
        transform.position = new Vector3(newPosition.x, newPosition.y, startingZ);
    }
}
