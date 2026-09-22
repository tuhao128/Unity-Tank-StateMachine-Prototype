using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "Standing", menuName = "Premise/Standing")]
public class StandingPremise : BasePremise
{
    [SerializeField]
    private LayerMask mask;
    public override bool ShouldSwitch(in TransitionInformation transitionInformation, in PremiseInformation premiseInformation, in Condition condition, in StateInformation information)
    {
        return false;
    }

    //这里会计入mask之外的所有层级
    public override bool ShouldSwitch(in TransitionInformation transitionInformation, in PremiseInformation premiseInformation, in Context context, in StateInformation information)
    {
        RaycastHit2D hit2D = Physics2D.Raycast(context.controlledTransform.position, Vector2.down, context.controlledCollider2D.bounds.extents.y * 1.1f, ~mask);
        if (premiseInformation.boolNum)
        {
            return hit2D.collider != null;
        }
        else
        {
            return hit2D.collider == null;
        }
    }
}
