using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using MyDB;

public class Daily : MonoBehaviour
{
    public GameObject DailyObject;
    public GameObject wakeupObject;
    public GameObject eatObject;
    public GameObject studyObject;
    public GameObject sleepObject;
    public TextMeshProUGUI wakeupText;
    public TextMeshProUGUI eatText;
    public TextMeshProUGUI studyText;
    public TextMeshProUGUI sleepText;
    public Animator wakeup_ani;
    public Animator eat_ani;
    public Animator study_ani;
    public Animator sleep_ani;

    private Character character;

    void Start()
    {
        
    }

    public void DailyStart()
    {
        DailyObject.SetActive(true);

        character = GameManager.instance.character;

        int num = Random.Range(1, 5);
        StartCoroutine(RunDailyRoutine(num));
    }

    public IEnumerator RunDailyRoutine(int num)
    {
        Debug.Log(num);
        switch (num)
        {
            case 1:
                yield return StartCoroutine(Wakeup());
                break;
            case 2:
                yield return StartCoroutine(Eat());
                break;
            case 3:
                yield return StartCoroutine(Study());
                break;
            case 4:
                yield return StartCoroutine(Sleep());
                break;
        }

        wakeupObject.SetActive(false);
        eatObject.SetActive(false);
        studyObject.SetActive(false);
        sleepObject.SetActive(false);

        DailyObject.SetActive(false);
    }

    public IEnumerator Wakeup()
    {
        wakeupObject.SetActive(true);

        int health = GameManager.instance.property[1];
        string wakeupTextStr = "";

        if (health >= 90)
        {
            wakeupTextStr = "���� ǫ ����� ����� ��ģ��!!!";
        }
        else if (health >= 60 && health <= 89)
        {
            wakeupTextStr = "���õ� �� �ڰ� �Ͼ��~";
        }
        else if (health >= 30 && health <= 59)
        {
            wakeupTextStr = "���ݸ� �� �ڰ� �ʹ١� 5�и���";
        }
        else if (health >= 10 && health <= 29)
        {
            wakeupTextStr = "�ǰ��ؼ� �Ͼ�� �ȴ�. �� �ھ���.";
        }
        else
        {
            wakeupTextStr = "Zzzz ����...";
        }

        wakeupText.text = wakeupTextStr;

        //wakeup_ani.SetInteger("Gender", GameManager.instance.gender);
        if (character.Gender == 0)
        {
            wakeup_ani.SetTrigger("Wakeup(f)");
        }
        else
        {
            wakeup_ani.SetTrigger("Wakeup(m)");
        }

        yield return new WaitForSeconds(5f);
    }

    public IEnumerator Eat()
    {
        eatObject.SetActive(true);

        int food = GameManager.instance.genre[6];
        string eatTextStr = "";

        if (food >= 90)
        {
            eatTextStr = "���� ��������� �ʹ� ���ֳ�? ��������!";
        }
        else if (food >= 70 && food <= 89)
        {
            eatTextStr = "���� ����� ������ ���ִ�!";
        }
        else if (food >= 40 && food <= 69)
        {
            eatTextStr = "����� ���̾�.";
        }
        else if (food >= 10 && food <= 39)
        {
            eatTextStr = "���� ���� �ѵ�.. �������� �׳� �� �Ծ����.";
        }
        else
        {
            eatTextStr = "����� ���� �� �ִ� �Ű���?";
        }

        eatText.text = eatTextStr;

        //eat_ani.SetInteger("Gender", GameManager.instance.gender);
        if (character.Gender == 0)
        {
            eat_ani.SetTrigger("eat(f)");
            //eatObject.GetComponent<Image>().sprite = eatAnimation_0000;
        }
        else
        {
            eat_ani.SetTrigger("eat(m)");
            //eatObject.GetComponent<Image>().sprite = eatAnimation_m_0000;
        }

        yield return new WaitForSeconds(5f);
    }

    public IEnumerator Study()
    {
        studyObject.SetActive(true);

        int education = GameManager.instance.genre[3];
        string educationTextStr = "";

        if (education >= 90)
        {
            educationTextStr = "�������� �̷� ���̱���.";
        }
        else if (education >= 70 && education <= 89)
        {
            educationTextStr = "�� �ʹ� ����? �� õ���� ��!";
        }
        else if (education >= 40 && education <= 69)
        {
            educationTextStr = "�� ����? ���� �� �� �� ���Ƶ� �ǰڴ�.";
        }
        else if (education >= 10 && education <= 39)
        {
            educationTextStr = "�����ؾ� �ϴµ��� (�ٹ� �ٹ�)  ������";
        }
        else
        {
            educationTextStr = "(�ڴ� ��...)";
        }

        studyText.text = educationTextStr;

        //study_ani.SetInteger("Gender", GameManager.instance.gender);
        if (character.Gender == 0)
        {
            study_ani.SetTrigger("study(f)");
            //studyObject.GetComponent<Image>().sprite = study_f_0000;
        }
        else
        {
            study_ani.SetTrigger("study(m)");
            //studyObject.GetComponent<Image>().sprite = study_m_0000;
        }

        yield return new WaitForSeconds(5f);
    }

    public IEnumerator Sleep()
    {
        sleepObject.SetActive(true);

        int addiction = GameManager.instance.property[2];
        string sleepTextStr = "";

        if (addiction >= 90)
        {
            sleepTextStr = "�� ���� �б� �� ���� �ʹ� ��մ�!";
        }
        else if (addiction >= 60 && addiction <= 89)
        {
            sleepTextStr = "�� ���� �� ���� �ھ���. �ٵ� ���� 6�þ�?";
        }
        else if (addiction >= 30 && addiction <= 59)
        {
            sleepTextStr = "���� 2�ó�? �ϳ��� ���� ���� �׸� �ھ߰ڴ�.";
        }
        else if (addiction >= 10 && addiction <= 29)
        {
            sleepTextStr = "���� �б� ������ ���� �׸� �ھ���.";
        }
        else
        {
            sleepTextStr = "Zzzz ����... ���ڸ��� ������.";
        }

        sleepText.text = sleepTextStr;

        //sleep_ani.SetInteger("Gender", GameManager.instance.gender);
        if (character.Gender == 0)
        {
            sleep_ani.SetTrigger("sleep(f)");
            //sleepObject.GetComponent<Image>().sprite = sleep_f_0000;
        }
        else
        {
            sleep_ani.SetTrigger("sleep(m)");
            //sleepObject.GetComponent<Image>().sprite = sleep_m_0000;
        }

        yield return new WaitForSeconds(5f);
    }
}