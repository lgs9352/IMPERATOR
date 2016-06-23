using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public struct UnitData
{
    public string branch;
    public string name;
    public string health;
    public string strikingPower;
    public string lnocomotivePower;
    public string explain;
};

public class DataParser
{
    private string m_str_data;
    private List<UnitData> m_unitdata_list;

    public void LoadTxtData(string _fileName)
    {
        StreamReader sr = new StreamReader(_fileName);
        m_str_data = sr.ReadToEnd();
    }

    public void ParsingUnitData()
    {
        m_unitdata_list = new List<UnitData>();

        string[] str_data_temp = m_str_data.Split('\n');
        string[] str_unit_temp;

        for (int i = 0; i < str_data_temp.Length; ++i)
        {
            str_unit_temp = str_data_temp[i].Split(',');
            UnitData data_temp;
            data_temp.branch = str_unit_temp[0];
            data_temp.name = str_unit_temp[1];
            data_temp.health = str_unit_temp[2];
            data_temp.strikingPower = str_unit_temp[3];
            data_temp.lnocomotivePower = str_unit_temp[4];
            data_temp.explain = str_unit_temp[5];

            m_unitdata_list.Add(data_temp);
        }
    }

    public void ParsingUnit()
    {
        
    }

    public List<UnitData> GetUnitDataList()
    {
        return m_unitdata_list;
    }
}