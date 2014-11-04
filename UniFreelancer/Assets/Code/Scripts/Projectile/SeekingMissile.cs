using UnityEngine;
using System.Collections;

public class SeekingMissile: MonoBehaviour
{
    public GameObject target;
    public float Damage;
    float awake = 0.0f;
    float speed = 10.0f;
    float homingSensitivity = 0.4f;
    public GameObject Explosion;

	void Start()
    {
        Quaternion rotation = Quaternion.LookRotation(GameController.Player.transform.forward);
        transform.rotation = rotation;
	}
	
	void FixedUpdate()
    {
        if (target != null && awake < 0)
        {
            Vector3 relativePos = target.transform.position - transform.position;
            Quaternion rotation = Quaternion.LookRotation(relativePos);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, homingSensitivity);
            rigidbody.velocity = relativePos * 1.1f;
        }
        else
        {
            awake -= Time.deltaTime;
            rigidbody.AddRelativeForce(GameController.Player.rigidbody.velocity + 50.0f * this.transform.forward);
        }

        Debug.DrawRay(this.transform.position, this.transform.forward * 5.0f);
    }

    void OnCollisionEnter(Collision collision)
    {
        //ContactPoint contact = collision.contacts[0];
        //Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
        //Vector3 pos = contact.point;

        if (collision.gameObject.GetComponent<Entity>() != null)
        {
            collision.gameObject.GetComponent<Entity>().TakeDamage(Damage);
        }

        GameObject g = GameObject.Instantiate(Explosion) as GameObject;
        g.transform.position = this.transform.position;

        Destroy(gameObject);
    }
}
