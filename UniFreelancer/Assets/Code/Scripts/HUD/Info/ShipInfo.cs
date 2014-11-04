using UnityEngine;
using System.Collections;

public class ShipInfo : MonoBehaviour
{
    public GUITexture Shield1;
    public GUITexture Shield2;
    public GUITexture Shield3;
    public GUITexture Status;

    public Texture StatusGreen;
    public Texture StatusYellow;
    public Texture StatusRed;

	void Start()
    {
	}
	
	void Update()
    {
        float h = GameController.Player.GetComponent<Entity>().GetHealthPercent();
        float s = GameController.Player.GetComponent<Entity>().GetShieldPercent();
        //Debug.Log("HP " + h + " SHIELD " + s);

        Shield3.enabled = (s >= 1 ? true : false);
        Shield2.enabled = (s > 0.66 ? true : false);
        Shield1.enabled = (s > 0.33 ? true : false);

        if (h > 0.75)
        {
            Status.guiTexture.texture = StatusGreen;
        }
        else if (h > 0.30)
        {
            Status.guiTexture.texture = StatusYellow;
        }
        else
        {
            Status.guiTexture.texture = StatusRed;
        }

	}
}
