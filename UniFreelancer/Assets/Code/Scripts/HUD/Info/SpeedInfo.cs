using UnityEngine;
using System.Collections;

public class SpeedInfo : MonoBehaviour
{
    public Sprite[] Speeds;

	void Start()
    {
	}
	
	void Update()
    {
        float f = GameController.Player.rigidbody.velocity.magnitude;
        float p = GameController.Player.GetComponent<ShipInput>().GetVelocityPercentage();
        
        float tab = (10 * ((p * 10f) / (100f / 15f))) - 0.1f;
        this.guiTexture.texture = Speeds[(int)tab].texture;

        if ((int)f == 0)
            this.guiTexture.texture = null;

        this.guiText.text = (int)f + " m/s";
	}
}
