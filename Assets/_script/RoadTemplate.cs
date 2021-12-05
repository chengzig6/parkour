using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 路快模板
/// </summary>
public class RoadTemplate : MonoBehaviour
{
    SubRoadBack[] subRoadBlocks;

    private void Awake()
    {
        subRoadBlocks = GetComponentsInChildren<SubRoadBack>();
    }

    /// <summary>
    /// 设置子路快的解体效果
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
