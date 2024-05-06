using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreSum : MonoBehaviour
{
    private List<Toggle> toggles = new List<Toggle>();
    public TextMeshProUGUI score;
    public GameObject showmarker;
    public bool isScoreOK = false;

    // Start is called before the first frame update
    void Start()
    {
        Toggle[] allToggles = GetComponentsInChildren<Toggle>();

        for (int i = 0; i < allToggles.Length; i++)
        {
            // Add the toggle to the list
            toggles.Add(allToggles[i]);

            // Add a listener for the toggle's value changed event
            allToggles[i].onValueChanged.AddListener(OnToggleValueChanged);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnToggleValueChanged(bool isOn)
    {
        int sum = 0;

        for (int i = 0; i < toggles.Count; i++)
        {
            int value = 0;

            if (toggles[i].interactable)
            {
                switch(i)
                {
                    case 0:
                        value = 10;
                        break;
                    case 1:
                        value = 8;
                        break;
                    case 2:
                        value = 6;
                        break;
                    case 3:
                        value = 5;
                        break;
                    case 4:
                        value = 4;
                        break;
                    case 5:
                        value = 4;
                        break;
                    case 6:
                        value = 4;
                        break;
                    case 7:
                        value = 4;
                        break;
                }
            }

            if (toggles[i].isOn)
            {
                sum -= value;
            } else
            {
                sum += value;
            }
        }

        ChangeScore(sum);
    }

    private void ChangeScore(int sum)
    {
        score.text = sum.ToString();
        RotateMarker(sum);
    }

    private void RotateMarker(int sum)
    {
        showmarker.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        //점수가 -10점이 90도 1점당 9도 회전
        if(sum >= -10 && sum <= 10)
        {
            isScoreOK = true;
            showmarker.transform.Rotate(0, 0, sum * -9);
        } else if(sum < -10)
        {
            isScoreOK = false;
            showmarker.transform.Rotate(0, 0, 100);
        } else if(sum > 10)
        {
            isScoreOK = false;
            showmarker.transform.Rotate(0, 0, -100);
        } else
        {
            Debug.Log("showmarker rotate error");
        }
    }
}
