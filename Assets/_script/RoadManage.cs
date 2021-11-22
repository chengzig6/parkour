using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadManage : MonoBehaviour
{
    static RoadManage _instance;
    /// <summary>
    /// 道引导预制体
    /// </summary>
    public GameObject roadGuidePrefab;

    /// <summary>
    /// 道路预制体
    /// </summary>
    public GameObject roadTemplatePrefab;

    /// <summary>
    /// 道路引导对象的transform组件
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
        // 创建道路引导对象
        roadGuideObj = Instantiate(roadGuidePrefab, Vector3.zero,Quaternion.identity);
        roadGuideTrans = roadGuideObj.transform;
        roadGuideTrans.name = "RoadGuideObj";

        //创建初始化道路
        for (int i = 0; i < startRoadLength; i++)
        {
            //创建一个路块
            Instantiate(roadTemplatePrefab, roadGuideTrans.position, roadGuideTrans.rotation);
            //更新道路引导对象的位置
            roadGuideTrans.position += Vector3.forward;
        }
    }
    /// <summary>
    /// 构建直线路线
    /// </summary>
    public void BuildGeneralRoad()
    {
        Instantiate(roadTemplatePrefab, roadGuideTrans.position, roadGuideTrans.rotation,transform);
        //更新道路引导对象的位置
        roadGuideTrans.position += roadGuideTrans.forward;
    }
}
