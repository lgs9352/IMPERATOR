using UnityEngine;
using System.Collections;

public class King2 : Unit
{
    public override void Skill()
    {
        if (this.gameObject.GetComponent<Unit>().GetUser() == "P1")
        {
            for (int i = 8; i < 16; ++i)
            {
                Global.unit[i].gameObject.GetComponent<Unit>().Move();
            }
        }
        else if(this.gameObject.GetComponent<Unit>().GetUser() == "P2")
        {
            for (int i = 24; i < 32; ++i)
            {
                Global.unit[i].gameObject.GetComponent<Unit>().Move();
            }
        }
    }

    void Update()
    {
        if (m_iLife <= 0)
            Application.LoadLevel(0);
    }
}