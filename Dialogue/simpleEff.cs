using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class simpleEff : MonoBehaviour
{
    // Start is called before the first frame update
    bool toleft = false;
    RectTransform image;
    void Start()
    {
        image = this.GetComponent<RectTransform>();
        image.anchoredPosition = new Vector2(60, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (this.image.anchoredPosition.x < 80 && !toleft)
        {
            image.anchoredPosition = new Vector2(this.transform.localPosition.x+0.1f,0);
        }
        else
        {
            image.anchoredPosition = new Vector2(this.transform.localPosition.x-0.1f, 0);
            toleft = true;
        }
        if(this.image.anchoredPosition.x > 60 && toleft)
            image.anchoredPosition = new Vector2(this.transform.localPosition.x-0.1f, 0);
        else
        {
            image.anchoredPosition = new Vector2(this.transform.localPosition.x + 0.1f, 0);
            toleft = false;
        }


    }
}
