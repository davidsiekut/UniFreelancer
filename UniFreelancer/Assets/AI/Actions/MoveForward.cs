using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;

[RAINAction]
public class MoveForward : RAINAction
{
    public override void Start(AI ai)
    {
        base.Start(ai);

        // move forward
        ai.WorkingMemory.SetItem<float>("up", 0);
        ai.WorkingMemory.SetItem<float>("right", 0);
        ai.WorkingMemory.SetItem<float>("forward", 1f);

        // countdown timer
        float f = ai.WorkingMemory.GetItem<float>("varTimeToNew");
        f -= ai.DeltaTime;
        ai.WorkingMemory.SetItem<float>("varTimeToNew", f);

        if (f < 0)
        {
            // reset timer
            float n = Random.Range(5f, 6f);
            ai.WorkingMemory.SetItem<float>("varTimeToNew", n);

            // start to turn
            ai.WorkingMemory.SetItem<bool>("varTurning", true);
        }
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