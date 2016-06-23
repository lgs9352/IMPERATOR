using UnityEngine;
using System.Collections;

public class Adviser2 : Unit
{
    public GameObject m_obj_prefab;

    public override void Skill(int _idx)
    {
        GameObject unit_obj = Instantiate(m_obj_prefab, Global.unit_pos[_idx], Quaternion.identity) as GameObject;
        unit_obj.gameObject.GetComponent<WoodenFence>().SetObstacle(_idx);
    }
}