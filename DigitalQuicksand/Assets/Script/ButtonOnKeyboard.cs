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
            //좌측 메뉴 바 없애기
            GameObject obj = GameObject.Find("MenuBar");
            obj.SetActive(false);
            //저장 파일 출력 탭 활성화
        } else if(btn_name == "endings")
        {
            //엔딩 갤러리 씬 연결
        } else if(btn_name == "quit")
        {
            //종료하시겠습니까? 탭 활성화
        }
    }
}
