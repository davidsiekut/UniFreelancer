using UnityEngine;
using System.Collections;

public class ShieldControl : BaseControl
{
    void Start()
    {
        c1 = new Color(0.2f, 1f, 1f, 1f);
        c2 = new Color(0.2f, 1f, 1f, 0.2f);

        base.Init();
    }

    void Update()
    {
        //base.Refresh(ship.GetComponent<ShipInput>().GetShield(), ship.GetComponent<ShipInput>().GetShieldMax(), "");
    }
}
