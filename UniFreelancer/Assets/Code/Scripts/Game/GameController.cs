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
    public static WeaponSystem WeaponSystem;

    public static float PlayerHeat;
    static float playerHeatMax = 200.0f;
    static float heatCooldownFactor = 7f;

	void Start()
    {
        Console = GameObject.Find("Console").GetComponent<Console>();
        TargetSystem = GameObject.Find("TargetSystem").GetComponent<TargetSystem>();
        WeaponSystem = GameObject.Find("WeaponSystem").GetComponent<WeaponSystem>();
        Entities = new List<GameObject>();
        load();

        Console.SystemCheck();
	}
	
	void FixedUpdate()
    {
        PlayerHeat -= heatCooldownFactor * Time.deltaTime;
        PlayerHeat = Mathf.Clamp(PlayerHeat, 0.0f, playerHeatMax);

        Debug.DrawRay(Player.transform.position, Player.transform.forward.normalized * 50.0f, Color.magenta);
	}

    void load()
    {
        GameObject l = GameObject.Instantiate(Resources.Load("Prefabs/Weapons/Lasers/Weapon_MLAS")) as GameObject;
        WeaponSystem.Equip(l, WeaponSystem.WeaponSlot.WeaponSlot_ChassisLeft);

        GameObject r = GameObject.Instantiate(Resources.Load("Prefabs/Weapons/Lasers/Weapon_MPLAS")) as GameObject;
        WeaponSystem.Equip(r, WeaponSystem.WeaponSlot.WeaponSlot_ChassisRight);

        GameObject w1 = GameObject.Instantiate(Resources.Load("Prefabs/Weapons/Lasers/Weapon_PPC")) as GameObject;
        WeaponSystem.Equip(w1, WeaponSystem.WeaponSlot.WeaponSlot_WingLeftLower);

        GameObject w2 = GameObject.Instantiate(Resources.Load("Prefabs/Weapons/Lasers/Weapon_ERLLAS")) as GameObject;
        WeaponSystem.Equip(w2, WeaponSystem.WeaponSlot.WeaponSlot_WingLeftUpper);

        GameObject w3 = GameObject.Instantiate(Resources.Load("Prefabs/Weapons/Missiles/Weapon_LRM5")) as GameObject;
        WeaponSystem.Equip(w3, WeaponSystem.WeaponSlot.WeaponSlot_WingRightLower);

        GameObject w4 = GameObject.Instantiate(Resources.Load("Prefabs/Weapons/Missiles/Weapon_SRM6")) as GameObject;
        WeaponSystem.Equip(w4, WeaponSystem.WeaponSlot.WeaponSlot_WingRightUpper);

        Player = GameObject.FindWithTag("Player");
        HUDSound = GameObject.FindWithTag("HUD").audio;

        GameObject e = GameObject.Instantiate(Resources.Load("Test/TestEnemyShip")) as GameObject;
        e.transform.position = new Vector3(0, 0, 50);
        Entities.Add(e);
        for (int i = 0; i < 100; i++)
        {
            float x = Random.Range(-1000f, 1000f);
            float y = Random.Range(-1000f, 1000f);
            float z = Random.Range(-1000f, 1000f);

            GameObject g = GameObject.Instantiate(Resources.Load("Prefabs/Test/TestTarget")) as GameObject;
            g.transform.position = new Vector3(x, y, z);
            //Entities.Add(g);
        }

        for (int i = 0; i < 100; i++)
        {
            float x = Random.Range(-1000f, 1000f);
            float y = Random.Range(-1000f, 1000f);
            float z = Random.Range(-1000f, 1000f);

            GameObject g = GameObject.Instantiate(Resources.Load("Prefabs/World/Asteroid")) as GameObject;
            g.transform.position = new Vector3(x, y, z);
        }
    }

    public static void YoureGonnaBurnAlright(float heat)
    {
        PlayerHeat += heat;
        PlayerHeat = Mathf.Clamp(PlayerHeat, 0.0f, playerHeatMax);
    }

    public static float GetPlayerHeatPercent()
    {
        return PlayerHeat / playerHeatMax;
    }
}