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

    public static float PlayerHeat;
    static float playerHeatMax = 300.0f;
    static float heatCooldownFactor = 3f;

	void Start()
    {
        Console = GameObject.Find("Console").GetComponent<Console>();
        TargetSystem = GameObject.Find("TargetSystem").GetComponent<TargetSystem>();

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
        GameObject l = GameObject.Instantiate(Resources.Load("Prefabs/Weapons/Lasers/Weapon_SLAS")) as GameObject;
        GameObject.Find("WeaponSystem").GetComponent<WeaponSystem>().Equip(l, WeaponSystem.WeaponSlot.WeaponSlot_ChassisLeft);

        GameObject r = GameObject.Instantiate(Resources.Load("Prefabs/Weapons/Lasers/Weapon_LLAS")) as GameObject;
        GameObject.Find("WeaponSystem").GetComponent<WeaponSystem>().Equip(r, WeaponSystem.WeaponSlot.WeaponSlot_ChassisRight);

        GameObject w1 = GameObject.Instantiate(Resources.Load("Prefabs/Weapons/Lasers/Weapon_LLAS")) as GameObject;
        GameObject.Find("WeaponSystem").GetComponent<WeaponSystem>().Equip(w1, WeaponSystem.WeaponSlot.WeaponSlot_WingLeftLower);

        GameObject w2 = GameObject.Instantiate(Resources.Load("Prefabs/Weapons/Lasers/Weapon_ERLLAS")) as GameObject;
        GameObject.Find("WeaponSystem").GetComponent<WeaponSystem>().Equip(w2, WeaponSystem.WeaponSlot.WeaponSlot_WingLeftUpper);

        GameObject w3 = GameObject.Instantiate(Resources.Load("Prefabs/Weapons/Lasers/Weapon_MLAS")) as GameObject;
        GameObject.Find("WeaponSystem").GetComponent<WeaponSystem>().Equip(w3, WeaponSystem.WeaponSlot.WeaponSlot_WingRightLower);

        GameObject w4 = GameObject.Instantiate(Resources.Load("Prefabs/Weapons/Missiles/Weapon_SRM6")) as GameObject;
        GameObject.Find("WeaponSystem").GetComponent<WeaponSystem>().Equip(w4, WeaponSystem.WeaponSlot.WeaponSlot_WingRightUpper);


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

    public static void TryDoDamage(GameObject g, float damage)
    {
        if (g.GetComponent<Entity>() != null)
        {
            g.GetComponent<Entity>().Health -= damage;

            GameObject p = GameObject.Instantiate(Resources.Load("Prefabs/HUD/DamagePopup")) as GameObject;
            p.GetComponent<DamagePopup>().Target = g;
            p.GetComponent<DamagePopup>().Damage = damage;

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