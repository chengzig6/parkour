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
    public Animator playerAnimator;

    GameObject currentRoad;

    //角色移动速度 在Z轴
    float runSpeed  = 2f;       //初始速度
    float runSpeedDelta = 0.2f;//没秒增加
    float dropSpeed = -1f;//下落
    float runSpeedMax = 8f;//最大速度
    
    float jumpPower = 5f;

    bool bJumpState = false;

    // 角色最终的移动增量
    Vector3 moveIncrement;
    private void Update()
    {
        if (runSpeedMax > runSpeed)
        {
            runSpeed += runSpeedDelta * Time.deltaTime;
        }

        //计算角色在Z轴方向的移动
        moveIncrement = transform.forward * runSpeed * Time.deltaTime;

        
        //计算角色在Y轴的方向的移动
        if(bJumpState) 
        {
            //moveIncrement.y += jumpPower * Time.deltaTime;
        }
        else
        {
            moveIncrement.y = playerC0ntroller.isGrounded ? 0f : dropSpeed * Time.deltaTime;
        }

        playerC0ntroller.Move(moveIncrement);
        playerAnimator.SetFloat("MoveSpeed", playerC0ntroller.velocity.magnitude);

        //
        if (Input.GetKeyDown(KeyCode.W))
        {
            bJumpState = true;
            playerAnimator.SetBool("isJump", true);
        }
        else if(Input.GetKeyDown(KeyCode.S))
        {
            playerAnimator.SetBool("isSlide", true);
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        //构建直线
        if(hit.gameObject != currentRoad)
        {
            
            currentRoad = hit.gameObject;
            Destroy(hit.gameObject, 1f);
            RoadManage.Instance.BuildRoad();
        }
       
    }

    void JumpEnd()
    {
        bJumpState = false;
        playerAnimator.SetBool("isJump", false);
    }

    void slideEnd()
    {
        //bSlideState = false;
        playerAnimator.SetBool("isSlide", false);
    }
}
