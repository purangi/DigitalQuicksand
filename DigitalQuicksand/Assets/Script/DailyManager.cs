using UnityEngine;
using UnityEngine.UI;
using System.Data;
using Mono.Data.Sqlite;
using System.IO;
using System.Collections;
using System.Collections.Generic;

public class DailyManager : MonoBehaviour
{
    public Text statusText;
    public Animator characterAnimator;

    private DBAccess m_DatabaseAccess;
    private string m_DatabaseFileName = "database.db";

    void Start()
    {

    }
    int ReadStatFromDatabase(int characterId, string statName)
    {
        int statValue = 0;

        string filePath = Path.Combine(Application.streamingAssetsPath, m_DatabaseFileName);
        DBAccess databaseAccess = new DBAccess("data source = " + filePath);

        SqliteDataReader reader = databaseAccess.ExecuteQuery("SELECT stats FROM Property WHERE char_id = " + characterId + " AND name = '" + statName + "'");

        if (reader.Read())
        {
            statValue = reader.GetInt32(reader.GetOrdinal("stats"));
        }

        databaseAccess.CloseSqlConnection();

        return statValue;
    }

    void Daily1_WakeUp(int characterId)
    {
        int healthStat = ReadStatFromDatabase(characterId, "Health");

        string status = "";
        if (healthStat >= 90)
        {
            status = "���� ǫ�ڼ� �������� ��ģ��.";
        }
        else if (healthStat >= 70)
        {
            status = "�� �ڰ� �Ͼ��.";
        }
        else if (healthStat >= 50)
        {
            status = "���� �� �ڰ�ʹ�. 5�и�...";
        }
        else if (healthStat >= 30)
        {
            status = "�ǰ��ؼ� �Ͼ�� �ȴ�.";
        }
        else if (healthStat >= 10)
        {
            status = "�� �� �߰ž�!!!!!!!!!!!!!!!";
        }
        else
        {
            status = "Zzz ... ����...";
            characterAnimator.Play("Sleeping");
            return;
        }

        characterAnimator.Play("WakeUp");

        statusText.text = status;

        Daily2_Eat(characterId);
    }
    void Daily2_Eat(int characterId)
    {
        int foodskill = ReadStatFromDatabase(characterId, "Food");

        string status = "";
        if (foodskill >= 90)
        {
            status = "���ִ� �����̴�!";
        }
        else if (foodskill >= 70)
        {
            status = "������ �����̴�.";
        }
        else if (foodskill >= 50)
        {
            status = "���� ������ �ʴ�.";
        }
        else
        {
            status = "�̰� ���� �� ����...";
            characterAnimator.Play("Eat_Disgusted");
        }

        characterAnimator.Play("Eat");

        statusText.text = status;
    }

    public IEnumerator ShowDailyRoutine(int characterId)
    {
        Daily1_WakeUp(characterId);
        yield return new WaitForSeconds(5);

        Daily2_Eat(characterId);
        yield return new WaitForSeconds(5);

    }
}