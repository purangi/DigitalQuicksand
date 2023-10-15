using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleScript : MonoBehaviour
{
    public Image TitlePress; // 깜빡이는 이미지
    public float blinkSpeed = 0.2f; // 이미지가 깜빡이는 속도

    private IEnumerator blinkCoroutine; // 깜빡이는 코루틴

    private void Start()
    {
        if (TitlePress != null)
        {
            // 이미지의 알파값과 기본 색상을 초기화합니다.
            TitlePress.color = new Color(1f, 1f, 1f, 0f);
            blinkCoroutine = BlinkTitlePress(); // 깜빡이는 코루틴 할당
            StartCoroutine(blinkCoroutine); // 깜빡이는 코루틴 시작
        }

    }

    private IEnumerator BlinkTitlePress()
    {
        while (true)
        {
            float alpha = Mathf.PingPong(Time.time * blinkSpeed, 1f); // Mathf.PingPong : 주어진 인자를 0부터 최댓값까지의 범위에서 왕복하도록 만드는 함수
            TitlePress.color = new Color(1f, 1f, 1f, alpha); // 이미지의 알파값을 변경합니다.
            yield return null;   // 실행 재개
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("TitleMenu"); // "TitleMenu" 씬으로 이동
        }
    }
}