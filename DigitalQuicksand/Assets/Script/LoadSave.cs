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

public class LoadSave : MonoBehaviour
{
    private DBAccess m_DatabaseAccess;
    private List<Toggle> save_files = new List<Toggle>();
    private int clickNum = 0;

    // Start is called before the first frame update
    void Start()
    {
        UpdateFiles();

    }
    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < save_files.Count; i++) //마우스로 이동했을 시 대비
        {
            if (save_files[i].isOn)
            {
                clickNum = i;
            }
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (clickNum - 1 < 0)
            {
                clickNum = 2;
            }

            save_files[clickNum - 1].isOn = true;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (clickNum + 1 > 2)
            {
                clickNum = 0;
            }

            save_files[clickNum + 1].isOn = true;
        }
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
        {
            buttonClicked();
        }
    }

    void buttonClicked()
    {
        GameObject obj = transform.GetChild(clickNum).gameObject;
        obj.GetComponent<SaveFileSelect>().Selected();
    }

    public void UpdateFiles()
    {
        Toggle[] toggles = GetComponentsInChildren<Toggle>();

        for (int i = 0; i < toggles.Length; i++)
        {
            save_files.Add(toggles[i]);
        }

        try
        {
            string filePath = Path.Combine(Application.streamingAssetsPath, "save.db");
            Debug.Log(filePath);
            m_DatabaseAccess = new DBAccess("data source = " + filePath);

            SqliteDataReader reader = m_DatabaseAccess.ReadFullTable("save_file");

            if (reader.HasRows)
            {
                int row = 0;

                while (reader.Read())
                {
                    if (!reader.IsDBNull(1))
                    {
                        int char_id = Int32.Parse(reader["char_id"].ToString());

                        SqliteDataReader get_char = m_DatabaseAccess.ExecuteQuery("SELECT * FROM character WHERE id = " + char_id);
                        get_char.Read();
                        int week = Int32.Parse(get_char["week"].ToString());
                        string name = get_char["full_name"].ToString();

                        GameObject go = transform.GetChild(row).gameObject;
                        go.GetComponentsInChildren<TextMeshProUGUI>()[0].text = name;
                        go.GetComponent<SaveFileSelect>().char_id = char_id;
                        go.GetComponentsInChildren<TextMeshProUGUI>()[1].text = week.ToString() + "주차";
                        go.GetComponent<SaveFileSelect>().week = week;
                    }
                    row++;
                }
            }

            m_DatabaseAccess.CloseSqlConnection();
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
    }
}
