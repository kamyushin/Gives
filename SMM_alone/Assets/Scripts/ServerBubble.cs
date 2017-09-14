using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerBubble : MonoBehaviour {
    public GameObject Sphere;
    int width = Screen.width;
    int height = Screen.height;
    Vector2 min;
    Vector2 max;
    

    // Use this for initialization
    void Start () {
        min = Camera.main.ScreenToWorldPoint(new Vector3(0, 0));
        max = Camera.main.ScreenToWorldPoint(new Vector3(width, height));
        

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void BubblesMake(ArrayList data)
    {
        
        foreach(string s in data)
        {
            BubbleMake(s);
        }
    }

    public void BubbleMake(string s )
    {
        GameObject g = Instantiate(Sphere,new Vector3(Random.Range(min.x,max.x),Random.Range(min.y,max.y),0),Quaternion.identity);
        g.transform.GetChild(0).GetComponent<TextMesh>().text = s;
    }
}
