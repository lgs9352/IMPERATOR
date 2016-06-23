using UnityEngine;
using System.Collections;

public enum DIRECTION
{
    UP,
    RIGHT,
    DOWN,
    LEFT,
};

public enum UNITTYPE
{
    UINT,
    KING1,
    KING2,
    KING3,
    QUEEN1,
    QUEEN2,
    QUEEN3,
    CLERIC1,
    CLERIC2,
    CLERIC3,
    ADVISOR1,
    ADVISOR2,
    ADVISOR3,
    AGENT1,
    AGENT2,
    CAVALRY,
    ARCHER,
};

public class Unit : MonoBehaviour
{
    public GameObject m_moveing;
    public GameObject m_attacking;
    public GameObject m_deathing;
    public GameObject[] m_arrow;

    protected int m_iIdx;
    protected string m_user;
    protected int m_iStrikingPower;
    protected int m_iMaxLife;
    protected int m_iLife;
    protected int m_iPosition;
    protected int m_iMobility;
    protected bool m_bIsBattle;
    protected DIRECTION m_direction;
    protected bool m_bMove;
    protected bool m_bAttack;

    private int m_iPrePosition;
    private bool m_bAttacked;
    private float current_time;
    private float prev_time = 0.0f;

    void Update()
    {
        current_time += Time.deltaTime;

        if (m_bMove)
        {
            GameObject obj = Instantiate(m_moveing,Global.unit_pos[m_iPrePosition],Quaternion.identity) as GameObject;
            obj.gameObject.GetComponent<Move>().SetDirection(m_direction);
            obj.gameObject.GetComponent<Move>().SetPos(m_iPrePosition, m_iPosition);
            obj.gameObject.GetComponent<Move>().SetObj(this.gameObject);
            m_bMove = false;
        }

        if (m_bAttack)
        {
            GameObject obj = Instantiate(m_attacking, Global.unit_pos[m_iPosition], Quaternion.identity) as GameObject;
            obj.gameObject.GetComponent<Attack>().SetObj(this.gameObject);
            obj.gameObject.GetComponent<Attack>().SetDirection(m_direction);
            m_bAttack = false;
        }

        if (m_bAttacked)
        {
            prev_time = current_time;
            m_bAttacked = false;
        }

        if (prev_time + 0.5f <= current_time)
        {
            if (m_iLife <= 0)
            {
                Global.unitIdx[m_iPosition].idx = -1;
                Global.unitIdx[m_iPosition].isUnit = false;
                GameObject obj = Instantiate(m_deathing, Global.unit_pos[m_iPosition], Quaternion.identity) as GameObject;
                obj.gameObject.GetComponent<Death>().SetDirection(m_direction);
                Destroy(this.gameObject);
            }
        }
    }

    public bool AttackCheck()
    {
        switch (m_direction)
        {
            case DIRECTION.UP:
                if (Global.unitIdx[m_iPosition + 8].isFence)
                {
                    return false;
                }
                else if (Global.unitIdx[m_iPosition + 8].isUnit)
                {
                    return true;
                }
                break;
            case DIRECTION.RIGHT:
                if (Global.unitIdx[m_iPosition + 1].isFence)
                {
                    return false;
                }
                else if (Global.unitIdx[m_iPosition + 1].isUnit)
                {
                    return true;
                }
                break;
            case DIRECTION.DOWN:
                if (Global.unitIdx[m_iPosition - 8].isFence)
                {
                    return false;
                }
                else if (Global.unitIdx[m_iPosition - 8].isUnit)
                {
                    return true;
                }
                break;
            case DIRECTION.LEFT:
                if (Global.unitIdx[m_iPosition - 1].isFence)
                {
                    return false;
                }
                else if (Global.unitIdx[m_iPosition - 1].isUnit)
                {
                    return true;
                }
                break;
            default:
                break;
        }
        return false;
    }

    public virtual void Skill()
    {
    }

    public virtual void Skill(GameObject _target)
    {
    }

    public virtual void Skill(GameObject[] _target)
    {
    }

    public virtual void Skill(int _idx)
    {
    }

    public virtual void Attack()
    {
        if (AttackCheck())
        {
            int enemy = 0;
            m_bAttack = true;
            switch (m_direction)
            {
                
                case DIRECTION.UP:
                    enemy = m_iPosition + 8;
                    Global.unit[FindUnit(enemy)].gameObject.GetComponent<Unit>().BeAttacked(m_iStrikingPower);
                    break;
                case DIRECTION.RIGHT:
                    enemy = m_iPosition + 1;
                    Global.unit[FindUnit(enemy)].gameObject.GetComponent<Unit>().BeAttacked(m_iStrikingPower);
                    break;
                case DIRECTION.DOWN:
                    enemy = m_iPosition - 8;
                    Global.unit[FindUnit(enemy)].gameObject.GetComponent<Unit>().BeAttacked(m_iStrikingPower);
                    break;
                case DIRECTION.LEFT:
                    enemy = m_iPosition - 1;
                    Global.unit[FindUnit(enemy)].gameObject.GetComponent<Unit>().BeAttacked(m_iStrikingPower);
                    break;
                default:
                    break;
            }
            Debug.Log("Attack!");
        }
        else
        {
            Debug.Log("Is Not Unit");
        }
    }

    private int FindUnit(int _enemy)
    {
        return Global.unitIdx[_enemy].idx;
    }

    public bool MoveCheck()
    {
        switch (m_direction)
        {
            case DIRECTION.UP:
                if (!Global.unitIdx[m_iPosition + 8].isUnit && !Global.unitIdx[m_iPosition + 8].isFence)
                    return true;
                break;
            case DIRECTION.RIGHT:
                if (!Global.unitIdx[m_iPosition + 1].isUnit && !Global.unitIdx[m_iPosition + 1].isFence)
                    return true;
                break;
            case DIRECTION.DOWN:
                if (!Global.unitIdx[m_iPosition - 8].isUnit && !Global.unitIdx[m_iPosition - 8].isFence)
                    return true;
                break;
            case DIRECTION.LEFT:
                if (!Global.unitIdx[m_iPosition - 1].isUnit && !Global.unitIdx[m_iPosition - 1].isFence)
                    return true;
                break;
            default:
                break;
        }
        return false;
    }

    public int GetTileIdx()
    {
        int num = 0;
        switch (m_direction)
        {
            case DIRECTION.UP:
                num = m_iPosition + 8;
                break;
            case DIRECTION.RIGHT:
                num = m_iPosition + 1;
                break;
            case DIRECTION.DOWN:
                num = m_iPosition - 8;
                break;
            case DIRECTION.LEFT:
                num = m_iPosition - 1;
                break;
            default:
                break;
        }
        return num;
    }

    public virtual void Move()
    {
        if (MoveCheck())
        {
            Global.unitIdx[m_iPosition].isUnit = false;
            Global.unitIdx[m_iPosition].idx = -1;
            m_iPrePosition = m_iPosition;
            switch (m_direction)
            {
                case DIRECTION.UP:
                    if (m_iPosition < 40) m_iPosition += 8;
                    break;
                case DIRECTION.RIGHT:
                    if ((m_iPosition + 1) % 8 == 0)
                        break;
                    else
                        m_iPosition += 1;
                    break;
                case DIRECTION.DOWN:
                    if (m_iPosition > 7) m_iPosition -= 8;
                    break;
                case DIRECTION.LEFT:
                    if ((m_iPosition % 8) == 0)
                        break;
                    else
                        m_iPosition -= 1;
                    break;
                default:
                    break;
            }
            transform.position = Global.unit_pos[m_iPosition];
            Global.unitIdx[m_iPosition].idx = m_iIdx;
            Global.unitIdx[m_iPosition].isUnit = true;
            m_bMove = true;
        }
        else
        {

        }
    }

    public void BeAttacked(int _strikingPower)
    {
        m_iLife -= _strikingPower;
        m_bAttacked = true;
    }

    public void BeHealed(int _healQuantity)
    {
        if (m_iMaxLife > m_iLife)
        {
            m_iLife += _healQuantity;
            if (m_iLife > m_iMaxLife)
                m_iLife = m_iMaxLife;
        }
        Debug.Log("Healed!");
    }

    public void SetUser(string _user)
    {
        m_user = _user;
    }

    public void SetDirection(DIRECTION _direction)
    {
        m_direction = _direction;
        Vector3 angle = new Vector3(0.0f, 90.0f * (int)m_direction, 0.0f);
        transform.rotation = Quaternion.identity;
        transform.Rotate(angle);
    }

    public void SetPos(int _pos)
    {
        m_iPosition = _pos;
    }

    public void SetIdx(int _idx)
    {
        m_iIdx = _idx;
    }

    public void SetLife(int _life)
    {
        m_iMaxLife = _life;
        m_iLife = _life;
    }

    public void SetStrikingPower(int _strikingPower)
    {
        m_iStrikingPower = _strikingPower;
    }

    public void SetMobility(int _mobility)
    {
        m_iMobility = _mobility;
    }

    public int GetIdx()
    {
        return m_iIdx;
    }

    public int GetPos()
    {
        return m_iPosition;
    }

    public int GetLife()
    {
        return m_iLife;
    }

    public DIRECTION GetDirection()
    {
        return m_direction;
    }

    public string GetUser()
    {
        return m_user;
    }
}