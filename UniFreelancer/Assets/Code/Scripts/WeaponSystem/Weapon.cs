using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour
{
    public enum WeaponType
    {
        _Debug = -1,
        Laser = 0,
        Missile = 1,
        WeedRipper = 420,

    }

    public string Name = "";
    public string ShortName = "";
    public WeaponType Type;
    public GameObject Prefab;

    public float Cooldown = 1.0f;
    float _cooldown;

    GameObject player;

	void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        _cooldown = Cooldown;
	}

	void Update()
    {
        if (Cooldown > 0)
            Cooldown -= Time.deltaTime;
	}

    public void Fire(Vector3 target)
    {
        if (Cooldown < 0)
        {
            this.audio.Play();

            GameObject g = GameObject.Instantiate(Prefab) as GameObject;
            g.transform.position = this.transform.parent.position;
            float moreForce = 2000 + player.rigidbody.velocity.magnitude * 70;
            g.rigidbody.AddForce(player.rigidbody.velocity + target.normalized * moreForce);
            g.GetComponent<Projectile>().target = target;
            Cooldown = _cooldown;
        }
    }
}
