using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour
{
    public GameObject lander;
    public GameObject tank;
    public GameObject hq;
    public int hp;
    public int ap;
    public Vector3[,,] camAngles;
 
	// Use this for initialization
	void Start ()
    {
        hp = 100;
        ap = 100;

        camAngles = new Vector3[3, 2, 2];//x,y,z positions

        //Land angles
        camAngles[0, 0, 0] = new Vector3(-28f, 3f, -18f);
        camAngles[1, 0, 0] = new Vector3(-16f,3f,-18f);
        camAngles[2, 0, 0] = new Vector3(-3f, 3f, -18f);

        camAngles[0, 1, 0] = new Vector3(-28f, 9f, -18f);
        camAngles[1, 1, 0] = new Vector3(-16f, 9f, -18f);
        camAngles[2, 1, 0] = new Vector3(-3f, 9f, -18f);

        //Sea angles
        camAngles[0, 0, 1] = new Vector3(-28f, 3f,-2f);
        camAngles[1, 0, 1] = new Vector3(-16f, 3f, -2f);
        camAngles[2, 0, 1] = new Vector3(-3f, 3f, -2f);

        camAngles[0, 1, 1] = new Vector3(-28f, 9f, -2f);
        camAngles[1, 1, 1] = new Vector3(-16f, 9f, -2f);
        camAngles[2, 1, 1] = new Vector3(-3f, 9f, -2f);
    }
	   
}
