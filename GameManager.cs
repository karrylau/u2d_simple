using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public Text timeScore; // 游戏时间分数显示
    public GameObject gameoverUI; // 游戏结束UI
    static public GameObject zhenUI;
    private float startTime; // 记录场景开始的时间
    private SqlAccess sql;
    private string playerName = "A"; // 假设玩家名称是已知的
    private static AudioSource backgroundMusic;

    private void Start()
    {
        // 记录开始时间
        startTime = Time.timeSinceLevelLoad;
        // 确保游戏从开始时就是运行状态
        Time.timeScale = 1f;
        zhenUI = gameoverUI;
        sql = SqlAccess.Instance; // 使用单例模式初始化数据库连接
        backgroundMusic = GameObject.Find("backmusic").GetComponent<AudioSource>();//背景音乐
    }

    private void Update()
    {
        // 更新UI显示时间，Time.timeSinceLevelLoad - startTime 给出了从场景加载到现在的时间
        timeScore.text = (Time.timeSinceLevelLoad - startTime).ToString("00");
    }

    public void RestartGame()
    {
        // 重新加载当前场景，重置游戏状态
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        // 恢复背景音乐
        if (backgroundMusic != null)
        {
            backgroundMusic.Play();
        }
    }

    public void Quit()
    {
        if (backgroundMusic != null)
        {
            backgroundMusic.Pause();
        }
        Application.Quit(); // 退出游戏
    }

    static public void GameOver(bool dead)
    {
        if (dead)
        {
            // 获取游戏管理器实例，更新玩家时间到数据库
            GameManager instance = FindObjectOfType<GameManager>();
            float finalTime = Time.timeSinceLevelLoad - instance.startTime;

            SqlAccess.Instance.OpenConnection();
            SqlAccess.Instance.UpdatePlayerTime(instance.playerName, finalTime);
            SqlAccess.Instance.CloseConnection();

            if (backgroundMusic != null)
            {
                backgroundMusic.Pause();
            }
            zhenUI.SetActive(true);
            Time.timeScale = 0f; // 暂停游戏
        }
    }

    public void rankings()
    {
        SceneManager.LoadScene(1);

       
    }

/*    private IEnumerator LoadRankingsScene()
    {
        yield return new WaitForSeconds(1); // 等待一秒以确保场景加载完成

        RankManager rankManager = FindObjectOfType<RankManager>();
        if (rankManager != null)
        {
            StartCoroutine(rankManager.LoadLeaderboardData());
        }
    }*/
}
