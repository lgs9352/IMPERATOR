using UnityEngine;
using System.Collections;
using System.Collections.Generic;

enum DECKSTATE
{
    READY,
    SETSELECT,
    SELECT,
    SETOBJECT,
    SELECTCEHCK,
    COMPLETE,
};

public class DeckManager : MonoBehaviour
{
    private GameObject m_target;
    private DECKSTATE m_state;
    private Vector3[] m_name_pos;
    private Vector3[] m_arrow_pos;
    private int m_select;
    private string m_user;
    private int m_current_unit_num;

    void Start()
    {
        m_current_unit_num = 0;
        m_user = "P1";
        m_select = -1;
        Global.unit_count = 0;
        m_name_pos = new Vector3[8];
        float PosY = 0.272f;
        for (int i = 0; i < 8; ++i)
        {
            m_name_pos[i] = new Vector3(-0.66f, PosY, 0.0f);
            PosY -= 0.146f;
        }
        float PosX = -1.205f;
        m_arrow_pos = new Vector3[8];
        for (int i = 0; i < 8; ++i)
        {
            m_arrow_pos[i] = new Vector3(PosX, 0.48f, 0.0f);
            PosX += 0.348f;
        }
        m_state = DECKSTATE.READY;
    }

    void Update()
    {
        Debug.Log(m_state);
        switch (m_state)
        {
            case DECKSTATE.READY:
                if (UserInput.IsClicked() && UserInput.GetClickedObject() != null)
                {
                    m_target = UserInput.GetClickedObject();
                    if (m_target.gameObject.tag == "Select")
                    {
                        m_state = DECKSTATE.SETSELECT;
                    }
                }
                break;
            case DECKSTATE.SETSELECT:
                SetSelect();
                break;
            case DECKSTATE.SELECT:
                Select();
                break;
            case DECKSTATE.SELECTCEHCK:
                SelectCheck();
                break;
            case DECKSTATE.SETOBJECT:
                SetObject();
                break;
            case DECKSTATE.COMPLETE:
                Complete();
                break;
            default:
                break;
        }
    }

    private void SelectCheck()
    {
        GameObject target = null;
        if (UserInput.IsClicked() && UserInput.GetClickedObject() != null)
        {
            target = UserInput.GetClickedObject();
            if (target != null)
            {
                if (target.gameObject.name != m_target.gameObject.name)
                {
                    HideExplain((UNITTYPE)m_current_unit_num);
                    m_state = DECKSTATE.SELECT;
                    if (UserInput.IsClicked())
                    {
                        m_target = UserInput.GetClickedObject();
                    }
                }

                if (target.gameObject.tag == "UnitSelect")
                {
                    SetUnit((UNITTYPE)m_current_unit_num);
                    HideExplain((UNITTYPE)m_current_unit_num);
                }
            }
        }
    }

    private void SetSelect()
    {
        if (m_target.gameObject.name == "Select0" || m_target.gameObject.name == "Select7"
             || m_target.gameObject.name == "Select1" || m_target.gameObject.name == "Select6"
             || m_target.gameObject.name == "Select8" || m_target.gameObject.name == "Select15"
             || m_target.gameObject.name == "Select9" || m_target.gameObject.name == "Select14")
        {
            if (m_target.gameObject.name == "Select0") m_select = 0;
            if (m_target.gameObject.name == "Select1") m_select = 1;
            if (m_target.gameObject.name == "Select6") m_select = 6;
            if (m_target.gameObject.name == "Select7") m_select = 7;
            if (m_target.gameObject.name == "Select8") m_select = 8;
            if (m_target.gameObject.name == "Select9") m_select = 9;
            if (m_target.gameObject.name == "Select14") m_select = 14;
            if (m_target.gameObject.name == "Select15") m_select = 15;
            GameObject.Find("Name15").gameObject.GetComponent<UILabel>().enabled = true;
            GameObject.Find("Name16").gameObject.GetComponent<UILabel>().enabled = true;
            GameObject.Find("Name15").gameObject.GetComponent<BoxCollider>().enabled = true;
            GameObject.Find("Name16").gameObject.GetComponent<BoxCollider>().enabled = true;
            GameObject.Find("Name15").gameObject.transform.position = m_name_pos[0];
            GameObject.Find("Name16").gameObject.transform.position = m_name_pos[1];
        }

        else if (m_target.gameObject.name == "Select2" || m_target.gameObject.name == "Select5"
             || m_target.gameObject.name == "Select10" || m_target.gameObject.name == "Select13")
        {
            if (m_target.gameObject.name == "Select2") m_select = 2;
            if (m_target.gameObject.name == "Select5") m_select = 5;
            if (m_target.gameObject.name == "Select10") m_select = 10;
            if (m_target.gameObject.name == "Select13") m_select = 13;
            GameObject.Find("Name7").gameObject.GetComponent<UILabel>().enabled = true;
            GameObject.Find("Name8").gameObject.GetComponent<UILabel>().enabled = true;
            GameObject.Find("Name9").gameObject.GetComponent<UILabel>().enabled = true;
            GameObject.Find("Name10").gameObject.GetComponent<UILabel>().enabled = true;
            GameObject.Find("Name11").gameObject.GetComponent<UILabel>().enabled = true;
            GameObject.Find("Name12").gameObject.GetComponent<UILabel>().enabled = true;
            GameObject.Find("Name13").gameObject.GetComponent<UILabel>().enabled = true;
            GameObject.Find("Name14").gameObject.GetComponent<UILabel>().enabled = true;

            GameObject.Find("Name7").gameObject.GetComponent<BoxCollider>().enabled = true;
            GameObject.Find("Name8").gameObject.GetComponent<BoxCollider>().enabled = true;
            GameObject.Find("Name9").gameObject.GetComponent<BoxCollider>().enabled = true;
            GameObject.Find("Name10").gameObject.GetComponent<BoxCollider>().enabled = true;
            GameObject.Find("Name11").gameObject.GetComponent<BoxCollider>().enabled = true;
            GameObject.Find("Name12").gameObject.GetComponent<BoxCollider>().enabled = true;
            GameObject.Find("Name13").gameObject.GetComponent<BoxCollider>().enabled = true;
            GameObject.Find("Name14").gameObject.GetComponent<BoxCollider>().enabled = true;
            GameObject.Find("Name7").gameObject.transform.position = m_name_pos[0];
            GameObject.Find("Name8").gameObject.transform.position = m_name_pos[1];
            GameObject.Find("Name9").gameObject.transform.position = m_name_pos[2];
            GameObject.Find("Name10").gameObject.transform.position = m_name_pos[3];
            GameObject.Find("Name11").gameObject.transform.position = m_name_pos[4];
            GameObject.Find("Name12").gameObject.transform.position = m_name_pos[5];
            GameObject.Find("Name13").gameObject.transform.position = m_name_pos[6];
            GameObject.Find("Name14").gameObject.transform.position = m_name_pos[7];
        }

        else if (m_target.gameObject.name == "Select3" || m_target.gameObject.name == "Select11")
        {
            if (m_target.gameObject.name == "Select3") m_select = 3;
            if (m_target.gameObject.name == "Select11") m_select = 11;
            GameObject.Find("Name1").gameObject.GetComponent<UILabel>().enabled = true;
            GameObject.Find("Name2").gameObject.GetComponent<UILabel>().enabled = true;
            GameObject.Find("Name3").gameObject.GetComponent<UILabel>().enabled = true;

            GameObject.Find("Name1").gameObject.GetComponent<BoxCollider>().enabled = true;
            GameObject.Find("Name2").gameObject.GetComponent<BoxCollider>().enabled = true;
            GameObject.Find("Name3").gameObject.GetComponent<BoxCollider>().enabled = true;
            GameObject.Find("Name1").gameObject.transform.position = m_name_pos[0];
            GameObject.Find("Name2").gameObject.transform.position = m_name_pos[1];
            GameObject.Find("Name3").gameObject.transform.position = m_name_pos[2];
        }

        else if (m_target.gameObject.name == "Select4" || m_target.gameObject.name == "Select12")
        {
            if (m_target.gameObject.name == "Select4") m_select = 4;
            if (m_target.gameObject.name == "Select12") m_select = 12;
            GameObject.Find("Name4").gameObject.GetComponent<UILabel>().enabled = true;
            GameObject.Find("Name5").gameObject.GetComponent<UILabel>().enabled = true;
            GameObject.Find("Name6").gameObject.GetComponent<UILabel>().enabled = true;

            GameObject.Find("Name4").gameObject.GetComponent<BoxCollider>().enabled = true;
            GameObject.Find("Name5").gameObject.GetComponent<BoxCollider>().enabled = true;
            GameObject.Find("Name6").gameObject.GetComponent<BoxCollider>().enabled = true;
            GameObject.Find("Name4").gameObject.transform.position = m_name_pos[0];
            GameObject.Find("Name5").gameObject.transform.position = m_name_pos[1];
            GameObject.Find("Name6").gameObject.transform.position = m_name_pos[2];
        }

        if (m_select < 8)
            GameObject.Find("Arrow").gameObject.transform.position = m_arrow_pos[m_select];
        else
            GameObject.Find("Arrow").gameObject.transform.position = m_arrow_pos[m_select - 8];

        Destroy(GameObject.Find("Select" + m_select).gameObject);
        m_state = DECKSTATE.SELECT;
        m_target = null;
    }

    private void Select()
    {
        if (UserInput.IsClicked())
        {
            m_target = UserInput.GetClickedObject();
        }

        if (m_target != null && m_target.gameObject.name == "Name1")
        {
            ShowExplain(UNITTYPE.KING1);
        }

        else if (m_target != null && m_target.gameObject.name == "Name2")
        {
            ShowExplain(UNITTYPE.KING2);
        }

        else if (m_target != null && m_target.gameObject.name == "Name3")
        {
            ShowExplain(UNITTYPE.KING3);
        }

        else if (m_target != null && m_target.gameObject.name == "Name4")
        {
            ShowExplain(UNITTYPE.QUEEN1);
        }

        else if (m_target != null && m_target.gameObject.name == "Name5")
        {
            ShowExplain(UNITTYPE.QUEEN2);
        }

        else if (m_target != null && m_target.gameObject.name == "Name6")
        {
            ShowExplain(UNITTYPE.QUEEN3);
        }

        else if (m_target != null && m_target.gameObject.name == "Name7")
        {
            ShowExplain(UNITTYPE.CLERIC1);
        }

        else if (m_target != null && m_target.gameObject.name == "Name8")
        {
            ShowExplain(UNITTYPE.CLERIC2);
        }

        else if (m_target != null && m_target.gameObject.name == "Name9")
        {
            ShowExplain(UNITTYPE.CLERIC3);
        }

        else if (m_target != null && m_target.gameObject.name == "Name10")
        {
            ShowExplain(UNITTYPE.ADVISOR1);
        }

        else if (m_target != null && m_target.gameObject.name == "Name11")
        {
            ShowExplain(UNITTYPE.ADVISOR2);
        }

        else if (m_target != null && m_target.gameObject.name == "Name12")
        {
            ShowExplain(UNITTYPE.ADVISOR3);
        }

        else if (m_target != null && m_target.gameObject.name == "Name13")
        {
            ShowExplain(UNITTYPE.AGENT1);
        }

        else if (m_target != null && m_target.gameObject.name == "Name14")
        {
            ShowExplain(UNITTYPE.AGENT2);
        }

        else if (m_target != null && m_target.gameObject.name == "Name15")
        {
            ShowExplain(UNITTYPE.CAVALRY);
        }

        else if (m_target != null && m_target.gameObject.name == "Name16")
        {
            ShowExplain(UNITTYPE.ARCHER);
        }
    }

    private void ShowExplain(UNITTYPE _type)
    {
        m_current_unit_num = (int)_type;
        switch (_type)
        {
            case UNITTYPE.KING1:
                GameObject.Find("SkillExplain1").gameObject.GetComponent<UILabel>().enabled = true;
                GameObject.Find("UnitExplain1").gameObject.GetComponent<UILabel>().enabled = true;
                GameObject.Find("StrikingPower1").gameObject.GetComponent<UILabel>().enabled = true;
                GameObject.Find("Health1").gameObject.GetComponent<UILabel>().enabled = true;
                GameObject.Find("lnocomotivePower1").gameObject.GetComponent<UILabel>().enabled = true;
                break;
            case UNITTYPE.KING2:
                GameObject.Find("SkillExplain2").gameObject.GetComponent<UILabel>().enabled = true;
                GameObject.Find("UnitExplain2").gameObject.GetComponent<UILabel>().enabled = true;
                GameObject.Find("StrikingPower2").gameObject.GetComponent<UILabel>().enabled = true;
                GameObject.Find("Health2").gameObject.GetComponent<UILabel>().enabled = true;
                GameObject.Find("lnocomotivePower2").gameObject.GetComponent<UILabel>().enabled = true;
                break;
            case UNITTYPE.KING3:
                GameObject.Find("UnitExplain3").gameObject.GetComponent<UILabel>().enabled = true;
                GameObject.Find("SkillExplain3").gameObject.GetComponent<UILabel>().enabled = true;
                GameObject.Find("StrikingPower3").gameObject.GetComponent<UILabel>().enabled = true;
                GameObject.Find("Health3").gameObject.GetComponent<UILabel>().enabled = true;
                GameObject.Find("lnocomotivePower3").gameObject.GetComponent<UILabel>().enabled = true;
                break;
            case UNITTYPE.QUEEN1:
                GameObject.Find("UnitExplain4").gameObject.GetComponent<UILabel>().enabled = true;
                GameObject.Find("SkillExplain4").gameObject.GetComponent<UILabel>().enabled = true;
                GameObject.Find("StrikingPower4").gameObject.GetComponent<UILabel>().enabled = true;
                GameObject.Find("Health4").gameObject.GetComponent<UILabel>().enabled = true;
                GameObject.Find("lnocomotivePower4").gameObject.GetComponent<UILabel>().enabled = true;
                break;
            case UNITTYPE.QUEEN2:
                GameObject.Find("UnitExplain5").gameObject.GetComponent<UILabel>().enabled = true;
                GameObject.Find("SkillExplain5").gameObject.GetComponent<UILabel>().enabled = true;
                GameObject.Find("StrikingPower5").gameObject.GetComponent<UILabel>().enabled = true;
                GameObject.Find("Health5").gameObject.GetComponent<UILabel>().enabled = true;
                GameObject.Find("lnocomotivePower5").gameObject.GetComponent<UILabel>().enabled = true;
                break;
            case UNITTYPE.QUEEN3:
                GameObject.Find("UnitExplain6").gameObject.GetComponent<UILabel>().enabled = true;
                GameObject.Find("SkillExplain6").gameObject.GetComponent<UILabel>().enabled = true;
                GameObject.Find("StrikingPower6").gameObject.GetComponent<UILabel>().enabled = true;
                GameObject.Find("Health6").gameObject.GetComponent<UILabel>().enabled = true;
                GameObject.Find("lnocomotivePower6").gameObject.GetComponent<UILabel>().enabled = true;
                break;
            case UNITTYPE.CLERIC1:
                GameObject.Find("UnitExplain7").gameObject.GetComponent<UILabel>().enabled = true;
                GameObject.Find("SkillExplain7").gameObject.GetComponent<UILabel>().enabled = true;
                GameObject.Find("StrikingPower7").gameObject.GetComponent<UILabel>().enabled = true;
                GameObject.Find("Health7").gameObject.GetComponent<UILabel>().enabled = true;
                GameObject.Find("lnocomotivePower7").gameObject.GetComponent<UILabel>().enabled = true;
                break;
            case UNITTYPE.CLERIC2:
                GameObject.Find("UnitExplain8").gameObject.GetComponent<UILabel>().enabled = true;
                GameObject.Find("SkillExplain8").gameObject.GetComponent<UILabel>().enabled = true;
                GameObject.Find("StrikingPower8").gameObject.GetComponent<UILabel>().enabled = true;
                GameObject.Find("Health8").gameObject.GetComponent<UILabel>().enabled = true;
                GameObject.Find("lnocomotivePower8").gameObject.GetComponent<UILabel>().enabled = true;
                break;
            case UNITTYPE.CLERIC3:
                GameObject.Find("UnitExplain9").gameObject.GetComponent<UILabel>().enabled = true;
                GameObject.Find("SkillExplain9").gameObject.GetComponent<UILabel>().enabled = true;
                GameObject.Find("StrikingPower9").gameObject.GetComponent<UILabel>().enabled = true;
                GameObject.Find("Health9").gameObject.GetComponent<UILabel>().enabled = true;
                GameObject.Find("lnocomotivePower9").gameObject.GetComponent<UILabel>().enabled = true;
                break;
            case UNITTYPE.ADVISOR1:
                GameObject.Find("UnitExplain10").gameObject.GetComponent<UILabel>().enabled = true;
                GameObject.Find("SkillExplain10").gameObject.GetComponent<UILabel>().enabled = true;
                GameObject.Find("StrikingPower10").gameObject.GetComponent<UILabel>().enabled = true;
                GameObject.Find("Health10").gameObject.GetComponent<UILabel>().enabled = true;
                GameObject.Find("lnocomotivePower10").gameObject.GetComponent<UILabel>().enabled = true;
                break;
            case UNITTYPE.ADVISOR2:
                GameObject.Find("UnitExplain11").gameObject.GetComponent<UILabel>().enabled = true;
                GameObject.Find("SkillExplain11").gameObject.GetComponent<UILabel>().enabled = true;
                GameObject.Find("StrikingPower11").gameObject.GetComponent<UILabel>().enabled = true;
                GameObject.Find("Health11").gameObject.GetComponent<UILabel>().enabled = true;
                GameObject.Find("lnocomotivePower11").gameObject.GetComponent<UILabel>().enabled = true;
                break;
            case UNITTYPE.ADVISOR3:
                GameObject.Find("UnitExplain12").gameObject.GetComponent<UILabel>().enabled = true;
                GameObject.Find("SkillExplain12").gameObject.GetComponent<UILabel>().enabled = true;
                GameObject.Find("StrikingPower12").gameObject.GetComponent<UILabel>().enabled = true;
                GameObject.Find("Health12").gameObject.GetComponent<UILabel>().enabled = true;
                GameObject.Find("lnocomotivePower12").gameObject.GetComponent<UILabel>().enabled = true;
                break;
            case UNITTYPE.AGENT1:
                GameObject.Find("UnitExplain13").gameObject.GetComponent<UILabel>().enabled = true;
                GameObject.Find("SkillExplain13").gameObject.GetComponent<UILabel>().enabled = true;
                GameObject.Find("StrikingPower13").gameObject.GetComponent<UILabel>().enabled = true;
                GameObject.Find("Health13").gameObject.GetComponent<UILabel>().enabled = true;
                GameObject.Find("lnocomotivePower13").gameObject.GetComponent<UILabel>().enabled = true;
                break;
            case UNITTYPE.AGENT2:
                GameObject.Find("UnitExplain14").gameObject.GetComponent<UILabel>().enabled = true;
                GameObject.Find("SkillExplain14").gameObject.GetComponent<UILabel>().enabled = true;
                GameObject.Find("StrikingPower14").gameObject.GetComponent<UILabel>().enabled = true;
                GameObject.Find("Health14").gameObject.GetComponent<UILabel>().enabled = true;
                GameObject.Find("lnocomotivePower14").gameObject.GetComponent<UILabel>().enabled = true;
                break;
            case UNITTYPE.CAVALRY:
                GameObject.Find("UnitExplain15").gameObject.GetComponent<UILabel>().enabled = true;
                GameObject.Find("SkillExplain15").gameObject.GetComponent<UILabel>().enabled = true;
                GameObject.Find("StrikingPower15").gameObject.GetComponent<UILabel>().enabled = true;
                GameObject.Find("Health15").gameObject.GetComponent<UILabel>().enabled = true;
                GameObject.Find("lnocomotivePower15").gameObject.GetComponent<UILabel>().enabled = true;
                break;
            case UNITTYPE.ARCHER:
                GameObject.Find("UnitExplain16").gameObject.GetComponent<UILabel>().enabled = true;
                GameObject.Find("SkillExplain16").gameObject.GetComponent<UILabel>().enabled = true;
                GameObject.Find("StrikingPower16").gameObject.GetComponent<UILabel>().enabled = true;
                GameObject.Find("Health16").gameObject.GetComponent<UILabel>().enabled = true;
                GameObject.Find("lnocomotivePower16").gameObject.GetComponent<UILabel>().enabled = true;
                break;
            case UNITTYPE.UINT:
                GameObject.Find("UnitExplain17").gameObject.GetComponent<UILabel>().enabled = true;
                GameObject.Find("SkillExplain17").gameObject.GetComponent<UILabel>().enabled = false;
                GameObject.Find("StrikingPower17").gameObject.GetComponent<UILabel>().enabled = false;
                GameObject.Find("Health17").gameObject.GetComponent<UILabel>().enabled = false;
                GameObject.Find("lnocomotivePower17").gameObject.GetComponent<UILabel>().enabled = false;
                break;
            default:
                break;
        }
        m_state = DECKSTATE.SELECTCEHCK;
    }

    private void HideExplain(UNITTYPE _type)
    {
        switch (_type)
        {
            case UNITTYPE.KING1:
                GameObject.Find("UnitExplain1").gameObject.GetComponent<UILabel>().enabled = false;
                GameObject.Find("SkillExplain1").gameObject.GetComponent<UILabel>().enabled = false;
                GameObject.Find("StrikingPower1").gameObject.GetComponent<UILabel>().enabled = false;
                GameObject.Find("Health1").gameObject.GetComponent<UILabel>().enabled = false;
                GameObject.Find("lnocomotivePower1").gameObject.GetComponent<UILabel>().enabled = false;
                break;
            case UNITTYPE.KING2:
                GameObject.Find("UnitExplain2").gameObject.GetComponent<UILabel>().enabled = false;
                GameObject.Find("SkillExplain2").gameObject.GetComponent<UILabel>().enabled = false;
                GameObject.Find("StrikingPower2").gameObject.GetComponent<UILabel>().enabled = false;
                GameObject.Find("Health2").gameObject.GetComponent<UILabel>().enabled = false;
                GameObject.Find("lnocomotivePower2").gameObject.GetComponent<UILabel>().enabled = false;
                break;
            case UNITTYPE.KING3:
                GameObject.Find("UnitExplain3").gameObject.GetComponent<UILabel>().enabled = false;
                GameObject.Find("SkillExplain3").gameObject.GetComponent<UILabel>().enabled = false;
                GameObject.Find("StrikingPower3").gameObject.GetComponent<UILabel>().enabled = false;
                GameObject.Find("Health3").gameObject.GetComponent<UILabel>().enabled = false;
                GameObject.Find("lnocomotivePower3").gameObject.GetComponent<UILabel>().enabled = false;
                break;
            case UNITTYPE.QUEEN1:
                GameObject.Find("UnitExplain4").gameObject.GetComponent<UILabel>().enabled = false;
                GameObject.Find("SkillExplain4").gameObject.GetComponent<UILabel>().enabled = false;
                GameObject.Find("StrikingPower4").gameObject.GetComponent<UILabel>().enabled = false;
                GameObject.Find("Health4").gameObject.GetComponent<UILabel>().enabled = false;
                GameObject.Find("lnocomotivePower4").gameObject.GetComponent<UILabel>().enabled = false;
                break;
            case UNITTYPE.QUEEN2:
                GameObject.Find("UnitExplain5").gameObject.GetComponent<UILabel>().enabled = false;
                GameObject.Find("SkillExplain5").gameObject.GetComponent<UILabel>().enabled = false;
                GameObject.Find("StrikingPower5").gameObject.GetComponent<UILabel>().enabled = false;
                GameObject.Find("Health5").gameObject.GetComponent<UILabel>().enabled = false;
                GameObject.Find("lnocomotivePower5").gameObject.GetComponent<UILabel>().enabled = false;
                break;
            case UNITTYPE.QUEEN3:
                GameObject.Find("UnitExplain6").gameObject.GetComponent<UILabel>().enabled = false;
                GameObject.Find("SkillExplain6").gameObject.GetComponent<UILabel>().enabled = false;
                GameObject.Find("StrikingPower6").gameObject.GetComponent<UILabel>().enabled = false;
                GameObject.Find("Health6").gameObject.GetComponent<UILabel>().enabled = false;
                GameObject.Find("lnocomotivePower6").gameObject.GetComponent<UILabel>().enabled = false;
                break;
            case UNITTYPE.CLERIC1:
                GameObject.Find("UnitExplain7").gameObject.GetComponent<UILabel>().enabled = false;
                GameObject.Find("SkillExplain7").gameObject.GetComponent<UILabel>().enabled = false;
                GameObject.Find("StrikingPower7").gameObject.GetComponent<UILabel>().enabled = false;
                GameObject.Find("Health7").gameObject.GetComponent<UILabel>().enabled = false;
                GameObject.Find("lnocomotivePower7").gameObject.GetComponent<UILabel>().enabled = false;
                break;
            case UNITTYPE.CLERIC2:
                GameObject.Find("UnitExplain8").gameObject.GetComponent<UILabel>().enabled = false;
                GameObject.Find("SkillExplain8").gameObject.GetComponent<UILabel>().enabled = false;
                GameObject.Find("StrikingPower8").gameObject.GetComponent<UILabel>().enabled = false;
                GameObject.Find("Health8").gameObject.GetComponent<UILabel>().enabled = false;
                GameObject.Find("lnocomotivePower8").gameObject.GetComponent<UILabel>().enabled = false;
                break;
            case UNITTYPE.CLERIC3:
                GameObject.Find("UnitExplain9").gameObject.GetComponent<UILabel>().enabled = false;
                GameObject.Find("SkillExplain9").gameObject.GetComponent<UILabel>().enabled = false;
                GameObject.Find("StrikingPower9").gameObject.GetComponent<UILabel>().enabled = false;
                GameObject.Find("Health9").gameObject.GetComponent<UILabel>().enabled = false;
                GameObject.Find("lnocomotivePower9").gameObject.GetComponent<UILabel>().enabled = false;
                break;
            case UNITTYPE.ADVISOR1:
                GameObject.Find("UnitExplain10").gameObject.GetComponent<UILabel>().enabled = false;
                GameObject.Find("SkillExplain10").gameObject.GetComponent<UILabel>().enabled = false;
                GameObject.Find("StrikingPower10").gameObject.GetComponent<UILabel>().enabled = false;
                GameObject.Find("Health10").gameObject.GetComponent<UILabel>().enabled = false;
                GameObject.Find("lnocomotivePower10").gameObject.GetComponent<UILabel>().enabled = false;
                break;
            case UNITTYPE.ADVISOR2:
                GameObject.Find("UnitExplain11").gameObject.GetComponent<UILabel>().enabled = false;
                GameObject.Find("SkillExplain11").gameObject.GetComponent<UILabel>().enabled = false;
                GameObject.Find("StrikingPower11").gameObject.GetComponent<UILabel>().enabled = false;
                GameObject.Find("Health11").gameObject.GetComponent<UILabel>().enabled = false;
                GameObject.Find("lnocomotivePower11").gameObject.GetComponent<UILabel>().enabled = false;
                break;
            case UNITTYPE.ADVISOR3:
                GameObject.Find("UnitExplain12").gameObject.GetComponent<UILabel>().enabled = false;
                GameObject.Find("SkillExplain12").gameObject.GetComponent<UILabel>().enabled = false;
                GameObject.Find("StrikingPower12").gameObject.GetComponent<UILabel>().enabled = false;
                GameObject.Find("Health12").gameObject.GetComponent<UILabel>().enabled = false;
                GameObject.Find("lnocomotivePower12").gameObject.GetComponent<UILabel>().enabled = false;
                break;
            case UNITTYPE.AGENT1:
                GameObject.Find("UnitExplain13").gameObject.GetComponent<UILabel>().enabled = false;
                GameObject.Find("SkillExplain13").gameObject.GetComponent<UILabel>().enabled = false;
                GameObject.Find("StrikingPower13").gameObject.GetComponent<UILabel>().enabled = false;
                GameObject.Find("Health13").gameObject.GetComponent<UILabel>().enabled = false;
                GameObject.Find("lnocomotivePower13").gameObject.GetComponent<UILabel>().enabled = false;
                break;
            case UNITTYPE.AGENT2:
                GameObject.Find("UnitExplain14").gameObject.GetComponent<UILabel>().enabled = false;
                GameObject.Find("SkillExplain14").gameObject.GetComponent<UILabel>().enabled = false;
                GameObject.Find("StrikingPower14").gameObject.GetComponent<UILabel>().enabled = false;
                GameObject.Find("Health14").gameObject.GetComponent<UILabel>().enabled = false;
                GameObject.Find("lnocomotivePower14").gameObject.GetComponent<UILabel>().enabled = false;
                break;
            case UNITTYPE.CAVALRY:
                GameObject.Find("UnitExplain15").gameObject.GetComponent<UILabel>().enabled = false;
                GameObject.Find("SkillExplain15").gameObject.GetComponent<UILabel>().enabled = false;
                GameObject.Find("StrikingPower15").gameObject.GetComponent<UILabel>().enabled = false;
                GameObject.Find("Health15").gameObject.GetComponent<UILabel>().enabled = false;
                GameObject.Find("lnocomotivePower15").gameObject.GetComponent<UILabel>().enabled = false;
                break;
            case UNITTYPE.ARCHER:
                GameObject.Find("UnitExplain16").gameObject.GetComponent<UILabel>().enabled = false;
                GameObject.Find("SkillExplain16").gameObject.GetComponent<UILabel>().enabled = false;
                GameObject.Find("StrikingPower16").gameObject.GetComponent<UILabel>().enabled = false;
                GameObject.Find("Health16").gameObject.GetComponent<UILabel>().enabled = false;
                GameObject.Find("lnocomotivePower16").gameObject.GetComponent<UILabel>().enabled = false;
                break;
            case UNITTYPE.UINT:
                GameObject.Find("UnitExplain17").gameObject.GetComponent<UILabel>().enabled = false;
                GameObject.Find("SkillExplain17").gameObject.GetComponent<UILabel>().enabled = false;
                GameObject.Find("StrikingPower17").gameObject.GetComponent<UILabel>().enabled = false;
                GameObject.Find("Health17").gameObject.GetComponent<UILabel>().enabled = false;
                GameObject.Find("lnocomotivePower17").gameObject.GetComponent<UILabel>().enabled = false;
                break;
            default:
                break;
        }
    }

    private void SetUnit(UNITTYPE _type)
    {
        Global.unit_list[Global.unit_count].user = m_user;
        Global.unit_list[Global.unit_count].type = _type;
        if (Global.unit_count < 8) Global.unit_list[Global.unit_count].pos = m_select;
        else
        {
            Global.unit_list[Global.unit_count].user = m_user;
            Global.unit_list[Global.unit_count].pos = m_select + 32;
        }
        Global.unit_count++;
        m_target = null;
        m_state = DECKSTATE.SETOBJECT;
    }

    private void SetObject()
    {
        GameObject.Find("Name1").gameObject.GetComponent<UILabel>().enabled = false;
        GameObject.Find("Name2").gameObject.GetComponent<UILabel>().enabled = false;
        GameObject.Find("Name3").gameObject.GetComponent<UILabel>().enabled = false;
        GameObject.Find("Name4").gameObject.GetComponent<UILabel>().enabled = false;
        GameObject.Find("Name5").gameObject.GetComponent<UILabel>().enabled = false;
        GameObject.Find("Name6").gameObject.GetComponent<UILabel>().enabled = false;
        GameObject.Find("Name7").gameObject.GetComponent<UILabel>().enabled = false;
        GameObject.Find("Name8").gameObject.GetComponent<UILabel>().enabled = false;
        GameObject.Find("Name9").gameObject.GetComponent<UILabel>().enabled = false;
        GameObject.Find("Name10").gameObject.GetComponent<UILabel>().enabled = false;
        GameObject.Find("Name11").gameObject.GetComponent<UILabel>().enabled = false;
        GameObject.Find("Name12").gameObject.GetComponent<UILabel>().enabled = false;
        GameObject.Find("Name13").gameObject.GetComponent<UILabel>().enabled = false;
        GameObject.Find("Name14").gameObject.GetComponent<UILabel>().enabled = false;
        GameObject.Find("Name15").gameObject.GetComponent<UILabel>().enabled = false;
        GameObject.Find("Name16").gameObject.GetComponent<UILabel>().enabled = false;

        GameObject.Find("Name1").gameObject.GetComponent<BoxCollider>().enabled = false;
        GameObject.Find("Name2").gameObject.GetComponent<BoxCollider>().enabled = false;
        GameObject.Find("Name3").gameObject.GetComponent<BoxCollider>().enabled = false;
        GameObject.Find("Name4").gameObject.GetComponent<BoxCollider>().enabled = false;
        GameObject.Find("Name5").gameObject.GetComponent<BoxCollider>().enabled = false;
        GameObject.Find("Name6").gameObject.GetComponent<BoxCollider>().enabled = false;
        GameObject.Find("Name7").gameObject.GetComponent<BoxCollider>().enabled = false;
        GameObject.Find("Name8").gameObject.GetComponent<BoxCollider>().enabled = false;
        GameObject.Find("Name9").gameObject.GetComponent<BoxCollider>().enabled = false;
        GameObject.Find("Name10").gameObject.GetComponent<BoxCollider>().enabled = false;
        GameObject.Find("Name11").gameObject.GetComponent<BoxCollider>().enabled = false;
        GameObject.Find("Name12").gameObject.GetComponent<BoxCollider>().enabled = false;
        GameObject.Find("Name13").gameObject.GetComponent<BoxCollider>().enabled = false;
        GameObject.Find("Name14").gameObject.GetComponent<BoxCollider>().enabled = false;
        GameObject.Find("Name15").gameObject.GetComponent<BoxCollider>().enabled = false;
        GameObject.Find("Name16").gameObject.GetComponent<BoxCollider>().enabled = false;

        m_state = DECKSTATE.READY;

        if (Global.unit_count > 7 && m_user == "P1")
        {
            m_user = "P2";
            GameObject.Find("Select8").gameObject.GetComponent<BoxCollider>().enabled = true;
            GameObject.Find("Select9").gameObject.GetComponent<BoxCollider>().enabled = true;
            GameObject.Find("Select10").gameObject.GetComponent<BoxCollider>().enabled = true;
            GameObject.Find("Select11").gameObject.GetComponent<BoxCollider>().enabled = true;
            GameObject.Find("Select12").gameObject.GetComponent<BoxCollider>().enabled = true;
            GameObject.Find("Select13").gameObject.GetComponent<BoxCollider>().enabled = true;
            GameObject.Find("Select14").gameObject.GetComponent<BoxCollider>().enabled = true;
            GameObject.Find("Select15").gameObject.GetComponent<BoxCollider>().enabled = true;
            GameObject.Find("Arrow").gameObject.transform.position = m_arrow_pos[0];
        }

        else if (Global.unit_count > 15 && m_user == "P2")
        {
            m_state = DECKSTATE.COMPLETE;
        }
    }

    private void Complete()
    {
        Application.LoadLevel(2);
    }
}