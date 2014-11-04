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

    public void Equip(GameObject weapon, WeaponSlot slot)
    {
        if (slot == WeaponSlot.WeaponSlot_ChassisLeft)
        {
            weapon.transform.parent = WeaponSlot_ChassisLeft.transform;
        }
        else if (slot == WeaponSlot.WeaponSlot_ChassisRight)
        {
            weapon.transform.parent = WeaponSlot_ChassisRight.transform;
        }
        else if (slot == WeaponSlot.WeaponSlot_WingLeftLower)
        {
            weapon.transform.parent = WeaponSlot_WingLeftLower.transform;
        }
        else if (slot == WeaponSlot.WeaponSlot_WingLeftUpper)
        {
            weapon.transform.parent = WeaponSlot_WingLeftUpper.transform;
        }
        else if (slot == WeaponSlot.WeaponSlot_WingRightLower)
        {
            weapon.transform.parent = WeaponSlot_WingRightLower.transform;
        }
        else if (slot == WeaponSlot.WeaponSlot_WingRightUpper)
        {
            weapon.transform.parent = WeaponSlot_WingRightUpper.transform;
        }

        weapon.transform.SetAsFirstSibling();
        weapon.transform.localPosition = Vector3.zero;
    }

    public GameObject Unequip(WeaponSlot slot)
    {
        GameObject g = null;
        if (slot == WeaponSlot.WeaponSlot_ChassisLeft)
        {
            g = WeaponSlot_ChassisLeft.transform.GetChild(0).gameObject;
            WeaponSlot_ChassisLeft.transform.GetChild(0).parent = null;
        }
        else if (slot == WeaponSlot.WeaponSlot_ChassisRight)
        {
            g = WeaponSlot_ChassisRight.transform.GetChild(0).gameObject;
            WeaponSlot_ChassisRight.transform.GetChild(0).parent = null;
        }
        else if (slot == WeaponSlot.WeaponSlot_WingLeftLower)
        {
            g = WeaponSlot_WingLeftLower.transform.GetChild(0).gameObject;
            WeaponSlot_WingLeftLower.transform.GetChild(0).parent = null;
        }
        else if (slot == WeaponSlot.WeaponSlot_WingLeftUpper)
        {
            g = WeaponSlot_WingLeftUpper.transform.GetChild(0).gameObject;
            WeaponSlot_WingLeftUpper.transform.GetChild(0).parent = null;
        }
        else if (slot == WeaponSlot.WeaponSlot_WingRightLower)
        {
            g = WeaponSlot_WingRightLower.transform.GetChild(0).gameObject;
            WeaponSlot_WingRightLower.transform.GetChild(0).parent = null;
        }
        else if (slot == WeaponSlot.WeaponSlot_WingRightUpper)
        {
            g = WeaponSlot_WingRightUpper.transform.GetChild(0).gameObject;
            WeaponSlot_WingRightUpper.transform.GetChild(0).parent = null;
        }

        return g;
    }

    public void FirePrimary(Ray r)
    {
        WeaponSlot_ChassisLeft.transform.GetChild(0).GetComponent<Weapon>().FireGimbaled(r);
        WeaponSlot_ChassisRight.transform.GetChild(0).GetComponent<Weapon>().FireGimbaled(r);
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
    }
}
