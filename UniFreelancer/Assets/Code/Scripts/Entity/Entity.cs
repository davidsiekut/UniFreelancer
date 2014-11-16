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
    public AudioClip[] Impact;

    public bool ejecting = false;

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
            //GameController.Console.Add("target destroyed");

            if (this.tag == "Player" && !ejecting)
            {
                ejecting = true;
                StartCoroutine(Eject());
            }
            else if (this.tag == "Player")
            {

            }
            else
            {
                GameObject g = GameObject.Instantiate(Explosion) as GameObject;
                g.transform.position = this.transform.position;

                Destroy(gameObject);
            }
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

    public void TakeHeatDamage(float d)
    {
        currentHealth -= d;
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
        else
        {
            int i = Random.Range(0, Impact.Length);
            GameController.PlaySoundAtPlayer(Impact[i], this.transform.position);

            shake_intensity = .5f;
            StopAllCoroutines();
            StartCoroutine(ShakeMe());
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

    private Vector3 originPosition;
    private Quaternion originRotation;
    private float shake_decay = 3f;
    private float shake_intensity;
    IEnumerator ShakeMe()
    {
        originPosition = transform.position;
        originRotation = transform.rotation;

        while (shake_intensity > 0)
        {
            //Debug.Log(shake_intensity);
            transform.position = originPosition + Random.insideUnitSphere * shake_intensity;
            transform.rotation = new Quaternion(
            originRotation.x + Random.Range(-shake_intensity, shake_intensity) * .2f,
            originRotation.y + Random.Range(-shake_intensity, shake_intensity) * .2f,
            originRotation.z + Random.Range(-shake_intensity, shake_intensity) * .2f,
            originRotation.w + Random.Range(-shake_intensity, shake_intensity) * .2f);
            shake_intensity -= shake_decay * Time.deltaTime;
            yield return null;
        }
    }

    IEnumerator Eject()
    {
        GameController.PlayerHeat = 0;

        GameController.Player.GetComponent<ShipInput>().CanControl = false;

        Screen.showCursor = false;

        Console c = GameObject.FindGameObjectWithTag("HUD").GetComponentInChildren<Console>();
        c.Status.SetActive(false);
        c.Heat.GetComponent<HeatInfo>().CancelInvoke();
        c.Heat.SetActive(false);
        c.Speed.SetActive(false);
        c.Crosshair.SetActive(false);
        c.CrosshairBounds.SetActive(false);
        c.Target.SetActive(false);
        c.Hardpoints.SetActive(false);

        c.WarningHeat.SetActive(false);
        c.MissileTrackerHUD.SetActive(false);



        float times = 5;

        while (times > 0)
        {
            GameObject g = GameObject.Instantiate(Explosion) as GameObject;
            g.transform.position = this.transform.position;

            times--;

            int i = Random.Range(0, Impact.Length);
            GameController.PlaySoundAtPlayer(Impact[i], this.transform.position);

            shake_intensity = .5f;
            StartCoroutine(ShakeMe());

            yield return new WaitForSeconds(1f);
        }

        GameController.Overlay.GetComponent<Fade>().FadeOutTex();

        yield return new WaitForSeconds(4f);
        // too lazy to make references and shit
        GameObject.Find("SeeYou").GetComponent<Fade>().FadeInText();

        yield return new WaitForSeconds(2f);
        StartCoroutine(coWaitForNew());

        yield return null;
    }

    IEnumerator coWaitForNew()
    {
        while (!Input.anyKey)
        {
            yield return null;
        }

        Application.LoadLevel("Load");
    }
}
