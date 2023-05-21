using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System;
using System.IO;
using System.Linq;
using Mono.Data.Sqlite;
using MyDB;

public class VideoList : MonoBehaviour
{
    public GameObject video;
    public GameObject parent;
    public Sprite[] Images;

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
        //소장르 interest 내림차순으로 검색 -> 동일하면 count 내림차순 검색 -> 1번 장르 5개 2,3번 장르 각 3개, 4,5번장르 최소 1개씩
        //1-2-3 동률일 때는 각 4개씩, 나머지 3개는 하위 중 랜덤 선택
        CheckInterest();
        SearchTopInterest();
    }

    private void CheckInterest() 
    {
        List<SmallGenre> sg_list = GameManager.instance.small_genre;

        List<SmallGenre> sg = sg_list.OrderByDescending(x => x.Interest).ToList();

        var query = sg.GroupBy(x => x.Interest); //groupby 쿼리

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

                        break;
                    } else
                    {
                        sg_num += temp2.Count;
                    }
                }

                break;
            } else
            {
                sg_num += temp.Count;
            }
        }
    }

    private void SearchTopInterest()
    {
        //소장르들 중에 선택 개수 정해야됨
        List<SmallGenre> sg_list = GameManager.instance.small_genre;

        List<int> need_random = new List<int>();

        for(int i = 0; i < sg_list.Count; i++)
        {
            if (sg_list[i].Interest > min_interest)
            {
                //전부 삽입
                top_sgid.Add(sg_list[i].Sgenre_id);
            } else if (sg_list[i].Interest == min_interest)
            {
                if (top_sgid.Count > 5)
                {
                    break;
                } else
                {
                    if (sg_list[i].Count > min_count)
                    {
                        //전부 삽입
                        top_sgid.Add(sg_list[i].Sgenre_id);
                    }
                    else if (sg_list[i].Count == min_count)
                    {
                        //랜덤 삽입
                        need_random.Add(sg_list[i].Sgenre_id);
                    }
                }
            }
        }

        if(top_sgid.Count < 5)
        {
            int num = 5 - top_sgid.Count;

            need_random.OrderBy(g => Guid.NewGuid()).Take(num).ToList().ForEach(x => top_sgid.Add(x));
        }

        Debug.Log("id 뽑은 개수 " + top_sgid.Count);

        SearchVideo();
    }

    private void SearchVideo()
    {
        //db에서 서치
        List<Video> temp_list = new List<Video>();

        for(int i = 0; i < top_sgid.Count; i++)
        {
            SqliteDataReader reader = m_DatabaseAccess.ExecuteQuery("SELECT * FROM video WHERE sg_id = " + top_sgid[i] + " OR sg_id2 = " + top_sgid[i]);
            
            while(reader.Read())
            {
                Video item = new Video(Int32.Parse(reader["id"].ToString()), reader["title"].ToString(), reader["summary"].ToString());
                temp_list.Add(item);
            }
        }

        List<Video> video_list = temp_list.OrderBy(g => Guid.NewGuid()).Take(15).ToList();

        for(int i = 0; i < video_list.Count; i++)
        {
            generateVideo(video_list[i].Video_id, video_list[i].Title, video_list[i].Summary);
        }
    }

    private void generateVideo(int id, string title, string summary)
    {
        GameObject temp = Instantiate(video);

        Image thumb = temp.transform.GetChild(0).gameObject.GetComponent<Image>();
        thumb.sprite = Images[id];

        TextMeshProUGUI title_text = temp.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>();
        title_text.text = title;

        TextMeshProUGUI summary_text = temp.transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>();
        summary_text.text = summary;

        temp.transform.SetParent(parent.transform);
    }
}
