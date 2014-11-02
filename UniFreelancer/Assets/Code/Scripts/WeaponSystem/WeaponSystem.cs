using UnityEngine;
using System.Collections;

public class WeaponSystem : MonoBehaviour
{
    public enum WeaponSlot
    {
        WeaponSlot_ChassisLeft = 0,
        WeaponSlot_ChassisRight = 1,
        WeaponSlot_WingLeftLower = 2,
        WeaponSlot_WingLeftUpper = 3,
        WeaponSlot_WingRightLower = 4,
        WeaponSlot_WingRightUpper = 5,
    }

    public GameObject WeaponSlot_ChassisLeft;
    public GameObject WeaponSlot_ChassisRight;
    public GameObject WeaponSlot_WingLeftLower;
    public GameObject WeaponSlot_WingLeftUpper;
    public GameObject WeaponSlot_WingRightLower;
    public GameObject WeaponSlot_WingRightUpper;

	void Start ()
    {
	}
	
	void Update ()
    {
	}

    public void FirePrimary()
    {
        //WeaponSlot_ChassisLeft.transform.GetChild(0).GetComponent<Weapon>().Fire(target);
        //WeaponSlot_ChassisRight.transform.GetChild(0).GetComponent<Weapon>().Fire(target);
    }

    public void FireSlot(int i)
    {
        if (i == 1)
        {
            WeaponSlot_WingLeftLower.transform.GetChild(0).GetComponent<Weapon>().Fire();
        }
        else if (i == 2)
        {
            WeaponSlot_WingLeftUpper.transform.GetChild(0).GetComponent<Weapon>().Fire();
        }
        else if (i == 3)
        {
            WeaponSlot_WingRightUpper.transform.GetChild(0).GetComponent<Weapon>().Fire();
        }
        else if (i == 4)
        {
            WeaponSlot_WingRightLower.transform.GetChild(0).GetComponent<Weapon>().Fire();
        }

        /*
        if (WeaponSlot_WingLeftLower.transform.GetChild(0).GetComponent<Weapon>().Type == Weapon.WeaponType.Missile)
        {
            // requires a lock
            GameObject target = GameController.TargetSystem.GetFrontLockTarget();
            if (target != null)
            {
                WeaponSlot_WingLeftLower.transform.GetChild(0).GetComponent<Weapon>().Fire( = target;
                WeaponSlot_WingLeftLower.transform.GetChild(0).GetComponent<SeekBehaviour>().target = target;
            }
        }
        else
        {
            Debug.Log("Firing primary");
            // fire non targeted through middle of screen
            Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            Debug.DrawRay(ray.origin, ray.direction * TEST_WEAPON_DISTANCE, Color.yellow);

            WeaponSlot_WingLeftLower.transform.GetChild(0).GetComponent<Weapon>().Fire(ray.direction * TEST_WEAPON_DISTANCE);
        }
        */
    }
}
