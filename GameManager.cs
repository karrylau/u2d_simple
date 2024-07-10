using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public Text timeScore; // ��Ϸʱ�������ʾ
    public GameObject gameoverUI; // ��Ϸ����UI
    static public GameObject zhenUI;
    private float startTime; // ��¼������ʼ��ʱ��
    private SqlAccess sql;
    private string playerName = "A"; // ���������������֪��
    private static AudioSource backgroundMusic;

    private void Start()
    {
        // ��¼��ʼʱ��
        startTime = Time.timeSinceLevelLoad;
        // ȷ����Ϸ�ӿ�ʼʱ��������״̬
        Time.timeScale = 1f;
        zhenUI = gameoverUI;
        sql = SqlAccess.Instance; // ʹ�õ���ģʽ��ʼ�����ݿ�����
        backgroundMusic = GameObject.Find("backmusic").GetComponent<AudioSource>();//��������
    }

    private void Update()
    {
        // ����UI��ʾʱ�䣬Time.timeSinceLevelLoad - startTime �����˴ӳ������ص����ڵ�ʱ��
        timeScore.text = (Time.timeSinceLevelLoad - startTime).ToString("00");
    }

    public void RestartGame()
    {
        // ���¼��ص�ǰ������������Ϸ״̬
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        // �ָ���������
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
        Application.Quit(); // �˳���Ϸ
    }

    static public void GameOver(bool dead)
    {
        if (dead)
        {
            // ��ȡ��Ϸ������ʵ�����������ʱ�䵽���ݿ�
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
            Time.timeScale = 0f; // ��ͣ��Ϸ
        }
    }

    public void rankings()
    {
        SceneManager.LoadScene(1);

       
    }

/*    private IEnumerator LoadRankingsScene()
    {
        yield return new WaitForSeconds(1); // �ȴ�һ����ȷ�������������

        RankManager rankManager = FindObjectOfType<RankManager>();
        if (rankManager != null)
        {
            StartCoroutine(rankManager.LoadLeaderboardData());
        }
    }*/
}
