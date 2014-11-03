using UnityEngine;
using System.Collections;

public class ShipInput : MonoBehaviour
{
    Camera cam;
    Vector3 velocity = Vector3.zero;

    float rotationSpeed = 1.0f;
    float accelForce = 0.5f; // how fast the ships speed increases
    float velocityMin = 0.0f; // reverse thrust
    float velocityMax = 200.0f;
    float velocityShake = 100.0f;
    float sensitivity = 100.0f;

    WeaponSystem weapons;

    void Start()
    {
        cam = Camera.main;
        rigidbody.freezeRotation = true;
        weapons = GameObject.FindGameObjectWithTag("WeaponSystem").GetComponent<WeaponSystem>();
    }

    void FixedUpdate()
    {
        Quaternion targetRotation;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        Plane plane;
        //RaycastHit hit;

        if (Input.GetMouseButton(0))
        {
            // http://docs.unity3d.com/ScriptReference/Plane.Raycast.html
            plane = new Plane(transform.forward, transform.position + transform.forward * sensitivity);

            float dist;
            //ray = cam.ScreenPointToRay(Input.mousePosition);
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
            //Vector3 hitPoint = ray.direction * fireDistance;
            //Debug.DrawRay(ray.origin, hitPoint, Color.white);
            //weapons.FirePrimary(hitPoint);
            weapons.FirePrimary();
        }

        if (Input.GetKey(KeyCode.E))
        {
            GameController.TargetSystem.RequestLock();
        }

        if (Input.GetKey(KeyCode.Alpha1))
        {
            weapons.FireSlot(1);
        }
        else if (Input.GetKey(KeyCode.Alpha2))
        {
            weapons.FireSlot(2);
        }
        else if (Input.GetKey(KeyCode.Alpha3))
        {
            weapons.FireSlot(3);
        }
        else if (Input.GetKey(KeyCode.Alpha4))
        {
            weapons.FireSlot(4);
        }
    }

    private Vector3 originPosition;
    private Quaternion originRotation;
    public float shake_decay;
    public float shake_intensity;

    void LateUpdate()
    {
        // http://www.mikedoesweb.com/2012/camera-shake-in-unity/
        if (velocity.magnitude > velocityShake)
        {
            originPosition = transform.position;
            originRotation = transform.rotation;
            shake_intensity = (velocity.magnitude - velocityShake) / 4000;
            shake_decay = 0.002f;
        }

        if (shake_intensity > 0)
        {
            transform.position = originPosition + Random.insideUnitSphere * shake_intensity;
            transform.rotation = new Quaternion(
            originRotation.x + Random.Range(-shake_intensity, shake_intensity) * .2f,
            originRotation.y + Random.Range(-shake_intensity, shake_intensity) * .2f,
            originRotation.z + Random.Range(-shake_intensity, shake_intensity) * .2f,
            originRotation.w + Random.Range(-shake_intensity, shake_intensity) * .2f);
            shake_intensity -= shake_decay;
        }
    }

    public float GetVelocityPercentage()
    {
        return this.velocity.magnitude / velocityMax;
    }
}