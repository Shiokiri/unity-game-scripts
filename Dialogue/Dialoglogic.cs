using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;
using UnityEngine.UI;

public class Dialoglogic : MonoBehaviour
{
    public DialogueManager Manager;

	private Collider playerCollider;

	public int currentLine; //对话索引行
    public Con_DialogData con_DialogData;//对话

    public Text DialogText;//对话内容文本
    public Text CharName;//对话主人名

    private bool canNext = true;
    public AudioSource m_SoundAudio;

    //自动播放
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
        playDialog();//播放对话方法
       
    }

    // Update is called once per frame
    void Update()
    {
        if (con_DialogData == null)
            return;

        // 自动播放
        if (autoPlay)
        {
            //播放对话
            if (currentLine < con_DialogData.dialoguedatas.Length && autoPlayNext)
                playDialog();
            else if (currentLine >= con_DialogData.dialoguedatas.Length && autoPlayNext)
            {
				// 解除固定第三人称控制器摄像机位置
				playerCollider.GetComponent<ThirdPersonController>().LockCameraPosition = false;
				Cursor.lockState = CursorLockMode.Locked;
				this.gameObject.SetActive(false);
              
            }

        }
        // 手动播放
        else
        {

            //播放对话
            if (currentLine < con_DialogData.dialoguedatas.Length && Input.GetMouseButton(0) && canNext)
                playDialog();
            else if (currentLine >= con_DialogData.dialoguedatas.Length && Input.GetMouseButton(0) && canNext)
            {
				// 解除固定第三人称控制器摄像机位置
				playerCollider.GetComponent<ThirdPersonController>().LockCameraPosition = false;
				Cursor.lockState = CursorLockMode.Locked;
				this.gameObject.SetActive(false);
              
            }

        }
    }

	public void playDialog()
    {
        m_SoundAudio.PlayOneShot(next);
        DialogText.text = "";//对话清空
        StopCoroutine("ShowDialog");
        canNext = false;
        autoPlayNext = false;


        //播放语音
        if (con_DialogData.dialoguedatas[currentLine].DialogueVoice != null)
            m_SoundAudio.PlayOneShot(con_DialogData.dialoguedatas[currentLine].DialogueVoice);

        //改变名字
        CharName.text = con_DialogData.dialoguedatas[currentLine].charName;
        CharName.color = con_DialogData.dialoguedatas[currentLine].NameColor;
        //CharName.color

        //开启对话
        StartCoroutine("ShowDialog");

        //执行事件
        if (con_DialogData.dialoguedatas[currentLine].Event != "")
		{
			Debug.Log(con_DialogData.dialoguedatas[currentLine].Event);
			Manager.DoEvent(con_DialogData.dialoguedatas[currentLine].Event);
		}

        if (!autoPlay)//不是自动播放
            Invoke("CanNext", 1.5F);//可以跳转

    }
    IEnumerator ShowDialog()
    {
		DialogText.color = con_DialogData.textColor;

		foreach (char c in con_DialogData.dialoguedatas[currentLine].Dialogue)//滚动输出字符
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

        // 没有回话选择选项
        if (con_DialogData.dialoguedatas[currentLine].diaselect.Length == 0)
        {
            canNext = true;
			if (con_DialogData.dialoguedatas[currentLine].nextoffet != 0)
				currentLine += con_DialogData.dialoguedatas[currentLine].nextoffet;
			else 
				currentLine++;
        }
        // 有回话选项
        else
        {

            autoPlayNext = false; // 停止自动播放下一句
            canNext = false; // 停止手动播放下一句
            Manager.dialogReplyInit(con_DialogData, currentLine); // 创建回话按钮

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
