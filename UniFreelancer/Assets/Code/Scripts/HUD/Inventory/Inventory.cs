using UnityEngine;
using System.Collections;

public class Inventory : MonoBehaviour
{
    public GUITexture ChassisLeft;
    public GUITexture ChassisRight;
    public GUITexture WingLeftLower;
    public GUITexture WingLeftUpper;
    public GUITexture WingRightUpper;
    public GUITexture WingRightLower;

    public GameObject Current;
    public GameObject LastClicked;

	void Start()
    {
	}
	
	void Update()
    {
        if (Input.GetMouseButtonUp(0) && Current != null)
        {
            if (ChassisLeft.HitTest(Input.mousePosition))
            {
                if (Current.GetComponent<Weapon>().Type != Weapon.WeaponType.LRMissile
                    && Current.GetComponent<Weapon>().Type != Weapon.WeaponType.SRMissile)
                {
                    GameObject g = GameController.WeaponSystem.Unequip(WeaponSystem.WeaponSlot.WeaponSlot_ChassisLeft);
                    GameController.WeaponSystem.Equip(Current, WeaponSystem.WeaponSlot.WeaponSlot_ChassisLeft);
                    g.transform.parent = LastClicked.transform;
                }
            }
            else if (ChassisRight.HitTest(Input.mousePosition))
            {
                if (Current.GetComponent<Weapon>().Type != Weapon.WeaponType.LRMissile
                    && Current.GetComponent<Weapon>().Type != Weapon.WeaponType.SRMissile)
                {
                    GameObject g = GameController.WeaponSystem.Unequip(WeaponSystem.WeaponSlot.WeaponSlot_ChassisRight);
                    GameController.WeaponSystem.Equip(Current, WeaponSystem.WeaponSlot.WeaponSlot_ChassisRight);
                    g.transform.parent = LastClicked.transform;
                }
            }
            else if (WingLeftLower.HitTest(Input.mousePosition))
            {
                GameObject g = GameController.WeaponSystem.Unequip(WeaponSystem.WeaponSlot.WeaponSlot_WingLeftLower);
                GameController.WeaponSystem.Equip(Current, WeaponSystem.WeaponSlot.WeaponSlot_WingLeftLower);
                g.transform.parent = LastClicked.transform;
            }
            else if (WingLeftUpper.HitTest(Input.mousePosition))
            {
                GameObject g = GameController.WeaponSystem.Unequip(WeaponSystem.WeaponSlot.WeaponSlot_WingLeftUpper);
                GameController.WeaponSystem.Equip(Current, WeaponSystem.WeaponSlot.WeaponSlot_WingLeftUpper);
                g.transform.parent = LastClicked.transform;
            }
            else if (WingRightUpper.HitTest(Input.mousePosition))
            {
                GameObject g = GameController.WeaponSystem.Unequip(WeaponSystem.WeaponSlot.WeaponSlot_WingRightUpper);
                GameController.WeaponSystem.Equip(Current, WeaponSystem.WeaponSlot.WeaponSlot_WingRightUpper);
                g.transform.parent = LastClicked.transform;
            }
            else if (WingRightLower.HitTest(Input.mousePosition))
            {
                GameObject g = GameController.WeaponSystem.Unequip(WeaponSystem.WeaponSlot.WeaponSlot_WingRightLower);
                GameController.WeaponSystem.Equip(Current, WeaponSystem.WeaponSlot.WeaponSlot_WingRightLower);
                g.transform.parent = LastClicked.transform;
            }

            Current = null;
        }
	}
}
