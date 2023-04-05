using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleMenuController : MonoBehaviour
{
    public GameObject start;
    public GameObject contin;
    public GameObject endings;
    public GameObject exit;

    private int clickNum = 0;

    // Start is called before the first frame update
    void Start()
    {
        Selected();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            clickNum -= 1;
            Selected();
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            clickNum += 1;
            Selected();
        }
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
        {
            buttonClicked();
        }
    }

    void Selected()
    {
        if (clickNum < 0)
        {
            clickNum = 4 + clickNum;
        }

        clickNum = clickNum % 4;

        if (clickNum == 0)
        {
            start.GetComponent<ButtonOnKeyboard>().OnMouseOver();
            contin.GetComponent<ButtonOnKeyboard>().OnMouseExit();
            endings.GetComponent<ButtonOnKeyboard>().OnMouseExit();
            exit.GetComponent<ButtonOnKeyboard>().OnMouseExit();
        }
        else if (clickNum == 1)
        {
            start.GetComponent<ButtonOnKeyboard>().OnMouseExit();
            contin.GetComponent<ButtonOnKeyboard>().OnMouseOver();
            endings.GetComponent<ButtonOnKeyboard>().OnMouseExit();
            exit.GetComponent<ButtonOnKeyboard>().OnMouseExit();
        }
        else if (clickNum == 2)
        {
            start.GetComponent<ButtonOnKeyboard>().OnMouseExit();
            contin.GetComponent<ButtonOnKeyboard>().OnMouseExit();
            endings.GetComponent<ButtonOnKeyboard>().OnMouseOver();
            exit.GetComponent<ButtonOnKeyboard>().OnMouseExit();
        }
        else if (clickNum == 3)
        {
            start.GetComponent<ButtonOnKeyboard>().OnMouseExit();
            contin.GetComponent<ButtonOnKeyboard>().OnMouseExit();
            endings.GetComponent<ButtonOnKeyboard>().OnMouseExit();
            exit.GetComponent<ButtonOnKeyboard>().OnMouseOver();
        }
        else
        {
            Debug.Log("오류");
        }
    }

    void buttonClicked()
    {
        if (clickNum < 0)
        {
            clickNum = 4 + clickNum;
        }

        clickNum = clickNum % 4;

        if (clickNum == 0)
        {
            start.GetComponent<ButtonOnKeyboard>().OnMouseDown();
        }
        else if (clickNum == 1)
        {
            contin.GetComponent<ButtonOnKeyboard>().OnMouseDown();
        }
        else if (clickNum == 2)
        {
            endings.GetComponent<ButtonOnKeyboard>().OnMouseDown();
        }
        else if (clickNum == 3)
        {
            exit.GetComponent<ButtonOnKeyboard>().OnMouseDown();
        }
        else
        {
            Debug.Log("오류");
        }
    }
}
