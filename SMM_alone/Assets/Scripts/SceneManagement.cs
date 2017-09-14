using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour {

    
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SceneChange()
    {
        if (SceneManager.GetActiveScene().name == "Game")
            SceneManager.LoadScene("Server");
        else
        {
            SceneManager.LoadScene("Game");
            WordList.add(FileOut.user_input);
        }
    }
}
