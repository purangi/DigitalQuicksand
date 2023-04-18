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
            popup_script.Showup("�̸��� �Է��� �ּ���.");
        } else if(!CheckPreference())
        {
            popup_script.Showup("��ȣ �������� 3�� ������ �ּ���.");
        } else if(!CheckProperty())
        {
            popup_script.Showup("Ȱ��ȭ�� Ư���� �����ϴ�.");
        } else if(!Property.GetComponent<ScoreSum>().isScoreOK)
        {
            popup_script.Showup("Ư�� ��ġ�� ���� ���� ���Դϴ�. <br>-10���� +10 ���̷� ������ �ּ���.");
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
            Debug.Log("ĳ���� ���� ����");
        }
    }
    private void CreateGenres()
    {
        int id = GameManager.instance.char_id;
        //genre insert
        List<string> genre_name = new List<string>()
        {
            "'����'", "'��Ƽ'", "'����'", "'����'", "'����'", "'����'", "'����'", "'�ǰ�'", "'��ġ'", "'����'"
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
                case 0: //����
                    s_genre.Add(new List<string> { "'���� �Ծ�'", "0", genre_id });
                    s_genre.Add(new List<string> { "'���� ������'", "0", genre_id });
                    s_genre.Add(new List<string> { "'���� �Ƿ�'", "0", genre_id });
                    s_genre.Add(new List<string> { "'���� ��ȣ Ȱ��'", "0", genre_id });
                    s_genre.Add(new List<string> { "'�߸��� ���� �Ծ�'", "1", genre_id });
                    s_genre.Add(new List<string> { "'�߸��� ���� ������'", "1", genre_id });
                    s_genre.Add(new List<string> { "'���� �д�'", "1", genre_id });
                    break;
                case 1: //��Ƽ
                    s_genre.Add(new List<string> { "'ȭ��ǰ ���� �м�'", "0", genre_id });
                    s_genre.Add(new List<string> { "'����ũ�� ��'", "0", genre_id });
                    s_genre.Add(new List<string> { "'�Ǻ� ������'", "0", genre_id });
                    s_genre.Add(new List<string> { "'�߸��� �Ǻ� ������'", "1", genre_id });
                    s_genre.Add(new List<string> { "'���к��� ȭ��ǰ ȫ��'", "1", genre_id });
                    s_genre.Add(new List<string> { "'���� ����'", "1", genre_id });
                    break;
                case 2: //����
                    s_genre.Add(new List<string> { "'�н� ����'", "0", genre_id });
                    s_genre.Add(new List<string> { "'���� ����'", "0", genre_id });
                    s_genre.Add(new List<string> { "'����'", "0", genre_id });
                    s_genre.Add(new List<string> { "'�߸��� ����'", "1", genre_id });
                    break;
                case 3: //����
                    s_genre.Add(new List<string> { "'��� �� ��ȭ'", "0", genre_id });
                    s_genre.Add(new List<string> { "'����'", "0", genre_id });
                    s_genre.Add(new List<string> { "'����'", "0", genre_id });
                    s_genre.Add(new List<string> { "'���� ����'", "1", genre_id });
                    s_genre.Add(new List<string> { "'������ �̵��'", "1", genre_id });
                    break;
                case 4: //����
                    s_genre.Add(new List<string> { "'���� ���'", "0", genre_id });
                    s_genre.Add(new List<string> { "'�� ����'", "0", genre_id });
                    s_genre.Add(new List<string> { "'���� ����'", "0", genre_id });
                    s_genre.Add(new List<string> { "'�ҹ��� ���� ���'", "1", genre_id });
                    s_genre.Add(new List<string> { "'������ ���� ����'", "1", genre_id });
                    s_genre.Add(new List<string> { "'���к��� ��� ȫ��'", "1", genre_id });
                    break;
                case 5: //����
                    s_genre.Add(new List<string> { "'���� ����'", "0", genre_id });
                    s_genre.Add(new List<string> { "'�丮��'", "0", genre_id });
                    s_genre.Add(new List<string> { "'���� ����'", "0", genre_id });
                    s_genre.Add(new List<string> { "'���� ����'", "1", genre_id });
                    s_genre.Add(new List<string> { "'���к��� ���� ����'", "1", genre_id });
                    break;
                case 6: //����
                    s_genre.Add(new List<string> { "'���� �Ұ�'", "0", genre_id });
                    s_genre.Add(new List<string> { "'���� ��'", "0", genre_id });
                    s_genre.Add(new List<string> { "'���� �÷��� ����'", "0", genre_id });
                    s_genre.Add(new List<string> { "'�ҹ� ���� ����'", "1", genre_id });
                    s_genre.Add(new List<string> { "'������ ����'", "1", genre_id });
                    break;
                case 7: //�ǰ�
                    s_genre.Add(new List<string> { "'� ���'", "0", genre_id });
                    s_genre.Add(new List<string> { "'�ǰ� ����'", "0", genre_id });
                    s_genre.Add(new List<string> { "'������ ���̾�Ʈ'", "1", genre_id });
                    s_genre.Add(new List<string> { "'�߸��� ���'", "1", genre_id });
                    break;
                case 8: //��ġ
                    s_genre.Add(new List<string> { "'��ġ �̽� �м�'", "0", genre_id });
                    s_genre.Add(new List<string> { "'��ġ ����'", "0", genre_id });
                    s_genre.Add(new List<string> { "'��ġ ���� ����'", "0", genre_id });
                    s_genre.Add(new List<string> { "'����� ��ġ ���'", "1", genre_id });
                    s_genre.Add(new List<string> { "'��¥ ����'", "1", genre_id });
                    break;
                case 9: //����
                    s_genre.Add(new List<string> { "'��ǰ ����'", "0", genre_id });
                    s_genre.Add(new List<string> { "'���� ����'", "0", genre_id });
                    s_genre.Add(new List<string> { "'���� ���̵�'", "0", genre_id });
                    s_genre.Add(new List<string> { "'���к��� ��ǰ ȫ��'", "1", genre_id });
                    s_genre.Add(new List<string> { "'���Һ� ����'", "1", genre_id });
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
            "'�ǰ�'", "'�ߵ�'", "'��Ʈ����'", "'���'", "'������'", "'���¼�'", "'��������'", "'�㿵��'"
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
            "'�ǰ�'", "'�ߵ�'", "'��Ʈ����'", "'���'", "'������'", "'���¼�'", "'��������'", "'�㿵��'"
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
