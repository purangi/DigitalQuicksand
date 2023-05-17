using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.IO;
using System.Linq;
using Mono.Data.Sqlite;
using MyDB;

public class VideoList : MonoBehaviour
{
    private DBAccess m_DatabaseAccess;
    private List<int> top_sgid = new List<int>();

    private int min_interest = 0;
    private int min_count = 0;

    // Start is called before the first frame update
    void Start()
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, "save.db");
        m_DatabaseAccess = new DBAccess("data source = " + filePath);

        RecommendVideo();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RecommendVideo()
    {
        //���帣 interest ������������ �˻� -> �����ϸ� count �������� �˻� -> 1�� �帣 5�� 2,3�� �帣 �� 3��, 4,5���帣 �ּ� 1����
        //1-2-3 ������ ���� �� 4����, ������ 3���� ���� �� ���� ����
        CheckInterest();
        SearchTopInterest();
    }

    private void CheckInterest() 
    {
        List<SmallGenre> sg_list = GameManager.instance.small_genre;

        List<SmallGenre> sg = sg_list.OrderByDescending(x => x.Interest).ToList();

        var query = sg.GroupBy(x => x.Interest); //groupby ����

        int sg_num = 0;

        foreach(var group in query)
        {
            List<SmallGenre> temp_list = new List<SmallGenre>();

            foreach(var item in group)
            {
                temp_list.Add(new SmallGenre(item.Sgenre_id, item.Interest, item.Count, item.Length));
            }

            List<SmallGenre> temp = temp_list.OrderByDescending(x => x.Count).ToList();

            if (sg_num + temp.Count > 5)
            {
                Debug.Log("sg_num + temp.Count > 5");

                var query2 = temp.GroupBy(x => x.Count);

                foreach (var group2 in query2)
                {
                    List<SmallGenre> temp2 = new List<SmallGenre>();

                    foreach (var item in group2)
                    {
                        temp2.Add(new SmallGenre(item.Sgenre_id, item.Interest, item.Count, item.Length));
                    }

                    if(sg_num + temp2.Count > 5)
                    {
                        SmallGenre last = temp2.Last();
                        min_interest = last.Interest;
                        min_count = last.Count;
                    } else
                    {
                        sg_num += temp2.Count;
                    }
                }
            } else
            {
                Debug.Log("sg_num + temp.Count <= 5");
                sg_num += temp.Count;
            }
        }

        Debug.Log(min_count + ", " + min_interest);
    }

    private void SearchTopInterest()
    {
        //���帣�� �߿� ���� ���� ���ؾߵ�
        List<SmallGenre> sg_list = GameManager.instance.small_genre;

        List<int> need_random = new List<int>();

        for(int i = 0; i < sg_list.Count; i++)
        {
            if (sg_list[i].Interest > min_interest)
            {
                //���� ����
                top_sgid.Add(sg_list[i].Sgenre_id);
                Debug.Log(i + "���Ե�");
            } else if (sg_list[i].Interest == min_interest)
            {
                if (sg_list[i].Count > min_count)
                {
                    //���� ����
                    top_sgid.Add(sg_list[i].Sgenre_id);
                    Debug.Log(i + "���Ե�");
                } else if (sg_list[i].Count == min_count)
                {
                    //���� ����
                    need_random.Add(sg_list[i].Sgenre_id);
                }
            }
        }

        if(top_sgid.Count < 5)
        {
            int num = 5 - top_sgid.Count;

            need_random.OrderBy(g => Guid.NewGuid()).Take(num).ToList().ForEach(x => top_sgid.Add(x));
        }

        Debug.Log("id ���� ���� " + top_sgid.Count);
    }
}
