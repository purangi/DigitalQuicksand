using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using MyDB;

public class WeekManager : MonoBehaviour
{
    public TextMeshProUGUI monthText;
    public TextMeshProUGUI weekText;
    private int week = 1;
    private Character character;

    void Update()
    {
        if(GameManager.instance != null)
        {
            character = GameManager.instance.character;
            week = character.Week;
        }

        UpdateWeekText();
    }

    void UpdateWeekText()
    {
        int month = 0;
        int weekInMonth = 0;

        if (week >= 1 && week <= 4 || week == 20)
        {
            month = 3;
            weekInMonth = week % 4 == 0 ? 4 : week % 4;
        }
        else if (week >= 5 && week <= 8)
        {
            month = 4;
            weekInMonth = week - 4;
        }
        else if (week >= 9 && week <= 12)
        {
            month = 5;
            weekInMonth = week - 8;
        }
        else if (week >= 13 && week <= 16)
        {
            month = 6;
            weekInMonth = week - 12;
        }
        else if (week >= 17 && week <= 20)
        {
            month = 7;
            weekInMonth = week - 16;
        }
        else if (week >= 21 && week <= 24)
        {
            month = 8;
            weekInMonth = week - 20;
        }

        monthText.text = month.ToString() + "¿ù";
        weekText.text = weekInMonth.ToString() + "ÁÖÂ÷";
    }

    /*public void AddWeek()
    {
       if (week < 24) 
        {
            week++;
            UpdateWeekText(week);
        }
    }
    */
}
