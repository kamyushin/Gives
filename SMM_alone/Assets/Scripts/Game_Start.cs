using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game_Start : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Game()
    {
        if (FileOut.text_input == true)
        {
            Debug.Log(true);
            SceneManager.LoadScene("Game");
            
        }
    }
}
