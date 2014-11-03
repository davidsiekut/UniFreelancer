using UnityEngine;
using System.Collections;

public class HardpointInfo : MonoBehaviour
{
    public GameObject Slot;
    Weapon weapon;

	void Start()
    {
        weapon = Slot.transform.GetChild(0).GetComponent<Weapon>();
	}

	void Update()
    {
        if (weapon != null)
        {
            this.guiText.text = weapon.Name;
            Rect rekt = new Rect(this.transform.GetChild(0).guiTexture.pixelInset.x, this.transform.GetChild(0).guiTexture.pixelInset.y,
                weapon.GetCooldownPercent() * 50.0f, this.transform.GetChild(0).guiTexture.pixelInset.height);
            this.transform.GetChild(0).guiTexture.pixelInset = rekt;
        }
	}
}