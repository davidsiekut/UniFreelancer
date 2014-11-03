using UnityEngine;
using System.Collections;

public class Entity : MonoBehaviour
{
    public float Shield;
    public float Health;
    public GameObject Explosion;

	void Start()
    {
	}
	
	void Update()
    {
        if (Health < 0)
        {
            GameObject g = GameObject.Instantiate(Explosion) as GameObject;
            g.transform.position = this.transform.position;

            GameController.Console.Add("target destroyed");

            Destroy(gameObject);
        }
	}

    void OnCollisionEnter(Collision c)
    {
        this.rigidbody.AddExplosionForce(50f, this.transform.forward * 2f, 5f);
        Debug.Log(this.name + " hit by " + c.transform.name);
    }
}
