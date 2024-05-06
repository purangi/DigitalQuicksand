using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShowPopUp : MonoBehaviour
{
    public GameObject obj;
    public TextMeshProUGUI popup_text;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Showup(string text)
    {
        obj.SetActive(true);
        StartCoroutine(PopUp(text));
    }

    IEnumerator PopUp(string text)
    {
        popup_text.text = text;

        yield return new WaitForSeconds(2.5f);

        obj.SetActive(false);
    }
}
