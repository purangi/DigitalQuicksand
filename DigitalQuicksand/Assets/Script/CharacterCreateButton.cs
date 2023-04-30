using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System;
using System.IO;

public class CharacterCreateButton : MonoBehaviour
{
    public TMP_InputField NameInput;
    public Toggle Gender;
    public GameObject Preference;
    public GameObject Property;
    public GameObject Popup;

    private List<Toggle> prefers = new List<Toggle>();
    private List<Toggle> properties = new List<Toggle>();

    private ShowPopUp popup_script;

    // Start is called before the first frame update
    void Start()
    {
        popup_script = Popup.GetComponent<ShowPopUp>();

        Toggle[] preferToggles = Preference.GetComponentsInChildren<Toggle>();
        Toggle[] propertyToggles = Property.GetComponentsInChildren<Toggle>();

        for (int i = 0; i < preferToggles.Length; i++)
        {
            prefers.Add(preferToggles[i]);
        }
        for (int i = 0; i < propertyToggles.Length; i++)
        {
            properties.Add(propertyToggles[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NextButton()
    {
        if(!CheckName())
        {
            popup_script.Showup("이름을 입력해 주세요.");
        } else if(!CheckPreference())
        {
            popup_script.Showup("선호 콘텐츠를 3개 선택해 주세요.");
        } else if(!CheckProperty())
        {
            popup_script.Showup("활성화된 특성이 없습니다.");
        } else if(!Property.GetComponent<ScoreSum>().isScoreOK)
        {
            popup_script.Showup("특성 수치의 합이 범위 밖입니다. <br>-10에서 +10 사이로 설정해 주세요.");
        } else
        {
            CharacterCreate();
            SceneManager.LoadScene("MainBasic");
        }
    }
    private bool CheckName()
    {
        if (NameInput.text != "")
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool CheckPreference()
    {
        int numTogglesOn = 0;
        for (int i = 0; i < prefers.Count; i++)
        {
            if (prefers[i].isOn)
            {
                numTogglesOn++;
            }
        }

        if (numTogglesOn >= 3)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool CheckProperty()
    {
        int numTogglesOn = 0;
        for (int i = 0; i < properties.Count; i++)
        {
            if (properties[i].interactable)
            {
                numTogglesOn++;
            }
        }

        if (numTogglesOn > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void CharacterCreate()
    {
        try
        {
            string name = NameInput.text;

            int gender = 0;

            if (!Gender.isOn)
            {
                gender = 1;
            }

            GameManager.instance.char_name = NameInput.text;
            GameManager.instance.gender = gender;

            CreateGenres();
            CreateProperties();
        } catch(Exception e)
        {
            Debug.Log(e);
        }
    }

    private void CreateGenres()
    {
        Dictionary<string, int> dic = new Dictionary<string, int>();
        List<string> genre_name = new List<string>()
        {
            "동물", "뷰티", "교육", "연예", "금융", "음식", "게임", "건강", "정치", "쇼핑"
        };

        for (int i = 0; i < genre_name.Count; i++)
        {
            dic.Add(genre_name[i], 0);
        }

        GameManager.instance.genre = dic;

        List<List<string>> sgenre_name = new List<List<string>>()
        {
            new List<string> { "동물 입양", "동물 관리법", "동물 의료", "동물 보호 활동", "잘못된 동물 입양", "잘못된 동물 관리법", "동물 학대" },
            new List<string> {"화장품 성분 분석", "메이크업 팁", "피부 관리법", "잘못된 피부 관리법", "무분별한 화장품 홍보", "성형 조장" },
            new List<string> {"학습 습관", "교육 정보", "강의", "잘못된 강의" },
            new List<string> {"드라마 및 영화", "예능", "음악", "연예 찌라시", "폭력적 미디어" },
            new List<string> {"금융 상식", "돈 관리", "투자 전략", "불법적 투자 방법", "위험한 금융 정보", "무분별한 기업 홍보" },
            new List<string> {"영양 정보", "요리법", "음식 리뷰", "과식 유도", "무분별한 광고 리뷰" },
            new List<string> {"게임 소개", "게임 팁", "게임 플레이 감상", "불법 도박 게임", "폭력적 게임" },
            new List<string> {"운동 방법", "건강 정보", "위험한 다이어트", "잘못된 운동법" },
            new List<string> {"정치 이슈 분석", "정치 뉴스", "정치 관련 강의", "편향된 정치 사고", "가짜 뉴스" },
            new List<string> {"제품 리뷰", "할인 정보", "구매 가이드", "무분별한 제품 홍보", "과소비 유도" }
        };

        List<List<int>> pos_neg = new List<List<int>>()
        {
            new List<int> { 0, 0, 0, 0, 1, 1, 1 },
            new List<int> { 0, 0, 0, 1, 1, 1 },
            new List<int> { 0, 0, 0, 1 },
            new List<int> { 0, 0, 0, 1, 1 },
            new List<int> { 0, 0, 0, 1, 1, 1 },
            new List<int> { 0, 0, 0, 1, 1 },
            new List<int> { 0, 0, 0, 1, 1 },
            new List<int> { 0, 0, 1, 1 },
            new List<int> { 0, 0, 0, 1, 1 },
            new List<int> { 0, 0, 0, 1, 1 }
        };

        List<Hashtable> s_gen = new List<Hashtable>();

        if (prefers.Count == sgenre_name.Count)
        {
            for(int i = 0; i < prefers.Count; i++)
            {
                int interest = 0; //대장르 구분에 따라 흥미도 설정
                if (prefers[i].isOn)
                {
                    interest = 10;
                }

                for(int j = 0; j < sgenre_name[i].Count; j++) //j는 대장르 구분 키
                {
                    s_gen.Add(new Hashtable());
                    string sgen_name = sgenre_name[i][j];
                    int sgen_pn = pos_neg[i][j];

                    int num = s_gen.Count - 1;
                    s_gen[num].Add("name", sgen_name);
                    s_gen[num].Add("pos_neg", sgen_pn);
                    s_gen[num].Add("genre_id", i); //저장 시에 genre_id는 별도 검색 후 입력 필요 
                    s_gen[num].Add("interest", interest);
                    s_gen[num].Add("count", 0);
                    s_gen[num].Add("length", 0);
                }
            }
        }

        GameManager.instance.small_genre = s_gen;
    }

    private void CreateProperties()
    {
        Dictionary<string, int> dic = new Dictionary<string, int>();

        List<string> property_name = new List<string>()
        {
            "건강", "중독", "스트레스", "재미", "자존감", "폭력성", "인지평향", "허영심"
        };

        for (int i = 0; i < properties.Count; i++)
        {
            int stat = 50;

            if (properties[i].interactable)
            {
                if (!properties[i].isOn)
                {
                    if (i == 0 || i == 3 || i == 4)
                    {
                        stat = 80;
                    }
                    else
                    {
                        stat = 20;
                    }
                }
                else
                {
                    if (i == 0 || i == 3 || i == 4)
                    {
                        stat = 40;
                    }
                    else
                    {
                        stat = 60;
                    }
                }
            }

            dic.Add(property_name[i], stat);
        }

        GameManager.instance.property = dic;
    }
}
