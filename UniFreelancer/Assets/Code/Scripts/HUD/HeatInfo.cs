using UnityEngine;
using System.Collections;

public class HeatInfo : MonoBehaviour
{
    //int flashCount = 0;
    //int flashSpeed = 10;
    bool flashing = false;

    public GUITexture WarningHeat;
    public AudioClip Sound;

	void Start()
    {
	}
	
	void Update()
    {
        float h = GameController.PlayerHeat;
        float p = GameController.GetPlayerHeatPercent();

        this.guiText.text = (int)h + "°";
        //Rect rekt = new Rect(this.guiTexture.pixelInset.x, this.guiTexture.pixelInset.y,
        //    GameController.GetPlayerHeatPercent() * 50.0f, this.guiTexture.pixelInset.height);
        //this.guiTexture.pixelInset = rekt;

        if (p > 0.9 && !flashing)
        {
            flashing = true;
            this.guiText.color = new Color(1f, 0.627f, 0.627f);
            InvokeRepeating("Flash", 0.3f, 0.3f);
        }
        else if (p > 0.9)
        {
            
        }
        else
        {
            flashing = false;
            this.guiText.color = new Color(1f, 1f, 1f);
            WarningHeat.enabled = false;
            CancelInvoke();
        }
	}

    void Flash()
    {
        WarningHeat.enabled = !WarningHeat.enabled;
        if (WarningHeat.enabled)
            GameController.HUDSound.PlayOneShot(Sound, 0.1f);
    }
}
