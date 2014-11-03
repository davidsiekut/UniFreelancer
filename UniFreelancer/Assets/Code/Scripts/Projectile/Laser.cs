using UnityEngine;
using System.Collections;

public class Laser : MonoBehaviour
{
    [HideInInspector]
    public Transform follow;
    [HideInInspector]
    public float Range;
    [HideInInspector]
    public float Damage;

    float damageCooldown = 0.1f; // DPS

	void Start()
    {
	}
	
	void Update()
    {
        Vector3 v = GameController.Player.transform.position + Camera.main.transform.forward * Range;

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
                    GameController.TryDoDamage(hit.collider.gameObject, Damage);
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
