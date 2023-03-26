using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TypingEffect : MonoBehaviour
{
    public TextMeshProUGUI tmp_text;
    public TextMeshProUGUI tmp_text2;
    public string message1;
    public string message2;
    float speed = 0.07f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Typing(tmp_text, tmp_text2, message1, message2, speed));
    }

    IEnumerator Typing(TextMeshProUGUI typingText, TextMeshProUGUI typingText2, string message1, string message2, float speed)
    {
        for(int i = 0; i < message1.Length; i++)
        {
            typingText.text = message1.Substring(0, i + 1);
            yield return new WaitForSeconds(speed);
        }
        for (int i = 0; i < message2.Length; i++)
        {
            typingText2.text = message2.Substring(0, i + 1);
            yield return new WaitForSeconds(speed);
        }
    }
}
