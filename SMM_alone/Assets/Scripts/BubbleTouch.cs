using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BubbleTouch : MonoBehaviour {
    Vector3 touch_start_pos;
    Vector3 touch_end_pos;
    Vector3 click_pos;

    public static bool drag_flag = false;
    public static bool touch_flag = false;

    public GameObject t;

    private GameObject click_sphere;

    private Sphere sphere;
    GameObject Collide_Sphere;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (!touch_flag)
        {
            if (Input.GetMouseButtonDown(0))
            {
                touch_start_pos = Input.mousePosition;


                Ray ray = Camera.main.ScreenPointToRay(touch_start_pos);

                //Rayの長さ
                float maxDistance = 10;
                RaycastHit2D hit = Physics2D.Raycast((Vector2)ray.origin, (Vector2)ray.direction, maxDistance);

                if (hit.collider)
                {
                    drag_flag = true;
                    click_sphere = hit.collider.gameObject;
                    click_sphere.layer = LayerMask.NameToLayer("Drag_Sphere");
                    sphere = click_sphere.GetComponent<Sphere>();
                    sphere.start_time = Time.time;
                    click_sphere.GetComponent<CircleCollider2D>().isTrigger = true;

                }
            }

            if (Input.GetMouseButton(0) && drag_flag == true)
            {
                click_pos = Input.mousePosition;
                Drag(click_pos);
                /*if (drag_flag == false)
                {
                    if (hit.collider.gameObject == null || hit.collider.gameObject != Collide_Sphere)
                        click_flag = false;
                }
                if (click_flag == true)
                {
                    sphere.touch_time = Time.time - sphere.start_time;
                    if (sphere.touch_time > 0.5)
                    {
                        click_sphere.layer = LayerMask.NameToLayer("Drag_Sphere");
                        sphere.drag(Camera.main.ScreenToWorldPoint(click_pos));
                    }

                }
                */
            }

            if (Input.GetMouseButtonUp(0) == true)
            {
                touch_end_pos = Input.mousePosition;
                if (touch_start_pos == touch_end_pos && drag_flag == true)
                    Click();
                if (drag_flag)
                    sphere.end_time = Time.time;

                if (drag_flag)
                {
                    if (sphere.end_time - sphere.start_time < 0.3 && touch_start_pos != touch_end_pos)
                    {
                        Flick();
                    }
                }
                drag_flag = false;
                /*
                click_sphere.layer = LayerMask.NameToLayer("Collide_Sphere");
                if (click_flag == true && drag_flag == false)
                {
                    click_sphere.GetComponent<AudioSource>().Play();
                    string post_text = click_sphere.transform.GetChild(0).GetComponent<TextMesh>().text;
                    if (post_text != FileOut.word)
                    {
                        GetComponent<NetWork>().connectionStart(post_text);

                    }
                    StartCoroutine("sleep");

                }
                if (drag_flag == true)
                {
                    sphere.reset();
                }
                click_sphere.GetComponent<CircleCollider2D>().isTrigger = false;
                */
            }
        }

        
	}
    IEnumerator sleep()
    {
        while (true)
        {
            if (NetWork.reply == true)
            {
                //click_sphere.GetComponent<AudioSource>().Play();
                click_sphere.GetComponent<Sphere>().disappear();
                NetWork.reply = false;
            }
                yield return new WaitForSeconds(0.1f);  //10秒待つ
        }
        

    }
    void Click()
    {
        drag_flag = false;
        GetComponent<AudioSource>().Play();
        string post_text = click_sphere.transform.GetChild(0).GetComponent<TextMesh>().text;
        t.GetComponent<LogText>().log_add(post_text);
        //if (post_text != FileOut.word)
        //{
          GetComponent<NetWork>().connectionStart(post_text);
        //}
        StartCoroutine("sleep");
    }
    void Drag(Vector3 click_pos)
    {
        sphere.drag(Camera.main.ScreenToWorldPoint(click_pos));
    }

    void Flick()
    {
        drag_flag = false;
        click_sphere.layer = LayerMask.NameToLayer("Sphere");
        Vector3 direction = touch_end_pos - touch_start_pos;
        sphere.flick(direction);
        
    }




}
