using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

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
    float transervseSpeed = 2f;

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
        //����ֱ��
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


    // �Ƿ���ת�����
    bool isTrunLeftEnd = true;

    bool isTrunRightEnd = true;
    /// <summary>
    /// ��ת��
    /// </summary>
    void TurnLeftStart()
    {
        isTrunLeftEnd = false;

        transform.Rotate(Vector3.up, -90f);
        Quaternion endValue = transform.rotation;

        //�ָ�����
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

        //�ָ�����
        transform.Rotate(Vector3.up, -90f);
        Tween tween = transform.DORotateQuaternion(endValue, 0.3f);
        tween.OnComplete(TurnRightEnd);
    }

    void TurnRightEnd()
    {
        isTrunRightEnd = true;
    }
}
