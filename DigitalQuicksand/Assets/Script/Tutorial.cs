using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    public Sprite[] Images;
    public GameObject nextBtn;
    public GameObject prevBtn;

    private int num = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ShowButton();
    }

    public void clickNext()
    {
        num++;

        if(num > 5)
        {
            num = 0;
        }

        GetComponent<Image>().sprite = Images[num];
    }

    public void clickPrev()
    {
        num--;

        if(num < 0)
        {
            num = 0;
        }

        GetComponent<Image>().sprite = Images[num];
    }

    private void ShowButton()
    {
        if (num == 0)
        {
            prevBtn.SetActive(false);
        }
        else if (num == 5)
        {
            nextBtn.SetActive(false);
        }
        else
        {
            prevBtn.SetActive(true);
            nextBtn.SetActive(true);
        }
    }
}
