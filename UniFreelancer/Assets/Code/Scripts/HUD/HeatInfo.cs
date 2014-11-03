using UnityEngine;
using System.Collections;

public class HeatInfo : MonoBehaviour
{
	void Start()
    {
	}
	
	void Update()
    {
        this.guiText.text = (int)GameController.PlayerHeat + "°";
        //Rect rekt = new Rect(this.guiTexture.pixelInset.x, this.guiTexture.pixelInset.y,
        //    GameController.GetPlayerHeatPercent() * 50.0f, this.guiTexture.pixelInset.height);
        //this.guiTexture.pixelInset = rekt;
	}
}
