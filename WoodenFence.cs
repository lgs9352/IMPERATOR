using UnityEngine;
using System.Collections;

public class WoodenFence : MonoBehaviour
{
    private int create_turn;
    private int idx;

	void Start ()
    {
        create_turn = Global.turn;
    }
	
	void Update ()
    {
        if ((create_turn + 2) <= Global.turn)
        {
            Global.unitIdx[idx].isFence = false;
            Global.unitIdx[idx].isUnit = false;
            Destroy(this.gameObject);
        }
	}

    public void SetObstacle(int _idx)
    {
        idx = _idx;
        Global.unitIdx[idx].isFence = true;
        Global.unitIdx[idx].isUnit = true;
    }
}