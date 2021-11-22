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
}
