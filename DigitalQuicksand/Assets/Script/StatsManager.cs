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

    // �����̴�
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
        int characterId = GameManager.instance.char_id; // ĳ������ ID�� �����ɴϴ�. GameManager.
        Initialize(characterId);
    }

    public void Initialize(int characterId)
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, m_DatabaseFileName);
        m_DatabaseAccess = new DBAccess("data source = " + filePath);

        Dictionary<string, Action<int>> propertyActions = new Dictionary<string, Action<int>> {
            { "�ǰ�", stats => Health = stats },
            { "�ߵ�", stats => Addiction = stats },
            { "���", stats => Fun = stats },
            { "��Ʈ����", stats => Stress = stats },
            { "������", stats => SelfEsteem = stats },
            { "���¼�", stats => Violence = stats },
            { "��������", stats => CognitiveBias = stats },
            { "�㿵��", stats => Vanity = stats }
        };

        try
        {
            // Property ���̺��� ���� ��������
            SqliteDataReader reader = m_DatabaseAccess.ExecuteQuery("SELECT * FROM Property WHERE char_id = " + characterId);
            while (reader.Read())
            {
                // Property ���̺��� ������ ������ ����Ͽ� ĳ������ Ư�� �ʱ�ȭ
                string propertyName = reader.GetString(reader.GetOrdinal("name"));
                int propertyStats = reader.GetInt32(reader.GetOrdinal("stats"));

                // propertyName�� key�� ���� ���ٽ��� ȣ���Ͽ� ���� ����
                if (propertyActions.TryGetValue(propertyName, out Action<int> action))
                {
                    action.Invoke(propertyStats);
                }
            }
        }
        catch (Exception ex)
        {
            // ���� ó��
            Debug.LogError($"Error while reading database: {ex.Message}");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // �� Ư�� ���� ���� �����̴��� ��ġ ����
        healthSlider.value = Health / 100.0f;
        addictionSlider.value = Addiction / 100.0f;
        funSlider.value = Fun / 100.0f;
        stressSlider.value = Stress / 100.0f;
        selfEsteemSlider.value = SelfEsteem / 100.0f;
        violenceSlider.value = Violence / 100.0f;
        cognitiveBiasSlider.value = CognitiveBias / 100.0f;
        vanitySlider.value = Vanity / 100.0f;

        // fill ���� ���� 
        SetFillArea(healthSlider, Health / 100.0f);
        SetFillArea(addictionSlider, Addiction / 100.0f);
        SetFillArea(funSlider, Fun / 100.0f);
        SetFillArea(stressSlider, Stress / 100.0f);
        SetFillArea(selfEsteemSlider, SelfEsteem / 100.0f);
        SetFillArea(violenceSlider, Violence / 100.0f);
        SetFillArea(cognitiveBiasSlider, CognitiveBias / 100.0f);
        SetFillArea(vanitySlider, Vanity / 100.0f);
    }

    // �����̴��� fill ���� ���� �Լ�
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

