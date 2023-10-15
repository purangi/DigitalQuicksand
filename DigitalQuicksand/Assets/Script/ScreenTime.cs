using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScreenTime : MonoBehaviour
{
    public int num;
    private int time;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (num == 0)
        {
            time = GameManager.instance.character.Last_st;
        }
        else
        {
            time = GameManager.instance.character.This_st;
        }

        if (time != 0)
        {
            GetComponent<TextMeshProUGUI>().text = time.ToString();
        }
    }
}
