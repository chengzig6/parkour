using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadManage : MonoBehaviour
{
    static RoadManage _instance;
    /// <summary>
    /// ������Ԥ����
    /// </summary>
    public GameObject roadGuidePrefab;

    /// <summary>
    /// ��·Ԥ����
    /// </summary>
    public GameObject roadTemplatePrefab;

    /// <summary>
    /// ��·���������transform���
    /// </summary>
    Transform roadGuideTrans;

    /// <summary>
    /// 
    /// </summary>
    GameObject roadGuideObj;

    int startRoadLength = 20;

    public static RoadManage Instance
    { 
        get
        {
            return _instance;
        }
    }



    private void Awake()
    {
        _instance = this;
    }
   


    private void Start()
    {
        // ������·��������
        roadGuideObj = Instantiate(roadGuidePrefab, Vector3.zero,Quaternion.identity);
        roadGuideTrans = roadGuideObj.transform;
        roadGuideTrans.name = "RoadGuideObj";

        //������ʼ����·
        for (int i = 0; i < startRoadLength; i++)
        {
            //����һ��·��
            Instantiate(roadTemplatePrefab, roadGuideTrans.position, roadGuideTrans.rotation);
            //���µ�·���������λ��
            roadGuideTrans.position += Vector3.forward;
        }
    }
    /// <summary>
    /// ����ֱ��·��
    /// </summary>
    public void BuildGeneralRoad()
    {
        Instantiate(roadTemplatePrefab, roadGuideTrans.position, roadGuideTrans.rotation,transform);
        //���µ�·���������λ��
        roadGuideTrans.position += roadGuideTrans.forward;
    }

    /// <summary>
    /// ������ƫ�Ƶ�·
    /// </summary>
    public void BuildUpRoad()
    {
        Instantiate(roadTemplatePrefab, roadGuideTrans.position, roadGuideTrans.rotation);
        //���µ�·���������λ��
        roadGuideTrans.position += roadGuideTrans.forward;
        //�������Y�᷽���ƶ�0.2����λ
        roadGuideTrans.position += roadGuideTrans.up * 0.2f;
    }

    public void BuildDownRoad()
    {
        Instantiate(roadTemplatePrefab, roadGuideTrans.position, roadGuideTrans.rotation);
        //���µ�·���������λ��
        roadGuideTrans.position += roadGuideTrans.forward;
        //�������Y�᷽���ƶ�0.2����λ
        roadGuideTrans.position -= roadGuideTrans.up * 0.2f;
    }

    public void BuildLeftRoad()
    {
        Instantiate(roadTemplatePrefab, roadGuideTrans.position, roadGuideTrans.rotation);
        //���µ�·���������λ��
        roadGuideTrans.position += roadGuideTrans.forward;
        //�������Y�᷽���ƶ�0.2����λ
        roadGuideTrans.position -= roadGuideTrans.right * 0.2f;
    }

    public void BuildRightRoad()
    {
        Instantiate(roadTemplatePrefab, roadGuideTrans.position, roadGuideTrans.rotation);
        //���µ�·���������λ��
        roadGuideTrans.position += roadGuideTrans.forward;
        //�������Y�᷽���ƶ�0.2����λ
        roadGuideTrans.position -= roadGuideTrans.right * 0.2f;
    }

    bool isBuildDirectRoad;//
    int directNumberCur = 0 ;//ƫ�Ƶ�··����
    int directNumberMax = 10;//ƫ�Ƶ�··����
    /// <summary>
    /// ������·
    /// </summary>
    public void BuildRoad()
    {
        if(isBuildDirectRoad && directNumberCur > 0)
        {
            directNumberCur--;
        }
        else
        {
            int randomValue = Random.Range(0, 10);
            if (0 == randomValue)
            {
                isBuildDirectRoad = true;
                directNumberCur = directNumberMax;

                //���ȷ��ƫ�Ƶĵ�·����
                int directRoadNumber = Random.Range(0, 4);
                switch(directRoadNumber)
                {
                    case (int)DirectRoadType.Up:
                        BuildUpRoad();
                        break;
                    case (int)DirectRoadType.Down:
                        BuildDownRoad();
                        break;
                    case (int)DirectRoadType.Left:
                        BuildLeftRoad();
                        break;
                    case (int)DirectRoadType.Right:
                        BuildDownRoad();
                        break;
                }
                directNumberCur--;
            }
            else
            {
                BuildGeneralRoad();
            }
        }


       

    }
}

public enum DirectRoadType
{
    Up,
    Down,
    Left,
    Right,
}