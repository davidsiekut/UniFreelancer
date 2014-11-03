using UnityEngine;
using System.Collections;

public class SpeedControl : BaseControl
{
	void Start()
    {
        c1 = new Color(1f, 1f, 0.2f, 1f);
        c2 = new Color(1f, 1f, 0.2f, 0.2f);

        base.Init();
	}

	void Update()
    {
        //base.Refresh(ship.rigidbody.velocity.magnitude, ship.GetComponent<ShipInput>().GetVelocityMax(), "m/s");
	}
}
