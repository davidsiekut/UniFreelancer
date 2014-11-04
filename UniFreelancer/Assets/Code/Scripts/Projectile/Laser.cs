using UnityEngine;
using System.Collections;

public class Laser : MonoBehaviour
{
    [HideInInspector]
    public Transform follow;
    [HideInInspector]
    public Vector3 Target = Vector3.zero;
    [HideInInspector]
    public float Range;
    [HideInInspector]
    public float Damage;

    float damageCooldown = 0.1f; // do damage every x seconds

	void Start()
    {
	}
	
	void Update()
    {
        // change this for gimbaled laser
        Vector3 v;

        if (Target != Vector3.zero)
        {
            // fire gimbaled through mouse cursor
            v = Target;
        }
        else
        {
            // fire straight ahead
            v = GameController.Player.transform.position + Camera.main.transform.forward * Range;
        }

        if (follow != null)
        {
            //Debug.DrawRay(follow.position, (v - follow.position) * Range, Color.red);
            //check for hits and assign damage
            RaycastHit hit;
            if (Physics.Raycast(follow.position, (v - follow.position) * Range, out hit, Range))
            {

                // stop the laser short due to hit
                v = hit.point;

                if (damageCooldown == 0.1f)
                {
                    // TODO TEMP HACK SO LASER DOESNT HIT SHIP
                    if (hit.transform.tag != "Player")
                    {
                        Debug.Log(hit.transform.name + " hit by laser");

                        if (hit.transform.GetComponent<Entity>() != null)
                        {
                            hit.transform.GetComponent<Entity>().TakeDamage(Damage);
                        }
                    }
                }
            }

            // draw the line
            this.GetComponent<LineRenderer>().SetPosition(0, follow.position);
            this.GetComponent<LineRenderer>().SetPosition(1, v);
        }

        damageCooldown -= Time.deltaTime;
        if (damageCooldown < 0)
            damageCooldown = 0.1f;
	}
}
