using UnityEngine;
using System.Collections;

public class ShipVitals : MonoBehaviour
{
	void Start()
    {
	}

	void Update()
    {
	}

    void OnCollisionEnter(Collision c)
    {
        Debug.Log("SHIP HIT");
    }
}
