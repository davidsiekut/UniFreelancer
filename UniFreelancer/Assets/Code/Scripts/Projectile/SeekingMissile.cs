using UnityEngine;
using System.Collections;

public class SeekingMissile: MonoBehaviour
{
    public GameObject Explosion;
    public AudioClip Impact;
    //[HideInInspector]
    public GameObject Target;
    [HideInInspector]
    public float Damage;

    float awake = 1.0f;
    //float awakeSpeed = 50f;
    float speed = 4.5f;
    float homingSensitivity = 0.7f;

    float lifetime = 15f;

	void Start()
    {
        //Quaternion rotation = Quaternion.LookRotation(GameController.Player.transform.forward);
        //transform.rotation = rotation;
	}
	
	void FixedUpdate()
    {
        if (Target != null)
        {
            Vector3 relativePos = Target.transform.position - transform.position;
            Quaternion rotation = Quaternion.LookRotation(relativePos);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, homingSensitivity);
            rigidbody.velocity = relativePos * speed;
        }

        if (awake > 0)
            awake -= Time.deltaTime;

        if (lifetime > 0)
        {
            lifetime -= Time.deltaTime;
        }
        else
        {
            GameObject g = GameObject.Instantiate(Explosion) as GameObject;
            g.transform.position = this.transform.position;
            Destroy(gameObject);
        }

        if (Target != null && Vector3.Distance(Target.transform.position, this.transform.position) < 5)
        {
            if (Target.GetComponent<Entity>() != null)
            {
                Target.GetComponent<Entity>().TakeDamage(Damage);
                GameController.PlaySoundAtPlayer(Impact, this.transform.position);

                GameObject g = GameObject.Instantiate(Explosion) as GameObject;
                g.transform.position = this.transform.position;
                Destroy(gameObject);
            }
        }


        //Debug.DrawRay(this.transform.position, this.transform.forward * 5.0f, Color.cyan);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.name == "Flare(Clone)")
        {
            Target = other.gameObject;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        //ContactPoint contact = collision.contacts[0];
        //Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
        //Vector3 pos = contact.point;

        if (awake < 0)
        {
            if (collision.gameObject.GetComponent<Entity>() != null)
            {
                collision.gameObject.GetComponent<Entity>().TakeDamage(Damage);
            }
            else if (collision.gameObject.GetComponent<Asteroid>() != null)
            {
                collision.gameObject.GetComponent<Asteroid>().TakeDamage(Damage);
            }

            GameController.PlaySoundAtPlayer(Impact, this.transform.position);

            GameObject g = GameObject.Instantiate(Explosion) as GameObject;
            g.transform.position = this.transform.position;
            Destroy(gameObject);
        }
    }
}
