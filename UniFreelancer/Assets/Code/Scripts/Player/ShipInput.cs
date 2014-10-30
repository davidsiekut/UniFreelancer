using UnityEngine;
using System.Collections;

public class ShipInput : MonoBehaviour
{
    Camera camera;
    Vector3 velocity = Vector3.zero;

    float rotationSpeed = 1.0f;
    float accelForce = 1f; // how fast the ships speed increases
    float velocityMin = 0.0f; // reverse thrust
    float velocityMax = 100.0f;
    float sensitivity = 100.0f;

    void Start()
    {
        camera = GameObject.FindGameObjectWithTag("MainCamera").camera;
        rigidbody.freezeRotation = true;
    }

    void FixedUpdate()
    {
        Quaternion targetRotation;

        if (Input.GetMouseButton(0))
        {
            // http://docs.unity3d.com/ScriptReference/Plane.Raycast.html
            Plane plane = new Plane(transform.forward, transform.position + transform.forward * sensitivity);

            float dist;
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            plane.Raycast(ray, out dist);

            Vector3 relativePos = (ray.origin + ray.direction * dist) - transform.position;
            targetRotation = Quaternion.LookRotation(relativePos, transform.up);
        }
        else
        {
            // reorient to xz axis
            targetRotation = Quaternion.LookRotation(this.transform.forward, Vector3.up);
        }

        // interpolate rotation nicely
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);

        // move forward or backward
        velocity.z += Input.GetAxis("Vertical") * accelForce;
        velocity.z = Mathf.Clamp(velocity.z, velocityMin, velocityMax);

        // local to world space
        this.rigidbody.velocity = this.transform.TransformDirection(velocity);
    }
}