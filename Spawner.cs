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
        countTime += Time.deltaTime;//����ʱ�䲻������
        spwanPostion = transform.position;//���ɵ�λ��
        //ʵ�ʵ����ҿ���Ϊ����3.3
        spwanPostion.x = Random.Range(-3.2f, 3.2f);

        if(countTime>=spwantime)
        {
            CreatePlatform();
            countTime = 0;
        }

    }
    public void CreatePlatform()
    {
        int index =Random.Range(0,platforms.Count);//�ӵ�һ�������һ������
        int SpikeNum = 0;
        //��ֹ��������spikeballӰ����Ϸ�Ŀ�����
        if (index == 4) {
            SpikeNum++;
        }
        if (SpikeNum > 1)
        {
            SpikeNum = 0;
            countTime = spwantime;//��������һ���µ�ƽ̨
            return;
        }
        GameObject newPlatform =Instantiate(platforms[index],spwanPostion,Quaternion.identity);
        newPlatform.transform.SetParent(this.transform);
    }
}
