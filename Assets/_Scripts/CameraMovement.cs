using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour
{
    public float speed;
    public int x, y, z; //coordinates in the camera angle matrix
    private Vector3 destination;//global location for the cam to go towards

    void Start()
    {
        x = 1;
        y = 0;
        z = 0;
    }
    
	void Update ()
    {
        GameController gc = GameObject.FindWithTag("GameController").GetComponent<GameController>();
        
        if (Input.GetKeyDown(KeyCode.A) && x > 0) x--;
        if (Input.GetKeyDown(KeyCode.D) && x < 2) x++;

        if (Input.GetKeyDown(KeyCode.W) && y == 0) y++;
        if (Input.GetKeyDown(KeyCode.S) && y != 0) y--;

        if (Input.GetKeyDown(KeyCode.Space))
            { if (z != 0) z--; else z++; }

        destination = gc.camAngles[x, y, z];

        transform.position = Vector3.Lerp(transform.position, destination, speed*Time.deltaTime);
    }
}
