using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DialogStatus
{
    Com=0,//��ͨ
    shop=1,//�̵�
    Quest,//����
}

[CreateAssetMenu(fileName = "Configuration", menuName = "DialogSystem/DialogConf")]
public class Con_DialogData : ScriptableObject
{
	public int NPCID;  //�öԻ���������ID
    public string DialogName;//�öԻ��Ի�����
	public Color textColor;
	public DialogStatus DialogStatus; //����״̬����ͬ״̬�в�ͬ�ĶԻ�  
    public DialogData[] dialoguedatas;//�Ի�����
    [Serializable]//�ṹ�壬�������л�����ʾ�ڲ�������
    public struct DialogData
    {
		public string charName;//��ɫ����
        public Color NameColor;//��ɫ��ɫ
        [TextArea(1, 5)]
        public string Dialogue;    //�Ի�����
        public AudioClip DialogueVoice;//�Ի�����
        public selectDialog[] diaselect;//�ػ�ѡ��
        public string Event;
        public bool end;
		public int nextoffet;
    }
    [Serializable]
    public struct selectDialog{
        public int next;//��һ���ػ��������
        public string selectText;//�ػ�ѡ������
    }

}

