﻿using UnityEngine;
using System.Collections;

public class SeekingMissile: MonoBehaviour
{
    public GameObject Explosion;
    [HideInInspector]
    public GameObject target;
    [HideInInspector]
    public float Damage;

    float awake = 0.0f;
    float awakeSpeed = 50f;
    float speed = 2.5f;
    float homingSensitivity = 0.7f;

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
            rigidbody.velocity = relativePos * speed;
        }
        else
        {
            awake -= Time.deltaTime;
            rigidbody.AddRelativeForce(GameController.Player.rigidbody.velocity + awakeSpeed * this.transform.forward);
        }

        //Debug.DrawRay(this.transform.position, this.transform.forward * 5.0f, Color.cyan);
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
