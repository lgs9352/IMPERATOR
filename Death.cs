using UnityEngine;
using System.Collections;

public class Death : MonoBehaviour
{
    private float t = 0.0f;
    private DIRECTION m_direction;

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
        if (t >= 1.5f)
            Destroy(this.gameObject);	
	}
}
