using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleManager : MonoBehaviour {
    [HideInInspector]
    public int max_stage = 0;
    public Material green;
    public Material red;
    public Material yellow;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public int up_stage()
    {
        if (max_stage++ > 3) {
            max_stage = 1;
        }

        return max_stage;
    }
}
