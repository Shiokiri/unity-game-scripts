using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{

    public List<Con_DialogData> allDialoglist = new List<Con_DialogData>();//所有对话列表

    public Transform dialogueButtons;//对话按钮框
    public Object ButtonsItem;//对话按钮预制体

    public Transform dialogueReply;//回复按钮框
    public Object RelyItem;//回复按钮预制体

    public Transform dialogueBox;//对话框
    public Dialoglogic dialoglogic;//对话逻辑处理类

    public ButtonManager buttonManager;

    private Collider playerCollider;

    public void dialogbuttonInit(int npcID)//对话选择按钮
    {
        dialogueButtons.gameObject.SetActive(true);
        dialogueReply.gameObject.SetActive(false);
        clearButten(dialogueButtons);
        //  int a = dialogueButtons.childCount;
        for (int i = 0; i < allDialoglist.Count; i++)
        {
            if (allDialoglist[i].NPCID == npcID)
            {
                //生成对话选择按钮
                GameObject selectButton = (GameObject)Instantiate(ButtonsItem, dialogueButtons);
                selectButton.GetComponent<dialogueItem>().Initbuttom(allDialoglist[i], this);
            }
        }

        Invoke("InitItem", 0.01F);
        // buttonManager.InitItem();//直接调用会出现一个子物体数量不匹配的Bug，这和untiy的Destroy()生命周期有关

    }
    void InitItem(){
        buttonManager.InitItem();
    }

    public void DialogButtonOver()
    {
        dialogueButtons.gameObject.SetActive(false);
        dialogueReply.gameObject.SetActive(false);
        dialogueBox.gameObject.SetActive(false);
    }

    public void dialogReplyInit(Con_DialogData con_DialogData, int currentLine)//对话选项生成
    {
        Debug.Log("进入对话回复控制");
        dialogueReply.gameObject.SetActive(true);
        clearButten(dialogueReply);
        dialogueButtons.gameObject.SetActive(false);
        for (int i = 0; i < con_DialogData.dialoguedatas[currentLine].diaselect.Length; i++)//获取具体某行有几个对话选项的长度
        {
             //生成对话选择按钮
            GameObject selectButton = (GameObject)Instantiate(RelyItem, dialogueReply);
            selectButton.GetComponent<dialogueItem>().InitSelect(this,con_DialogData,currentLine,i);
        }
     }
            
    

    void clearButten(Transform transform)//清除所有子物体
    {
        for (int i = 0; i < transform.childCount; i++)
            Destroy(transform.GetChild(i).gameObject);
    }
   

   public void dialogPlay(Con_DialogData Dialog,int strat)
   {
		// 固定第三人称控制器摄像机位置
		dialoglogic.setPlayerCollider(this.playerCollider);
		playerCollider.GetComponent<ThirdPersonController>().LockCameraPosition = true;
		Cursor.lockState = CursorLockMode.None;

		//对话框出现
		dialogueBox.gameObject.SetActive (true);
        //选项按钮框隐藏
        dialogueReply.gameObject.SetActive(false);
        dialogueButtons.gameObject.SetActive(false);

        dialoglogic.DialogInit(Dialog, strat);
   }

    public void setPlayerCollider(Collider playercollider)
    {
        this.playerCollider = playercollider;
    }

    public void FinshDialog()
    {
        dialogueBox.gameObject.SetActive(false) ;
    }

    
    public void DoEvent(string EventName)
    {
		Debug.Log(EventName+"yes");
		Invoke(EventName, 0.01F);//延迟执行
    }

	public void AddBullets()
	{
		GameManager.Instance.playerStats.CurrentBulletsNumber
			= GameManager.Instance.playerStats.MaxBulletsNumber;
	}

	public void AddKey()
	{
		GameManager.Instance.playerStats.characterData.key
			= GameManager.Instance.playerStats.characterData.trueKey;
	}

    public void TestEvent()
    {
        Debug.Log("TestEvent");
    }
   
}
