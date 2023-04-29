using MySql.Data.MySqlClient;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System.IO;
using UnityEngine.TextCore.Text;
using System;
using UnityEngine.UI;

public class StatsManager : MonoBehaviour
{
    private DBAccess m_DatabaseAccess;
    private string m_DatabaseFileName = "database.db";

    public int Health { get; private set; }
    public int Addiction { get; private set; }
    public int Fun { get; private set; }
    public int Stress { get; private set; }
    public int SelfEsteem { get; private set; }
    public int Violence { get; private set; }
    public int CognitiveBias { get; private set; }
    public int Vanity { get; private set; }

    // 슬라이더
    public Slider healthSlider;
    public Slider addictionSlider;
    public Slider funSlider;
    public Slider stressSlider;
    public Slider selfEsteemSlider;
    public Slider violenceSlider;
    public Slider cognitiveBiasSlider;
    public Slider vanitySlider;

    void Start()
    {
        int characterId = GameManager.instance.char_id; // 캐릭터의 ID를 가져옵니다. GameManager.
        Initialize(characterId);
    }

    public void Initialize(int characterId)
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, m_DatabaseFileName);
        m_DatabaseAccess = new DBAccess("data source = " + filePath);

        Dictionary<string, Action<int>> propertyActions = new Dictionary<string, Action<int>> {
            { "건강", stats => Health = stats },
            { "중독", stats => Addiction = stats },
            { "재미", stats => Fun = stats },
            { "스트레스", stats => Stress = stats },
            { "자존감", stats => SelfEsteem = stats },
            { "폭력성", stats => Violence = stats },
            { "인지평향", stats => CognitiveBias = stats },
            { "허영심", stats => Vanity = stats }
        };

        try
        {
            // Property 테이블에서 정보 가져오기
            SqliteDataReader reader = m_DatabaseAccess.ExecuteQuery("SELECT * FROM Property WHERE char_id = " + characterId);
            while (reader.Read())
            {
                // Property 테이블에서 가져온 정보를 사용하여 캐릭터의 특성 초기화
                string propertyName = reader.GetString(reader.GetOrdinal("name"));
                int propertyStats = reader.GetInt32(reader.GetOrdinal("stats"));

                // propertyName을 key로 갖는 람다식을 호출하여 값을 설정
                if (propertyActions.TryGetValue(propertyName, out Action<int> action))
                {
                    action.Invoke(propertyStats);
                }
            }
        }
        catch (Exception ex)
        {
            // 예외 처리
            Debug.LogError($"Error while reading database: {ex.Message}");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // 각 특성 값에 따라 슬라이더의 위치 설정
        healthSlider.value = Health / 100.0f;
        addictionSlider.value = Addiction / 100.0f;
        funSlider.value = Fun / 100.0f;
        stressSlider.value = Stress / 100.0f;
        selfEsteemSlider.value = SelfEsteem / 100.0f;
        violenceSlider.value = Violence / 100.0f;
        cognitiveBiasSlider.value = CognitiveBias / 100.0f;
        vanitySlider.value = Vanity / 100.0f;

        // fill 영역 설정 
        SetFillArea(healthSlider, Health / 100.0f);
        SetFillArea(addictionSlider, Addiction / 100.0f);
        SetFillArea(funSlider, Fun / 100.0f);
        SetFillArea(stressSlider, Stress / 100.0f);
        SetFillArea(selfEsteemSlider, SelfEsteem / 100.0f);
        SetFillArea(violenceSlider, Violence / 100.0f);
        SetFillArea(cognitiveBiasSlider, CognitiveBias / 100.0f);
        SetFillArea(vanitySlider, Vanity / 100.0f);
    }

    // 슬라이더의 fill 영역 설정 함수
    private void SetFillArea(Slider slider, float fillValue)
    {
        fillValue = Mathf.Clamp01(fillValue);

        if (fillValue <= 0f)
        {
            slider.fillRect.gameObject.SetActive(false);
        }
        else if (fillValue >= 1f)
        {
            slider.fillRect.gameObject.SetActive(true);
            slider.fillRect.localScale = new Vector3(1f, 1f, 1f);
        }
        else if (fillValue > 0f && fillValue < 1f)
        {
            slider.fillRect.gameObject.SetActive(true);
            slider.fillRect.localScale = new Vector3(fillValue, 1f, 1f);
        }
    }
}

