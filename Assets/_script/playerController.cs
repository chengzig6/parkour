using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ˵�� ��ɫ������
/// </summary>
public class playerController : MonoBehaviour
{
    /// <summary>
    /// ��ɫ������
    /// </summary>
    public CharacterController playerC0ntroller;
    public Animator playerAnimator;

    GameObject currentRoad;

    //��ɫ�ƶ��ٶ� ��Z��
    float runSpeed  = 2f;       //��ʼ�ٶ�
    float runSpeedDelta = 0.2f;//û������
    float dropSpeed = -1f;//����
    float runSpeedMax = 8f;//����ٶ�
    
    float jumpPower = 5f;

    bool bJumpState = false;

    // ��ɫ���յ��ƶ�����
    Vector3 moveIncrement;
    private void Update()
    {
        if (runSpeedMax > runSpeed)
        {
            runSpeed += runSpeedDelta * Time.deltaTime;
        }

        //�����ɫ��Z�᷽����ƶ�
        moveIncrement = transform.forward * runSpeed * Time.deltaTime;

        
        //�����ɫ��Y��ķ�����ƶ�
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
        //����ֱ��
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
