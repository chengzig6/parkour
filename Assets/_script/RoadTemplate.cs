using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ·��ģ��
/// </summary>
public class RoadTemplate : MonoBehaviour
{
    SubRoadBack[] subRoadBlocks;

    private void Awake()
    {
        subRoadBlocks = GetComponentsInChildren<SubRoadBack>();
    }

    /// <summary>
    /// ������·��Ľ���Ч��
    /// </summary>
    public void setSubRoadBreakupEffect()
    {
        for(int i = 0; i < 3; i++)
        {
            subRoadBlocks[i].ChangePosition();
            subRoadBlocks[i].ChangeRotation();
            subRoadBlocks[i].IsRotate = true;
        }
    }

    public void setSubRoadCombinationEffect()
    {
        for (int i = 0; i < 3; i++)
        {
            subRoadBlocks[i].Reset();
        }
    }
}
