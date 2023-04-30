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
            "����", "��Ƽ", "����", "����", "����", "����", "����", "�ǰ�", "��ġ", "����"
        };

        for (int i = 0; i < genre_name.Count; i++)
        {
            dic.Add(genre_name[i], 0);
        }

        GameManager.instance.genre = dic;

        List<List<string>> sgenre_name = new List<List<string>>()
        {
            new List<string> { "���� �Ծ�", "���� ������", "���� �Ƿ�", "���� ��ȣ Ȱ��", "�߸��� ���� �Ծ�", "�߸��� ���� ������", "���� �д�" },
            new List<string> {"ȭ��ǰ ���� �м�", "����ũ�� ��", "�Ǻ� ������", "�߸��� �Ǻ� ������", "���к��� ȭ��ǰ ȫ��", "���� ����" },
            new List<string> {"�н� ����", "���� ����", "����", "�߸��� ����" },
            new List<string> {"��� �� ��ȭ", "����", "����", "���� ����", "������ �̵��" },
            new List<string> {"���� ���", "�� ����", "���� ����", "�ҹ��� ���� ���", "������ ���� ����", "���к��� ��� ȫ��" },
            new List<string> {"���� ����", "�丮��", "���� ����", "���� ����", "���к��� ���� ����" },
            new List<string> {"���� �Ұ�", "���� ��", "���� �÷��� ����", "�ҹ� ���� ����", "������ ����" },
            new List<string> {"� ���", "�ǰ� ����", "������ ���̾�Ʈ", "�߸��� ���" },
            new List<string> {"��ġ �̽� �м�", "��ġ ����", "��ġ ���� ����", "����� ��ġ ���", "��¥ ����" },
            new List<string> {"��ǰ ����", "���� ����", "���� ���̵�", "���к��� ��ǰ ȫ��", "���Һ� ����" }
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
                int interest = 0; //���帣 ���п� ���� ��̵� ����
                if (prefers[i].isOn)
                {
                    interest = 10;
                }

                for(int j = 0; j < sgenre_name[i].Count; j++) //j�� ���帣 ���� Ű
                {
                    s_gen.Add(new Hashtable());
                    string sgen_name = sgenre_name[i][j];
                    int sgen_pn = pos_neg[i][j];

                    int num = s_gen.Count - 1;
                    s_gen[num].Add("name", sgen_name);
                    s_gen[num].Add("pos_neg", sgen_pn);
                    s_gen[num].Add("genre_id", i); //���� �ÿ� genre_id�� ���� �˻� �� �Է� �ʿ� 
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
            "�ǰ�", "�ߵ�", "��Ʈ����", "���", "������", "���¼�", "��������", "�㿵��"
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
