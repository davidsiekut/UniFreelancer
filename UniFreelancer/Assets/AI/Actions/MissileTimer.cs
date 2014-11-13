using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;

[RAINAction]
public class MissileTimer : RAINAction
{
    public override void Start(AI ai)
    {
        base.Start(ai);
        float f = ai.WorkingMemory.GetItem<float>("varMissile");
        f -= ai.DeltaTime;
        ai.WorkingMemory.SetItem<float>("varMissile", f);
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