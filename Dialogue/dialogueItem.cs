using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class dialogueItem : MonoBehaviour
{
    Con_DialogData Dialog;//�ð�ť�����Ի�
    
    DialogueManager Manager;//������

    public Button Button;

    public Image state;
    public Sprite[] Imagestate;
    public Text nameText;

 


    int StartIndx=0;//�Ի���ʼλ��

    public GameObject SelectImage;


    public void Initbuttom(Con_DialogData dialog, DialogueManager manager)//��ʼ���Ի�ѡ��
    {
        this.Manager = manager;
        this.Dialog = dialog;
        this.Button = this.GetComponent<Button>();
        StartIndx = 0;//�Ի���ʼλ��
        Button.onClick.AddListener(dialogStart);
        stateInit(Dialog.DialogStatus);

        //Button.Select();
        //Button.
    }
    public void InitSelect( DialogueManager manager,Con_DialogData dialog, int Start,int select)//��ʼ���ظ��Ի�
    {
        this.Manager = manager;
        this.Dialog = dialog;
        this.Button = this.GetComponent<Button>();
        StartIndx = dialog.dialoguedatas[Start].diaselect[select].next;//�Ի���ʼλ��
        Button.onClick.AddListener(dialogStart);

        nameText.text = dialog.dialoguedatas[Start].diaselect[select].selectText;//��Start��ĵڼ���ѡ��
    }




    public void dialogStart()
    {

        Manager.dialogPlay(Dialog,StartIndx);

        // �Ի���ʼ

    }
    void stateInit(DialogStatus status)
    {
        state.sprite=Imagestate[(int)status];
        nameText.text = Dialog.DialogName;
    }

    

}
