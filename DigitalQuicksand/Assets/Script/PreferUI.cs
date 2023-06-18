using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using MyDB;
using System.Linq;

public class PreferUI : MonoBehaviour
{
    public GameObject[] prefers;

    private List<SmallGenre> sg_list;
    private Dictionary<int, int> top3;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetTop3();
    }

    private void GetSG()
    {
        top3 = new Dictionary<int, int>();

        for (int i = 1; i < 11; i++)
        {
            top3.Add(i, 0);
        }

        sg_list = GameManager.instance.small_genre;

        for (int i = 0; i < sg_list.Count; i++)
        {
            int genre = sg_list[i].Genre_id;

            switch (genre)
            {
                case 1:
                    top3[genre] += sg_list[i].Interest;
                    break;
                case 2:
                    top3[genre] += sg_list[i].Interest;
                    break;
                case 3:
                    top3[genre] += sg_list[i].Interest;
                    break;
                case 4:
                    top3[genre] += sg_list[i].Interest;
                    break;
                case 5:
                    top3[genre] += sg_list[i].Interest;
                    break;
                case 6:
                    top3[genre] += sg_list[i].Interest;
                    break;
                case 7:
                    top3[genre] += sg_list[i].Interest;
                    break;
                case 8:
                    top3[genre] += sg_list[i].Interest;
                    break;
                case 9:
                    top3[genre] += sg_list[i].Interest;
                    break;
                case 10:
                    top3[genre] += sg_list[i].Interest;
                    break;
            }
        }
    }

    public void GetTop3()
    {
        GetSG();

        List<string> genre_name = new List<string>() {
            "µ¿¹°", "ºäÆ¼", "±³À°", "¿¬¿¹", "±ÝÀ¶", "À½½Ä", "°ÔÀÓ", "°Ç°­", "Á¤Ä¡", "¼îÇÎ"
        };

        int count = 0;

        var dic = top3.OrderByDescending(x => x.Value);

        foreach (var item in dic)
        {
            if(count > 2)
            {
                break;
            }

            int genre_id = item.Key;
            string genre = genre_name[genre_id - 1];
            int genre_score = item.Value;

            switch (genre_id)
            {
                case 1:
                    genre_score /= 7;
                    break;
                case 2:
                    genre_score /= 5;
                    break;
                case 3:
                    genre_score /= 4;
                    break;
                case 4:
                    genre_score /= 6;
                    break;
                case 5:
                    genre_score /= 4;
                    break;
                case 6:
                    genre_score /= 5;
                    break;
                case 7:
                    genre_score /= 6;
                    break;
                case 8:
                    genre_score /= 4;
                    break;
                case 9:
                    genre_score /= 5;
                    break;
                case 10:
                    genre_score /= 4;
                    break;
            }

            prefers[count].transform.Find("Genre").gameObject.GetComponent<TextMeshProUGUI>().text = genre;
            prefers[count].transform.Find("Genre_score").gameObject.GetComponent<TextMeshProUGUI>().text = genre_score.ToString();

            count++;
        }
    }
}
