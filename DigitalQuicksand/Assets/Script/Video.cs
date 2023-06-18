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
        
        private VideoResult vid_result;

        public Video(int video_id, string title, string summary, VideoResult vid_result)
        {
            this.video_id = video_id;
            this.title = title;
            this.summary = summary;
            this.vid_result = vid_result;
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

        public VideoResult VidResult
        {
            get { return vid_result; }
            set { vid_result = value; }
        }
    }

    public class VideoResult
    {
        private string reaction;
        private int length;

        private int sg_id;
        private int sg_id2;

        private int genre_id;
        private int skill;

        private List<int> property; //health, addiction, fun, stress, selfesteem, violence, cognitivebias, vanity ¼ø¼­

        public VideoResult(string reaction, int length, int sg_id, int sg_id2, int genre_id, int skill, List<int> property)
        {
            this.reaction = reaction;
            this.length = length;
            this.sg_id = sg_id;
            this.sg_id2 = sg_id2;
            this.genre_id = genre_id;
            this.skill = skill;
            this.property = property;
        }

        public string Reaction
        {
            get { return reaction; }
            set { reaction = value; }
        }

        public int Length
        {
            get { return length; }
            set { length = value; }
        }

        public int SG_ID
        {
            get { return sg_id; }
            set { sg_id = value; }
        }

        public int SG_ID2
        {
            get { return sg_id2; }
            set { sg_id2 = value; }
        }

        public int Genre_id
        {
            get { return genre_id; }
            set { genre_id = value; }
        }

        public int Skill
        {
            get { return skill; }
            set { skill = value; }
        }

        public List<int> Property
        {
            get { return property; }
            set { property = value; }
        }

    }
}

