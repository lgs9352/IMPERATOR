using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CreateUnitExplain : MonoBehaviour
{
    public UIFont font;
    public UIAtlas atlas;
    private List<UnitData> m_unitData_list;

    void Start()
    {
        CreateLabel();
    }

    private void CreateLabel()
    {
        DataParser dataparser = new DataParser();

        m_unitData_list = new List<UnitData>();
        dataparser.LoadTxtData("Assets/Resources/DeckEdit/UnitData.txt");
        dataparser.ParsingUnitData();
        m_unitData_list = dataparser.GetUnitDataList();

        for (int i = 0; i < m_unitData_list.Count; ++i)
        {
            // 병종 라벨생성
            UILabel newBranchLabel = NGUITools.AddWidget<UILabel>(GameObject.Find("UnitBranch"));
            newBranchLabel.font = font;
            newBranchLabel.text = m_unitData_list[i].branch;
            newBranchLabel.name = "Branch" + i;
            newBranchLabel.transform.localScale = new Vector3(30, 30, 30);
            newBranchLabel.color = new Color(0.0f, 0.0f, 0.0f);
            newBranchLabel.enabled = false;

            //// 이름 라벨생성
            UILabel newNameLabel = NGUITools.AddWidget<UILabel>(GameObject.Find("UnitName"));
            newNameLabel.font = font;
            newNameLabel.text = m_unitData_list[i].name;
            newNameLabel.name = "Name" + i;
            newNameLabel.transform.localScale = new Vector3(30, 30, 30);
            newNameLabel.color = new Color(0.0f, 0.0f, 0.0f);
            newNameLabel.gameObject.AddComponent<BoxCollider>();
            newNameLabel.gameObject.GetComponent<BoxCollider>().size = new Vector3(10.0f, 2.0f, 0.01f);
            newNameLabel.gameObject.GetComponent<BoxCollider>().enabled = false;
            newNameLabel.enabled =  false;

            // 체력 라벨생성
            UILabel newHealthLabel = NGUITools.AddWidget<UILabel>(GameObject.Find("UnitHealth"));
            newHealthLabel.font = font;
            newHealthLabel.text = m_unitData_list[i].health;
            newHealthLabel.name = "Health" + i;
            newHealthLabel.transform.localScale = new Vector3(30, 30, 30);
            newHealthLabel.transform.position = new Vector3(0.6f, -0.7f, 0.0f);
            newHealthLabel.color = new Color(0.0f, 0.0f, 0.0f);
            newHealthLabel.enabled = false;

            // 공격력 라벨생성
            UILabel newStrikingPowerLabel = NGUITools.AddWidget<UILabel>(GameObject.Find("UnitStrikingPower"));
            newStrikingPowerLabel.font = font;
            newStrikingPowerLabel.text = m_unitData_list[i].strikingPower;
            newStrikingPowerLabel.name = "StrikingPower" + i;
            newStrikingPowerLabel.transform.localScale = new Vector3(30, 30, 30);
            newStrikingPowerLabel.transform.position = new Vector3(0.88f, -0.7f, 0.0f);
            newStrikingPowerLabel.color = new Color(0.0f, 0.0f, 0.0f);
            newStrikingPowerLabel.enabled = false;

            // 이동력 라벨생성
            UILabel newlnocomotivePowerLabel = NGUITools.AddWidget<UILabel>(GameObject.Find("UnitlnocomotivePower"));
            newlnocomotivePowerLabel.font = font;
            newlnocomotivePowerLabel.text = m_unitData_list[i].lnocomotivePower;
            newlnocomotivePowerLabel.name = "lnocomotivePower" + i;
            newlnocomotivePowerLabel.transform.localScale = new Vector3(30, 30, 30);
            newlnocomotivePowerLabel.transform.position = new Vector3(1.16f, -0.7f, 0.0f);
            newlnocomotivePowerLabel.color = new Color(0.0f, 0.0f, 0.0f);
            newlnocomotivePowerLabel.enabled = false;

            //// 스킬설명 라벨생성
            //UILabel newExplainLabel = NGUITools.AddWidget<UILabel>(GameObject.Find("UnitExplain"));
            //newExplainLabel.font = font;
            //newExplainLabel.text = m_unitData_list[i].explain;
            //newExplainLabel.name = "Explain" + i;
            //newExplainLabel.transform.localScale = new Vector3(30, 30, 30);
            //newExplainLabel.enabled = false;
        }
    }
}