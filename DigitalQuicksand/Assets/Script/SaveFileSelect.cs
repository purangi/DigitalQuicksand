using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System;
using System.IO;
using Mono.Data.Sqlite;

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
        Dictionary<string, int> genre = new Dictionary<string, int>();
        List<Hashtable> small_genre = new List<Hashtable>();

        SqliteDataReader reader = m_DatabaseAccess.SelectWhere("genre", new string[] { "*" }, new string[] { "char_id" }, new string[] { "=" }, new string[] { char_id.ToString() });

        while(reader.Read())
        {
            string name = reader["name"].ToString();
            int skill = Int32.Parse(reader["skill"].ToString());
            genre.Add(name, skill);

            SqliteDataReader s_reader = m_DatabaseAccess.SelectWhere("small_genre", new string[] { "*" }, new string[] { "genre_id" }, new string[] { "=" }, new string[] { reader["id"].ToString() });

            while (s_reader.Read())
            {
                small_genre.Add(new Hashtable());
                int i = small_genre.Count - 1;

                small_genre[i].Add("name", s_reader["name"].ToString());
                small_genre[i].Add("pos_neg", Int32.Parse(s_reader["pos_neg"].ToString()));
                small_genre[i].Add("genre_id", Int32.Parse(s_reader["genre_id"].ToString())); //저장 시에 genre_id는 별도 검색 후 입력 필요 
                small_genre[i].Add("interest", Int32.Parse(s_reader["interest"].ToString()));
                small_genre[i].Add("count", Int32.Parse(s_reader["count"].ToString()));
                small_genre[i].Add("length", Int32.Parse(s_reader["length"].ToString()));
            }
        }

        GameManager.instance.genre = genre;
        GameManager.instance.small_genre = small_genre;
    }

    private void UpdateProperties()
    {
        Dictionary<string, int> property = new Dictionary<string, int>();

        SqliteDataReader reader = m_DatabaseAccess.SelectWhere("property", new string[] { "*" }, new string[] { "char_id" }, new string[] { "=" }, new string[] { char_id.ToString() });

        while(reader.Read())
        {
            property.Add(reader["name"].ToString(), Int32.Parse(reader["stat"].ToString()));
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

