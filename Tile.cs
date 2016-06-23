using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour
{
    private int m_idx;

    public void SetIdx(int _idx)
    {
        m_idx = _idx;
    }

    public int GetIdx()
    {
        return m_idx;
    }
}