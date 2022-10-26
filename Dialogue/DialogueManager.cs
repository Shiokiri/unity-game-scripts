using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{

    public List<Con_DialogData> allDialoglist = new List<Con_DialogData>();//���жԻ��б�

    public Transform dialogueButtons;//�Ի���ť��
    public Object ButtonsItem;//�Ի���ťԤ����

    public Transform dialogueReply;//�ظ���ť��
    public Object RelyItem;//�ظ���ťԤ����

    public Transform dialogueBox;//�Ի���
    public Dialoglogic dialoglogic;//�Ի��߼�������

    public ButtonManager buttonManager;

    private Collider playerCollider;

    public void dialogbuttonInit(int npcID)//�Ի�ѡ��ť
    {
        dialogueButtons.gameObject.SetActive(true);
        dialogueReply.gameObject.SetActive(false);
        clearButten(dialogueButtons);
        //  int a = dialogueButtons.childCount;
        for (int i = 0; i < allDialoglist.Count; i++)
        {
            if (allDialoglist[i].NPCID == npcID)
            {
                //���ɶԻ�ѡ��ť
                GameObject selectButton = (GameObject)Instantiate(ButtonsItem, dialogueButtons);
                selectButton.GetComponent<dialogueItem>().Initbuttom(allDialoglist[i], this);
            }
        }

        Invoke("InitItem", 0.01F);
        // buttonManager.InitItem();//ֱ�ӵ��û����һ��������������ƥ���Bug�����untiy��Destroy()���������й�

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

    public void dialogReplyInit(Con_DialogData con_DialogData, int currentLine)//�Ի�ѡ������
    {
        Debug.Log("����Ի��ظ�����");
        dialogueReply.gameObject.SetActive(true);
        clearButten(dialogueReply);
        dialogueButtons.gameObject.SetActive(false);
        for (int i = 0; i < con_DialogData.dialoguedatas[currentLine].diaselect.Length; i++)//��ȡ����ĳ���м����Ի�ѡ��ĳ���
        {
             //���ɶԻ�ѡ��ť
            GameObject selectButton = (GameObject)Instantiate(RelyItem, dialogueReply);
            selectButton.GetComponent<dialogueItem>().InitSelect(this,con_DialogData,currentLine,i);
        }
     }
            
    

    void clearButten(Transform transform)//�������������
    {
        for (int i = 0; i < transform.childCount; i++)
            Destroy(transform.GetChild(i).gameObject);
    }
   

   public void dialogPlay(Con_DialogData Dialog,int strat)
   {
		// �̶������˳ƿ����������λ��
		dialoglogic.setPlayerCollider(this.playerCollider);
		playerCollider.GetComponent<ThirdPersonController>().LockCameraPosition = true;
		Cursor.lockState = CursorLockMode.None;

		//�Ի������
		dialogueBox.gameObject.SetActive (true);
        //ѡ�ť������
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
		Invoke(EventName, 0.01F);//�ӳ�ִ��
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
