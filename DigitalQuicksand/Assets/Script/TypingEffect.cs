using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class TypingEffect : MonoBehaviour
{
    public TextMeshProUGUI tmp_text;
    public GameObject button;
    float speed = 0.07f;
    List<string> messages;
    int count = 0;
    public SpriteRenderer img_renderer;

    public Sprite prologue2;
    public Sprite prologue3;
    public Sprite prologue4;
    public Sprite prologue5;

    // Start is called before the first frame update
    void Start()
    {
        messages = new List<string>()
        {
            "대학교, 대학원의 힘든 시간을 거쳐 드디어 세계 최고 동영상 플랫폼 회사 킴고리즘에 입사했다.\n그것도 무려 수석 연구원으로 !!",
            "난 여기서 어떻게든 내 능력을 증명해내고 더 높이 올라가겠어. \n알고리즘 개발 쯤이야 나한텐 식은 죽 먹기지 으하하하하 !!!",
            "회장 : 킴고리즘에 온 걸 환영하네. 앞으로 멋진 성과 보여주길 기대해도 되겠나? \n내가 자네한테 거는 아주 기대가 커 하하",
            "네 ! 열심히만 하는 게 아니라 숫자로 증명하겠습니다.",
            "선임 연구원 : 우리가 할 일은 가상 인간의 삶을 시뮬레이션 해보면서\n어떤 방향으로 추천 알고리즘을 발전시켜야 수익이 클지 연구하는 일이야.",
            "선임 연구원 :  말로  설명하기 보다는… 직접 해보는 게 이해가 빠를 거야\n 자 받아."
        };

        string message = messages[count];
        StartCoroutine(Typing(tmp_text, message, speed));
    }

    void Update()
    {
        if(button.activeSelf)
        {
            if(Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return)) {
                ButtonClicked();
            }
        }
    }

    IEnumerator Typing(TextMeshProUGUI typingText, string message1, float speed)
    {
        for(int i = 0; i < message1.Length; i++)
        {
            typingText.text = message1.Substring(0, i + 1);
            yield return new WaitForSeconds(speed);
        }
        StartCoroutine(showButton());
    }

    IEnumerator showButton()
    {
        yield return new WaitForSeconds(0.5f);
        button.SetActive(true);
    }

    public void ButtonClicked()
    {
        button.SetActive(false);
        count++;

        if(count == 1)
        {
            img_renderer.sprite = prologue2;
        } else if(count == 2)
        {
            tmp_text.color = new Color32(19, 179, 242, 255);
            img_renderer.sprite = prologue3;
        } else if (count == 3)
        {
            tmp_text.color = new Color32(255, 255, 255, 255);
        }
        else if (count == 4)
        {
            tmp_text.color = new Color32(19, 179, 242, 255);
            img_renderer.sprite = prologue4;
        }
        else if (count == 5)
        {
            img_renderer.sprite = prologue5;
        } else if(count >= 6)
        {
            //씬체인지
            SceneManager.LoadScene("CharacterCreation");
        } else
        {
            Debug.Log("씬 체인지 오류");
        }

        if(count < 6)
        {
            string message = messages[count];
            StartCoroutine(Typing(tmp_text, message, speed));
        }
    }
}
