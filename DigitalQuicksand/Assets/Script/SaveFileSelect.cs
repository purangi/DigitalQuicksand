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

    public string m_DatabaseFileName = "save.db";
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
        try
        {
            SqliteDataReader reader = m_DatabaseAccess.ExecuteQuery("SELECT full_name FROM character WHERE id = " + char_id);
            reader.Read();

            string full_name = reader["full_name"].ToString();

            GameManager.instance.char_name = full_name;
            GameManager.instance.char_id = char_id;
            GameManager.instance.week = week;

            UpdateGenres();
            UpdateProperties();

            SceneManager.LoadScene("MainScene");
            m_DatabaseAccess.CloseSqlConnection();
        } catch(Exception e)
        {
            Debug.Log(e);
        }
    }

    private void UpdateGenres()
    {
        Dictionary<int, int> genre = new Dictionary<int, int>();
        List<SmallGenre> s_gen = new List<SmallGenre>();

        SqliteDataReader reader = m_DatabaseAccess.SelectWhere("char_g", new string[] { "*" }, new string[] { "char_id" }, new string[] { "=" }, new string[] { char_id.ToString() });

        while(reader.Read())
        {
            int name = Int32.Parse(reader["genre_id"].ToString());
            int skill = Int32.Parse(reader["skill"].ToString());
            genre.Add(name, skill);
        }

        SqliteDataReader s_reader = m_DatabaseAccess.SelectWhere("char_sg", new string[] { "*" }, new string[] { "char_id" }, new string[] { "=" }, new string[] { char_id.ToString() });

        while (s_reader.Read())
        {
            int id = Int32.Parse(s_reader["sgenre_id"].ToString());
            int interest = Int32.Parse(s_reader["interest"].ToString());
            int count = Int32.Parse(s_reader["count"].ToString());
            int length = Int32.Parse(s_reader["length"].ToString());

            s_gen.Add(new SmallGenre(id, interest, count, length));
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
        if(char_id != 0 && week != 0)
        {
            Selected();
        } else
        {
            Debug.Log("저장된 파일 없음");
            //저장된 파일 없음 안내 창 연결
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        toggle.isOn = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        toggle.isOn = false;
    }
}

