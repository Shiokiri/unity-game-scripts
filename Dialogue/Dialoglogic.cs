using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;
using UnityEngine.UI;

public class Dialoglogic : MonoBehaviour
{
    public DialogueManager Manager;

	private Collider playerCollider;

	public int currentLine; //�Ի�������
    public Con_DialogData con_DialogData;//�Ի�

    public Text DialogText;//�Ի������ı�
    public Text CharName;//�Ի�������

    private bool canNext = true;
    public AudioSource m_SoundAudio;

    //�Զ�����
    public GameObject AutoStart;
    public GameObject AutoEnd;
    private bool autoPlay = false;
    private bool autoPlayNext = true;

    public AudioClip next;

	public void setAutoPlayStatus()
    {
        AutoStart.SetActive(false);
        AutoEnd.SetActive(true);
    }

    private void Awake()
    {
       m_SoundAudio = this.gameObject.AddComponent<AudioSource>();
    }


    // Start is called before the first frame update
    public void DialogInit(Con_DialogData DialogData, int start)
    {
        con_DialogData = DialogData;
        currentLine = start;

        canNext = true;
        autoPlayNext = true;

		AutoPlayEnd();
        playDialog();//���ŶԻ�����
       
    }

    // Update is called once per frame
    void Update()
    {
        if (con_DialogData == null)
            return;

        // �Զ�����
        if (autoPlay)
        {
            //���ŶԻ�
            if (currentLine < con_DialogData.dialoguedatas.Length && autoPlayNext)
                playDialog();
            else if (currentLine >= con_DialogData.dialoguedatas.Length && autoPlayNext)
            {
				// ����̶������˳ƿ����������λ��
				playerCollider.GetComponent<ThirdPersonController>().LockCameraPosition = false;
				Cursor.lockState = CursorLockMode.Locked;
				this.gameObject.SetActive(false);
              
            }

        }
        // �ֶ�����
        else
        {

            //���ŶԻ�
            if (currentLine < con_DialogData.dialoguedatas.Length && Input.GetMouseButton(0) && canNext)
                playDialog();
            else if (currentLine >= con_DialogData.dialoguedatas.Length && Input.GetMouseButton(0) && canNext)
            {
				// ����̶������˳ƿ����������λ��
				playerCollider.GetComponent<ThirdPersonController>().LockCameraPosition = false;
				Cursor.lockState = CursorLockMode.Locked;
				this.gameObject.SetActive(false);
              
            }

        }
    }

	public void playDialog()
    {
        m_SoundAudio.PlayOneShot(next);
        DialogText.text = "";//�Ի����
        StopCoroutine("ShowDialog");
        canNext = false;
        autoPlayNext = false;


        //��������
        if (con_DialogData.dialoguedatas[currentLine].DialogueVoice != null)
            m_SoundAudio.PlayOneShot(con_DialogData.dialoguedatas[currentLine].DialogueVoice);

        //�ı�����
        CharName.text = con_DialogData.dialoguedatas[currentLine].charName;
        CharName.color = con_DialogData.dialoguedatas[currentLine].NameColor;
        //CharName.color

        //�����Ի�
        StartCoroutine("ShowDialog");

        //ִ���¼�
        if (con_DialogData.dialoguedatas[currentLine].Event != "")
		{
			Debug.Log(con_DialogData.dialoguedatas[currentLine].Event);
			Manager.DoEvent(con_DialogData.dialoguedatas[currentLine].Event);
		}

        if (!autoPlay)//�����Զ�����
            Invoke("CanNext", 1.5F);//������ת

    }
    IEnumerator ShowDialog()
    {
		DialogText.color = con_DialogData.textColor;

		foreach (char c in con_DialogData.dialoguedatas[currentLine].Dialogue)//��������ַ�
        {
            DialogText.text += c;
            yield return new WaitForSeconds(0.07f);
        }

        if (autoPlay)
        {
            yield return new WaitForSeconds(1f);
            autoPlayNext = true;
            CanNext();
        }

    }
	public void setPlayerCollider(Collider playercollider)
	{
		this.playerCollider = playercollider;
	}
	void CanNext()
    {
		//if (con_DialogData.dialoguedatas[currentLine].end)
        //{
			//this.gameObject.SetActive(false);
        //}

        // û�лػ�ѡ��ѡ��
        if (con_DialogData.dialoguedatas[currentLine].diaselect.Length == 0)
        {
            canNext = true;
			if (con_DialogData.dialoguedatas[currentLine].nextoffet != 0)
				currentLine += con_DialogData.dialoguedatas[currentLine].nextoffet;
			else 
				currentLine++;
        }
        // �лػ�ѡ��
        else
        {

            autoPlayNext = false; // ֹͣ�Զ�������һ��
            canNext = false; // ֹͣ�ֶ�������һ��
            Manager.dialogReplyInit(con_DialogData, currentLine); // �����ػ���ť

        }
    }

    public void AutoPlayStart()
    {
        // Debug.Log("AutoPlayStart");
        autoPlay = true;
        AutoStart.SetActive(false);
        AutoEnd.SetActive(true);
        autoPlayNext = true;
    }
    public void AutoPlayEnd()
    {
        // Debug.Log("AutoPlayEnd");
        autoPlay = false;
        AutoStart.SetActive(true);
        AutoEnd.SetActive(false);
        canNext = true;
    }


}
