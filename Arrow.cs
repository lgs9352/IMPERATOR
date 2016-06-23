using UnityEngine;
using System.Collections;

public class Arrow : MonoBehaviour
{
    private int idx;

    public void SetIdx(int _idx)
    {
        idx = _idx;
    }

    public void SetRotate(Vector3 _angle)
    {
        transform.rotation = Quaternion.identity;
        transform.Rotate(_angle);
    }

    public void SetPosOne(Vector3 _pos)
    {
        Vector3 pos = Vector3.zero;

        if (this.gameObject.tag == "DirectionArrowLeft") _pos.x += 0.1f;
        else _pos.x -= 0.1f;

        _pos.y += 0.25f;
        pos = _pos;

        transform.position = pos;
    }

    public void SetPosTwo(Vector3 _pos)
    {
        Vector3 pos = Vector3.zero;

        if (this.gameObject.tag == "DirectionArrowLeft") _pos.x += 0.1f;
        else _pos.x -= 0.1f;

        _pos.y += 0.25f;
        pos = _pos;

        transform.position = _pos;
    }

    public void SetPosThree(Vector3 _pos)
    {
        Vector3 pos = Vector3.zero;
        if (this.gameObject.tag == "DirectionArrowLeft") _pos.z += 0.1f;
        else _pos.z -= 0.1f;

        _pos.y += 0.25f;
        pos = _pos;

        transform.position = _pos;
    }

    public void SetPosFour(Vector3 _pos)
    {
        Vector3 pos = Vector3.zero;

        if (this.gameObject.tag == "DirectionArrowLeft") _pos.z += 0.1f;
        else _pos.z -= 0.1f;

        _pos.y += 0.25f;
        pos = _pos;

        transform.position = _pos;
    }
}