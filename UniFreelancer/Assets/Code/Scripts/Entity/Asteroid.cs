using UnityEngine;
using System.Collections;

public class Asteroid : MonoBehaviour
{
    float currentHealth = 50f;

	void Start()
    {
        this.transform.rotation = Random.rotation;
	}
	
	void Update()
    {
        if (currentHealth < 0)
        {
            float s = this.transform.localScale.x;
            if (s > 0.4)
            {
                s *= 0.8f;
                // TODO registry this
                GameObject g = GameObject.Instantiate(Resources.Load("Prefabs/World/Asteroid")) as GameObject;
                g.transform.position = this.transform.position;
                g.transform.localScale = new Vector3(s, s, s);
                GameObject gg = GameObject.Instantiate(Resources.Load("Prefabs/World/Asteroid")) as GameObject;
                gg.transform.position = this.transform.position;
                gg.transform.localScale = new Vector3(s, s, s);

                float x = Random.Range(-1, 2) * 4000f;
                float y = Random.Range(-1, 2) * 4000f;
                float z = Random.Range(-1, 2) * 4000f;
                g.rigidbody.AddForce(new Vector3(x, y, z), ForceMode.Impulse);
                gg.rigidbody.AddForce(-x, -y, -z, ForceMode.Impulse);

            }

            Destroy(gameObject);
        }
	}

    public void TakeDamage(float d)
    {
        currentHealth -= d;
    }
}
