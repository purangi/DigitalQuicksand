using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using MyDB;

public class Gold : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Character character = GameManager.instance.character;
        GetComponent<TextMeshProUGUI>().text = character.Gold + "¿ø";
    }
}
