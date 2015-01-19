using UnityEngine;
using System.Collections;

public class Rotate : MonoBehaviour
{
    public Vector3 Velocity = new Vector3(0, 0, 0);

	void Start()
    {
	}
	
	void FixedUpdate()
    {
        Quaternion deltaRotation = Quaternion.Euler(Velocity * Time.deltaTime);
        rigidbody.MoveRotation(rigidbody.rotation * deltaRotation);
	}
}