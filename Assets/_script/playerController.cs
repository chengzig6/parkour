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

    GameObject currentRoad;

    //��ɫ�ƶ��ٶ� ��Z��
    float runSpeed  = 2f;
    float runSpeedDelta = 0.2f;
    float dropSpeed = -1f;
    float runSpeedMax = 8f;

    public Animator playerAnimator;

    // ��ɫ���յ��ƶ�����
    Vector3 moveIncrement;
    private void Update()
    {
        //�����ɫ��Z�᷽����ƶ�
        moveIncrement += transform.forward * runSpeed * Time.deltaTime;

        moveIncrement.y = playerC0ntroller.isGrounded ? 0f : dropSpeed * Time.deltaTime;


        playerC0ntroller.Move(moveIncrement);
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        //����ֱ��
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
