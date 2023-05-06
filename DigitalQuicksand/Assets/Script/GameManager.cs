using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    public int char_id; //���� ���� ��&����� ���� �ҷ��� �� ���, ù ���� �ÿ��� ���� �� �˻��ؿͼ� ����
    public int gender;
    public int week = 1;
    public string char_name;

    public Dictionary<int, int> genre; //genre_id, skill
    public List<Hashtable> small_genre; //Hashtable ����(name, pos_neg, genre_id, interest, count, length)
    public Dictionary<int, int> property; //property_id, stat

    public List<Hashtable> video;

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
