using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class init_game : MonoBehaviour {
    public GameObject g;
    // Use this for initialization
    void Start()
    {
        GameObject s = Instantiate(g, g.transform.position, Quaternion.identity);
        s.transform.GetChild(0).GetComponent<TextMesh>().text = WordList.push();
        s.transform.localScale = new Vector3(3, 3, 1);
        s.transform.parent = GameObject.Find("Spheres").transform;
        s.GetComponent<Sphere>().stage = 0;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
