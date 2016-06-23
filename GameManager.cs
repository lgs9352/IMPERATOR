using UnityEngine;
using System.Collections;

public enum GAMESTATE
{
    READY,
    PLAYERONEINPUT,
    PLAYERTWOINPUT,
    PLAYERONEORDER,
    PLAYERTWOORDER,
    SELECTSKILLTARGET,
    SELECTACTION,
    SELECTTILE,
    CHECK,
};

public class GameManager : MonoBehaviour
{
    public Material[] m_material;
    public GameObject[] m_arrow;

    private GAMESTATE m_state;

    private GameObject m_target;
    private GameObject m_skill_target;

    private int m_userOne_count;
    private int m_userOne_move;
    private int m_userOne_attack;
    private int m_userOne_skill;
    private int m_userOne_direction;
    private int[] m_userOne_tile;
    private int[] m_userOne_moveUnit;
    private int[] m_userOne_attackUnit;
    private int[] m_userOne_skillUnit;
    private int[] m_userOne_directionUnit;
    private DIRECTION[] m_userOneDirection;
    private GameObject[] m_userOne_skillTarget;

    private int m_userTwo_count;
    private int m_userTwo_move;
    private int m_userTwo_attack;
    private int m_userTwo_skill;
    private int m_userTwo_direction;
    private int[] m_userTwo_tile;
    private int[] m_userTwo_moveUnit;
    private int[] m_userTwo_attackUnit;
    private int[] m_userTwo_skillUnit;
    private int[] m_userTwo_directionUnit;
    private DIRECTION[] m_userTwoDirection;
    private GameObject[] m_userTwo_skillTarget;

    private int m_skill_count;
    private string m_phase;
    private bool m_bMove;
    private bool m_bAttack;
    private bool m_bDirection;
    private bool m_bArrow;
    private bool m_bSkill;

    void Start ()
    {
        Global.turn = 1;
        m_state = GAMESTATE.PLAYERONEINPUT;
        m_skill_count = 0;
        m_phase = "User1";

        m_userOne_count = 0;
        m_userOne_move = 0;
        m_userOne_attack = 0;
        m_userOne_skill = 0;
        m_userOne_direction = 0;
        m_userOne_moveUnit = new int[3];
        m_userOne_attackUnit = new int[3];
        m_userOne_skillUnit = new int[3];
        m_userOne_skillTarget = new GameObject[4];
        m_userOne_directionUnit = new int[3];
        m_userOne_tile = new int[3];
        m_userOneDirection = new DIRECTION[3];

        m_userTwo_move = 0;
        m_userTwo_attack = 0;
        m_userTwo_skill = 0;
        m_userTwo_count = 0;
        m_userTwo_direction = 0;
        m_userTwo_moveUnit = new int[3];
        m_userTwo_attackUnit = new int[3];
        m_userTwo_skillUnit = new int[3];
        m_userTwo_skillTarget = new GameObject[4];
        m_userTwo_directionUnit = new int[3];
        m_userTwo_tile = new int[3];
        m_userTwoDirection = new DIRECTION[3];

        for (int i = 0; i < 4; ++i)
        {
            m_userOne_skillTarget[i] = null;
            m_userTwo_skillTarget[i] = null;
        }
    }
	
	void Update ()
    {
        DoPlay();
        Debug.Log(m_state);
    }

    private void DoPlay()
    {
        Global.user_one_count = m_userOne_count;
        Global.user_two_count = m_userTwo_count;
        
        switch (m_state)
        {
            case GAMESTATE.READY:
                break;
            case GAMESTATE.PLAYERONEINPUT:
                UserOneInput();
                break;
            case GAMESTATE.PLAYERONEORDER:
                UserOneOrder();
                break;
            case GAMESTATE.PLAYERTWOINPUT:
                UserTwoInput();
                break;
            case GAMESTATE.PLAYERTWOORDER:
                UserTwoOrder();
                break;
            case GAMESTATE.SELECTACTION:
                SelectMoveTile();
                SelectDirection();
                SelectAttackTarget();
                if (Input.GetKeyDown(KeyCode.S))
                {
                    if (m_target.gameObject.tag == "Advisor2")
                    {
                        m_bSkill = true;
                        m_state = GAMESTATE.SELECTTILE;
                    }

                    if (m_target.gameObject.tag == "Cleric1")
                    {
                        m_bSkill = true;
                        m_state = GAMESTATE.SELECTSKILLTARGET;
                    }

                    if (m_target.gameObject.tag == "King2")
                    {
                        m_bSkill = true;
                        if (m_phase == "User1") m_state = GAMESTATE.PLAYERONEORDER;
                        else if (m_phase == "User2") m_state = GAMESTATE.PLAYERTWOORDER;
                    }
                }
                break;
            case GAMESTATE.SELECTSKILLTARGET:
                SelectTarget();
                break;
            case GAMESTATE.SELECTTILE:
                SelectTile();
                break;
            case GAMESTATE.CHECK:
                SkillCheck();
                MoveCheck();
                AttackCheck();
                break;
            default:
                break;
        }
    }

    private void SelectMoveTile()
    {
        GameObject obj = null;
        if(m_target.gameObject.GetComponent<Unit>().MoveCheck())
            Global.Tile[m_target.gameObject.GetComponent<Unit>().GetTileIdx()].gameObject.GetComponent<Renderer>().material = m_material[1];

        if (UserInput.GetClickedObject() != null && UserInput.IsClicked() && m_target.gameObject.GetComponent<Unit>().MoveCheck())
        {
            obj = UserInput.GetClickedObject();
            if (obj.gameObject.tag == "Tile" && m_target.gameObject.GetComponent<Unit>().GetTileIdx() == obj.gameObject.GetComponent<Tile>().GetIdx())
            {
                Global.Tile[m_target.gameObject.GetComponent<Unit>().GetTileIdx()].gameObject.GetComponent<Renderer>().material = m_material[0];
                
                m_bMove = true;
                if (m_userOne_count < 3) m_state = GAMESTATE.PLAYERONEORDER;
                else m_state = GAMESTATE.PLAYERTWOORDER;
            }
        }
    }

    private void SelectAttackTarget()
    {
        GameObject obj = null;

        if (m_target.gameObject.GetComponent<Unit>().AttackCheck())
        {
            Global.Tile[m_target.gameObject.GetComponent<Unit>().GetTileIdx()].gameObject.GetComponent<Renderer>().material = m_material[2];
        }

        if(UserInput.GetClickedObject() != null && UserInput.IsClicked() && m_target.gameObject.GetComponent<Unit>().AttackCheck())
        {
            obj = UserInput.GetClickedObject();
            if (m_target.gameObject.tag != "Tile" && !m_bArrow)
            {
                if(m_target.gameObject.GetComponent<Unit>().GetTileIdx() == obj.gameObject.GetComponent<Unit>().GetPos())
                Global.Tile[m_target.gameObject.GetComponent<Unit>().GetTileIdx()].gameObject.GetComponent<Renderer>().material = m_material[0];
                m_bAttack = true;
                if (m_userOne_count < 3) m_state = GAMESTATE.PLAYERONEORDER;
                else m_state = GAMESTATE.PLAYERTWOORDER;
            }
        }
    }

    private void SelectDirection()
    {
        DIRECTION direction = 0;
        direction = m_target.GetComponent<Unit>().GetDirection();
        if (!m_bDirection)
        {
            int pos1 = 0;
            int pos2 = 0;
            bool bpos1 = true;
            bool bpos2 = true;

            if (direction == DIRECTION.UP)
            {
                pos1 = m_target.gameObject.GetComponent<Unit>().GetPos() - 1;
                pos2 = m_target.gameObject.GetComponent<Unit>().GetPos() + 1;
                if ((m_target.gameObject.GetComponent<Unit>().GetPos()) % 8 == 0)
                {
                    bpos1 = false;
                    pos1 = 20;
                }
                if ((m_target.gameObject.GetComponent<Unit>().GetPos() + 1) % 8 == 0)
                {
                    bpos2 = false;
                    pos2 = 20;
                }
            }
            else if (direction == DIRECTION.DOWN)
            {
                pos1 = m_target.gameObject.GetComponent<Unit>().GetPos() - 1;
                pos2 = m_target.gameObject.GetComponent<Unit>().GetPos() + 1;
                if (m_target.gameObject.GetComponent<Unit>().GetPos() % 8 == 0)
                    bpos2 = false;
                if ((m_target.gameObject.GetComponent<Unit>().GetPos() + 1) % 8 == 0)
                    bpos1 = false;
            }
            else if (direction == DIRECTION.LEFT)
            {
                pos1 = m_target.gameObject.GetComponent<Unit>().GetPos() - 8;
                pos2 = m_target.gameObject.GetComponent<Unit>().GetPos() + 8;
                if (m_target.gameObject.GetComponent<Unit>().GetPos() < 8)
                {
                    bpos1 = false;
                    pos1 = 20;
                }
                if (m_target.gameObject.GetComponent<Unit>().GetPos() > 39)
                {
                    bpos2 = false;
                    pos1 = 20;
                }
            }
            else if (direction == DIRECTION.RIGHT)
            {
                pos1 = m_target.gameObject.GetComponent<Unit>().GetPos() + 8;
                pos2 = m_target.gameObject.GetComponent<Unit>().GetPos() - 8;
                if (m_target.gameObject.GetComponent<Unit>().GetPos() < 8)
                {
                    bpos2 = false;
                    pos2 = 0;
                }
                if (m_target.gameObject.GetComponent<Unit>().GetPos() > 39) bpos1 = false;
            }

            if (m_phase == "User1")
            {
                if (direction == DIRECTION.UP)
                {
                    m_arrow[0].GetComponent<Arrow>().SetRotate(new Vector3(90.0f, -90.0f, 0.0f));
                    m_arrow[1].GetComponent<Arrow>().SetRotate(new Vector3(90.0f, 90.0f, 0.0f));
                    m_arrow[0].gameObject.GetComponent<Arrow>().SetPosOne(Global.unit_pos[pos1]);
                    m_arrow[1].gameObject.GetComponent<Arrow>().SetPosOne(Global.unit_pos[pos2]);
                }
                else if (direction == DIRECTION.LEFT)
                {
                    m_arrow[0].GetComponent<Arrow>().SetRotate(new Vector3(90.0f, 180.0f, 0.0f));
                    m_arrow[1].GetComponent<Arrow>().SetRotate(new Vector3(90.0f, 0.0f, 0.0f));
                    m_arrow[0].gameObject.GetComponent<Arrow>().SetPosThree(Global.unit_pos[pos1]);
                    m_arrow[1].gameObject.GetComponent<Arrow>().SetPosThree(Global.unit_pos[pos2]);
                }
                else if (direction == DIRECTION.RIGHT)
                {
                    m_arrow[0].GetComponent<Arrow>().SetRotate(new Vector3(90.0f, 0.0f, 0.0f));
                    m_arrow[1].GetComponent<Arrow>().SetRotate(new Vector3(90.0f, 180.0f, 0.0f));
                    m_arrow[0].gameObject.GetComponent<Arrow>().SetPosThree(Global.unit_pos[pos1 - 8]);
                    m_arrow[1].gameObject.GetComponent<Arrow>().SetPosThree(Global.unit_pos[pos2 + 8]);
                }
                else if (direction == DIRECTION.DOWN)
                {
                    m_arrow[0].GetComponent<Arrow>().SetRotate(new Vector3(90.0f, 90.0f, 0.0f));
                    m_arrow[1].GetComponent<Arrow>().SetRotate(new Vector3(90.0f, -90.0f, 0.0f));
                    m_arrow[0].gameObject.GetComponent<Arrow>().SetPosOne(Global.unit_pos[pos2 - 1]);
                    m_arrow[1].gameObject.GetComponent<Arrow>().SetPosOne(Global.unit_pos[pos1 + 1]);
                }
                m_arrow[0].SetActive(bpos1);
                m_arrow[1].SetActive(bpos2);
            }
            else
            {
                if (direction == DIRECTION.DOWN)
                {
                    m_arrow[2].GetComponent<Arrow>().SetRotate(new Vector3(90.0f, 90.0f, 0.0f));
                    m_arrow[3].GetComponent<Arrow>().SetRotate(new Vector3(90.0f, -90.0f, 0.0f));
                    m_arrow[3].gameObject.GetComponent<Arrow>().SetPosTwo(Global.unit_pos[pos1 + 1]);
                    m_arrow[2].gameObject.GetComponent<Arrow>().SetPosTwo(Global.unit_pos[pos2 - 1]);
                }
                else if (direction == DIRECTION.LEFT)
                {
                    m_arrow[2].GetComponent<Arrow>().SetRotate(new Vector3(90.0f, 180.0f, 0.0f));
                    m_arrow[3].GetComponent<Arrow>().SetRotate(new Vector3(90.0f, 0.0f, 0.0f));
                    m_arrow[2].gameObject.GetComponent<Arrow>().SetPosFour(Global.unit_pos[pos2 - 16]);
                    m_arrow[3].gameObject.GetComponent<Arrow>().SetPosFour(Global.unit_pos[pos1 + 16]);
                }
                else if (direction == DIRECTION.RIGHT)
                {
                    m_arrow[2].GetComponent<Arrow>().SetRotate(new Vector3(90.0f, 0.0f, 0.0f));
                    m_arrow[3].GetComponent<Arrow>().SetRotate(new Vector3(90.0f, 180.0f, 0.0f));
                    m_arrow[3].gameObject.GetComponent<Arrow>().SetPosFour(Global.unit_pos[pos1 - 8]);
                    m_arrow[2].gameObject.GetComponent<Arrow>().SetPosFour(Global.unit_pos[pos2 + 8]);
                }
                else if (direction == DIRECTION.UP)
                {
                    m_arrow[2].GetComponent<Arrow>().SetRotate(new Vector3(90.0f, -90.0f, 0.0f));
                    m_arrow[3].GetComponent<Arrow>().SetRotate(new Vector3(90.0f, 90.0f, 0.0f));
                    m_arrow[3].gameObject.GetComponent<Arrow>().SetPosTwo(Global.unit_pos[pos2]);
                    m_arrow[2].gameObject.GetComponent<Arrow>().SetPosTwo(Global.unit_pos[pos1]);
                }
                m_arrow[2].SetActive(bpos1);
                m_arrow[3].SetActive(bpos2);
            }
            m_bDirection = true;
        }
        else if(m_bDirection)
        {
            GameObject obj = null;
            if (UserInput.GetClickedObject() != null && UserInput.IsClicked())
            {
                int num = 0;
                obj = UserInput.GetClickedObject();
                if (obj.gameObject.tag == "DirectionArrowLeft")
                {
                    if (m_phase == "User1")
                    {
                        num = (int)direction - 1;
                        if (num < 0) num = 3;
                        m_userOneDirection[m_userOne_count] = (DIRECTION)num;
                    }
                    else
                    {
                        num = (int)direction - 1;
                        if (num < 0) num = 3;
                        m_userTwoDirection[m_userTwo_count] = (DIRECTION)num;
                    }
                    m_bArrow = true;
                }
                else if (obj.gameObject.tag == "DirectionArrowRight")
                {
                    if (m_phase == "User1")
                    {
                        num = (int)direction + 1;
                        if (num > 3) num = 0;
                        m_userOneDirection[m_userOne_count] = (DIRECTION)num;
                    }
                    else
                    {
                        num = (int)direction + 1;
                        if (num > 3) num = 0;
                        
                        m_userTwoDirection[m_userTwo_count] = (DIRECTION)num;
                    }
                    m_bArrow = true;
                    Global.Tile[m_target.gameObject.GetComponent<Unit>().GetTileIdx()].gameObject.GetComponent<Renderer>().material = m_material[0];
                }
                if (m_userOne_count < 3) m_state = GAMESTATE.PLAYERONEORDER;
                else m_state = GAMESTATE.PLAYERTWOORDER;
            }
        }
    }

    private void UserOneInput()
    {
        m_arrow[0].SetActive(false);
        m_arrow[1].SetActive(false);
        m_arrow[2].SetActive(false);
        m_arrow[3].SetActive(false);
        m_bDirection = false;
        if (m_userOne_count == 3)
        {
            m_state = GAMESTATE.PLAYERTWOINPUT;
            m_phase = "User2";
        }
        if (UserInput.GetClickedObject() != null)
        {
            if (UserInput.IsClicked() && UserInput.GetClickedObject().gameObject.GetComponent<Unit>().GetUser() == "P1")
            {
                m_target = UserInput.GetClickedObject();
                m_state = GAMESTATE.SELECTACTION;
            }
        }
    }

    private void UserOneOrder()
    {
        if (m_bMove)
        {
            m_userOne_moveUnit[m_userOne_count] = m_target.gameObject.GetComponent<Unit>().GetIdx();
            m_userOne_count++;
            m_userOne_move++;
            m_target = null;
            m_bMove = false;
            m_state = GAMESTATE.PLAYERONEINPUT;
        }

        if (m_bAttack)
        {
            m_userOne_attackUnit[m_userOne_attack] = m_target.gameObject.GetComponent<Unit>().GetIdx();
            m_userOne_count++;
            m_userOne_attack++;
            m_target = null;
            m_bAttack = false;
            m_state = GAMESTATE.PLAYERONEINPUT;
        }

        if (m_bArrow)
        {
            m_userOne_directionUnit[m_userOne_count] = m_target.gameObject.GetComponent<Unit>().GetIdx();
            m_userOne_count++;
            m_userOne_direction++;
            Global.Tile[m_target.gameObject.GetComponent<Unit>().GetTileIdx()].gameObject.GetComponent<Renderer>().material = m_material[0];
            m_target = null;
            m_bDirection = false;
            m_bArrow = false;
            m_state = GAMESTATE.PLAYERONEINPUT;
        }

        if (m_bSkill)
        {
            m_userOne_skillUnit[m_userOne_skill] = m_target.gameObject.GetComponent<Unit>().GetIdx();
            m_userOne_skill++;
            m_userOne_count++;
            Global.Tile[m_target.gameObject.GetComponent<Unit>().GetTileIdx()].gameObject.GetComponent<Renderer>().material = m_material[0];
            m_bSkill = false;
            m_state = GAMESTATE.PLAYERONEINPUT;
        }
    }

    private void UserTwoInput()
    {
        m_arrow[0].SetActive(false);
        m_arrow[1].SetActive(false);
        m_arrow[2].SetActive(false);
        m_arrow[3].SetActive(false);
        m_bDirection = false;
        if (m_userTwo_count == 3)
        {
            m_state = GAMESTATE.CHECK;
        }
        if (UserInput.GetClickedObject() != null)
        {
            if (UserInput.IsClicked() &&  UserInput.GetClickedObject().gameObject.GetComponent<Unit>().GetUser() == "P2")
            {
                m_target = UserInput.GetClickedObject();
                m_state = GAMESTATE.SELECTACTION;
            }
        }
    }

    private void UserTwoOrder()
    {
        if (m_bMove)
        {
            m_userTwo_moveUnit[m_userTwo_count] = m_target.gameObject.GetComponent<Unit>().GetIdx();
            m_userTwo_count++;
            m_userTwo_move++;
            m_target = null;
            m_bMove = false;
            m_state = GAMESTATE.PLAYERTWOINPUT;
        }

        if (m_bAttack)
        {
            m_userTwo_attackUnit[m_userTwo_attack] = m_target.gameObject.GetComponent<Unit>().GetIdx();
            m_userTwo_count++;
            m_userTwo_attack++;
            m_target = null;
            m_bAttack = false;
            m_state = GAMESTATE.PLAYERTWOINPUT;
        }

        if (m_bArrow)
        {
            m_userTwo_directionUnit[m_userTwo_count] = m_target.gameObject.GetComponent<Unit>().GetIdx();
            m_userTwo_count++;
            m_userTwo_direction++;
            Global.Tile[m_target.gameObject.GetComponent<Unit>().GetTileIdx()].gameObject.GetComponent<Renderer>().material = m_material[0];
            m_target = null;
            m_bDirection = false;
            m_bArrow = false;
            m_state = GAMESTATE.PLAYERTWOINPUT;
        }

        if(m_bSkill)
        {
            m_userTwo_skillUnit[m_userTwo_skill] = m_target.gameObject.GetComponent<Unit>().GetIdx();
            m_userTwo_skill++;
            m_userTwo_count++;
            Global.Tile[m_target.gameObject.GetComponent<Unit>().GetTileIdx()].gameObject.GetComponent<Renderer>().material = m_material[0];
            m_target = null;
            m_bSkill = false;
            m_state = GAMESTATE.PLAYERTWOINPUT;
        }
    }

    private void SelectTarget()
    {
        GameObject skill_target = null;

        if (UserInput.IsClicked() && UserInput.GetClickedObject() != null && UserInput.GetClickedObject().tag != "Tile")
        {
            skill_target = UserInput.GetClickedObject();
            if (m_phase == "User1")
            {
                m_userOne_skillTarget[m_userOne_skill] = skill_target;
                m_state = GAMESTATE.PLAYERONEORDER;
            }
            else
            {
                m_userTwo_skillTarget[m_userTwo_skill] = skill_target;
                m_state = GAMESTATE.PLAYERTWOORDER;
            }
        }
    }

    private void SelectTile()
    {
        GameObject tile = null;

        if (UserInput.IsClicked() && UserInput.GetClickedObject() != null && UserInput.GetClickedObject().tag == "Tile")
        {
            tile = UserInput.GetClickedObject();
            if (m_phase == "User1")
            {
                m_userOne_tile[m_userOne_skill] = tile.GetComponent<Tile>().GetIdx();
                m_state = GAMESTATE.PLAYERONEORDER;
            }
            else if(m_phase == "User2")
            {
                m_userTwo_tile[m_userTwo_skill] = tile.GetComponent<Tile>().GetIdx();
                m_state = GAMESTATE.PLAYERTWOORDER;
            }
        }
    }

    private void SkillCheck()
    {
        for (int i = 0; i < m_userOne_skill; ++i)
        {
            Global.unit[m_userOne_skillUnit[i]].gameObject.GetComponent<Unit>().Skill();
            Global.unit[m_userOne_skillUnit[i]].gameObject.GetComponent<Unit>().Skill(m_userOne_skillTarget[i]);
            Global.unit[m_userOne_skillUnit[i]].gameObject.GetComponent<Unit>().Skill(m_userOne_tile[i]);
        }

        for (int i = 0; i < m_userTwo_skill; ++i)
        {
            Global.unit[m_userTwo_skillUnit[i]].gameObject.GetComponent<Unit>().Skill();
            Global.unit[m_userTwo_skillUnit[i]].gameObject.GetComponent<Unit>().Skill(m_userTwo_skillTarget[i]);
            Global.unit[m_userTwo_skillUnit[i]].gameObject.GetComponent<Unit>().Skill(m_userTwo_tile[i]);
        }

        m_userOne_skill = 0;
        m_userTwo_skill = 0;
    }

    private void MoveCheck()
    {
        for (int i = 0; i < m_userOne_direction; ++i)
            Global.unit[m_userOne_directionUnit[i]].gameObject.GetComponent<Unit>().SetDirection(m_userOneDirection[i]);

        for (int i = 0; i < m_userTwo_direction; ++i)
            Global.unit[m_userTwo_directionUnit[i]].gameObject.GetComponent<Unit>().SetDirection(m_userTwoDirection[i]);

        for (int i = 0; i < m_userOne_move; ++i)
            Global.unit[m_userOne_moveUnit[i]].gameObject.GetComponent<Unit>().Move();

        for (int i = 0; i < m_userTwo_move; ++i)
            Global.unit[m_userTwo_moveUnit[i]].gameObject.GetComponent<Unit>().Move();

        m_userOne_direction = 0;
        m_userTwo_direction = 0;
        m_userOne_move = 0;
        m_userTwo_move = 0;      
    }

    private void AttackCheck()
    {
        for (int i = 0; i < m_userOne_attack; ++i)
            Global.unit[m_userOne_attackUnit[i]].gameObject.GetComponent<Unit>().Attack();

        for (int i = 0; i < m_userTwo_attack; ++i)
            Global.unit[m_userTwo_attackUnit[i]].gameObject.GetComponent<Unit>().Attack();

        m_userOne_attack = 0;
        m_userTwo_attack = 0;
        m_userOne_count = 0;
        m_userTwo_count = 0;
        m_phase = "User1";
        Global.turn++;
        m_state = GAMESTATE.PLAYERONEINPUT;
    }
}