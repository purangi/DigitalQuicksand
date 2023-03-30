using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DBwrite : MonoBehaviour
{
    public string m_DatabaseFileName = "save.db";
    public string m_TableName = "character";
    private DBAccess m_DatabaseAccess;

    void Start()
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, m_DatabaseFileName);
        Debug.Log(filePath);
        m_DatabaseAccess = new DBAccess("data source = " + filePath);

    }

    public void MakeCharacter()
    {
        m_DatabaseAccess.InsertIntoSpecific(m_TableName, new string[] { "full_name" }, new string[] { "'test'" });

        m_DatabaseAccess.CloseSqlConnection();
    }
}
