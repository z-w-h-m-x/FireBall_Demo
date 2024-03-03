using System;
using UnityEngine;

namespace ShortLog
{
    //多此一举
    public class Log
    {
        public static  Log operator -(Log a,string b)
        {
            Debug.Log(b);
            return new();
        }
    }
}