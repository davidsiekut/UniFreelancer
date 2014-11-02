using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : ScriptableObject
{
    public static GameObject Player;
    public static List<GameObject> Entities;
    public static AudioSource HUDSound;
    public static Console Console;
    public static TargetSystem TargetSystem;

	void Start()
    {
        Console = GameObject.Find("Console").GetComponent<Console>();
        TargetSystem = GameObject.Find("TargetSystem").GetComponent<TargetSystem>();

        Entities = new List<GameObject>();
        load();

        Console.SystemCheck();
	}
	
	void Update()
    {
	}

    void load()
    {
        Player = GameObject.FindWithTag("Player");
        HUDSound = GameObject.FindWithTag("HUD").audio;

        for (int i = 0; i < 40; i++)
        {
            float x = Random.Range(-500f, 500f);
            float y = Random.Range(-500f, 500f);
            float z = Random.Range(-500f, 500f);

            GameObject g = GameObject.Instantiate(Resources.Load("Prefabs/Test/TestTarget")) as GameObject;
            g.transform.position = new Vector3(x, y, z);
            Entities.Add(g);
        }
    }
}