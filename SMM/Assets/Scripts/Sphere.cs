using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Sphere : MonoBehaviour {
    public static int child_count = 6;
    public GameObject child_sphere;

    public int speed = 2;
    [HideInInspector]
    public float start_time;
    [HideInInspector]
    //public float finish_time;
    public float end_time;

    private Rigidbody2D rigid;
    private Vector3 start_pos;

    private bool stop = false;

    private bool flick_flag = false;

    private BubbleManager bm;

    public int stage;
    
	// Use this for initialization
	void Start () {
        bm = GameObject.Find("manager").GetComponent<BubbleManager>();
        rigid = GetComponent<Rigidbody2D>();
        if (SceneManager.GetActiveScene().name == "Game")
        {
            if (WordList.count > 0)
                this.transform.GetChild(0).GetComponent<TextMesh>().text = WordList.push();
        }
        StartCoroutine("Layer");

	}

    IEnumerator Layer()
    {
        yield return new WaitForSeconds(0.5f);
        gameObject.layer = LayerMask.NameToLayer("Collide_Sphere");
        yield break;
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        if (flick_flag == false)
        {
            //rigid.AddForce(new Vector2(0, 1) * 100);
            if (Mathf.Abs(rigid.velocity.x) > 0.1 || Mathf.Abs(rigid.velocity.y) > 0.1)
                rigid.AddForce(rigid.velocity * -2);
            else if (stop == false)
            {
                stop = true;
                rigid.velocity = new Vector2(0, 0);
                rigid.AddForce(new Vector2(0, 1) * 100);
                start_pos = transform.position;
            }


            if (stop == true)
            {
                if (transform.position.y > start_pos.y)
                {
                    rigid.AddForce(new Vector2(0,-1));
                }
                else
                    rigid.AddForce(new Vector2(0, 1));
            }
        }
        else
        {
            if (!GetComponent<Renderer>().isVisible)
            {
                Debug.Log(true);
                string post_text = transform.GetChild(0).GetComponent<TextMesh>().text;
                GameObject.Find("manager").GetComponent<NetWork>().postServer(post_text);
                Destroy(this.gameObject);
            }
        }
    }

    

    public void disappear()
    {
        float degree = 360 / child_count;
        for (int i = 0; i < child_count; i++)
        {
            GameObject g = born();
            g.GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Sin((degree * i+Random.Range(-20,20))*Mathf.Deg2Rad),Mathf.Cos((degree *i+ Random.Range(-20,20))*Mathf.Deg2Rad)) * speed;
        }
        

        Destroy(this.transform.gameObject);
    }

    void OnTriggerEnter2D(Collider2D c)
    {
        string layerName = LayerMask.LayerToName(c.gameObject.layer);
        if (layerName == "Drag_Sphere")
        {
            BubbleTouch.drag_flag = false;
            Sphere c_Sphere;
            string c1, c2;
            c1 = c.transform.GetChild(0).GetComponent<TextMesh>().text;
            c2 = this.transform.GetChild(0).GetComponent<TextMesh>().text;

            c_Sphere = c.GetComponent<Sphere>();
            int up_stage = (c_Sphere.stage > stage) ? c_Sphere.stage : stage;
            marge(c1, c2,up_stage);
            Destroy(c.gameObject);
            Destroy(this.gameObject);
        }
    }

    private GameObject born()
    {
        Sphere s;
        GameObject g = Instantiate(child_sphere, transform.position, Quaternion.identity);
        g.transform.localScale = new Vector3(1.5f, 1.5f, 1);
        g.layer = LayerMask.NameToLayer("Sphere");
        s = g.GetComponent<Sphere>();
        s.stage = stage + 1;
        if (bm.max_stage < s.stage)
        {
            string stage_name;
            bm.max_stage = s.stage;
            switch (bm.max_stage % 3)
            {
                case 1:
                    stage_name = "First_Spheres";
                    break;
                case 2:
                    stage_name = "Second_Spheres";
                    break;
                case 0:
                    stage_name = "Third_Spheres";
                    break;
                default:
                    stage_name = "First_Spheres";
                    break;
            }
            foreach (Transform n in GameObject.Find(stage_name).transform)
            {
                Destroy(n.gameObject);
            }
        }
        switch (s.stage % 3 )
        {
            case 1:
                g.transform.parent = GameObject.Find("First_Spheres").transform;
                g.GetComponent<MeshRenderer>().material = bm.red;
                break;
            case 2:
                g.transform.parent = GameObject.Find("Second_Spheres").transform;
                g.GetComponent<MeshRenderer>().material = bm.yellow;
                break;
            case 0:
                g.transform.parent = GameObject.Find("Third_Spheres").transform;
                g.GetComponent<MeshRenderer>().material = bm.green;
                break;
        }
        g.GetComponent<CircleCollider2D>().isTrigger = false;
        return g;
    }


    private GameObject marge(string c1,string c2, int stage)
    {
        GameObject g = Instantiate(child_sphere, transform.position, Quaternion.identity);
        g.layer = LayerMask.NameToLayer("Sphere");
        switch (stage % 3)
        {
            case 1:
                g.transform.parent = GameObject.Find("First_Spheres").transform;
                g.GetComponent<MeshRenderer>().material = bm.red;
                break;
            case 2:
                g.transform.parent = GameObject.Find("Second_Spheres").transform;
                g.GetComponent<MeshRenderer>().material = bm.yellow;
                break;
            case 0:
                g.transform.parent = GameObject.Find("Third_Spheres").transform;
                g.GetComponent<MeshRenderer>().material = bm.green;
                break;
        }
        g.GetComponent<CircleCollider2D>().isTrigger = false;
        g.transform.GetChild(0).GetComponent<TextMesh>().text = c1 + c2;
        g.GetComponent<Sphere>().stage = stage;
        return g;
    }

    public void drag(Vector3 click_pos)
    {
        Vector3 po = new Vector3(click_pos.x, click_pos.y, 0);
        BubbleTouch.drag_flag = true;
        
        transform.position = po;
    }

    public void flick(Vector3 direction)
    {
        rigid.velocity = direction*0.2f;
        flick_flag = true;

    }
}
