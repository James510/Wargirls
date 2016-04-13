using UnityEngine;
using System.Collections;

public class LanderScript : MonoBehaviour
{
    public Vector3 target;
    public int troops=0;
    public GameObject soldier;
    private bool landed,launch;
	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (!landed)
            transform.Translate(Vector3.down/10);
        if (Vector3.Distance(target, transform.position) < 1)
            landed = true;
        if(landed)
        {
            StartCoroutine("DeployTroops");
        }
        if(launch)
            transform.Translate(-Vector3.down / 10);
    }
    IEnumerator DeployTroops()
    {
        if (troops > 0)
        {
            Instantiate(soldier, new Vector3(transform.position.x + Random.Range(-1.0f, 1.0f), transform.position.y + Random.Range(-1.0f, -0.5f), 0.0f), transform.rotation);
            yield return new WaitForSeconds(0.5f);
            troops -= 1;
            DeployTroops();
        }
        else
        {
            yield return new WaitForSeconds(0.1f);
            launch = true;
        }
    }
    void SetTarget(Vector3 target)
    {
        this.target = target;
    }

}
