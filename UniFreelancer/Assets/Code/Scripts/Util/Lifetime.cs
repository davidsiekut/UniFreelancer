using UnityEngine;
using System.Collections;

public class Lifetime : MonoBehaviour
{
    public float KeepAlive = 1.0f;

	void Start()
    {
	}

	void Update()
    {
        KeepAlive -= Time.deltaTime;

        if (KeepAlive < 0)
            GameObject.Destroy(this.gameObject);
	}
}
