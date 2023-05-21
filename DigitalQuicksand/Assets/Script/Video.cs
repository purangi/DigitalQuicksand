using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyDB
{
    public class Video
    {
        private int video_id;
        private string title;
        private string summary;

        public Video(int video_id, string title, string summary)
        {
            this.video_id = video_id;
            this.title = title;
            this.summary = summary;
        }

        public int Video_id
        {
            get { return video_id; }
            set { video_id = value; }
        }

        public string Title
        {
            get { return title; }
            set { title = value; }
        }

        public string Summary
        {
            get { return summary; }
            set { summary = value; }
        }
    }
}

