using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;

[RAINAction]
public class FireMissiles : RAINAction
{
    public override void Start(AI ai)
    {
        base.Start(ai);

        // reset
        ai.WorkingMemory.SetItem<float>("varMissile", 20f);
        Transform t = ai.WorkingMemory.GetItem<GameObject>("varFront").transform;

        // TODO disable this or make it permanent
        ai.Body.rigidbody.freezeRotation = true;

        // TODO needs to be different for other ships
        ai.Body.GetComponentInChildren<WeaponSystem>().FireSlot(1, t);
    }

    public override ActionResult Execute(AI ai)
    {
        return ActionResult.SUCCESS;
    }

    public override void Stop(AI ai)
    {
        base.Stop(ai);
    }
}