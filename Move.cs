using UnityEngine;
using System.Collections;

public class Move : MonoBehaviour
{
    private DIRECTION m_direction;
    private int m_iPosition;
    private int m_iPrePosition;
    private float t = 0.0f;
    private GameObject m_obj;

    void Start()
    {

    }

    public void SetObj(GameObject _obj)
    {
        m_obj = _obj;
        m_obj.gameObject.SetActive(false);
    }

    public void SetPos(int _preposition, int _position)
    {
        m_iPrePosition = _preposition;
        m_iPosition = _position;
    }

    public void SetDirection(DIRECTION _direction)
    {
        m_direction = _direction;
        Vector3 angle = new Vector3(0.0f, 90.0f * (int)m_direction, 0.0f);
        transform.rotation = Quaternion.identity;
        transform.Rotate(angle);
    }

    void Update ()
    {
        t += Time.deltaTime * 1.5f;
        switch (m_direction)
        {
            case DIRECTION.UP:
                transform.position = new Vector3(transform.position.x, transform.position.y, Mathf.Lerp(Global.unit_pos[m_iPrePosition].z, Global.unit_pos[m_iPosition].z, t));
                if (transform.position.z >= Global.unit_pos[m_iPosition].z)
                {
                    if (m_obj != null)
                        m_obj.SetActive(true);
                    Destroy(this.gameObject);
                }
                break;
            case DIRECTION.DOWN:
                transform.position = new Vector3(transform.position.x, transform.position.y, Mathf.Lerp(Global.unit_pos[m_iPrePosition].z, Global.unit_pos[m_iPosition].z, t));
                if (transform.position.z <= Global.unit_pos[m_iPosition].z)
                {
                    if (m_obj != null)
                        m_obj.SetActive(true);
                    Destroy(this.gameObject);
                }
                break;
            case DIRECTION.LEFT:
                break;
            case DIRECTION.RIGHT:
                break;
            default:
                break;
        }
	}
}