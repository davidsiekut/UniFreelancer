using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour
{
    public GameObject Particle;

    Vector3 vel;

	void Start()
    {
	}

    void FixedUpdate()
    {
        rigidbody.AddRelativeForce(vel);
    }


    public void Fire(Vector3 v)
    {
        vel = v;
    }

    void OnTriggerEnter(Collider c)
    {
        if (c.tag != "Player")
        {
            GameObject g = GameObject.Instantiate(Particle) as GameObject;
            g.transform.position = this.transform.position;
            GameObject.Destroy(this.gameObject);
        }
    }
}
