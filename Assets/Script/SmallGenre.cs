using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace MyDB
{
    public class SmallGenre
    {
        private int sgenre_id;
        private int genre_id;
        private int interest;
        private int count;

        public SmallGenre(int sgenre_id, int genre_id, int interest, int count)
        {
            this.sgenre_id = sgenre_id;
            this.genre_id = genre_id;
            this.interest = interest;
            this.count = count;
        }

        public int Sgenre_id
        {
            get { return sgenre_id; }
            set { sgenre_id = value; }
        }

        public int Genre_id
        {
            get { return genre_id; }
            set { genre_id = value; }
        }
        public int Interest
        {
            get { return interest; }
            set { interest = value; }
        }

        public int Count
        {
            get { return count; }
            set { count = value; }
        }
    }
}

