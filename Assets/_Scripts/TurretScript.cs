using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TurretScript : MonoBehaviour
{
    public float angle = 10;
    public float rotSpeed = 360.0f;
    public float fireRate;
    public float inaccuracy;
    public float lowangle, highangle;
    public int damage;
    public bool isEnemy = false;
    public float bulletScale = 1.0f;
    public int bulletBurst;
    public GameObject apround;
    private float nextFire;
    private bool hasTarget;
    private GameObject target;
    private Light flash;
    private ParticleSystem smoke;
    private bool isAlive = true;
    private float randX, randY, randZ;
    public List<GameObject> barrels = new List<GameObject>();
    private Vector3 targetPos;
    private Vector3 dir;
    private Quaternion rot;


    void Start()
    {
        foreach (Transform child in transform)
        {
            if (child.tag == "Barrel")
            {
                barrels.Add(child.gameObject);
                //child.GetComponent<ParticleSystem>().startSize = bulletScale;

            }
            if (child.tag == "FireFX")
            {
                flash = child.GetComponent<Light>();
                smoke = child.GetComponent<ParticleSystem>();
            }
        }
        //Debug.Log(LayerMask.GetMask("Friendly"));
        nextFire = Time.time;
        StartCoroutine("DelayStart");
        //GetComponent<ParticleSystem>().shape = inaccuracy;
    }
    IEnumerator DelayStart()
    {
        yield return new WaitForSeconds(1.0f);
        if (isEnemy)
        {
            foreach (Transform child in transform)
            {
                if (child.tag == "Barrel")
                {
                    //ParticleSystem temp = child.GetComponent<ParticleSystem>();
                    //ParticleSystem.CollisionModule temp2 = temp.collision;
                    //temp2.collidesWith = 256;
                }
            }
        }

    }
    void Update()
    {
        if (isAlive)
        {
            if (hasTarget && target != null)
            {
                targetPos = transform.InverseTransformPoint(target.transform.position);
                transform.Rotate(Vector3.up * rotSpeed * Mathf.Sign(targetPos.x) * Time.deltaTime);

                for (int i = 0; i < barrels.Count; i++)
                {
                    float arc = 0;
                    arc = Vector3.Distance(target.transform.position, transform.position)/10;
                    targetPos = barrels[i].transform.InverseTransformPoint(target.transform.position);
                    barrels[i].transform.Rotate(-Vector3.right * rotSpeed * Mathf.Sign(targetPos.y + arc) * Time.deltaTime);
                    if (Time.time > nextFire && (Vector3.Angle(barrels[i].transform.forward, target.transform.position - barrels[i].transform.position) < angle)) //Recalculate this for target aquisition
                    {
                        for (int x = 0; x < barrels.Count; x++)
                        {
                            transform.parent.GetComponent<Rigidbody>().AddForceAtPosition(-transform.forward * 50, transform.position);
                            //barrels[x].GetComponent<ParticleSystem>().Emit(bulletBurst);
                            GameObject round = Instantiate(apround, new Vector3(barrels[x].transform.position.x, barrels[x].transform.position.y, barrels[x].transform.position.z), Quaternion.Euler(transform.rotation.eulerAngles.x+Random.Range(-1.0f,1.0f), 0.0f, transform.rotation.eulerAngles.z + Random.Range(-1.0f, 1.0f))) as GameObject;
                            round.GetComponent<Rigidbody>().AddRelativeForce(barrels[x].transform.forward * 500);
                            flash.enabled = true;
                            StartCoroutine("GunFlash");
                            smoke.Emit(100);
                        }
                        nextFire = Time.time + fireRate;
                    }
                }
            }
            else
            {
                hasTarget = false;
            }
        }
    }
    IEnumerator GunFlash()
    {
        yield return new WaitForSeconds(0.1f);
        flash.enabled = false;
    }
    void SetTarget(GameObject tgt)
    {
        target = tgt;
        hasTarget = true;
    }
}