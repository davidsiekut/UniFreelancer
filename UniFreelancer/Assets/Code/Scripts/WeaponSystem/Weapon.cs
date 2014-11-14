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
        PPC = 4,

    }

    public string Name = "";
    public string ShortName = "";
    public WeaponType Type;
    public float Damage; // for lasers, this is DPS
    public float Range;
    public float Heat;
    public GameObject Projectile;
    public int Missiles;
    public float FireRate;
    public AudioClip Shoot;

    public float Cooldown = 1.0f;
    float _cooldown;

    Camera cam;
    Transform owner;

	void Start()
    {
        _cooldown = Cooldown;
        cam = Camera.main;
        // this will throw exception if weapon is in cargo
        if (this.transform.parent.parent.GetComponent<WeaponSystem>() != null)
            owner = this.transform.parent.parent.GetComponent<WeaponSystem>().Owner.transform;
    }

	void Update()
    {
        if (Cooldown > 0)
            Cooldown -= Time.deltaTime;
	}

    public void FireGimbaled(Ray r)
    {
        //Vector3 initial = this.transform.position;
        Vector3 final = GameController.Player.transform.position + r.direction * Range;

        if (Cooldown < 0)
        {
            //Debug.DrawLine(initial, final.normalized * Range, Color.yellow);

            GameObject g = GameObject.Instantiate(Projectile) as GameObject;
            g.GetComponent<Laser>().Origin = this.transform;
            g.GetComponent<Laser>().Owner = owner;
            g.GetComponent<Laser>().Damage = Damage;
            g.GetComponent<Laser>().Range = Range;
            g.GetComponent<Laser>().Target = final;


            GameController.PlaySoundAtPlayer(Shoot, this.transform.position);
            GameController.YoureGonnaBurnAlright(Heat);
            Cooldown = _cooldown;
        }
    }

    public void Fire(Transform t)
    {
        //Vector3 initial = this.transform.position;
        //Vector3 final = GameController.Player.transform.position + cam.transform.forward * Range;
        //Vector3 direction = final - initial;

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
                g.GetComponent<Laser>().Origin = this.transform;
                //g.GetComponent<LineRenderer>().SetPosition(0, initial);
                //g.GetComponent<LineRenderer>().SetPosition(1, final);

                GameController.PlaySoundAtPlayer(Shoot, this.transform.position);
                GameController.YoureGonnaBurnAlright(Heat);
                Cooldown = _cooldown;
            }
            else if (Type == WeaponType.SRMissile)
            {
                StartCoroutine(FireMany());
                Cooldown = _cooldown;
            }
            else if (Type == WeaponType.LRMissile)
            {
                GameObject target;

                if (t == null)
                {
                    // player shooting this one
                    target = GameController.TargetSystem.GetFrontLockTarget();
                }
                else
                {
                    // npc shooting this one
                    target = t.gameObject;
                }


                StartCoroutine(FireMany(target));
                Cooldown = _cooldown;
            }
        }
    }

    // shoot missiles in a direction, no lock required
    IEnumerator FireMany()
    {
        for (int i = 0; i < Missiles; i++)
        {
            GameController.PlaySoundAtPlayer(Shoot, this.transform.position);
            GameController.YoureGonnaBurnAlright(Heat);

            GameObject g = GameObject.Instantiate(Projectile) as GameObject;
            g.transform.position = this.transform.parent.position;

            Vector3 initial = this.transform.position;
            Vector3 final = GameController.Player.transform.position + cam.transform.forward * Range;
            Vector3 direction = final - initial;

            g.GetComponent<Missile>().Damage = Damage;
            g.GetComponent<Missile>().Direction = direction;

            yield return new WaitForSeconds(FireRate);
        }
    }

    // shoot missiles that lock in on a target
    IEnumerator FireMany(GameObject target)
    {
        for (int i = 0; i < Missiles; i++)
        {
            GameController.PlaySoundAtPlayer(Shoot, this.transform.position);
            GameController.YoureGonnaBurnAlright(Heat);

            GameObject g = GameObject.Instantiate(Projectile) as GameObject;
            g.transform.position = this.transform.parent.position;
            g.GetComponent<SeekingMissile>().Target = target;

            yield return new WaitForSeconds(FireRate);
        }
    }

    public float GetCooldownPercent()
    {
        if (Type == WeaponType.PulseLaser)
            return 0f;

        return Cooldown / _cooldown;
    }
}
