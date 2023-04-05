using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonOnKeyboard : MonoBehaviour
{
    public GameObject sign;
    public string btn_name;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnMouseOver()
    {
        sign.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
    }

    public void OnMouseExit()
    {
        sign.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
    }

    public void OnMouseDown()
    {
        if(btn_name == "start")
        {
            SceneManager.LoadScene("Prologue");
        } else if(btn_name == "continue")
        {
            //���� �޴� �� ���ֱ�
            GameObject obj = GameObject.Find("MenuBar");
            obj.SetActive(false);
            //���� ���� ��� �� Ȱ��ȭ
        } else if(btn_name == "endings")
        {
            //���� ������ �� ����
        } else if(btn_name == "quit")
        {
            //�����Ͻðڽ��ϱ�? �� Ȱ��ȭ
        }
    }
}
