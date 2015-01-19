using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;

[RAINAction]
public class RandomTurn : RAINAction
{
    const float TURN_TIME_DEFAULT = 2f;

    public override void Start(AI ai)
    {
        base.Start(ai);

        float t = ai.WorkingMemory.GetItem<float>("varTurnTime");

        if (t == TURN_TIME_DEFAULT)
        {
            // set up initial turn vectors, this needs to only be done once due to randoms
            int u = Random.Range(-10, 10);
            ai.WorkingMemory.SetItem<float>("up", u);

            int r = Random.Range(-10, 10);
            ai.WorkingMemory.SetItem<float>("right", r);

            t -= ai.DeltaTime;
            ai.WorkingMemory.SetItem<float>("varTurnTime", t);
        }
        else if (t < 0)
        {
            //timer up, reset
            ai.WorkingMemory.SetItem<float>("varTurnTime", TURN_TIME_DEFAULT);
            // stop turning
            ai.WorkingMemory.SetItem<bool>("varTurning", false);
        }
        else
        {
            t -= ai.DeltaTime;
            ai.WorkingMemory.SetItem<float>("varTurnTime", t);
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