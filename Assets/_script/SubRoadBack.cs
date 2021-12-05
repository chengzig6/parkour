using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubRoadBack : MonoBehaviour
{
    bool isRotate = false;
    Vector3 originPosition;
    Quaternion originRotation;

    float resetTime = 1f;

    private void Awake()
    {
        originPosition = transform.localPosition;
        originRotation = transform.localRotation;
    }
    /// <summary>
    /// 改变位置
    /// </summary>
    public void ChangePosition()
    {
  
        int xChangeValue = 1;
        int yChangeValue = 1;

        while( Mathf.Abs( xChangeValue) <= 4)
        {
            xChangeValue = Random.Range(-10, 10);
        }

        while(Mathf.Abs(yChangeValue) <= 4)
        {
            yChangeValue = Random.Range(-10, 10);
        }

        transform.position += transform.right * xChangeValue;
        transform.position += transform.up * yChangeValue;
    }

    /// <summary>
    /// 改变子路快朝向
    /// </summary>
    public void ChangeRotation()
    {
        transform.Rotate(transform.right, Random.Range(-180f, 180f));
        transform.Rotate(transform.up, Random.Range(-180f, 180f));
        transform.Rotate(transform.forward, Random.Range(-180f, 180f));
    }

    /// <summary>
    /// 复位
    /// </summary>
    public void Reset()
    {
        transform.DOLocalMove(originPosition, resetTime);
        Tween tween = transform.DORotateQuaternion(originRotation, resetTime);
        tween.OnComplete(CreateCoin);
        isRotate = false;

    }


    public bool IsRotate
    {
        get { return isRotate; }
        set { isRotate = value; }
    }

    public void Update()
    {
        if (isRotate)
        {
            transform.RotateAround(transform.parent.position, transform.parent.forward, 30f * Time.deltaTime);
        }
    }
    public void CreateCoin()
    {
        bool isCreateCoin = (Random.Range(0, 20) == 10 ? true : false);
        if (isCreateCoin)
        {
            GameObject coin = Resources.Load("Coin") as GameObject ;
            Vector3 coinPostion = transform.position + transform.up * 1f;
            Instantiate(coin, coinPostion, Quaternion.identity, transform);
        }
    }
}
