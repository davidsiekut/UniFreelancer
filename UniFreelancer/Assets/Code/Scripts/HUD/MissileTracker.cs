using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MissileTracker : MonoBehaviour
{
    //int flashCount = 0;
    //int flashSpeed = 10;
    bool flashing = false;

    public GUITexture MissileLocked;
    public AudioClip Sound;


    List<GameObject> colliders;

    void Start()
    {
        colliders = new List<GameObject>();
    }

	void Update()
    {
        if (colliders.Count > 0 && !flashing)
        {
            flashing = true;
            InvokeRepeating("Flash", 0.3f, 0.3f);
        }
        else if (colliders.Count > 0)
        {

        }
        else
        {
            flashing = false;
            MissileLocked.enabled = false;
            CancelInvoke();
        }

        for(int i = 0; i < colliders.Count; i++)
        {
            if (colliders[i] == null)
                colliders.RemoveAt(i);
        }
	}

    void OnTriggerEnter(Collider other)
    {
        if (other != null && other.GetComponent<SeekingMissile>() != null && other.GetComponent<SeekingMissile>().Target != null)
        {
            if (other.GetComponent<SeekingMissile>().Target.tag == "Player")
            {
                colliders.Add(other.gameObject);
            }
        }
    }

    void Flash()
    {
        MissileLocked.enabled = !MissileLocked.enabled;
        if (MissileLocked.enabled)
            GameController.HUDSound.PlayOneShot(Sound, 0.1f);
    }
}
