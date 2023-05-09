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
            wakeupTextStr = "잠을 푹 잤더니 기운이 넘친다!!!";
        }
        else if (health >= 60 && health <= 89)
        {
            wakeupTextStr = "오늘도 잘 자고 일어났다~";
        }
        else if (health >= 30 && health <= 59)
        {
            wakeupTextStr = "조금 만 더 자고 싶다… 5분만…";
        }
        else if (health >= 10 && health <= 29)
        {
            wakeupTextStr = "피곤해서 일어나기 싫다. 더 자야지.";
        }
        else
        {
            wakeupTextStr = "Zzzz 쿨쿨...";
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
            eatTextStr = "내가 만들었지만 너무 맛있네? 우적우적!";
        }
        else if (food >= 70 && food <= 89)
        {
            eatTextStr = "직접 만들어 먹으니 맛있다!";
        }
        else if (food >= 40 && food <= 69)
        {
            eatTextStr = "평범한 맛이야.";
        }
        else if (food >= 10 && food <= 39)
        {
            eatTextStr = "먹을 만은 한데.. 다음부터 그냥 사 먹어야지.";
        }
        else
        {
            eatTextStr = "사람이 먹을 수 있는 거겠지?";
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
            educationTextStr = "끄덕끄덕 이런 뜻이구나.";
        }
        else if (education >= 70 && education <= 89)
        {
            educationTextStr = "아 너무 쉽네? 나 천재인 듯!";
        }
        else if (education >= 40 && education <= 69)
        {
            educationTextStr = "개 쉽네? 시험 잘 볼 듯 졸아도 되겠다.";
        }
        else if (education >= 10 && education <= 39)
        {
            educationTextStr = "집중해야 하는데… (꾸벅 꾸벅)  졸려…";
        }
        else
        {
            educationTextStr = "(자는 중...)";
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
            sleepTextStr = "아 내일 학교 안 갈래 너무 재밌다!";
        }
        else if (addiction >= 60 && addiction <= 89)
        {
            sleepTextStr = "몇 개만 더 보고 자야지. 근데 벌써 6시야?";
        }
        else if (addiction >= 30 && addiction <= 59)
        {
            sleepTextStr = "벌써 2시네? 하나만 보고 이제 그만 자야겠다.";
        }
        else if (addiction >= 10 && addiction <= 29)
        {
            sleepTextStr = "내일 학교 가려면 이제 그만 자야지.";
        }
        else
        {
            sleepTextStr = "Zzzz 쿨쿨... 눕자마자 잠들었다.";
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