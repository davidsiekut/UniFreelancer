using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DCM;

public class GameController : ScriptableObject
{
    public static GameObject Player;
    public static List<GameObject> Entities;
    static GameObject _entities;
    static GameObject _environment;
    public static AudioSource HUDSound;
    public static Console Console;
    public static TargetSystem TargetSystem;
    public static WeaponSystem WeaponSystem;

    public static float PlayerHeat;
    static float playerHeatMax = 200.0f;
    static float heatCooldownFactor = 10f;

	void Start()
    {
        Console = GameObject.Find("Console").GetComponent<Console>();
        TargetSystem = GameObject.Find("TargetSystem").GetComponent<TargetSystem>();
        WeaponSystem = GameObject.Find("WeaponSystem").GetComponent<WeaponSystem>();
        Entities = new List<GameObject>();
        _entities = GameObject.Find("Entities");
        _environment = GameObject.Find("Environment");
        load();

        Console.SystemCheck();
	}
	
	void FixedUpdate()
    {
        PlayerHeat -= heatCooldownFactor * Time.deltaTime;
        PlayerHeat = Mathf.Clamp(PlayerHeat, 0.0f, playerHeatMax);
	}

    void load()
    {
        GameObject l = GameObject.Instantiate(Resources.Load("Prefabs/Weapons/Lasers/Weapon_MPLAS")) as GameObject;
        WeaponSystem.Equip(l, WeaponSystem.WeaponSlot.WeaponSlot_ChassisLeft);

        GameObject r = GameObject.Instantiate(Resources.Load("Prefabs/Weapons/Lasers/Weapon_MPLAS")) as GameObject;
        WeaponSystem.Equip(r, WeaponSystem.WeaponSlot.WeaponSlot_ChassisRight);

        GameObject w1 = GameObject.Instantiate(Resources.Load("Prefabs/Weapons/Lasers/Weapon_PPC")) as GameObject;
        WeaponSystem.Equip(w1, WeaponSystem.WeaponSlot.WeaponSlot_WingLeftLower);

        GameObject w2 = GameObject.Instantiate(Resources.Load("Prefabs/Weapons/Missiles/Weapon_SRM2")) as GameObject;
        WeaponSystem.Equip(w2, WeaponSystem.WeaponSlot.WeaponSlot_WingLeftUpper);

        GameObject w3 = GameObject.Instantiate(Resources.Load("Prefabs/Weapons/Defensive/Weapon_Countermeasure")) as GameObject;
        WeaponSystem.Equip(w3, WeaponSystem.WeaponSlot.WeaponSlot_WingRightLower);

        GameObject w4 = GameObject.Instantiate(Resources.Load("Prefabs/Weapons/Missiles/Weapon_LRM5")) as GameObject;
        WeaponSystem.Equip(w4, WeaponSystem.WeaponSlot.WeaponSlot_WingRightUpper);

        Player = GameObject.FindWithTag("Player");
        HUDSound = GameObject.FindWithTag("HUD").audio;

        for (int i = 0; i < 20; i++)
        {
            float x = Random.Range(-1000f, 1000f);
            float y = Random.Range(-1000f, 1000f);
            float z = Random.Range(-1000f, 1000f);

            GameObject e = GameObject.Instantiate(Resources.Load("Prefabs/Ships/Viper")) as GameObject;
            e.transform.position = new Vector3(x, y, z);
            Entities.Add(e);
            e.transform.parent = _entities.transform;
        }

        for (int i = 0; i < 1000; i++)
        {
            float z = Random.Range(-400f, 400f);
            Vector2 xy = onUnitCircle(1000f);

            GameObject g = GameObject.Instantiate(Resources.Load("Prefabs/World/Asteroid")) as GameObject;
            g.transform.position = new Vector3(xy.x, xy.y, z);
            float s = Random.Range(1f, 5f);
            g.transform.localScale = new Vector3(s, s, s);
            g.transform.parent = _environment.transform;
        }
        //_environment.GetComponent<DrawCallMinimizer>().Go();
    }

    private static Vector2 onUnitCircle(float radius)
    {
        Vector2 r = Random.insideUnitCircle;
        r.Normalize();
        r *= radius;
        Vector2 rr = new Vector2(Random.Range(-400f, 400f), Random.Range(-400f, 400f));
        r += rr;
        return r;
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

    const float MAX_SOUND_DISTANCE = 2000f;
    const float MAX_SOUND_VOLUME = 0.01f;
    public static void PlaySoundAtPlayer(AudioClip a, Vector3 other)
    {
        float d = Vector3.Distance(Player.transform.position, other);
        if (d < MAX_SOUND_DISTANCE)
        {
            float vol = Mathf.Lerp(0, MAX_SOUND_VOLUME, d / MAX_SOUND_DISTANCE);
            vol -= MAX_SOUND_VOLUME;
            vol *= -1f;
            HUDSound.PlayOneShot(a, vol);
        }
    }
}