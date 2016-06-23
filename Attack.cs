using UnityEngine;
using System.Collections;

public class Attack : MonoBehaviour
{
    private GameObject m_obj;
    private float t = 0.0f;
    private DIRECTION m_direction;

    public void SetObj(GameObject _obj)
    {
        m_obj = _obj;
        m_obj.SetActive(false);
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
        t += Time.deltaTime;
        if (t >= 0.7f)
        {
            if(m_obj != null)
                m_obj.SetActive(true);
            Destroy(this.gameObject);
        }
	}
}