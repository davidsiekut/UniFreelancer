﻿using UnityEngine;
using System.Collections;

public class Missile : MonoBehaviour
{
    public GameObject Explosion;
    [HideInInspector]
    public float Damage;
    [HideInInspector]
    public Vector3 Direction;

	void Start()
    {
        //Quaternion rotation = Quaternion.LookRotation(GameController.Player.transform.forward);
        //transform.localRotation = rotation;
	}

    void FixedUpdate()
    {
        //rigidbody.AddRelativeForce(50f * Direction,ForceMode.VelocityChange);
        rigidbody.AddRelativeForce(GameController.Player.rigidbody.velocity + 300f * Direction.normalized);

        Debug.DrawRay(this.transform.position, Direction.normalized * 5.0f, Color.cyan);
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name + " hit by missile");

        if (collision.gameObject.GetComponent<Entity>() != null)
        {
            collision.gameObject.GetComponent<Entity>().TakeDamage(Damage);
        }

        GameObject g = GameObject.Instantiate(Explosion) as GameObject;
        g.transform.position = this.transform.position;

        Destroy(gameObject);
    }
}
