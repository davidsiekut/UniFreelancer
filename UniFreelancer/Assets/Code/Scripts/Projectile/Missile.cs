using UnityEngine;
using System.Collections;

public class Missile : MonoBehaviour
{
    public GameObject Explosion;
    [HideInInspector]
    public float Damage;
    [HideInInspector]
    public Vector3 Direction;
    [HideInInspector]
    public float Range = 100f;

    Vector3 target;
    float speed = 2.5f;

    float lifetime = 10f;

	void Start()
    {
        Quaternion rotation = Quaternion.LookRotation(GameController.Player.transform.forward);
        transform.rotation = rotation;
        target = GameController.Player.transform.forward * Range;
	}

    void FixedUpdate()
    {
        rigidbody.velocity = target * speed;
        //Debug.DrawRay(this.transform.position, Direction.normalized * 5.0f, Color.cyan);

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
    }

    void OnCollisionEnter(Collision collision)
    {
        //Debug.Log(collision.gameObject.name + " hit by missile");

        if (collision.gameObject.GetComponent<Entity>() != null)
        {
            collision.gameObject.GetComponent<Entity>().TakeDamage(Damage);
        }
        else if (collision.gameObject.GetComponent<Asteroid>() != null)
        {
            collision.gameObject.GetComponent<Asteroid>().TakeDamage(Damage);
        }

        GameObject g = GameObject.Instantiate(Explosion) as GameObject;
        g.transform.position = this.transform.position;

        Destroy(gameObject);
    }
}
