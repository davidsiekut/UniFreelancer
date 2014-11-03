using UnityEngine;
using System.Collections;

public class MenuBehaviour : MonoBehaviour
{
    public AudioClip Tick;
    public GUITexture Mark;
    float timer = 0.5f;

	void Start()
    {
        InvokeRepeating("Flash", 1f, 1f);
	}
	
	void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0)
            Mark.enabled = false;

        if (Input.GetKey(KeyCode.Y))
            Application.LoadLevel("Game");
        if (Input.GetKey(KeyCode.N))
            Application.Quit();
	}

    void Flash()
    {
        this.audio.PlayOneShot(Tick, 0.1f);
        Mark.enabled = true;
        timer = 0.5f;
    }
}
