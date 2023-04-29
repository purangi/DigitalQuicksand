using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    public int char_id; //���� ���� ��&����� ���� �ҷ��� �� ���, ù ���� �ÿ��� ��� x
    public int gender;
    public int week = 1;
    public string char_name;

    public Dictionary<string, int> genre; //string = genre_name, int = skill
    public List<Hashtable> small_genre; //Hashtable ����(name, pos_neg, genre_id, interest, count, length)
    public Dictionary<string, int> property;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        } else if(instance != this)
        {
            Destroy(instance);
        }

        DontDestroyOnLoad(instance);
    }
}
