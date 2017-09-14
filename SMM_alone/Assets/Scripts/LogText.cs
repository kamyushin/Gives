using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogText : MonoBehaviour {


    private Text text;
    private ArrayList Log_Text = new ArrayList();
	// Use this for initialization
	void Start () {
        text = GetComponent<Text>();

    }
	
	// Update is called once per frame
	void Update () {
	}

    public void log_add(string log)
    {
        text.text += log + " " + "→";
    }
}
