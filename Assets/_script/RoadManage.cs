using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RoadType
{
    Direct = 1,
    Swerve
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

    //����ת���·֮�����С���
    int trunRoadLimit;

    int startRoadLength = 20;

    bool isBuildDirectRoad;//
    int directNumberCur = 0;//ƫ�Ƶ�··����
    int directNumberMax = 10;//ƫ�Ƶ�··����
    int directCurType;

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

    /// <summary>
    /// ������·
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

                //���ȷ��ƫ�Ƶĵ�·����
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


            else
            {
                BuildGeneralRoad();
            }
        }
        trunRoadLimit--;
    }

    //��ת��·
    public void BuildTurnLeftRoad()
    {
        for(int i = 0; i < 3; i++)
        {
            Instantiate(roadTemplatePrefab, roadGuideTrans.position, roadGuideTrans.rotation);
            //���µ�·���������λ��
            roadGuideTrans.position += roadGuideTrans.forward;
        }

        roadGuideTrans.position -= roadGuideTrans.forward * 2f;
        roadGuideTrans.Rotate(Vector3.up, -90f);
        roadGuideTrans.position += roadGuideTrans.forward * 2f;
    }

    //��ת��·
    public void BuildTurnRightRoad()
    {
        for (int i = 0; i < 3; i++)
        {
            Instantiate(roadTemplatePrefab, roadGuideTrans.position, roadGuideTrans.rotation);
            //���µ�·���������λ��
            roadGuideTrans.position += roadGuideTrans.forward;
        }

        roadGuideTrans.position -= roadGuideTrans.forward * 2f;
        roadGuideTrans.Rotate(Vector3.up, 90f);
        roadGuideTrans.position += roadGuideTrans.forward * 2f;
    }


}
