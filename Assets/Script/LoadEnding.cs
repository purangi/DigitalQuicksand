using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using TMPro;
using System;
using System.IO;
using Mono.Data.Sqlite;

public class LoadEnding : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    private DBAccess m_DatabaseAccess;
    private Toggle toggle;

    public int num;
    public Image locked;
    public GameObject thumb;
    public Sprite play;
    public TextMeshProUGUI tmp;
    public GameObject img;

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

        toggle = GetComponent<Toggle>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
        {
            ClickedEvent();
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        ClickedEvent();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        toggle.isOn = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        toggle.isOn = false;
    }

    public void ClickedEvent()
    {
        if (thumb.activeSelf == true)
        {
            Image ending = img.GetComponent<Image>();
            ending.sprite = thumb.transform.Find("Ending").gameObject.GetComponent<Image>().sprite;
            img.SetActive(true);
            SoundManager.instance.PlaySound("click_bobit");
        }
        else
        {
            //엔딩 오픈 전
            Debug.Log("엔딩 안열림");
        }
    }
}
