using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerShow : MonoBehaviour
{
    public Button playerShowBtn;
    // Start is called before the first frame update

    public Animator playerShowAnimator;

    public AudioClip  waitAudio;
    
    void Awake()
    {
        playerShowBtn.onClick.AddListener(clickPlayerShowBtn);
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //角色展示按钮的点击相应方法
    void clickPlayerShowBtn()
    {
        int randomValue = Random.Range(0,5);
        playerShowAnimator.SetBool("isWait0"+ randomValue, true);
        

    }
    void Wait01End()
    {
        playerShowAnimator.SetBool("isWait01", false);
    }

    void Wait02End()
    {
        playerShowAnimator.SetBool("isWait02", false);
    }

    void Wait03End()
    {
        playerShowAnimator.SetBool("isWait03", false);
    }

    void Wait04End()
    {
        playerShowAnimator.SetBool("isWait04", false);
    }

}
