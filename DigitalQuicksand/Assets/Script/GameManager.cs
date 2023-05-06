using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    public int char_id; //파일 저장 시&저장된 파일 불러올 때 사용, 첫 생성 시에는 삽입 후 검색해와서 쓰기
    public int gender;
    public int week = 1;
    public string char_name;

    public Dictionary<int, int> genre; //genre_id, skill
    public List<Hashtable> small_genre; //Hashtable 구성(name, pos_neg, genre_id, interest, count, length)
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
