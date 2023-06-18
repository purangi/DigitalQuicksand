using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System;
using System.IO;
using Mono.Data.Sqlite;

public class LoadEnding : MonoBehaviour
{
    private DBAccess m_DatabaseAccess;

    public int num;
    public Image locked;
    public GameObject thumb;
    public Sprite play;
    public TextMeshProUGUI tmp;

    // Start is called before the first frame update
    void Start()
    {
        try
        {
            string filePath = Path.Combine(Application.streamingAssetsPath, "save.db");
            Debug.Log(filePath);
            m_DatabaseAccess = new DBAccess("data source = " + filePath);

            SqliteDataReader reader = m_DatabaseAccess.ExecuteQuery("SELECT * FROM endings where id = " + num);

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    int isUnlocked = Int32.Parse(reader["is_unlocked"].ToString());

                    if(isUnlocked == 1)
                    {
                        if(num < 10)
                        {
                            tmp.text = "0" + num.ToString();
                        } else
                        {
                            tmp.text = num.ToString();
                        }

                        locked.sprite = play;
                        thumb.SetActive(true);
                    } else
                    {

                    }
                }
            }

            m_DatabaseAccess.CloseSqlConnection();
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
