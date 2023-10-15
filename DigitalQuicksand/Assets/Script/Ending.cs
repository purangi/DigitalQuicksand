using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;
using Mono.Data.Sqlite;
using UnityEngine.SceneManagement;

public class Ending : MonoBehaviour
{
    public Image ending_image;
    public Sprite[] images;
    public TextMeshProUGUI ending_text;
    public TextMeshProUGUI gold;
    public TextMeshProUGUI[] prop;

    private DBAccess m_DatabaseAccess;

    // Start is called before the first frame update
    void Start()
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, "save.db");
        m_DatabaseAccess = new DBAccess("data source = " + filePath);

        ShowEnding();
        m_DatabaseAccess.CloseSqlConnection();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ShowEnding()
    {
        int id = GameManager.instance.ending;
        ending_image.sprite = images[id - 1];

        SqliteDataReader reader = m_DatabaseAccess.ExecuteQuery("SELECT summary FROM endings WHERE id = " + id);
        reader.Read();

        ending_text.text = "#" + reader["summary"].ToString();

        gold.text = "¼öÀÍ : " + GameManager.instance.character.Gold;

        for(int i = 1; i <= GameManager.instance.property.Count; i++)
        {
            prop[i - 1].text = GameManager.instance.property[i] + "";
        }

        UnlockEnding(id);
    }

    private void UnlockEnding(int id)
    {
        SqliteDataReader reader = m_DatabaseAccess.ExecuteQuery("UPDATE endings SET is_unlocked = 1 WHERE id = " + id);
    }

    public void BackToTitle()
    {
        SceneManager.LoadScene("TitleScene");
    }
}
