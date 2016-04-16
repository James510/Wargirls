using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour
{
    public float speed;
    public int x, y, z; //coordinates in the camera angle matrix
    public float xMin, xMax,yMin,yMax,zMin,zMax;
    private Vector3 destination;//global location for the cam to go towards
    private float vert;
    private bool jump = false, jumping = false;
    void Start()
    {
        x = 1;
        y = 0;  
        z = 0;
    }

    void Update()
    {
        GameController gc = GameObject.FindWithTag("GameController").GetComponent<GameController>();

        if (Input.GetKey(KeyCode.Q))
            vert = 1;
        else if (Input.GetKey(KeyCode.E))
            vert = -1;
        else
            vert = 0;

        transform.Translate(new Vector3(Input.GetAxis("Horizontal") * speed * Time.deltaTime, vert * speed * Time.deltaTime, Input.GetAxis("Vertical") * speed * Time.deltaTime));
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, xMin, xMax), Mathf.Clamp(transform.position.y, yMin, yMax), Mathf.Clamp(transform.position.z, zMin, zMax));


        if (Input.GetKeyDown(KeyCode.Alpha1) && x > 0) { x--; jump = true; }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && x < 2) { x++; jump = true; }
        else if(Input.GetKeyDown(KeyCode.Alpha3) && y == 0) { y++; jump = true; }
        else if(Input.GetKeyDown(KeyCode.Alpha4) && y != 0) { y--; jump = true; }
        else if (Input.GetKeyDown(KeyCode.Space)) { if (z != 0) z--; else z++; }
        else jump = false;
        destination = gc.camAngles[x, y, z];
        if(jump || jumping)
        {
            jumping = true;
            transform.position = Vector3.Lerp(transform.position, destination, speed * Time.deltaTime);
        }
        if (transform.position == destination)
            jumping = false;
    }
}
