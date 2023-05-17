using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyDB;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    public int char_id; //���� ���� ��&����� ���� �ҷ��� �� ���, ù ���� �ÿ��� ���� �� �˻��ؿͼ� ����
    public int gender;
    public int week = 1;
    public int gold;
    public string char_name;

    public Dictionary<int, int> genre; //genre_id, skill
    public List<SmallGenre> small_genre;
    public Dictionary<int, int> property; //property_id, stat

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
