using UnityEngine;
using System.Collections;

public class King3 : Unit
{
    public override void Skill()
    {
        Debug.Log("Kings Skill");
    }

    void Update()
    {
        if (m_iLife <= 0)
            Application.LoadLevel(0);
    }
}