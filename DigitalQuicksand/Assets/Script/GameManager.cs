using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyDB;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    public Character character;

    public Dictionary<int, int> genre; //genre_id, skill
    public List<SmallGenre> small_genre;
    public Dictionary<int, int> property; //property_id, stat

    public int ending;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
}
