using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="PlayerData", fileName = "PlayerData")]
public class PlayerData : ScriptableObject
{
    // 名字
    public string playerName;
    // 图像
 
    // 最高分
    public int playerHighestScore;
    // 击杀数
    public int playerKillNum;
}
