using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordList : MonoBehaviour {
    public static ArrayList words = new ArrayList();
    public static int count;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public static void add(string s)
    {
        words.Add(s);
        count++;
    }

    public static string push()
    {
        if (words.Count > 0)
        {
            string s = words[0].ToString();
            words.RemoveAt(0);
            count--;
            return s;
        }
        else
        {
            return "s";
        }
    }
}
