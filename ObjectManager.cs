using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectManager : MonoBehaviour
{
    public GameObject m_tile_prefab;
    public GameObject m_unit1_prefab;
    public GameObject m_unit2_prefab;
    public GameObject m_archer1_prefab;
    public GameObject m_archer2_prefab;
    public GameObject m_cavalry1_prefab;
    public GameObject m_cavalry2_prefab;

    public GameObject m_clericOne1_prefab;
    public GameObject m_clericOne2_prefab;
    public GameObject m_clericTwo1_prefab;
    public GameObject m_clericTwo2_prefab;
    public GameObject m_clericThree1_prefab;
    public GameObject m_clericThree2_prefab;

    public GameObject m_agentOne1_prefab;
    public GameObject m_agentOne2_prefab;
    public GameObject m_agentTwo1_prefab;
    public GameObject m_agentTwo2_prefab;

    public GameObject m_advisorOne1_prefab;
    public GameObject m_advisorOne2_prefab;
    public GameObject m_advisorTwo1_prefab;
    public GameObject m_advisorTwo2_prefab;
    public GameObject m_advisorThree1_prefab;
    public GameObject m_advisorThree2_prefab;

    public GameObject m_queenOne1_prefab;
    public GameObject m_queenOne2_prefab;
    public GameObject m_queenTwo1_prefab;
    public GameObject m_queenTwo2_prefab;
    public GameObject m_queenThree1_prefab;
    public GameObject m_queenThree2_prefab;

    public GameObject m_kingOne1_prefab;
    public GameObject m_kingOne2_prefab;
    public GameObject m_kingTwo1_prefab;
    public GameObject m_kingTwo2_prefab;
    public GameObject m_kingThree1_prefab;
    public GameObject m_kingThree2_prefab;

    private GameObject[] m_unit_obj;
    private GameObject[] m_tile;
    private bool m_bMouseInput;

    private int m_state;
    private int m_unit_count;
    private int count;
    private int life;
    private int attack;
    private int mobility;
    
    void Start ()
    {
        life = 3;
        attack = 1;
        mobility = 1;
        count = 0;
        m_unit_count = 0;
        m_state = 0;
        for (int i = 0; i < 48; ++i)
            Global.unitIdx[i].idx = -1;
    }

    void Update()
    {
        switch (m_state)
        {
            case 0:
                CreateTile();
                break;
            case 1:
                SetGlobalPos();
                break;
            case 2:
                CreateDeckUnit();
                break;
            case 3:
                CerateUnit();
                break;
            default:
                break;
        }
    }

    private void CreateTile()
    {
        m_tile = new GameObject[48];
        int current_num = 0;
        int count = 0;
        float posX = -0.8f;
        float posY = -0.5f;
        float posZ = -0.2f;
        Vector3 tile_pos = new Vector3(posX, posY, posZ);

        while (current_num < 48)
        {
            m_tile[current_num] = Instantiate(m_tile_prefab, tile_pos, Quaternion.identity) as GameObject;
            m_tile[current_num].gameObject.transform.Rotate(new Vector3(-90.0f, 0.0f, 0.0f));
            m_tile[current_num].gameObject.GetComponent<Tile>().SetIdx(current_num);
            m_tile[current_num].gameObject.name = "Tile" + current_num;
            Global.Tile[current_num] = m_tile[current_num];
            current_num++;
            count++;
            posX += 0.22f;
            tile_pos.x = posX;
            if (count > 7)
            {
                count = 0;
                posX = -0.8f;
                tile_pos.x = posX;
                posZ += 0.22f;
                tile_pos.z = posZ;
            }
        }
        m_state = 1;
    }

    private void SetGlobalPos()
    {
        int current_num = 0;
        int count = 0;
        float posX = -0.8f;
        float posY = -0.4f;
        float posZ = -0.22f;

        while (current_num < 48)
        {
            Global.unit_pos[current_num] = new Vector3(posX, posY, posZ);
            current_num++;
            count++;
            posX += 0.22f;
            if (count > 7)
            {
                count = 0;
                posX = -0.8f;
                posZ += 0.22f;
            }
        }
        current_num = 0;
        count = 0;
        posX = -0.53f;
        posY = 0.15f;
        posZ = -0.33f;

        while (current_num < 42)
        {
            Global.arrow_pos[current_num] = new Vector3(posX, posY, posZ);
            current_num++;
            count++;
            posX += 0.17f;
            if (count > 6)
            {
                count = 0;
                posX = -0.53f;
                posY = 0.15f;
                posZ += 0.16f;
            }
        }
        m_state = 2;
    }

    private void CreateDeckUnit()
    {
        switch (Global.unit_list[m_unit_count].type)
        {
            case UNITTYPE.ARCHER:
                if(m_unit_count < 8) CreateDeckUnit(m_archer1_prefab);
                else CreateDeckUnit(m_archer2_prefab);
                attack = 1;
                life = 3;
                mobility = 1;
                break;
            case UNITTYPE.CAVALRY:
                if (m_unit_count < 8) CreateDeckUnit(m_cavalry1_prefab);
                else CreateDeckUnit(m_cavalry2_prefab);
                attack = 1;
                life = 3;
                mobility = 3;
                break;
            case UNITTYPE.CLERIC1:
                if (m_unit_count < 8) CreateDeckUnit(m_clericOne1_prefab);
                else CreateDeckUnit(m_clericOne2_prefab);
                attack = 1;
                life = 3;
                mobility = 1;
                break;
            case UNITTYPE.CLERIC2:
                if (m_unit_count < 8) CreateDeckUnit(m_clericTwo1_prefab);
                else CreateDeckUnit(m_clericTwo2_prefab);
                attack = 1;
                life = 3;
                mobility = 1;
                break;
            case UNITTYPE.CLERIC3:
                if (m_unit_count < 8) CreateDeckUnit(m_clericThree1_prefab);
                else CreateDeckUnit(m_clericThree2_prefab);
                attack = 1;
                life = 3;
                mobility = 1;
                break;
            case UNITTYPE.ADVISOR1:
                if (m_unit_count < 8) CreateDeckUnit(m_advisorOne1_prefab);
                else CreateDeckUnit(m_advisorOne2_prefab);
                attack = 1;
                life = 3;
                mobility = 1;
                break;
            case UNITTYPE.ADVISOR2:
                if (m_unit_count < 8) CreateDeckUnit(m_advisorTwo1_prefab);
                else CreateDeckUnit(m_advisorTwo2_prefab);
                attack = 1;
                life = 3;
                mobility = 1;
                break;
            case UNITTYPE.ADVISOR3:
                if (m_unit_count < 8) CreateDeckUnit(m_advisorThree1_prefab);
                else CreateDeckUnit(m_advisorThree2_prefab);
                attack = 1;
                life = 3;
                mobility = 1;
                break;
            case UNITTYPE.AGENT1:
                if (m_unit_count < 8) CreateDeckUnit(m_agentOne1_prefab);
                else CreateDeckUnit(m_agentOne2_prefab);
                attack = 1;
                life = 3;
                mobility = 1;
                break;
            case UNITTYPE.AGENT2:
                if (m_unit_count < 8) CreateDeckUnit(m_agentTwo1_prefab);
                else CreateDeckUnit(m_agentTwo2_prefab);
                attack = 1;
                life = 3;
                mobility = 1;
                break;
            case UNITTYPE.KING1:
                if (m_unit_count < 8) CreateDeckUnit(m_kingOne1_prefab);
                else CreateDeckUnit(m_kingOne2_prefab);
                attack = 1;
                life = 5;
                mobility = 1;
                break;
            case UNITTYPE.KING2:
                if (m_unit_count < 8) CreateDeckUnit(m_kingTwo1_prefab);
                else CreateDeckUnit(m_kingTwo2_prefab);
                attack = 1;
                life = 5;
                mobility = 1;
                break;
            case UNITTYPE.KING3:
                if (m_unit_count < 8) CreateDeckUnit(m_kingThree1_prefab);
                else CreateDeckUnit(m_kingThree2_prefab);
                attack = 1;
                life = 5;
                mobility = 1;
                break;
            case UNITTYPE.QUEEN1:
                if (m_unit_count < 8) CreateDeckUnit(m_queenOne1_prefab);
                else CreateDeckUnit(m_queenOne2_prefab);
                attack = 1;
                life = 5;
                mobility = 2;
                break;
            case UNITTYPE.QUEEN2:
                if (m_unit_count < 8) CreateDeckUnit(m_queenTwo1_prefab);
                else CreateDeckUnit(m_queenTwo2_prefab);
                attack = 2;
                life = 5;
                break;
            case UNITTYPE.QUEEN3:
                if (m_unit_count < 8) CreateDeckUnit(m_queenThree1_prefab);
                else CreateDeckUnit(m_queenThree2_prefab);
                attack = 2;
                life = 5;
                mobility = 4;
                break;
            default:
                break;
        }
    }

    private void CreateDeckUnit(GameObject _prefab)
    {
        if (m_unit_count < 16)
        {
            if (m_unit_count < 8)
            {
                GameObject unit_obj = Instantiate(_prefab, Global.unit_pos[Global.unit_list[m_unit_count].pos], Quaternion.identity) as GameObject;
                unit_obj.gameObject.GetComponent<Unit>().SetPos(Global.unit_list[m_unit_count].pos);
                unit_obj.gameObject.GetComponent<Unit>().SetUser("P1");
                unit_obj.gameObject.GetComponent<Unit>().SetDirection(DIRECTION.UP);
                unit_obj.gameObject.GetComponent<Unit>().SetIdx(m_unit_count);
                unit_obj.gameObject.GetComponent<Unit>().SetLife(life);
                unit_obj.gameObject.GetComponent<Unit>().SetStrikingPower(attack);
                unit_obj.gameObject.GetComponent<Unit>().SetMobility(mobility);
                Global.unitIdx[m_unit_count].idx = m_unit_count;
                Global.unitIdx[Global.unit_list[m_unit_count].pos].isUnit = true;
                Global.unit[m_unit_count] = unit_obj.gameObject.GetComponent<Unit>();

                GameObject unit_obj2 = Instantiate(m_unit1_prefab, Global.unit_pos[m_unit_count + 8], Quaternion.identity) as GameObject;
                unit_obj2.gameObject.GetComponent<Unit>().SetPos(m_unit_count + 8);
                unit_obj2.gameObject.GetComponent<Unit>().SetUser("P1");
                unit_obj2.gameObject.GetComponent<Unit>().SetDirection(DIRECTION.UP);
                unit_obj2.gameObject.GetComponent<Unit>().SetIdx(m_unit_count + 8);
                unit_obj2.gameObject.GetComponent<Unit>().SetLife(3);
                unit_obj2.gameObject.GetComponent<Unit>().SetStrikingPower(1);
                Global.unitIdx[m_unit_count + 8].isUnit = true;
                Global.unitIdx[m_unit_count + 8].idx = m_unit_count + 8;
                Global.unit[m_unit_count + 8] = unit_obj2.gameObject.GetComponent<Unit>();
            }

            else
            {
                GameObject unit_obj = Instantiate(_prefab, Global.unit_pos[Global.unit_list[m_unit_count].pos], Quaternion.identity) as GameObject;
                unit_obj.gameObject.GetComponent<Unit>().SetPos(Global.unit_list[m_unit_count].pos);
                unit_obj.gameObject.GetComponent<Unit>().SetUser("P2");
                unit_obj.gameObject.GetComponent<Unit>().SetDirection(DIRECTION.DOWN);
                unit_obj.gameObject.GetComponent<Unit>().SetIdx(m_unit_count + 8);
                unit_obj.gameObject.GetComponent<Unit>().SetLife(life);
                unit_obj.gameObject.GetComponent<Unit>().SetStrikingPower(attack);
                Global.unitIdx[Global.unit_list[m_unit_count].pos].isUnit = true;
                Global.unitIdx[m_unit_count + 32].idx = m_unit_count + 8;
                Global.unit[m_unit_count + 8] = unit_obj.gameObject.GetComponent<Unit>();

                GameObject unit_obj2 = Instantiate(m_unit2_prefab, Global.unit_pos[m_unit_count + 24], Quaternion.identity) as GameObject;
                unit_obj2.gameObject.GetComponent<Unit>().SetPos(m_unit_count + 24);
                unit_obj2.gameObject.GetComponent<Unit>().SetUser("P2");
                unit_obj2.gameObject.GetComponent<Unit>().SetDirection(DIRECTION.DOWN);
                unit_obj2.gameObject.GetComponent<Unit>().SetIdx(m_unit_count + 16);
                unit_obj2.gameObject.GetComponent<Unit>().SetLife(3);
                unit_obj2.gameObject.GetComponent<Unit>().SetStrikingPower(1);
                Global.unitIdx[m_unit_count + 24].isUnit = true;
                Global.unitIdx[m_unit_count + 24].idx = m_unit_count + 16;
                Global.unit[m_unit_count + 16] = unit_obj2.gameObject.GetComponent<Unit>();
            }
            m_unit_count++;
            if (m_unit_count == 16) m_state = 3;
        }
    }

    private void CerateUnit()
    {
        m_state = 4;
    }
}