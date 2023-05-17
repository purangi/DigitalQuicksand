using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System;
using System.IO;
using Mono.Data.Sqlite;
using MyDB;

public class CharacterCreateButton : MonoBehaviour
{
    public TMP_InputField NameInput;
    public Toggle Gender;
    public GameObject Preference;
    public GameObject Property;
    public GameObject Popup;
    public string scene_name;

    private List<Toggle> prefers = new List<Toggle>();
    private List<Toggle> properties = new List<Toggle>();

    private DBAccess m_DatabaseAccess;

    private ShowPopUp popup_script;

    // Start is called before the first frame update
    void Start()
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, "save.db");
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
            SceneManager.LoadScene(scene_name);
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
        Dictionary<int, int> genre = new Dictionary<int, int>();

        for (int i = 0; i < 10; i++)
        {
            genre.Add(i + 1, 0);
        }

        GameManager.instance.genre = genre;

        List<SmallGenre> s_gen = new List<SmallGenre>();

        SqliteDataReader reader = m_DatabaseAccess.ExecuteQuery("SELECT id, genre_id FROM small_genre");
        
        while(reader.Read())
        {
            int id = Int32.Parse(reader["id"].ToString());
            int genre_id = Int32.Parse(reader["genre_id"].ToString());
            
            int interest = 0;
            if (prefers[genre_id -1].isOn)
            {
                interest = 10;
            }

            SmallGenre item = new SmallGenre(id, interest, 0, 0);
            s_gen.Add(item);
        }

        GameManager.instance.small_genre = s_gen;
    }

    private void CreateProperties()
    {
        Dictionary<int, int> dic = new Dictionary<int, int>();

        /*
        List<string> property_name = new List<string>()
        {
            "건강", "중독", "스트레스", "재미", "자존감", "폭력성", "인지평향", "허영심"
        }; */

        for (int i = 0; i < properties.Count; i++)
        {
            int stat = 50;

            if (properties[i].interactable)
            {
                if(!properties[i].isOn)
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

            dic.Add(i + 1, stat);
        }

        GameManager.instance.property = dic;
    }
}
