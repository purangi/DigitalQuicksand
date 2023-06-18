using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MyDB;

public class MainChar : MonoBehaviour
{
    public Image mainCharimg;
    public Sprite Char_male;
    public Sprite Char_female;

    // Start is called before the first frame update
    void Start()
    {
        SetCharacterImage();
    }
    private void SetCharacterImage()
    {
        Character character = GameManager.instance.character;
        int gender = character.Gender;

        if (gender == 0)
        {
            mainCharimg.sprite = Char_female;
        }
        else if (gender == 1)
        {
            mainCharimg.sprite = Char_male;
        }
    }
}
