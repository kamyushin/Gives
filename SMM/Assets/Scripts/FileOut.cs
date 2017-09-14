using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class FileOut : MonoBehaviour {
    private InputField input;
    Data d;
    public static bool text_input = false;
    NetWork net;

    public static string user_input;

    public static string word;
	// Use this for initialization
	void Start () {
        input = transform.GetChild(2).GetComponent<InputFieldButton>().inputField;
        net = GetComponent<NetWork>();
	}
	
	// Update is called once per frame
	void Update () {
        

    }

    public void file_out() {
        if (input.text.Length != 0) {
            text_input = true;
            word = input.text;
            user_input = word;
            //net.connectionStart(word);
            WordList.add(word);
            /*
            d.input = word;
            StreamWriter sw;
            sw = new StreamWriter(Application.dataPath +"/TextData.txt", false);
            sw.WriteLine(JsonUtility.ToJson(d));
            sw.Close();
            */
        }
        
    }
}
