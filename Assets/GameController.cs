using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{
    public GameObject Lander;
	// Use this for initialization
	void Start ()
    {
	
	}
	    
	// Update is called once per frame
	void Update ()
    {
	    if(Input.GetKeyDown(KeyCode.Alpha1))
        {
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
        }
	}
}
