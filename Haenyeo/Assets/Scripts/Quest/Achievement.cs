using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName ="Quest/Achievement", fileName ="Achievement_")]
public class Achievement : Quest_
{
    public override bool IsCancelable => base.IsCancelable;
    public override bool IsSavable => true;

    public override void Cancel()
    {
        Debug.LogAssertion("Achieve cant be canceled");
    }
}
