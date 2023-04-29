using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    public int char_id; //파일 저장 시&저장된 파일 불러올 때 사용, 첫 생성 시에는 사용 x
    public int gender;
    public int week = 1;
    public string char_name;

    public Dictionary<string, int> genre; //string = genre_name, int = skill
    public List<Hashtable> small_genre; //Hashtable 구성(name, pos_neg, genre_id, interest, count, length)
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
