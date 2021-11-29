using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

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
    float transervseSpeed = 2f;

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

        float moveDir = Input.GetAxis("Horizontal");
        moveIncrement += transform.right * moveDir * transervseSpeed * Time.deltaTime;


        playerC0ntroller.Move(moveIncrement);
        playerAnimator.SetFloat("MoveSpeed", playerC0ntroller.velocity.magnitude);

        //
        if (Input.GetKeyDown(KeyCode.I))
        {
            bJumpState = true;
            playerAnimator.SetBool("isJump", true);
        }
        else if(Input.GetKeyDown(KeyCode.K))
        {
            playerAnimator.SetBool("isSlide", true);
        }
        else if (Input.GetKeyDown(KeyCode.J)&& isTrunLeftEnd)
        {
            TurnLeftStart();
        }
        else if (Input.GetKeyDown(KeyCode.L) && isTrunRightEnd)
        {
            TurnRightStart();
        }

        
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        //构建直线
        if(hit.gameObject != currentRoad)
        {
            
            currentRoad = hit.gameObject;
            Destroy(hit.gameObject.transform.parent.gameObject, 1f);
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


    // 是否左转向结束
    bool isTrunLeftEnd = true;

    bool isTrunRightEnd = true;
    /// <summary>
    /// 左转向
    /// </summary>
    void TurnLeftStart()
    {
        isTrunLeftEnd = false;

        transform.Rotate(Vector3.up, -90f);
        Quaternion endValue = transform.rotation;

        //恢复朝向
        transform.Rotate(Vector3.up, 90f);
        Tween tween = transform.DORotateQuaternion(endValue,0.3f);
        tween.OnComplete(TurnLeftEnd);
    }

    void TurnLeftEnd()
    {
        isTrunLeftEnd = true;
    }

    void TurnRightStart()
    {
        isTrunRightEnd = false;

        transform.Rotate(Vector3.up, 90f);
        Quaternion endValue = transform.rotation;

        //恢复朝向
        transform.Rotate(Vector3.up, -90f);
        Tween tween = transform.DORotateQuaternion(endValue, 0.3f);
        tween.OnComplete(TurnRightEnd);
    }

    void TurnRightEnd()
    {
        isTrunRightEnd = true;
    }
}
