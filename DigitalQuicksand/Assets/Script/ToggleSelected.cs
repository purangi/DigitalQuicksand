using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class ToggleSelected : MonoBehaviour, IPointerClickHandler
{
    public TextMeshProUGUI text_1;
    public TextMeshProUGUI text_2;
    public Image image;
    public GameObject property;

    private Toggle obj;

    private Color good = new Color (157 / 255f, 173 / 255f, 196 / 255f, 255 / 255f);
    private Color bad = new Color(196 / 255f, 166 / 255f, 151 / 255f, 255 / 255f);
    private Color black = new Color(0, 0, 0, 255);
    private Color gray = new Color(190 / 255f, 190 / 255f, 190 / 255f, 190 / 255f);

    // Start is called before the first frame update
    void Start()
    {
        obj = GetComponent<Toggle>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TextBold(Toggle toggle)
    {
        if(toggle.isOn)
        {
            text_1.color = black;
        } else
        {
            text_1.color = new Color(211 / 255f, 220 / 255f, 234 / 255f, 255 / 255f);
        }
    }

    public void TextColorChange(Toggle toggle)
    {
        if (toggle.isOn)
        {
            text_1.color = good;
            text_2.color = black;
        }
        else
        {
            text_1.color = black;
            text_2.color = bad;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Right)
        {
            obj.interactable = !obj.interactable;
            property.GetComponent<ScoreSum>().OnToggleValueChanged(obj.interactable);
            SelectionActive();
        }
    }

    public void SelectionActive()
    {
        if(obj.interactable)
        {
            image.gameObject.SetActive(false);
            TextColorChange(obj);        
        }
        else
        {
            image.gameObject.SetActive(true);
            text_1.color = gray;
            text_2.color = gray;
        }
        
    }
}
