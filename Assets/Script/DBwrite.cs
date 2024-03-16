using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

//DB에 작성하는 함수 예시
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
        //캐릭터 생성 test
        m_DatabaseAccess.InsertIntoSpecific( m_TableName, new string[] { "full_name" }, new string[] { "'test'" });
    }

    public void CloseConnection()
    {
        //db 연결 해제 나중에 필요 시 넣기
        m_DatabaseAccess.CloseSqlConnection();
    }
}
