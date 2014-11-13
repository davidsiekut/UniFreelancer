using UnityEngine;
using System.Collections;

public class Entity : MonoBehaviour
{
    public float MaxHealth;
    public float MaxShield;
    public float currentHealth;
    public float currentShield;

    float shieldRegen = 5f;
    float shieldRecharge = 10f;
    float _shieldRecharge;

    public GameObject Explosion;

	void Start()
    {
        currentHealth = MaxHealth;
        currentShield = MaxShield;
        _shieldRecharge = shieldRecharge;
	}
	
	void Update()
    {
        if (currentHealth < 0)
        {
            GameObject g = GameObject.Instantiate(Explosion) as GameObject;
            g.transform.position = this.transform.position;

            //GameController.Console.Add("target destroyed");

            Destroy(gameObject);
        }

        if (_shieldRecharge < shieldRecharge)
        {
            // if shields have been damaged past 0
            _shieldRecharge += Time.deltaTime;
        }
        else
        {
            // otherwise, regen shields as normal
            if (currentShield < MaxShield)
            {
                currentShield += shieldRegen * Time.deltaTime;
            }
        }
	}

    void OnCollisionEnter(Collision c)
    {
        //Debug.Log(this.name + " collided with " + c.transform.name);

        // be careful to check for stuff here, because things like missiles
        // will also assign damage

        float damage = this.rigidbody.velocity.magnitude + c.rigidbody.velocity.magnitude;
        damage /= 2;
        //Debug.Log("Damage taken " + damage);

        if (c.gameObject.name == "Asteroid(Clone)")
            TakeDamage(damage);
    }

    public void TakeDamage(float d)
    {
        _shieldRecharge = 0; // break shields
        if (currentShield > 0)
        {
            currentShield -= d;

            if (currentShield < 0)
            {
                // rollover to health
                currentHealth += currentShield;
            }
        }
        else
        {
            currentHealth -= d;
        }

        if (this.tag != "Player")
        {
            GameObject p = GameObject.Instantiate(Resources.Load("Prefabs/HUD/DamagePopup")) as GameObject;
            p.GetComponent<DamagePopup>().Target = this.gameObject;
            p.GetComponent<DamagePopup>().Damage = d;
        }
    }

    public float GetHealthPercent()
    {
        return currentHealth / MaxHealth;
    }

    public float GetShieldPercent()
    {
        return currentShield / MaxShield;
    }
}
