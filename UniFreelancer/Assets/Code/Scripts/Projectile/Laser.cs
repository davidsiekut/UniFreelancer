using UnityEngine;
using System.Collections;

public class Laser : MonoBehaviour
{
    public Transform follow;
    public float Range;

	void Start()
    {
	}
	
	void Update()
    {
        Vector3 v = GameController.Player.transform.position + Camera.main.transform.forward * Range;

        if (follow != null)
        {
            this.GetComponent<LineRenderer>().SetPosition(0, follow.position);
            this.GetComponent<LineRenderer>().SetPosition(1, v);
        }
	}
}
