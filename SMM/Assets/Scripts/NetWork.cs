using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MiniJSON;
using UnityEngine.SceneManagement;


public class NetWork : MonoBehaviour {

    public static bool reply = true;

    public string get_first_text;
    bool firstAccess = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void connectionStart(string name)
    {
        reply = false;
        WWWForm form = new WWWForm();
        form.AddField("input", name);
        //string POST_URL = "https://team-smm.herokuapp.com/unity";
        string POST_URL = "http://25.5.91.115:3000/unity";
        WWW www = new WWW(POST_URL, form);

        BubbleTouch.touch_flag = true;
        StartCoroutine("WaitForRequest", www);

    }
    public void postServer(string name)
    {
        reply = false;
        WWWForm form = new WWWForm();
        form.AddField("input", name);
        string POST_URL = "http://25.5.91.115:3000/space-push";
        WWW post_www = new WWW(POST_URL, form);
    }

    public void getServer()
    {
        reply = false;
        string GET_URL = "http://25.5.91.115:3000/space-fetch";
        WWW get_www = new WWW(GET_URL);
        StartCoroutine("WaitForRequestServer", get_www);
    }
    //通信の処理待ち
    private IEnumerator WaitForRequest(WWW www)
    {
        yield return www;
        connectionEnd(www);
    }
    private IEnumerator WaitForRequestServer(WWW www)
    {
        yield return www;
        connectionServerEnd(www);
    }


    //通信終了後の処理
    private void connectionEnd(WWW www)
    {
        //通信結果をLogで出す
        if (www.error != null)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log(www.text);

            string[] stdata = dataSplit(www);
            if (stdata.Length < Sphere.child_count)
                Sphere.child_count = stdata.Length;
            if (Sphere.child_count < stdata.Length)
            {
                Sphere.child_count = stdata.Length;
                if (Sphere.child_count > 6)
                    Sphere.child_count = 6;
            }
            for (int i = 0; i<Sphere.child_count;i++)
            {
                WordList.add(stdata[i]);
            }

            /*if (SceneManager.GetActiveScene().name == "Title")
                Game_Start.Game();
                */

            reply = true;
            BubbleTouch.touch_flag = false;
        }
    }

    private void connectionServerEnd(WWW www)
    {
        if (www.error != null)
        {
            Debug.Log(www.error);
        }
        else
        {
            string[] serverWords = dataSplit(www);

            for (int i = 0; i<serverWords.Length;i++)
            {
                if (WordServer.dataWords.Count < serverWords.Length && i+1 >WordServer.count)
                    {
                        GetComponent<WordServer>().add(serverWords[i]);
                    }
            }
            

        }
    }

    string[] dataSplit(WWW www)
    {
        string data;
        if (www.text == "[]")
        {
            data = "";
        }
        else
        {
            data = www.text.Replace("[", "").Replace("]", "");
        }
            
        

        string[] stArrayData = data.Split(',');
        string[] stdata = new string[stArrayData.Length];
        if (stArrayData.Length > 1)
        {
            for (int i = 0; i < stArrayData.Length; i++)
            {
                stdata[i] = stArrayData[i].Replace("\"", "");

            }
        }
        else
            stdata[0] = " ";
        return stdata;
    }
    
}
