using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace MyDB
{
    public class Character
    {
        private int id;
        private string name;
        private int gender;
        private int week;
        private int gold;
        private int last_st;
        private int this_st;
        private int sum_st;

        public Character(int id, string name, int gender, int week, int gold, int last_st, int this_st, int sum_st)
        {
            this.id = id;
            this.name = name;
            this.gender = gender;
            this.week = week;
            this.gold = gold;
            this.last_st = last_st;
            this.this_st = this_st;
            this.sum_st = sum_st;
        }

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public int Gender
        {
            get { return gender; }
            set { gender = value; }
        }
        public int Week
        {
            get { return week; }
            set { week = value; }
        }

        public int Gold
        {
            get { return gold; }
            set { gold = value; }
        }

        public int Last_st
        {
            get { return last_st; }
            set { last_st = value; }
        }

        public int This_st
        {
            get { return this_st; }
            set { this_st = value; }
        }

        public int Sum_st
        {
            get { return sum_st; }
            set { sum_st = value; }
        }
    }
}