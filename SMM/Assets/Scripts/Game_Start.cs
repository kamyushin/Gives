using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game_Start : MonoBehaviour {

	// Use this for initialization
	void Start () {
        switch (Screen.orientation)
        {
            // 縦画面のとき
            case ScreenOrientation.Portrait:
                // 左回転して左向きの横画面にする
                Screen.orientation = ScreenOrientation.LandscapeLeft;
                break;
            // 上下反転の縦画面のとき
            case ScreenOrientation.PortraitUpsideDown:
                // 右回転して左向きの横画面にする
                Screen.orientation = ScreenOrientation.LandscapeRight;
                break;
        }
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
