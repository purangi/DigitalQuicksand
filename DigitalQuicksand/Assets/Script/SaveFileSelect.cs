using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System;
using System.IO;
using Mono.Data.Sqlite;
using MyDB;

public class SaveFileSelect : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public int char_id;
    public int week;
    public int num;
    public bool is_title;
    public GameObject load_save;

    private string m_DatabaseFileName = "save.db";
    private DBAccess m_DatabaseAccess;
    

    private Toggle toggle;
    // Start is called before the first frame update
    void Start()
    {
        toggle = GetComponent<Toggle>();

        string filePath = Path.Combine(Application.streamingAssetsPath, m_DatabaseFileName);
        Debug.Log(filePath);
        m_DatabaseAccess = new DBAccess("data source = " + filePath);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Selected()
    {
        if (is_title)
        {
            if (char_id != 0 && week != 0)
            {
                try
                {
                    SqliteDataReader reader = m_DatabaseAccess.ExecuteQuery("SELECT * FROM character WHERE id = " + char_id);
                    if (reader.Read() != null)
                    {
                        string name = reader["full_name"].ToString();
                        int gender = Int32.Parse(reader["gender"].ToString());
                        int week = Int32.Parse(reader["week"].ToString());
                        int gold = Int32.Parse(reader["gold"].ToString());
                        int last_st = Int32.Parse(reader["last_st"].ToString());
                        int this_st = Int32.Parse(reader["this_st"].ToString());
                        int sum_st = Int32.Parse(reader["sum_st"].ToString());

                        Character character = new Character(char_id, name, gender, week, gold, last_st, this_st, sum_st);
                        GameManager.instance.character = character;

                        UpdateGenres();
                        UpdateProperties();

                        SceneManager.LoadScene("MainScene");
                        m_DatabaseAccess.CloseSqlConnection();
                    }
                    else
                    {
                        //저장된 파일없음
                        Debug.Log("저장된 파일이 없습니다.");
                    }
                }
                catch (Exception e)
                {
                    Debug.Log(e);
                }
            }
            else
            {
                Debug.Log("저장된 파일 없음");
            }
        }
        else
        {
            Save();
        }
        
    }

    private void UpdateGenres()
    {
        Dictionary<int, int> genre = new Dictionary<int, int>();
        List<SmallGenre> s_gen = new List<SmallGenre>();

        SqliteDataReader reader = m_DatabaseAccess.SelectWhere("char_g", new string[] { "*" }, new string[] { "char_id" }, new string[] { "=" }, new string[] { char_id.ToString() });

        while(reader.Read())
        {
            int id = Int32.Parse(reader["genre_id"].ToString());
            int skill = Int32.Parse(reader["skill"].ToString());
            genre.Add(id, skill);
        }

        SqliteDataReader s_reader = m_DatabaseAccess.SelectWhere("char_sg", new string[] { "*" }, new string[] { "char_id" }, new string[] { "=" }, new string[] { char_id.ToString() });

        while (s_reader.Read())
        {
            int id = Int32.Parse(s_reader["sgenre_id"].ToString());
            int interest = Int32.Parse(s_reader["interest"].ToString());
            int count = Int32.Parse(s_reader["count"].ToString());

            SqliteDataReader s_reader2 = m_DatabaseAccess.ExecuteQuery("SELECT genre_id FROM small_genre WHERE id = " + id);

            int genre_id = Int32.Parse(s_reader2["genre_id"].ToString());

            s_gen.Add(new SmallGenre(id, genre_id, interest, count));
        }

        GameManager.instance.genre = genre;
        GameManager.instance.small_genre = s_gen;

    }

    private void UpdateProperties()
    {
        Dictionary<int, int> property = new Dictionary<int, int>();

        SqliteDataReader reader = m_DatabaseAccess.SelectWhere("char_p", new string[] { "*" }, new string[] { "char_id" }, new string[] { "=" }, new string[] { char_id.ToString() });

        while(reader.Read())
        {
            property.Add(Int32.Parse(reader["property_id"].ToString()), Int32.Parse(reader["stat"].ToString()));
        }

        GameManager.instance.property = property;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Selected();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        toggle.isOn = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        toggle.isOn = false;
    }

    private void Save()
    {
        Character character = GameManager.instance.character;
        string[] cols = new string[] { "full_name", "gender", "week", "gold", "last_st", "this_st", "sum_st" };
        string[] values = new string[] {"'" + character.Name + "'", character.Gender.ToString(), character.Week.ToString(), character.Gold.ToString(),
                                        character.Last_st.ToString(), character.This_st.ToString(), character.Sum_st.ToString() };

        SqliteDataReader reader = m_DatabaseAccess.InsertSpecificAndReturn("character", cols, values, "RETURNING id");
        reader.Read();
        int id = Convert.ToInt32(reader["id"]);

        m_DatabaseAccess.ExecuteQuery("UPDATE save_file SET char_id = " + id + ", got_ending = 0 WHERE id = " + num);
        SaveGenre(id);
        SaveProperty(id);
        m_DatabaseAccess.CloseSqlConnection();

        load_save.GetComponent<LoadSave>().UpdateFiles();
    }


    private void SaveGenre(int char_id)
    {
        Dictionary<int, int> genre = GameManager.instance.genre;

        for (int i = 1; i <= genre.Count; i++)
        {
            m_DatabaseAccess.InsertIntoSpecific("char_g", new string[] { "char_id", "genre_id", "skill" }, new string[] { char_id.ToString(), i.ToString(), genre[i].ToString() });
        }

        List<SmallGenre> small_genre = GameManager.instance.small_genre;

        string[] cols = new string[] { "char_id", "sgenre_id", "interest", "count"};

        for (int i = 0; i < small_genre.Count; i++)
        {
            SmallGenre sg = small_genre[i];
            m_DatabaseAccess.InsertIntoSpecific("char_sg", cols, new string[] { char_id.ToString(), sg.Sgenre_id.ToString(), sg.Interest.ToString(), sg.Count.ToString()});
        }

        Debug.Log("장르 저장 성공");
    }

    private void SaveProperty(int char_id)
    {
        Dictionary<int, int> property = GameManager.instance.property;

        for (int i = 1; i <= property.Count; i++)
        {
            m_DatabaseAccess.InsertIntoSpecific("char_p", new string[] { "char_id", "property_id", "stat" }, new string[] { char_id.ToString(), i.ToString(), property[i].ToString() });
        }

        Debug.Log("특성 저장 성공");
    }
}

