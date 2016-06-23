using UnityEngine;
using System.Collections;

public class GameGUI : MonoBehaviour
{
    void OnGUI()
    {
        GUI.Label(new Rect(1500, 950, 200, 20),"유저1 명령 카운트 : " + Global.user_one_count);
        GUI.Label(new Rect(1500, 970, 200, 20),"유저2 명령 카운트 : " + Global.user_two_count);
    }
}