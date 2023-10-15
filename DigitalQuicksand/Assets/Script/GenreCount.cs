using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using MyDB;

public class GenreCount : MonoBehaviour
{
    public int genre_id;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        List<SmallGenre> sg = GameManager.instance.small_genre;
        List<SmallGenre> list = sg.FindAll(x => x.Genre_id == genre_id);

        int sum = 0;

        foreach (var item in list)
        {
            sum += item.Count;
        }

        GetComponent<TextMeshProUGUI>().text = sum.ToString();
    }
}
