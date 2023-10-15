using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Reaction : MonoBehaviour
{
    public TextMeshProUGUI text;
    public Image icon;
    public Sprite male_icon;

    private IEnumerator coroutine;

    // Start is called before the first frame update
    void Start()
    {
        if(GameManager.instance.character.Gender == 1)
        {
            icon.sprite = male_icon;
        }
        coroutine = ExitPopUp();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PopUp(string reaction)
    {
        text.text = reaction;

        StartCoroutine("ExitPopUp");
    }

    public void Stop()
    {
        StopCoroutine(coroutine);
    }

    IEnumerator ExitPopUp()
    {
        yield return new WaitForSeconds(3.0f);
        gameObject.SetActive(false);
    }
}
