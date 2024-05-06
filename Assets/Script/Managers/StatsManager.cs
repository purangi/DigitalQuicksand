using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class StatsManager : MonoBehaviour
{
    public Slider healthSlider;
    public Slider addictionSlider;
    public Slider funSlider;
    public Slider stressSlider;
    public Slider selfEsteemSlider;
    public Slider violenceSlider;
    public Slider cognitiveBiasSlider;
    public Slider vanitySlider;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        int health = GameManager.instance.property[1];
        int addiction = GameManager.instance.property[2];
        int fun = GameManager.instance.property[3];
        int stress = GameManager.instance.property[4];
        int selfesteem = GameManager.instance.property[5];
        int violence = GameManager.instance.property[6];
        int cognitivebias = GameManager.instance.property[7];
        int vanity = GameManager.instance.property[8];

        // 각 슬라이더에 스탯 값 할당
        healthSlider.value = (float)health / 100;
        addictionSlider.value = (float)addiction / 100;
        funSlider.value = (float)fun / 100;
        stressSlider.value = (float)stress / 100;
        selfEsteemSlider.value = (float)selfesteem / 100;
        violenceSlider.value = (float)violence / 100;
        cognitiveBiasSlider.value = (float)cognitivebias / 100;
        vanitySlider.value = (float)vanity / 100;

        // 각 슬라이더의 Fill Area 크기 조절
        healthSlider.GetComponentInChildren<Image>().fillAmount = (float)health / 100;
        addictionSlider.GetComponentInChildren<Image>().fillAmount = (float)addiction / 100;
        funSlider.GetComponentInChildren<Image>().fillAmount = (float)fun / 100;
        stressSlider.GetComponentInChildren<Image>().fillAmount = (float)stress / 100;
        selfEsteemSlider.GetComponentInChildren<Image>().fillAmount = (float)selfesteem / 100;
        violenceSlider.GetComponentInChildren<Image>().fillAmount = (float)violence / 100;
        cognitiveBiasSlider.GetComponentInChildren<Image>().fillAmount = (float)cognitivebias / 100;
        vanitySlider.GetComponentInChildren<Image>().fillAmount = (float)vanity / 100;
    }
}
