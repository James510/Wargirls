using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour
{
    public float speed;
    public float xMin, xMax;

	void Update ()
    {
        transform.Translate(new Vector3(Input.GetAxis("Horizontal") * speed *Time.deltaTime,0.0f,0.0f));
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, xMin, xMax), 5.0f, -10.0f);
    }
}
