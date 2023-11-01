using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleScript : MonoBehaviour
{
    public Image TitlePress; // �����̴� �̹���
    public float blinkSpeed = 0.2f; // �̹����� �����̴� �ӵ�

    private IEnumerator blinkCoroutine; // �����̴� �ڷ�ƾ

    private void Start()
    {
        Screen.SetResolution(1920, 1080, true);
        if (TitlePress != null)
        {
            // �̹����� ���İ��� �⺻ ������ �ʱ�ȭ�մϴ�.
            TitlePress.color = new Color(1f, 1f, 1f, 0f);
            blinkCoroutine = BlinkTitlePress(); // �����̴� �ڷ�ƾ �Ҵ�
            StartCoroutine(blinkCoroutine); // �����̴� �ڷ�ƾ ����
        }

    }

    private IEnumerator BlinkTitlePress()
    {
        while (true)
        {
            float alpha = Mathf.PingPong(Time.time * blinkSpeed, 1f); // Mathf.PingPong : �־��� ���ڸ� 0���� �ִ񰪱����� �������� �պ��ϵ��� ����� �Լ�
            TitlePress.color = new Color(1f, 1f, 1f, alpha); // �̹����� ���İ��� �����մϴ�.
            yield return null;   // ���� �簳
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("TitleMenu"); // "TitleMenu" ������ �̵�
        }
    }
}