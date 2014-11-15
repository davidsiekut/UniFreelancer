using UnityEngine;
using System.Collections;

public class Countermeasure : MonoBehaviour
{
    public GameObject Explosion;
    [HideInInspector]
    public Vector3 Target;

    float awake = 0.5f;
    float speed = 1.8f;
    float homingSensitivity = 1f;
    float activate = 2f;

    void Start()
    {
        //Quaternion rotation = Quaternion.LookRotation(GameController.Player.transform.forward);
        //transform.rotation = rotation;
    }

    void FixedUpdate()
    {
        if (Target != Vector3.zero)
        {
            Vector3 relativePos = Target - transform.position;
            Quaternion rotation = Quaternion.LookRotation(relativePos);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, homingSensitivity);
            rigidbody.velocity = relativePos * speed;
        }

        if (awake > 0)
            awake -= Time.deltaTime;

        if (activate > 0)
        {
            activate -= Time.deltaTime;
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
        if (awake < 0)
        {
            GameObject g = GameObject.Instantiate(Explosion) as GameObject;
            g.transform.position = this.transform.position;
            Destroy(gameObject);
        }
    }
}
