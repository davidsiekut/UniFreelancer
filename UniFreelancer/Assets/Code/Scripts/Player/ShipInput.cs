using UnityEngine;
using System.Collections;

public class ShipInput : MonoBehaviour
{
    public bool CanControl = false;

    Camera cam;
    Vector3 velocity = Vector3.zero;

    float rotationSpeed = 1.5f;
    float accelForce = 0.5f; // how fast the ships speed increases
    float velocityMin = 0.0f; // reverse thrust
    float velocityMax = 200.0f;
    float velocityShake = 160.0f;
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

        if (Input.GetMouseButton(0) && CanControl)
        {
            // http://docs.unity3d.com/ScriptReference/Plane.Raycast.html
            plane = new Plane(transform.forward, transform.position + transform.forward * sensitivity);
            //DrawPlane(transform.position + transform.forward * 10f, transform.forward);
            
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
        velocity.z += Input.GetAxis("Vertical") * accelForce * (CanControl ? 1 : 0);
        velocity.z = Mathf.Clamp(velocity.z, velocityMin, velocityMax);

        // move camera in/out
        if (Input.GetAxis("Vertical") > 0 && velocity.magnitude < velocityMax)
        {
            //cam.GetComponent<ShipCamera>().SetAccelerating();
        }
        else if (Input.GetAxis("Vertical") < 0 && velocity.magnitude > velocityMin)
        {
            //cam.GetComponent<ShipCamera>().SetDecelerating();
        }
        else
        {
            //cam.GetComponent<ShipCamera>().SetNeutral();
        }

        // local to world space
        this.rigidbody.velocity = this.transform.TransformDirection(velocity);

        if (CanControl)
        {
            if (Input.GetMouseButton(1))
            {
                weapons.FirePrimary(ray);
            }

            if (Input.GetKey(KeyCode.E))
            {
                GameController.TargetSystem.RequestLock();
            }
            if (Input.GetKey(KeyCode.Alpha1))
            {
                weapons.FireSlot(1);
            }
            if (Input.GetKey(KeyCode.Alpha2))
            {
                weapons.FireSlot(2);
            }
            if (Input.GetKey(KeyCode.Alpha3))
            {
                weapons.FireSlot(3);
            }
            if (Input.GetKey(KeyCode.Alpha4))
            {
                weapons.FireSlot(4);
            }
        }
    }

    private Vector3 originPosition;
    private Quaternion originRotation;
    private float shake_decay;
    private float shake_intensity;

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

    void DrawPlane(Vector3 position, Vector3 normal)
    {

        Vector3 v3;

        if (normal.normalized != Vector3.forward)
            v3 = Vector3.Cross(normal, Vector3.forward).normalized * normal.magnitude;
        else
            v3 = Vector3.Cross(normal, Vector3.up).normalized * normal.magnitude; ;

        var corner0 = position + v3;
        var corner2 = position - v3;

        var q = Quaternion.AngleAxis(90.0f, normal);
        v3 = q * v3;
        var corner1 = position + v3;
        var corner3 = position - v3;

        Debug.DrawLine(corner0, corner2, Color.green);
        Debug.DrawLine(corner1, corner3, Color.green);
        Debug.DrawLine(corner0, corner1, Color.green);
        Debug.DrawLine(corner1, corner2, Color.green);
        Debug.DrawLine(corner2, corner3, Color.green);
        Debug.DrawLine(corner3, corner0, Color.green);
        Debug.DrawRay(position, normal, Color.red);
    }
}