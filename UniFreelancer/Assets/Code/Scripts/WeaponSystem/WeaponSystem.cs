using UnityEngine;
using System.Collections;

public class WeaponSystem : MonoBehaviour
{
    public enum WeaponSlot
    {
        WeaponSlot_WingUpperLeft = 0,
        WeaponSlot_WingUpperRight = 1,

    }

    public GameObject WeaponSlot_WingUpperLeft;
    public GameObject WeaponSlot_WingUpperRight;

	void Start ()
    {
	}
	
	void Update ()
    {
	}

    public void FireAll(Vector3 target)
    {
        WeaponSlot_WingUpperLeft.transform.GetChild(0).GetComponent<Weapon>().Fire(target);
        WeaponSlot_WingUpperRight.transform.GetChild(0).GetComponent<Weapon>().Fire(target);
    }
}
