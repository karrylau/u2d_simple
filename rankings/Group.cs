using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Group : MonoBehaviour
{
    public PlayerData playerData;
    public Text playerNameText;
    public Text playerScoreText;
    public Text playerKillNumText;

    public void updateGroup()
    {
        if (playerData != null)
        {
            playerNameText.text = playerData.playerName;
            playerScoreText.text = playerData.playerHighestScore.ToString();
            playerKillNumText.text = playerData.playerKillNum.ToString();
        }
    }
}
