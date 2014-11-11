using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;

[RAINAction]
public class FireWeapons : RAINAction
{
    public override void Start(AI ai)
    {
        base.Start(ai);

        Transform t = ai.WorkingMemory.GetItem<GameObject>("varFront").transform;
        float m = Mathf.Sqrt(ai.WorkingMemory.GetItem<GameObject>("varFront").rigidbody.velocity.magnitude);

        Ray r = new Ray(ai.Body.transform.position, (t.position + t.forward * m) - ai.Body.transform.position);
        ai.Body.GetComponentInChildren<WeaponSystem>().FirePrimary(r);
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