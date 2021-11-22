using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 说明 角色控制类
/// </summary>
public class playerController : MonoBehaviour
{
    /// <summary>
    /// 角色控制器
    /// </summary>
    public CharacterController playerC0ntroller;

    GameObject currentRoad;

    //角色移动速度 在Z轴
    float runSpeed  = 2f;
    float runSpeedDelta = 0.2f;
    float dropSpeed = -1f;
    float runSpeedMax = 8f;

    public Animator playerAnimator;

    // 角色最终的移动增量
    Vector3 moveIncrement;
    private void Update()
    {
        //计算角色在Z轴方向的移动
        moveIncrement += transform.forward * runSpeed * Time.deltaTime;

        moveIncrement.y = playerC0ntroller.isGrounded ? 0f : dropSpeed * Time.deltaTime;


        playerC0ntroller.Move(moveIncrement);
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        //构建直线
        if(hit.gameObject != currentRoad)
        {
            if(runSpeedMax > runSpeed)
            {
                runSpeed += runSpeedDelta * Time.deltaTime;
            }

            playerAnimator.SetFloat("MoveSpeed", playerC0ntroller.velocity.magnitude);

            currentRoad = hit.gameObject;
            Destroy(hit.gameObject, 1f);
            RoadManage.Instance.BuildGeneralRoad();
        }
       
    }
}
