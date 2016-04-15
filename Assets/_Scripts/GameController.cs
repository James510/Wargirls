using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{
    public GameObject lander;
    public GameObject tank;
    public GameObject hq;
    public int hp;
    public int ap;
	// Use this for initialization
	void Start ()
    {
        hp = 100;
        ap = 100;
	}
	    
	// Update is called once per frame
	void Update ()
    {
	    if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            GameObject clone = Instantiate(tank, hq.transform.position, transform.rotation) as GameObject;
            /*dropship
            //Debug.Log("hit");
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if(hit.transform.tag=="Floor")
                {
                    Debug.Log(hit.transform.position.x);
                    GameObject clone = Instantiate(Lander, new Vector3(hit.point.x, 20, hit.point.z), transform.rotation) as GameObject;
                    clone.SendMessage("SetTarget", hit.point);
                }
            }
            */
        }
	}
}
