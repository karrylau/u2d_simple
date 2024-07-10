using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public List<GameObject>platforms = new List<GameObject>();
    public float spwantime;
    private float countTime;
    private Vector3 spwanPostion;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SpwanPlatform(); 
    }

    public void SpwanPlatform()
    {
        countTime += Time.deltaTime;//随着时间不断增加
        spwanPostion = transform.position;//生成的位置
        //实际的左右可以为左右3.3
        spwanPostion.x = Random.Range(-3.2f, 3.2f);

        if(countTime>=spwantime)
        {
            CreatePlatform();
            countTime = 0;
        }

    }
    public void CreatePlatform()
    {
        int index =Random.Range(0,platforms.Count);//从第一个到最后一个生成
        int SpikeNum = 0;
        //防止生成两个spikeball影响游戏的可玩性
        if (index == 4) {
            SpikeNum++;
        }
        if (SpikeNum > 1)
        {
            SpikeNum = 0;
            countTime = spwantime;//立刻生成一个新的平台
            return;
        }
        GameObject newPlatform =Instantiate(platforms[index],spwanPostion,Quaternion.identity);
        newPlatform.transform.SetParent(this.transform);
    }
}
