using UnityEngine;
using System.Collections;

public class Cleric1 : Unit
{
    public override void Skill()
    {
        
    }

    public override void Skill(GameObject _target)
    {
        _target.GetComponent<Unit>().BeHealed(2);
    }
}