using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProfileChange : MonoBehaviour
{
    public Toggle toggle;
    public Image image;

    public Sprite onSprite;
    public Sprite offSprite;

    public void ChangeImage()
    {
        image.sprite = toggle.isOn ? onSprite : offSprite;
    }
}
