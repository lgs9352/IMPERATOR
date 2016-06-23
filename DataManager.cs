using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DataManager : MonoBehaviour
{
    private DataParser m_dataParser;

    void Start ()
    {
        m_dataParser = new DataParser();
        m_dataParser.LoadTxtData("Assets/Test.txt");
        m_dataParser.ParsingUnitData();
    }
}