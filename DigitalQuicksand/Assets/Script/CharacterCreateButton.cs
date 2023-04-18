using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System;
using System.IO;
using Mono.Data.Sqlite;

public class CharacterCreateButton : MonoBehaviour
{
    public TMP_InputField NameInput;
    public Toggle Gender;
    public GameObject Preference;
    public GameObject Property;
    public GameObject Popup;

    private List<Toggle> prefers = new List<Toggle>();
    private List<Toggle> properties = new List<Toggle>();

    public string m_DatabaseFileName = "save.db";

    private DBAccess m_DatabaseAccess;
    private ShowPopUp popup_script;

    // Start is called before the first frame update
    void Start()
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, m_DatabaseFileName);
        Debug.Log(filePath);
        m_DatabaseAccess = new DBAccess("data source = " + filePath);
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
            SceneManager.LoadScene("MainScene");
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
            string name = "'" + NameInput.text + "'";

            int gender = 0;

            if (!Gender.isOn)
            {
                gender = 1;
            }

            m_DatabaseAccess.InsertIntoSpecific("character", new string[] { "full_name", "gender" }, new string[] { name, gender.ToString() });

            SqliteDataReader reader = m_DatabaseAccess.ExecuteQuery("SELECT id FROM character WHERE full_name = " + name + " ORDER BY ROWID DESC LIMIT 1");
            reader.Read();

            string id_s = reader["id"].ToString();

            GameManager.instance.char_id = Int32.Parse(id_s);

            CreateGenres();
            CreateProperties();
        } catch
        {
            Debug.Log("캐릭터 생성 오류");
        }
    }
    private void CreateGenres()
    {
        int id = GameManager.instance.char_id;
        //genre insert
        List<string> genre_name = new List<string>()
        {
            "'동물'", "'뷰티'", "'교육'", "'연예'", "'금융'", "'음식'", "'게임'", "'건강'", "'정치'", "'쇼핑'"
        };

        for(int i = 0; i < genre_name.Count; i++)
        {
            m_DatabaseAccess.InsertIntoSpecific("genre", new string[] { "name", "char_id" }, new string[] { genre_name[i], id.ToString() });
        }

        //small_genre insert;
        SqliteDataReader reader = m_DatabaseAccess.ExecuteQuery("SELECT id FROM genre WHERE char_id = '" + id.ToString() + "'");

        List<List<string>> s_genre = new List<List<string>>();
        int row = 0;

        while(reader.Read())
        {
            string genre_id = reader["id"].ToString();
            
            switch(row)
            {
                case 0: //동물
                    s_genre.Add(new List<string> { "'동물 입양'", "0", genre_id });
                    s_genre.Add(new List<string> { "'동물 관리법'", "0", genre_id });
                    s_genre.Add(new List<string> { "'동물 의료'", "0", genre_id });
                    s_genre.Add(new List<string> { "'동물 보호 활동'", "0", genre_id });
                    s_genre.Add(new List<string> { "'잘못된 동물 입양'", "1", genre_id });
                    s_genre.Add(new List<string> { "'잘못된 동물 관리법'", "1", genre_id });
                    s_genre.Add(new List<string> { "'동물 학대'", "1", genre_id });
                    break;
                case 1: //뷰티
                    s_genre.Add(new List<string> { "'화장품 성분 분석'", "0", genre_id });
                    s_genre.Add(new List<string> { "'메이크업 팁'", "0", genre_id });
                    s_genre.Add(new List<string> { "'피부 관리법'", "0", genre_id });
                    s_genre.Add(new List<string> { "'잘못된 피부 관리법'", "1", genre_id });
                    s_genre.Add(new List<string> { "'무분별한 화장품 홍보'", "1", genre_id });
                    s_genre.Add(new List<string> { "'성형 조장'", "1", genre_id });
                    break;
                case 2: //교육
                    s_genre.Add(new List<string> { "'학습 습관'", "0", genre_id });
                    s_genre.Add(new List<string> { "'교육 정보'", "0", genre_id });
                    s_genre.Add(new List<string> { "'강의'", "0", genre_id });
                    s_genre.Add(new List<string> { "'잘못된 강의'", "1", genre_id });
                    break;
                case 3: //연예
                    s_genre.Add(new List<string> { "'드라마 및 영화'", "0", genre_id });
                    s_genre.Add(new List<string> { "'예능'", "0", genre_id });
                    s_genre.Add(new List<string> { "'음악'", "0", genre_id });
                    s_genre.Add(new List<string> { "'연예 찌라시'", "1", genre_id });
                    s_genre.Add(new List<string> { "'폭력적 미디어'", "1", genre_id });
                    break;
                case 4: //금융
                    s_genre.Add(new List<string> { "'금융 상식'", "0", genre_id });
                    s_genre.Add(new List<string> { "'돈 관리'", "0", genre_id });
                    s_genre.Add(new List<string> { "'투자 전략'", "0", genre_id });
                    s_genre.Add(new List<string> { "'불법적 투자 방법'", "1", genre_id });
                    s_genre.Add(new List<string> { "'위험한 금융 정보'", "1", genre_id });
                    s_genre.Add(new List<string> { "'무분별한 기업 홍보'", "1", genre_id });
                    break;
                case 5: //음식
                    s_genre.Add(new List<string> { "'영양 정보'", "0", genre_id });
                    s_genre.Add(new List<string> { "'요리법'", "0", genre_id });
                    s_genre.Add(new List<string> { "'음식 리뷰'", "0", genre_id });
                    s_genre.Add(new List<string> { "'과식 유도'", "1", genre_id });
                    s_genre.Add(new List<string> { "'무분별한 광고 리뷰'", "1", genre_id });
                    break;
                case 6: //게임
                    s_genre.Add(new List<string> { "'게임 소개'", "0", genre_id });
                    s_genre.Add(new List<string> { "'게임 팁'", "0", genre_id });
                    s_genre.Add(new List<string> { "'게임 플레이 감상'", "0", genre_id });
                    s_genre.Add(new List<string> { "'불법 도박 게임'", "1", genre_id });
                    s_genre.Add(new List<string> { "'폭력적 게임'", "1", genre_id });
                    break;
                case 7: //건강
                    s_genre.Add(new List<string> { "'운동 방법'", "0", genre_id });
                    s_genre.Add(new List<string> { "'건강 정보'", "0", genre_id });
                    s_genre.Add(new List<string> { "'위험한 다이어트'", "1", genre_id });
                    s_genre.Add(new List<string> { "'잘못된 운동법'", "1", genre_id });
                    break;
                case 8: //정치
                    s_genre.Add(new List<string> { "'정치 이슈 분석'", "0", genre_id });
                    s_genre.Add(new List<string> { "'정치 뉴스'", "0", genre_id });
                    s_genre.Add(new List<string> { "'정치 관련 강의'", "0", genre_id });
                    s_genre.Add(new List<string> { "'편향된 정치 사고'", "1", genre_id });
                    s_genre.Add(new List<string> { "'가짜 뉴스'", "1", genre_id });
                    break;
                case 9: //쇼핑
                    s_genre.Add(new List<string> { "'제품 리뷰'", "0", genre_id });
                    s_genre.Add(new List<string> { "'할인 정보'", "0", genre_id });
                    s_genre.Add(new List<string> { "'구매 가이드'", "0", genre_id });
                    s_genre.Add(new List<string> { "'무분별한 제품 홍보'", "1", genre_id });
                    s_genre.Add(new List<string> { "'과소비 유도'", "1", genre_id });
                    break;
            }
            row++;
        }

        for (int i = 0; i < s_genre.Count; i++)
        {
            List<string> list = s_genre[i];
            
            m_DatabaseAccess.InsertIntoSpecific("small_genre", new string[] { "name", "pos_neg", "genre_id" }, list.ToArray());
        }

        UpdateGenreInterest();
    }

    private void CreateProperties()
    {
        int id = GameManager.instance.char_id;

        List<string> property_name = new List<string>()
        {
            "'건강'", "'중독'", "'스트레스'", "'재미'", "'자존감'", "'폭력성'", "'인지평향'", "'허영심'"
        };

        for (int i = 0; i < property_name.Count; i++)
        {
            m_DatabaseAccess.InsertIntoSpecific("property", new string[] { "name", "char_id" }, new string[] { property_name[i], id.ToString() });
        }

        UpdatePropertyStat();
    }

    private void UpdateGenreInterest()
    {
        int id = GameManager.instance.char_id;

        SqliteDataReader reader = m_DatabaseAccess.ExecuteQuery("SELECT id FROM genre WHERE char_id = '" + id.ToString() + "'");

        int row = 0;
        while (reader.Read())
        {
            string genre_id = reader["id"].ToString();

            int interest = 0;

            if (prefers[row].isOn)
            {
                interest = 10;
            }

            m_DatabaseAccess.UpdateInto("small_genre", new string[] { "interest" }, new string[] { interest.ToString() }, "genre_id", genre_id);

            row++;
        }
    }

    private void UpdatePropertyStat()
    {
        int id = GameManager.instance.char_id;

        List<string> property_name = new List<string>()
        {
            "'건강'", "'중독'", "'스트레스'", "'재미'", "'자존감'", "'폭력성'", "'인지평향'", "'허영심'"
        };

        for (int i = 0; i < properties.Count; i++)
        {
            int stat = 50;

            if (properties[i].interactable)
            {
                if (!properties[i].isOn)
                {
                    if(i == 0 || i == 3 || i == 4)
                    {
                        stat = 80;
                    } else
                    {
                        stat = 20;
                    }
                } else
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

            m_DatabaseAccess.UpdateWhere("property", new string[] { "stat" }, new string[] { stat.ToString() }, new string[] { "char_id", "name" }, new string[] { id.ToString(), property_name[i] });
        }
    }

}
