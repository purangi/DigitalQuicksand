using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int char_id;

    void Awake()
    {
        instance = this;
    }
}
