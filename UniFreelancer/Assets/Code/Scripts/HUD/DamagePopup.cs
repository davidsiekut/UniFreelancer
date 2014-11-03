using UnityEngine;
using System.Collections;

public class DamagePopup : MonoBehaviour
{
    float lifeTime = 0.5f;
    public GameObject Target;
    public float Damage;

	void Start()
    {
	}
	
	void Update()
    {
        if (Target != null)
        {
            Vector3 screen = Camera.main.WorldToScreenPoint(Target.transform.position);
            if (screen.z > 0
                && screen.x > 0 && screen.x < Screen.width
                && screen.y > 0 && screen.y < Screen.height)
            {
                this.guiText.enabled = true;
                this.transform.localPosition = Camera.main.ScreenToViewportPoint(screen);
                this.guiText.text = (int)Damage + "";
            }
            else
            {
                this.guiText.enabled = false;
            }
        }

        Vector2 o = this.guiText.pixelOffset;
        o.y += 80 * Time.deltaTime;
        this.guiText.pixelOffset = o;

        lifeTime -= Time.deltaTime;
        if (lifeTime < 0)
            GameObject.Destroy(this.gameObject);
	}
}
