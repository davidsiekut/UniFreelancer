using UnityEngine;
using System.Collections;

public class Laser : MonoBehaviour
{
    [HideInInspector]
    public Transform Owner;
    [HideInInspector]
    public Transform Origin;
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

        if (Origin != null)
        {
            //Debug.DrawRay(Origin.position, (v - Origin.position) * Range, Color.red);
            //check for hits and assign damage
            RaycastHit hit;
            if (Physics.Raycast(Origin.position, (v - Origin.position) * Range, out hit, Range))
            {

                if (Owner != hit.transform)
                {
                    // stop the laser short due to hit
                    v = hit.point;

                    if (damageCooldown == 0.1f)
                    {
                        //Debug.Log(hit.transform.name + " hit by laser");

                        if (hit.transform.GetComponent<Entity>() != null)
                        {
                            hit.transform.GetComponent<Entity>().TakeDamage(Damage);
                        }
                        else if (hit.transform.GetComponent<Asteroid>() != null)
                        {
                            hit.transform.GetComponent<Asteroid>().TakeDamage(Damage);
                        }
                   }
                }
            }

            // draw the line
            this.GetComponent<LineRenderer>().SetPosition(0, Origin.position);
            this.GetComponent<LineRenderer>().SetPosition(1, v);
        }

        damageCooldown -= Time.deltaTime;
        if (damageCooldown < 0)
            damageCooldown = 0.1f;
	}
}
