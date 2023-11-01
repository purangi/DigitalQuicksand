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
            "���б�, ���п��� ���� �ð��� ���� ���� ���� �ְ� ������ �÷��� ȸ�� Ŵ���� �Ի��ߴ�.\n�װ͵� ���� ���� ���������� !!",
            "�� ���⼭ ��Ե� �� �ɷ��� �����س��� �� ���� �ö󰡰ھ�. \n�˰��� ���� ���̾� ������ ���� �� �Ա��� ���������� !!!",
            "ȸ�� : Ŵ���� �� �� ȯ���ϳ�. ������ ���� ���� �����ֱ� ����ص� �ǰڳ�? \n���� �ڳ����� �Ŵ� ���� ��밡 Ŀ ����",
            "�� ! �������� �ϴ� �� �ƴ϶� ���ڷ� �����ϰڽ��ϴ�.",
            "���� ������ : �츮�� �� ���� ���� �ΰ��� ���� �ùķ��̼� �غ��鼭\n� �������� ��õ �˰����� �������Ѿ� ������ Ŭ�� �����ϴ� ���̾�.",
            "���� ������ :  ����  �����ϱ� ���ٴ¡� ���� �غ��� �� ���ذ� ���� �ž�\n �� �޾�."
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
            //��ü����
            SceneManager.LoadScene("CharacterCreation");
        } else
        {
            Debug.Log("�� ü���� ����");
        }

        if(count < 6)
        {
            string message = messages[count];
            StartCoroutine(Typing(tmp_text, message, speed));
        }
    }
}
