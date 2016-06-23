using UnityEngine;
using System.Collections;

public class King1 : Unit
{
    void Update()
    {
        if (m_iLife <= 0)
            Application.LoadLevel(0);
    }
}