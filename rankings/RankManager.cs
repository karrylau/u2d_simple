using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RankManager : MonoBehaviour
{
    // �������
    public List<PlayerData> playerDatas = new List<PlayerData>();
    // UI
    public Group[] groups;
    private SqlAccess sql;
    public int Flag=0;
    void Start()
    {
        sql = new SqlAccess("localhost", "3306", "root", "1234", "sys"); // ��ʼ�����ݿ�����
       
        Debug.Log("�����������");
        CalHighestScore();
        Debug.Log(CalHighestScore());
        UpdateLeaderboardUI();
        restart();
        /*
         * if(Flag == 2)
        {   
             
    }*/
    }


 /*    public IEnumerator LoadLeaderboardData()
    {
        Debug.Log("DATA:" + Flag);
        yield return new WaitForSeconds(1); // ģ������ӳ�
        Debug.Log("DATA:" + Flag);
        SqlAccess.Instance.OpenConnection();
        playerDatas = SqlAccess.Instance.GetLeaderboardData(); // �����ݿ��ȡ����
        SqlAccess.Instance.CloseConnection();
        foreach (var playerData in playerDatas)
        {
            Debug.Log($"Player: {playerData.playerName}, Score: {playerData.playerHighestScore}, Kills: {playerData.playerKillNum}");
        }
        SortScoreBubble(); // Ĭ�ϰ��շ�������
        UpdateLeaderboardUI(); // ����UI
        Flag++;
       
    }*/
    public void restart()
    {
        SqlAccess.Instance.OpenConnection();
        playerDatas = SqlAccess.Instance.GetLeaderboardData(); // �����ݿ��ȡ����
        SqlAccess.Instance.CloseConnection();
        foreach (var playerData in playerDatas)
        {
            Debug.Log($"Player: {playerData.playerName}, Score: {playerData.playerHighestScore}, Kills: {playerData.playerKillNum}");
        }
        SortScoreBubble(); // Ĭ�ϰ��շ�������
        UpdateLeaderboardUI(); // ����UI
    }

    private string CalHighestScore()// ������߷�
    {
        Debug.Log("TOP:" + Flag);
        int highestScore = 0;
        string topName = "";
        for (int i = 0; i < playerDatas.Count; i++)
        {
            if (playerDatas[i].playerHighestScore > highestScore)
            {
                highestScore = playerDatas[i].playerHighestScore;
                topName = playerDatas[i].playerName;
            }
        }
        Flag++;
        return topName;
    }

    // ð������(����score����
    public void SortScoreBubble()
    {
        for (int i = 0; i < playerDatas.Count - 1; i++)
        {
            for (int j = 0; j < playerDatas.Count - 1 - i; j++)
            {
                if (playerDatas[j].playerHighestScore < playerDatas[j + 1].playerHighestScore)
                {
                    PlayerData tmp = playerDatas[j];
                    playerDatas[j] = playerDatas[j + 1];
                    playerDatas[j + 1] = tmp;
                }
            }
        }
        UpdateLeaderboardUI();
    }

    // ð�����򣨸���killNum����
    public void SortKillBubble()
    {
        for (int i = 0; i < playerDatas.Count - 1; i++)
        {
            for (int j = 0; j < playerDatas.Count - 1 - i; j++)
            {
                if (playerDatas[j].playerKillNum < playerDatas[j + 1].playerKillNum)
                {
                    PlayerData tmp = playerDatas[j];
                    playerDatas[j] = playerDatas[j + 1];
                    playerDatas[j + 1] = tmp;
                }
            }
        }
        UpdateLeaderboardUI();
    }

    private void UpdateLeaderboardUI()
    {
        Debug.Log("UPDATE:" + Flag);
        for (int i = 0; i < groups.Length && i < playerDatas.Count; i++)
        {
            groups[i].playerData = playerDatas[i];
            groups[i].updateGroup();
        }
        // ������º��UI��Ϣ������̨
        for (int i = 0; i < groups.Length && i < playerDatas.Count; i++)
        {
            Debug.Log($"Group {i} - Player: {groups[i].playerData.playerName}, Score: {groups[i].playerData.playerHighestScore}, Kills: {groups[i].playerData.playerKillNum}");
        }
        Flag++;
    }
    public void LoadScenestart()
    {
        SceneManager.LoadScene(0);
    }
}
