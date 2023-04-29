using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        int gender = GameManager.instance.gender;

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
