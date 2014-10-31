using UnityEngine;
using System.Collections;

public class ShipInput : MonoBehaviour
{
    Camera cam;
    Vector3 velocity = Vector3.zero;

    float rotationSpeed = 1.0f;
    float accelForce = 5f; // how fast the ships speed increases
    float velocityMin = 0.0f; // reverse thrust
    float velocityMax = 2000.0f;
    float sensitivity = 100.0f;

    float shield = 100.0f;
    float shieldMax = 100.0f;

    float fireDistance = 1000.0f;

    // systems
    WeaponSystem weapons;

    void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").camera;
        rigidbody.freezeRotation = true;

        weapons = GameObject.FindGameObjectWithTag("WeaponSystem").GetComponent<WeaponSystem>();
    }

    void Update()
    {
        Quaternion targetRotation;
        Ray ray;
        Plane plane;

        if (Input.GetMouseButton(0))
        {
            // http://docs.unity3d.com/ScriptReference/Plane.Raycast.html
            plane = new Plane(transform.forward, transform.position + transform.forward * sensitivity);

            float dist;
            ray = cam.ScreenPointToRay(Input.mousePosition);
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

        // move camera in/out
        if (Input.GetAxis("Vertical") > 0 && velocity.magnitude < velocityMax)
        {
            cam.GetComponent<ShipCamera>().SetAccelerating();
        }
        else if (Input.GetAxis("Vertical") < 0 && velocity.magnitude > velocityMin)
        {
            cam.GetComponent<ShipCamera>().SetDecelerating();
        }
        else
        {
            cam.GetComponent<ShipCamera>().SetNeutral();
        }

        // local to world space
        this.rigidbody.velocity = this.transform.TransformDirection(velocity);

        if (Input.GetMouseButton(1))
        {
            // from camera through mouse click position
            ray = cam.ScreenPointToRay(Input.mousePosition);
            // the endpoint of the ray
            Vector3 hitPoint = ray.direction * fireDistance;

            Debug.DrawRay(ray.origin, hitPoint, Color.red);
            //Debug.Log(ray.direction * fireDistance);

            // this checks if the ray actually hit something
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                Debug.DrawRay(ray.origin, ray.direction * fireDistance, Color.green);
            }

            // fire weapons at target point
            weapons.FireAll(hitPoint);
        }
    }

    void OnTriggerEnter(Collider c)
    {
        //shield -= 11;

    }

    public float GetShield()
    {
        return shield;
    }

    public float GetShieldMax()
    {
        return shieldMax;
    }

    public float GetVelocityMax()
    {
        return velocityMax;
    }
}