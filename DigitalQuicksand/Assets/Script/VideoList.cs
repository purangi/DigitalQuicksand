using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class VideoList : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Video()
    {
        List<Hashtable> video = new List<Hashtable>();

        List<List<string>> videolist = new List<List<string>>()
        {
            new List<string> { "장르", "제목", "요약", "길이(int 변환 필요)", "특성", "특성수치(int)", "기술", "기술수치(int)" }
        };

        for (int i = 0; i < videolist.Count; i++)
        {
            List<string> list = videolist[i];

            video.Add(new Hashtable());

            int count = video.Count;

            video[count - 1].Add("genre", list[0]);
            video[count - 1].Add("title", list[1]);
            video[count - 1].Add("summary", list[2]);
            video[count - 1].Add("length", Int32.Parse(list[3]));
            video[count - 1].Add("p_stat", list[4]);
            video[count - 1].Add("p_amount", Int32.Parse(list[5]));
            video[count - 1].Add("s_stat", list[6]);
            video[count - 1].Add("s_amount", Int32.Parse(list[7]));
        }
    }
}
