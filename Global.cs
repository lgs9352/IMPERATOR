using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public struct DeckUnit
{
    public string user;
    public UNITTYPE type;
    public int pos;
};

public struct UnitIdx
{
    public bool isUnit;
    public bool isFence;
    public int idx;
};

public static class Global
{
    public static Vector3[] unit_pos = new Vector3[48];
    public static Vector3[] arrow_pos = new Vector3[42];
    public static UnitIdx[] unitIdx = new UnitIdx[48];
    public static DeckUnit[] unit_list = new DeckUnit[16];
    public static GameObject[] Tile = new GameObject[48];
    public static int unit_count;
    public static Unit[] unit = new Unit[32];
    public static int turn;
    public static int user_one_count = 0;
    public static int user_two_count = 0;
}