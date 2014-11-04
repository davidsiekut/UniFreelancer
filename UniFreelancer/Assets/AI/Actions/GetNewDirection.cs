using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;

[RAINAction]
public class GetNewDirection : RAINAction
{
    public Vector3 directionVector;

    public GetNewDirection()
    {
        this.actionName = "GetNewDirection";
    }

    public override void Start(AI ai)
    {
        base.Start(ai);

        //Calculate Distance to Wander
        float distanceToWander = Random.Range(0, 500);

        //Set Random Direction
        Vector3 newVector = Random.insideUnitCircle;
        directionVector.x = newVector.x * distanceToWander;
        directionVector.z = newVector.y * distanceToWander;
        directionVector.y = newVector.z * distanceToWander;

        float angle = Vector3.Angle(ai.Body.transform.forward, directionVector);

        //Set Position to Move to relative to Current Position
        directionVector = ai.Body.transform.position + directionVector;
        //directionVector.y = 0f;

        //Set location into working memory for Move action in BT
        ai.WorkingMemory.SetItem<Vector3>("directionVector", directionVector);
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