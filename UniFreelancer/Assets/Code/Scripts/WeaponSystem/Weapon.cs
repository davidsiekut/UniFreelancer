using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour
{
    public enum WeaponType
    {
        _Debug = -1,
        Laser = 0,
        PulseLaser = 1,
        SRMissile = 2,
        LRMissile = 3,
        WeedRipper = 420,

    }

    public string Name = "";
    public string ShortName = "";
    public WeaponType Type;
    public float Damage; // for lasers, this is DPS
    public float Range;
    public float Heat;
    public GameObject Projectile;
    public int Missiles;
    public AudioClip Shoot;

    public float Cooldown = 1.0f;
    float _cooldown;

    Camera cam;

	void Start()
    {
        _cooldown = Cooldown;
        cam = Camera.main;
    }

	void Update()
    {
        if (Cooldown > 0)
            Cooldown -= Time.deltaTime;
	}

    public void Fire()
    {
        Vector3 initial = this.transform.position;
        Vector3 final = GameController.Player.transform.position + cam.transform.forward * Range;
        Vector3 direction = final - initial;

        if (Cooldown < 0)
        {
            if (Type == WeaponType.Laser || Type == WeaponType.PulseLaser)
            {
                // moved into the laser script itself
                /*RaycastHit hit;
                if (Physics.Raycast(initial, direction, out hit, Range))
                {
                    //Debug.DrawLine(initial, final, Color.green);
                    final = hit.point;

                    //GameController.TryDoDamage(hit.collider.gameObject, Damage);
                }*/

                GameObject g = GameObject.Instantiate(Projectile) as GameObject;
                g.GetComponent<Laser>().Damage = Damage;
                g.GetComponent<Laser>().Range = Range;
                g.GetComponent<Laser>().follow = this.transform;
                //g.GetComponent<LineRenderer>().SetPosition(0, initial);
                //g.GetComponent<LineRenderer>().SetPosition(1, final);

                GameController.HUDSound.PlayOneShot(Shoot, 0.05f);
                GameController.YoureGonnaBurnAlright(Heat);
                Cooldown = _cooldown;
            }
            else if (Type == WeaponType.SRMissile)
            {
                GameController.HUDSound.PlayOneShot(Shoot, 0.05f);
                GameController.YoureGonnaBurnAlright(Heat);
                Cooldown = _cooldown;
            }
            else if (Type == WeaponType.LRMissile)
            {
                GameObject target = GameController.TargetSystem.GetFrontLockTarget();
                if (target != null)
                {
                    Cooldown = _cooldown;

                    StartCoroutine(FireMany(target));
                }
            }
        }
    }

    IEnumerator FireMany(GameObject target)
    {
        for (int i = 0; i < Missiles; i++)
        {
            GameController.HUDSound.PlayOneShot(Shoot, 0.05f);
            GameController.YoureGonnaBurnAlright(Heat);

            GameObject g = GameObject.Instantiate(Projectile) as GameObject;
            g.transform.position = this.transform.parent.position;
            //g.transform.forward = player.transform.forward;

            g.GetComponent<SeekingMissile>().target = target;

            // current player velocity so projectile doesnt fall behind, with the target direction
            //float moreForce = 2000 + Mathf.Sqrt(player.rigidbody.velocity.magnitude) / 10;
            //g.GetComponent<Projectile>().Fire((player.rigidbody.velocity + target.transform.position.normalized) * moreForce);
            yield return new WaitForSeconds(0.3f);
        }
    }

    public float GetCooldownPercent()
    {
        if (Type == WeaponType.PulseLaser)
            return 0f;

        return Cooldown / _cooldown;
    }
}
