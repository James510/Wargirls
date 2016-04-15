using UnityEngine;
using System.Collections;

public class LockRotation : MonoBehaviour
{
	void Update ()
    {
        transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
	}
}
