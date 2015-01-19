using UnityEngine;
using System.Collections;

public class InventorySlot : MonoBehaviour
{
	void Start()
    {
	}
	
	void Update()
    {
        this.guiText.text = this.transform.GetChild(0).GetComponent<Weapon>().Name;
	}

    void OnMouseDown()
    {
        this.transform.parent.GetComponent<Inventory>().Current = this.transform.GetChild(0).gameObject;
        this.transform.parent.GetComponent<Inventory>().LastClicked = this.gameObject;
    }
}
