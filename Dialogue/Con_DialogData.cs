using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DialogStatus
{
    Com=0,//普通
    shop=1,//商店
    Quest,//任务
}

[CreateAssetMenu(fileName = "Configuration", menuName = "DialogSystem/DialogConf")]
public class Con_DialogData : ScriptableObject
{
	public int NPCID;  //该对话的所有者ID
    public string DialogName;//该对话对话名字
	public Color textColor;
	public DialogStatus DialogStatus; //任务状态。不同状态有不同的对话  
    public DialogData[] dialoguedatas;//对话数据
    [Serializable]//结构体，方便序列化，显示在层次面板上
    public struct DialogData
    {
		public string charName;//角色名字
        public Color NameColor;//角色颜色
        [TextArea(1, 5)]
        public string Dialogue;    //对话内容
        public AudioClip DialogueVoice;//对话语音
        public selectDialog[] diaselect;//回话选项
        public string Event;
        public bool end;
		public int nextoffet;
    }
    [Serializable]
    public struct selectDialog{
        public int next;//下一条回话索引入口
        public string selectText;//回话选项内容
    }

}

