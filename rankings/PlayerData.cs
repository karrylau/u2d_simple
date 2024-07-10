using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="PlayerData", fileName = "PlayerData")]
public class PlayerData : ScriptableObject
{
    // ����
    public string playerName;
    // ͼ��
 
    // ��߷�
    public int playerHighestScore;
    // ��ɱ��
    public int playerKillNum;
}
