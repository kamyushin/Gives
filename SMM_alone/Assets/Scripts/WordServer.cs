using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordServer : MonoBehaviour {
    NetWork net;
    public static ArrayList dataWords = new ArrayList();
    public static int count = 0;
    int now_count = 0;
    bool make_flag = false;
	// Use this for initialization
	void Start () {
        net = GetComponent<NetWork>();
        StartCoroutine("get");
        
	}

    IEnumerator get()
    {
        while (true)
        {
            net.getServer();
            yield return new WaitForSeconds(1);
        }
    }
	
	// Update is called once per frame
	void Update () {
        
        if (count > 0 && make_flag == false)
        {
            now_count = count;
            GetComponent<ServerBubble>().BubblesMake(dataWords);
            make_flag = true;
        }
        /*
        if (now_count != count)
        {
            int n = now_count - count;
            now_count = count;
            
            GetComponent<ServerBubble>().BubblesMake(dataWords,n);
        }
        */

        
		
	}

    public  void add(string s)
    {
        dataWords.Add(s);
        count++;
        if ( now_count != 0 && now_count < count)
        {
            now_count = count;
            GetComponent<ServerBubble>().BubbleMake(s);
        }
    }

    public static ArrayList push()
    {
        return dataWords;
    }
}
