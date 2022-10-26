using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    // Start is called before the first frame update
    public List<dialogueItem> buttonItems = new List<dialogueItem>();
    public int select=-1;

    float ScrollWheel;

    void Start()
    {
        buttonItems.Clear();//清空列表
    }

    // Update is called once per frame
    void Update()
    {
        
        if (buttonItems.Count > 0)
        {
            ScrollWheel = Input.GetAxis("Mouse ScrollWheel");
            Debug.Log(ScrollWheel);
            if (ScrollWheel != 0)
            {
                print(ScrollWheel);
                select -= (int)(ScrollWheel*10);
                toSelectButton();
            }
            if (Input.GetKeyDown(KeyCode.F))
                buttonItems[select].dialogStart();
        }
    }
    
    public void InitItem()
    {
        buttonItems=new List<dialogueItem>();
        buttonItems.Clear();//清空列表
        select = -1;
        for (int i = 0; i < this.transform.childCount; i++)
        {
            if (i > buttonItems.Count - 1)
                buttonItems.Add(this.transform.GetChild(i).GetComponent<dialogueItem>());
            else
                buttonItems[i] = this.transform.GetChild(i).GetComponent<dialogueItem>();
        }
        if (buttonItems.Count > 0) // 打开第一个选择图标
        {
            select = 0;
            buttonItems[0].SelectImage.SetActive(true);
        }
    }
    void toSelectButton()
    {
        for (int i = 0; i < buttonItems.Count; i++) // 关闭所有选择图标
        {
            buttonItems[i].SelectImage.SetActive(false);
        
        }
        if (select < 0)
        {
			select = buttonItems.Count - 1;
			buttonItems[select].SelectImage.SetActive(true);
        }
        else if(select > buttonItems.Count - 1)
        {
            select = 0;
            buttonItems[select].SelectImage.SetActive(true);
        }
        else
        {
            buttonItems[select].SelectImage.SetActive(true);
        }

    }

   


}
