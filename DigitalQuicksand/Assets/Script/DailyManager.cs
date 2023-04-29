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
            status = "잠을 푹자서 에너지가 넘친다.";
        }
        else if (healthStat >= 70)
        {
            status = "잘 자고 일어났다.";
        }
        else if (healthStat >= 50)
        {
            status = "조금 더 자고싶다. 5분만...";
        }
        else if (healthStat >= 30)
        {
            status = "피곤해서 일어나기 싫다.";
        }
        else if (healthStat >= 10)
        {
            status = "아 더 잘거야!!!!!!!!!!!!!!!";
        }
        else
        {
            status = "Zzz ... 쿨쿨...";
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
            status = "맛있는 음식이다!";
        }
        else if (foodskill >= 70)
        {
            status = "괜찮은 음식이다.";
        }
        else if (foodskill >= 50)
        {
            status = "별로 맛있지 않다.";
        }
        else
        {
            status = "이건 먹을 수 없다...";
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