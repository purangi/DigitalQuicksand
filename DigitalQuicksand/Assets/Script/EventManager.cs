using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
using MyDB;
using UnityEngine.SceneManagement;

public class EventManager : MonoBehaviour
{
    private DialogueRunner dialogueRunner;

    public GameObject video;
    public GameObject top_menu;
    public GameObject ui;
    public GameObject character;
    public GameObject daily;
    public GameObject blur;
    public GameObject content;
    public GameObject monthly;
    public GameObject setting_exit;
    public GameObject off;

    private List<string> triggeredEvents = new List<string>();
    private bool isBlur = false;

    void Start()
    {
        dialogueRunner = FindObjectOfType<Yarn.Unity.DialogueRunner>();
    }

    void Update()
    {
        if (GameManager.instance.property[1] <= 0)
        {
            Ending();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            off.SetActive(true);
        }
    }

    public void TurnOff()
    {
        Application.Quit();
    }

    [YarnCommand("show_blur")] 
    public void ShowBlur()
    {
        isBlur = isBlur ? false : true;

        if(isBlur)
        {
            blur.SetActive(true);
        } else
        {
            blur.SetActive(false);
        }
    }

    [YarnCommand("tutorial_ok")]
    public void TutorialOK()
    {
        setting_exit.GetComponent<SettingExit>().TutorialFinish();
    }

    [YarnCommand("start_video")]
    public void StartVideo()
    {
        video.SetActive(true);
        character.SetActive(false);
        video.GetComponent<VideoList>().RecommendVideo();
    }

    [YarnCommand("daily_starter")]
    public void DailyStarter()
    {
        character.SetActive(false);
        daily.GetComponent<Daily>().DailyStart();
    }

    private void DestroyAll()
    {
        Transform[] childList = content.GetComponentsInChildren<Transform>();

        if(childList != null)
        {
            for(int i = 1; i < childList.Length; i++)
            {
                Destroy(childList[i].gameObject);
            }
        }
    }

    [YarnCommand("event_starter")]
    public void EventStarter()
    {
        DestroyAll();
        CheckAddiction();

        Character ch = GameManager.instance.character;
        if (new List<int> { 1, 2, 6, 10, 14, 18, 22 }.Contains(ch.Week)) // 주차 확인
        {
            // 이벤트 조건 검사
            string eventName = CheckEvent(ch.Week);

            // 이벤트 실행
            if (eventName != null)
            {
                character.SetActive(true);
                StartConversation(eventName);
            }
            else
            {
                character.SetActive(true);
                // 어떤 조건에도 해당하지 않을 때는 해당 주차 끝내기 ( 주차끝 알림 오브젝트 활성화 )
                StartConversation("WeekFinish");
            }
        }
        else
        {
            //주차 끝
            character.SetActive(true);
            ShowBlur();
            StartConversation("WeekFinish");
        }
    }

    public void StartConversation(string node)
    {
        dialogueRunner.StartDialogue(node);
    }

    private void CheckAddiction()
    {
        int prop = GameManager.instance.property[2];
        int time = GameManager.instance.character.This_st;

        if (prop < 10)
        {
            if (time < 30)
            {
                prop -= 2;
            }
            else if (time > 90)
            {
                prop += 5;
            }
        }
        else if (prop < 20)
        {
            if (time < 50)
            {
                prop -= 2;
            }
            else if (time > 120)
            {
                prop += 5;
            }
        }
        else if (prop < 30)
        {
            if (time < 90)
            {
                prop -= 3;
            }
            else if (time > 240)
            {
                prop += 5;
            }
        }
        else if (prop < 40)
        {
            if (time < 120)
            {
                prop -= 3;
            }
            else if (time > 360)
            {
                prop += 5;
            }
        }
        else if (prop < 50)
        {
            if (time < 240)
            {
                prop -= 3;
            }
            else if (time > 450)
            {
                prop += 7;
            }
        }
        else if (prop < 60)
        {
            if (time < 360)
            {
                prop -= 3;
            }
            else if (time > 600)
            {
                prop += 7;
            }
        }
        else if (prop < 70)
        {
            if (time < 450)
            {
                prop -= 3;
            }
            else if (time > 800)
            {
                prop += 7;
            }
        }
        else if (prop < 80)
        {
            if (time < 600)
            {
                prop -= 3;
            }
            else if (time > 900)
            {
                prop += 10;
            }
        }
        else if (prop < 90)
        {
            if (time < 600)
            {
                prop -= 3;
            }
            else if (time > 900)
            {
                prop += 10;
            }
        }
        else if (prop < 96)
        {
            if (time < 600)
            {
                prop -= 3;
            }
            else if (time > 900)
            {
                prop += 10;
            }
        }
        else
        {
            if (time < 600)
            {
                prop -= 10;
            }
            else if (time > 900)
            {
                prop += 10;
            }
        }

        if (prop > 100)
        {
            prop = 100;
        }

        GameManager.instance.property[2] = prop;
    }

    [YarnCommand("set_genre")]
    public void SetGenre(int id, int num)
    {
        GameManager.instance.genre[id] += num;
    }

    [YarnCommand("set_property")]
    public void SetProperty(int id, int num)
    {
        GameManager.instance.property[id] += num;
    }

    [YarnCommand("next_week")]
    public void NextWeek()
    {
        GameManager.instance.character.Last_st = GameManager.instance.character.This_st;
        GameManager.instance.character.This_st = 0;
        GameManager.instance.character.Week++;
    }

    [YarnCommand("show_monthly")]
    public void ShowMonthly()
    {
        monthly.SetActive(true);
        monthly.GetComponent<Monthly>().ShowResult();
    }

    private string CheckEvent(int week)                                 // 이벤트 선택 및 반환
    {
        int animalCount = 0;
        int beautyCount = 0;
        int educationCount = 0;
        int entertainmentCount = 0;
        int financeCount = 0;
        int foodCount = 0;
        int gameCount = 0;
        int healthCount = 0;
        int politicsCount = 0;
        int shoppingCount = 0;

        // 소장르 count 가져오기 (이부분을 어떻게 작성할지 잘 모르겠음)
        foreach (var smallGenre in GameManager.instance.small_genre)
        {
            if (smallGenre.Sgenre_id >= 1 && smallGenre.Sgenre_id <= 7)
            {
                animalCount += smallGenre.Count;
            }
            else if (smallGenre.Sgenre_id >= 8 && smallGenre.Sgenre_id <= 12)
            {
                beautyCount += smallGenre.Count;
            }
            else if (smallGenre.Sgenre_id >= 13 && smallGenre.Sgenre_id <= 16)
            {
                educationCount += smallGenre.Count;
            }
            else if (smallGenre.Sgenre_id >= 17 && smallGenre.Sgenre_id <= 22)
            {
                entertainmentCount += smallGenre.Count;
            }
            else if (smallGenre.Sgenre_id >= 23 && smallGenre.Sgenre_id <= 26)
            {
                financeCount += smallGenre.Count;
            }
            else if (smallGenre.Sgenre_id >= 27 && smallGenre.Sgenre_id <= 31)
            {
                foodCount += smallGenre.Count;
            }
            else if (smallGenre.Sgenre_id >= 32 && smallGenre.Sgenre_id <= 37)
            {
                gameCount += smallGenre.Count;
            }
            else if (smallGenre.Sgenre_id >= 38 && smallGenre.Sgenre_id <= 41)
            {
                healthCount += smallGenre.Count;
            }
            else if (smallGenre.Sgenre_id >= 42 && smallGenre.Sgenre_id <= 46)
            {
                politicsCount += smallGenre.Count;
            }
            else if (smallGenre.Sgenre_id >= 47 && smallGenre.Sgenre_id <= 50)
            {
                shoppingCount += smallGenre.Count;
            }
        }

        var events = new List<(int count, string name)> {               // events 리스트에 소장르 count합과 이벤트 이름 저장
            (animalCount, "Animal"),
            (beautyCount, "Beauty"),
            (educationCount, "Education"),
            (entertainmentCount, "Entertainment"),
            (financeCount, "Finance"),
            (foodCount, "Food"),
            (gameCount, "Game"),
            (healthCount, "Health"),
            (politicsCount, "Politics"),
            (shoppingCount, "Shopping")
        };

        int condition = (week / 5) * 5; // 5, 10, 15 ... 30  이벤트 주에 5의 배수로 조건 검사
        List<string> availableEvents = new List<string>();          

        foreach (var (count, name) in events) 
        {
            if (count >= condition && !triggeredEvents.Contains(name))  // 소장르 count합이 condition에 저장된 임계값 이상이고, triggerEvents 리스트에 해당 이벤트 이름이 없으면 
            {
                availableEvents.Add(name);                              // 해당 이벤트 이름을 availableEvents 에 추가
            }
        }

        if (availableEvents.Count > 0 && week != 1)     
        {
            int index = Random.Range(0, availableEvents.Count);         // 조건을 2개이상 만족할 경우 랜덤으로 Event 선택
            string selectedEvent = availableEvents[index];
            triggeredEvents.Add(selectedEvent);
            return selectedEvent;
        }
        else
        {
            if(week == 1)
            {
                return "OT";
            } else if(week == 6)
            {
                return "MidtermExam";
            } else if(week == 10)
            {
                return "NoClass";
            } else if(week == 14)
            {
                return "FinalExam";
            } else if(week == 18)
            {
                return "rest";
            } else if(week == 22)
            {
                return "NextSemesterPlan";
            } else
            {
                return null;
            }
        }
    }
    
    //엔딩 분기점
    [YarnCommand("ending")]
    public void Ending()
    {
        int ending = 0;
        int skill_max = 0;
        int grade = 0;

        List<int> good_endings = new List<int>();
        List<int> normal_endings = new List<int>();
        List<int> bad_endings = new List<int>();

        Dictionary<int, int> genre = GameManager.instance.genre;
        Dictionary<int, int> prop = GameManager.instance.property;

        for(int i = 1; i <= genre.Count; i++)
        {
            if (genre[i] >= 70)
            {
                good_endings.Add(i);
            } else if (genre[i] >= -49 && genre[i] <= 69)
            {
                normal_endings.Add(i);
            } else
            {
                bad_endings.Add(i);
            }
        }

        try
        {
            if (good_endings.Count > 0)
            {
                for (int i = 0; i < good_endings.Count; i++)
                {
                    int num = good_endings[i];
                    switch (num)
                    {
                        case 1:
                            if (prop[6] <= 50)
                            {
                                if (genre[num] > skill_max)
                                {
                                    skill_max = genre[num];
                                    ending = num;
                                    grade = 1;
                                } else
                                {
                                    normal_endings.Add(num);
                                }
                            }
                            break;
                        case 2:
                            if (prop[5] >= 60)
                            {
                                if (genre[num] > skill_max)
                                {
                                    skill_max = genre[num];
                                    ending = num;
                                    grade = 1;
                                }
                                else
                                {
                                    normal_endings.Add(num);
                                }
                            }
                            break;
                        case 3:
                            if (prop[2] <= 30)
                            {
                                if (genre[num] > skill_max)
                                {
                                    skill_max = genre[num];
                                    ending = num;
                                    grade = 1;
                                }
                                else
                                {
                                    normal_endings.Add(num);
                                }
                            }
                            break;
                        case 4:
                            if (prop[4] >= 60)
                            {
                                if (genre[num] > skill_max)
                                {
                                    skill_max = genre[num];
                                    ending = num;
                                    grade = 1;
                                }
                                else
                                {
                                    normal_endings.Add(num);
                                }
                            }
                            break;
                        case 5:
                            if (prop[8] <= 30)
                            {
                                if (genre[num] > skill_max)
                                {
                                    skill_max = genre[num];
                                    ending = num;
                                    grade = 1;
                                }
                                else
                                {
                                    normal_endings.Add(num);
                                }
                            }
                            break;
                        case 6:
                            if (genre[num] > skill_max)
                            {
                                skill_max = genre[num];
                                ending = num;
                                grade = 1;
                            }
                            else
                            {
                                normal_endings.Add(num);
                            }
                            break;
                        case 7:
                            if (prop[3] <= 50)
                            {
                                if (genre[num] > skill_max)
                                {
                                    skill_max = genre[num];
                                    ending = num;
                                    grade = 1;
                                }
                                else
                                {
                                    normal_endings.Add(num);
                                }
                            }
                            break;
                        case 8:
                            if (prop[1] <= 70)
                            {
                                if (genre[num] > skill_max)
                                {
                                    skill_max = genre[num];
                                    ending = num;
                                    grade = 1;
                                }
                                else
                                {
                                    normal_endings.Add(num);
                                }
                            }
                            break;
                        case 9:
                            if (prop[7] <= 30)
                            {
                                if (genre[num] > skill_max)
                                {
                                    skill_max = genre[num];
                                    ending = num;
                                    grade = 1;
                                }
                                else
                                {
                                    normal_endings.Add(num);
                                }
                            }
                            break;
                        case 10:
                            if (prop[8] <= 20)
                            {
                                if (genre[num] > skill_max)
                                {
                                    skill_max = genre[num];
                                    ending = num;
                                    grade = 1;
                                }
                                else
                                {
                                    normal_endings.Add(num);
                                }
                            }
                            break;
                    }
                }
            }

            if (ending == 0 && normal_endings.Count > 0)
            {
                skill_max = 0;

                for (int i = 0; i < normal_endings.Count; i++)
                {
                    int num = normal_endings[i];

                    if (genre[num] > skill_max)
                    {
                        skill_max = genre[num];
                        ending = num;
                        grade = 2;
                    }
                }
            }

            if (ending == 0 && bad_endings.Count > 0)
            {
                skill_max = 0;

                for (int i = 0; i < bad_endings.Count; i++)
                {
                    int num = bad_endings[i];

                    if (genre[num] > skill_max)
                    {
                        skill_max = genre[num];
                        ending = num;
                        grade = 3;
                    }
                }
            }

            if (GameManager.instance.property[1] <= 0)
            {
                ending = 31;
            }
            else if (grade == 1)
            {
                ending = ending * 3 - 2;
            }
            else if (grade == 2)
            {
                ending = ending * 3 - 1;
            }
            else if(grade == 3)
            {
                ending = ending * 3;
            }
        } catch
        {
            ending = 31;
        }
        
        GameManager.instance.ending = ending;
        SceneManager.LoadScene("Ending");
    }
}
