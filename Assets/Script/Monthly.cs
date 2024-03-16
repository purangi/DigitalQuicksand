using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using MyDB;

public class Monthly : MonoBehaviour
{
    public TextMeshProUGUI[] number; // month, name, age, gender. most. count. money, hour
    public TextMeshProUGUI[] genre;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowResult()
    {
        SoundManager.instance.PlaySound("result4");
        Character character = GameManager.instance.character;

        number[0].text = (character.Week / 4 + 2) + "월";
        number[1].text = character.Name;

        if(character.Gender == 1)
        {
            number[3].text = "남";
        }

        CheckCount();

        number[6].text = character.Gold / 1000.0 + "K";
        number[7].text = character.Sum_st / 60 + "h";

        SoundManager.instance.PlaySound("result4");
    }

    private void MostGenre(int id, int count)
    {
        List<string> genre_name = new List<string>() {
            "동물", "뷰티", "교육", "연예", "금융", "음식", "게임", "건강", "정치", "쇼핑"
        };

        number[4].text = genre_name[id-1];
        number[5].text = count + "회";
    }

    private void CheckCount()
    {
        List<SmallGenre> sg = GameManager.instance.small_genre;

        int max = 0;
        int id = 0;

        for(int i = 1; i <= 10; i++)
        {
            List<SmallGenre> list = sg.FindAll(x => x.Genre_id == i);

            int sum = 0;

            foreach (var item in list)
            {
                sum += item.Count;
            }

            if (sum > max)
            {
                id = i;
                max = sum;
            }

            genre[i - 1].text = GameManager.instance.genre[i] + "";
        }

        MostGenre(id, max);
    }
}