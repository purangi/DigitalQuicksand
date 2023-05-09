using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Daily : MonoBehaviour
{
    public GameObject DailyObject;
    public GameObject wakeupObject;
    public GameObject eatObject;
    public GameObject studyObject;
    public GameObject sleepObject;
    public Text wakeupText;
    public Text eatText;
    public Text studyText;
    public Text sleepText;
    private Animator wakeup_ani;
    private Animator eat_ani;
    private Animator study_ani;
    private Animator sleep_ani;
    //public Sprite wake_f_0000;
    //public Sprite wake_m_0000;
    //public Sprite eatAnimation_0000;
    //public Sprite eatAnimation_m_0000;
    //public Sprite study_f_0000;
    //public Sprite study_m_0000;
    //public Sprite sleep_f_0000;
    //public Sprite sleep_m_0000;

    private void Start()
    {

        wakeupText = wakeupObject.GetComponentInChildren<Text>();
        eatText = eatObject.GetComponentInChildren<Text>();
        studyText = studyObject.GetComponentInChildren<Text>();
        sleepText = sleepObject.GetComponentInChildren<Text>();
        wakeup_ani = wakeupObject.GetComponentInChildren<Animator>();
        eat_ani = eatObject.GetComponentInChildren<Animator>();
        study_ani = studyObject.GetComponentInChildren<Animator>();
        sleep_ani = sleepObject.GetComponentInChildren<Animator>();

        StartCoroutine(RunDailyRoutine());
    }
    public IEnumerator RunDailyRoutine()
    {
        yield return StartCoroutine(Wakeup());
        yield return StartCoroutine(Eat());
        yield return StartCoroutine(Study());
        yield return StartCoroutine(Sleep());

        DailyObject.SetActive(false);
    }

    public IEnumerator Wakeup()
    {
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
            wakeupTextStr = "���� �� �� �ڰ� �ʹ١� 5�и���";
        }
        else if (health >= 10 && health <= 29)
        {
            wakeupTextStr = "�ǰ��ؼ� �Ͼ�� �ȴ�. �� �ھ���.";
        }
        else
        {
            wakeupTextStr = "Zzzz ����...";
        }

        wakeupObject.SetActive(true);
        eatObject.SetActive(false);
        studyObject.SetActive(false);
        sleepObject.SetActive(false);

        wakeupText.text = wakeupTextStr;

        wakeup_ani.SetInteger("Gender", GameManager.instance.gender);
        if (GameManager.instance.gender == 0)
        {
            wakeup_ani.SetTrigger("Wakeup(f)");
           // wakeupObject.GetComponent<Image>().sprite = wake_f_0000;

        }
        else
        {
            wakeup_ani.SetTrigger("Wakeup(m)");
            //wakeupObject.GetComponent<Image>().sprite = wake_m_0000;

        }


        yield return new WaitForSeconds(3f);
    }

    public IEnumerator Eat()
    {
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

        wakeupObject.SetActive(false);
        eatObject.SetActive(true);
        studyObject.SetActive(false);
        sleepObject.SetActive(false);

        eatText.text = eatTextStr;

        eat_ani.SetInteger("Gender", GameManager.instance.gender);
        if (GameManager.instance.gender == 0)
        {
            eat_ani.SetTrigger("eat(f)");
            //eatObject.GetComponent<Image>().sprite = eatAnimation_0000;
        }
        else
        {
            eat_ani.SetTrigger("eat(m)");
            //eatObject.GetComponent<Image>().sprite = eatAnimation_m_0000;
        }

        yield return new WaitForSeconds(3f);
    }

    public IEnumerator Study()
    {
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

        wakeupObject.SetActive(false);
        eatObject.SetActive(false);
        studyObject.SetActive(true);
        sleepObject.SetActive(false);

        studyText.text = educationTextStr;

        study_ani.SetInteger("Gender", GameManager.instance.gender);
        if (GameManager.instance.gender == 0)
        {
            study_ani.SetTrigger("study(f)");
            //studyObject.GetComponent<Image>().sprite = study_f_0000;
        }
        else
        {
            study_ani.SetTrigger("study(m)");
            //studyObject.GetComponent<Image>().sprite = study_m_0000;
        }

        yield return new WaitForSeconds(3f);
    }

    public IEnumerator Sleep()
    {
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

        wakeupObject.SetActive(false);
        eatObject.SetActive(false);
        studyObject.SetActive(false);
        sleepObject.SetActive(true);

        sleepText.text = sleepTextStr;

        sleep_ani.SetInteger("Gender", GameManager.instance.gender);
        if (GameManager.instance.gender == 0)
        {
            sleep_ani.SetTrigger("sleep(f)");
            //sleepObject.GetComponent<Image>().sprite = sleep_f_0000;
        }
        else
        {
            sleep_ani.SetTrigger("sleep(m)");
            //sleepObject.GetComponent<Image>().sprite = sleep_m_0000;
        }

        yield return new WaitForSeconds(3f);
    }

}