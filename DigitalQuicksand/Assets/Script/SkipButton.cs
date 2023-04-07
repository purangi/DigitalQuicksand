using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using Mono.Data.Sqlite;

public class SkipButton : MonoBehaviour
{
    public Button skip;
    private string m_DatabaseFileName = "save.db";
    private DBAccess m_DatabaseAccess;
    private SqliteDataReader m_Reader;

    // Start is called before the first frame update
    void Start()
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, m_DatabaseFileName);
        Debug.Log(filePath);
        m_DatabaseAccess = new DBAccess("data source = " + filePath);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
