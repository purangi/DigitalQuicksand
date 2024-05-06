using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using Mono.Data.Sqlite;

public class TutorialSkip : MonoBehaviour
{
    private string m_DatabaseFileName = "save.db";
    private string m_TableName = "character";
    private DBAccess m_DatabaseAccess;

    public Button skipButton;

    // Start is called before the first frame update
    void Start()
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, m_DatabaseFileName);
        Debug.Log(filePath);
        m_DatabaseAccess = new DBAccess("data source = " + filePath);

        SqliteDataReader reader = m_DatabaseAccess.ExecuteQuery("SELECT id FROM " + m_TableName + ";");

        if(!reader.HasRows)
        {
            skipButton.gameObject.SetActive(false);
        }
            
    }

    public void SkipTutorial()
    {
        SceneManager.LoadScene("CharacterCreation");
    }
}
