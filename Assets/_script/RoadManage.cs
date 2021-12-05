using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum RoadType
{
    Direct = 1,
    Swerve,
    Trap,
}

public enum DirectRoadType
{
    Up = 1,
    Down,
    Left,
    Right,
}

public enum SwerveRoadType
{
    TurnLeft = 1,
    TurnRight,
}

public enum TrapRoadType
{
    Left = 1,
    Center,
    Right
}

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


    List<GameObject> roadList = new List<GameObject>();

    //相邻转向道路之间的最小间隔
    int trunRoadLimit;

    int startRoadLength = 20;

    bool isBuildDirectRoad;//
    int directNumberCur = 0;//偏移道路路快数
    int directNumberMax = 10;//偏移道路路快数
    int directCurType;
    int goldNumber = 0;

    public Text goldNumberText;
    public Text scoreNumberText;

    public int GoldNumber
    {
        set { goldNumber = value; }
        get { return goldNumber; }
    }

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
        GameObject obj = Instantiate(roadTemplatePrefab, roadGuideTrans.position, roadGuideTrans.rotation,transform);
        //更新道路引导对象的位置
        roadGuideTrans.position += roadGuideTrans.forward;
        //路块解体
        ShowSubRoadBlockBreakupEffect(obj);
    }

    /// <summary>
    /// 构建上偏移道路
    /// </summary>
    public void BuildUpRoad()
    {
        GameObject obj = Instantiate(roadTemplatePrefab, roadGuideTrans.position, roadGuideTrans.rotation);
        //更新道路引导对象的位置
        roadGuideTrans.position += roadGuideTrans.forward;
        //向自身的Y轴方向移动0.2个单位
        roadGuideTrans.position += roadGuideTrans.up * 0.2f;
        //路块解体
        ShowSubRoadBlockBreakupEffect(obj);
    }

    public void BuildDownRoad()
    {
        GameObject obj = Instantiate(roadTemplatePrefab, roadGuideTrans.position, roadGuideTrans.rotation);
        //更新道路引导对象的位置
        roadGuideTrans.position += roadGuideTrans.forward;
        //向自身的Y轴方向移动0.2个单位
        roadGuideTrans.position -= roadGuideTrans.up * 0.2f;
        //路块解体
        ShowSubRoadBlockBreakupEffect(obj);
    }

    public void BuildLeftRoad()
    {
        GameObject obj = Instantiate(roadTemplatePrefab, roadGuideTrans.position, roadGuideTrans.rotation);
        //更新道路引导对象的位置
        roadGuideTrans.position += roadGuideTrans.forward;
        //向自身的Y轴方向移动0.2个单位
        roadGuideTrans.position -= roadGuideTrans.right * 0.2f;
        //路块解体
        ShowSubRoadBlockBreakupEffect(obj);
    }

    public void BuildRightRoad()
    {
        GameObject obj = Instantiate(roadTemplatePrefab, roadGuideTrans.position, roadGuideTrans.rotation);
        //更新道路引导对象的位置
        roadGuideTrans.position += roadGuideTrans.forward;
        //向自身的Y轴方向移动0.2个单位
        roadGuideTrans.position -= roadGuideTrans.right * 0.2f;
        //路块解体
        ShowSubRoadBlockBreakupEffect(obj);
    }

    /// <summary>
    /// 构建道路
    /// </summary>
    public void BuildRoad()
    {
        if(isBuildDirectRoad && directNumberCur > 0)
        {
            directNumberCur--;
            switch (directCurType)
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
            if(directNumberCur <= 0)
            {
                isBuildDirectRoad = false;
            }
        }
        else
        {
            int randomValue = Random.Range(1, 11);
            if ((int)RoadType.Direct == randomValue)
            {
                isBuildDirectRoad = true;
                directNumberCur = directNumberMax;

                //随机确定偏移的道路类型
                int directRoadNumber = Random.Range(1, 5);
                directCurType = directRoadNumber;
                switch (directRoadNumber)
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

                --directNumberCur;
            }
            else if (randomValue == (int)(RoadType.Swerve) && trunRoadLimit <= 0)
            {
                trunRoadLimit = 10;

                int swerveRoadType = Random.Range(1, 3);
                switch (swerveRoadType)
                {
                    case (int)SwerveRoadType.TurnLeft:
                        BuildTurnLeftRoad();
                        break;
                    case (int)SwerveRoadType.TurnRight:
                        BuildTurnRightRoad();
                        break;
                }
            }
            else if(randomValue == (int)(RoadType.Trap))
            {
                BuildTrapRoad();
            }
            else
            {
                BuildGeneralRoad();
            }
        }
        trunRoadLimit--;
    }

    //左转道路
    public void BuildTurnLeftRoad()
    {
        for(int i = 0; i < 3; i++)
        {
            GameObject obj = Instantiate(roadTemplatePrefab, roadGuideTrans.position, roadGuideTrans.rotation);
            //更新道路引导对象的位置
            roadGuideTrans.position += roadGuideTrans.forward;
            //路块解体
            ShowSubRoadBlockBreakupEffect(obj);
        }

        roadGuideTrans.position -= roadGuideTrans.forward * 2f;
        roadGuideTrans.Rotate(Vector3.up, -90f);
        roadGuideTrans.position += roadGuideTrans.forward * 2f;
    }

    //右转道路
    public void BuildTurnRightRoad()
    {
        for (int i = 0; i < 3; i++)
        {
            GameObject obj = Instantiate(roadTemplatePrefab, roadGuideTrans.position, roadGuideTrans.rotation);
            //更新道路引导对象的位置
            roadGuideTrans.position += roadGuideTrans.forward;
            //路块解体
            ShowSubRoadBlockBreakupEffect(obj);
        }

        roadGuideTrans.position -= roadGuideTrans.forward * 2f;
        roadGuideTrans.Rotate(Vector3.up, 90f);
        roadGuideTrans.position += roadGuideTrans.forward * 2f;
    }
    //陷阱道路
    public void BuildTrapRoad()
    {
        roadGuideTrans.position += roadGuideTrans.forward;

        GameObject tempRoad = Instantiate(roadTemplatePrefab, roadGuideTrans.position, roadGuideTrans.rotation);
        tempRoad.transform.Rotate(Vector3.up, 90f);

        int randomValue = Random.Range(1, 4);
        switch (randomValue)
        {
            case (int)TrapRoadType.Left:
                tempRoad.transform.position -= tempRoad.transform.forward;
                break;
            case (int)TrapRoadType.Center:
                break;
            case (int)TrapRoadType.Right:
                tempRoad.transform.position += tempRoad.transform.forward;
                break;
        }

        roadGuideTrans.position += roadGuideTrans.forward * 2f;
        //路块解体
        ShowSubRoadBlockBreakupEffect(tempRoad);
    }

    /// <summary>
    /// 路块解体
    /// </summary>
    /// <param name="parentRoad"></param>
    public void ShowSubRoadBlockBreakupEffect(GameObject parentRoad)
    {

        RoadTemplate tempRoad = parentRoad.GetComponent<RoadTemplate>();
        if (tempRoad)
        {
            tempRoad.setSubRoadBreakupEffect();
        }


        roadList.Add(parentRoad);


    }

    /// <summary>
    /// 路块组合
    /// </summary>
    public void ShowSubRoadBlockCombinationEffect()
    {
        RoadTemplate tmp = roadList[0].GetComponent<RoadTemplate>();
        if (tmp)
        {
            tmp.setSubRoadCombinationEffect();
        }
        roadList.RemoveAt(0);
    }

    private void Update()
    {
        goldNumberText.text = goldNumber.ToString();
    }
}
