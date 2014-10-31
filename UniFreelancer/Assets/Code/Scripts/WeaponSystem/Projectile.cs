using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour
{
    public GameObject Particle;

    public Vector3 target;

	void Start()
    {
	}

	void Update()
    {
        //this.transform.position = Vector3.MoveTowards(this.transform.position, target, 1000*Time.deltaTime);
	}

    void OnTriggerEnter(Collider c)
    {
        if (c.tag == "Respawn")
        {
            GameObject g = GameObject.Instantiate(Particle) as GameObject;
            g.transform.position = this.transform.position;
            GameObject.Destroy(this.gameObject);
        }
    }
}
