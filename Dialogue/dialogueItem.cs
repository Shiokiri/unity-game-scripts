using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class dialogueItem : MonoBehaviour
{
    Con_DialogData Dialog;//该按钮所属对话
    
    DialogueManager Manager;//管理器

    public Button Button;

    public Image state;
    public Sprite[] Imagestate;
    public Text nameText;

 


    int StartIndx=0;//对话初始位置

    public GameObject SelectImage;


    public void Initbuttom(Con_DialogData dialog, DialogueManager manager)//初始化对话选项
    {
        this.Manager = manager;
        this.Dialog = dialog;
        this.Button = this.GetComponent<Button>();
        StartIndx = 0;//对话开始位置
        Button.onClick.AddListener(dialogStart);
        stateInit(Dialog.DialogStatus);

        //Button.Select();
        //Button.
    }
    public void InitSelect( DialogueManager manager,Con_DialogData dialog, int Start,int select)//初始化回复对话
    {
        this.Manager = manager;
        this.Dialog = dialog;
        this.Button = this.GetComponent<Button>();
        StartIndx = dialog.dialoguedatas[Start].diaselect[select].next;//对话开始位置
        Button.onClick.AddListener(dialogStart);

        nameText.text = dialog.dialoguedatas[Start].diaselect[select].selectText;//第Start句的第几个选项
    }




    public void dialogStart()
    {

        Manager.dialogPlay(Dialog,StartIndx);

        // 对话开始

    }
    void stateInit(DialogStatus status)
    {
        state.sprite=Imagestate[(int)status];
        nameText.text = Dialog.DialogName;
    }

    

}
