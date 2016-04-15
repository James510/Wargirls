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
    public float bulletScale=1.0f;
    public int bulletBurst;
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
                child.GetComponent<ParticleSystem>().startSize = bulletScale;
                
            }
            if(child.tag == "FireFX")
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
            Debug.Log("hit");
            foreach (Transform child in transform)
            {
                Debug.Log(child.name);
                if (child.tag == "Barrel")
                {
                    Debug.Log("Setting");
                    ParticleSystem temp = child.GetComponent<ParticleSystem>();
                    ParticleSystem.CollisionModule temp2 = temp.collision;
                    temp2.collidesWith = 256;
                }
            }
        }
        
    }
    void Update()
    {
        //Debug.Log(Vector3.Angle(transform.forward, target.transform.position - transform.position));
        if(isAlive)
        {
            if (hasTarget && target != null)
            {


                /*targetPos = new Vector3(target.transform.position.x, 0.0f, target.transform.position.z) - new Vector3(transform.position.x,0.0f,transform.position.z);
                rot = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(targetPos), rotSpeed * Time.deltaTime);
                transform.rotation = rot;*/

                //targetPos = new Vector3(target.transform.position.x, 0.0f, target.transform.position.z) - new Vector3(transform.position.x, 0.0f, transform.position.z);
                //rot = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(targetPos,transform.parent.up), rotSpeed * Time.deltaTime);
                //transform.rotation = rot;
                targetPos = transform.InverseTransformPoint(target.transform.position);
                transform.Rotate(Vector3.up * rotSpeed * Mathf.Sign(targetPos.x) * Time.deltaTime);

                for (int i=0;i<barrels.Count;i++)
                {
                    //dir = target.transform.position - transform.position;
                    //float tgtAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                    targetPos = barrels[i].transform.InverseTransformPoint(target.transform.position);
                    barrels[i].transform.Rotate(-Vector3.right * rotSpeed * Mathf.Sign(targetPos.y+2) * Time.deltaTime);
                    //barrels[i].transform.Rotate(barrels[i].transform.right, eulers.x);
                    /*dir = target.transform.position - transform.position;
                    float tgtAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                    if (Mathf.Abs(tgtAngle) >= 90)
                    {
                        //Debug.Log("active");
                        dir = target.transform.InverseTransformDirection(dir);
                        tgtAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

                        //barrels[i].transform.localRotation = Quaternion.Euler(Mathf.Clamp(-angle * 2, -highangle, -lowangle), 0.0f, 0.0f);
                        rot = Quaternion.Slerp(barrels[i].transform.localRotation, Quaternion.Euler(tgtAngle - 180, 0.0f, 0.0f), rotSpeed * Time.deltaTime);
                        barrels[i].transform.localRotation = rot;
                    }
                    else
                    {
                        rot = Quaternion.Slerp(barrels[i].transform.localRotation, Quaternion.Euler(Mathf.Clamp(-tgtAngle, lowangle, highangle), 0.0f, 0.0f), rotSpeed * Time.deltaTime);
                        barrels[i].transform.localRotation = rot;
                        //barrels[i].transform.localRotation = Quaternion.Euler(Mathf.Clamp(-angle * 2, lowangle, highangle), 0.0f, 0.0f);
                    }*/


                    //barrels[i].transform.rotation = Quaternion.Slerp(barrels[i].transform.rotation, Quaternion.Euler(-angle, 0.0f, 0.0f), rotSpeed * Time.deltaTime);
                    //Debug.Log(Vector3.Angle(barrels[i].transform.forward, target.transform.position - barrels[i].transform.position) + "tgtangle");
                    //Debug.Log(angle + "angle");
                    if (Time.time > nextFire && (Vector3.Angle(barrels[i].transform.forward, target.transform.position - barrels[i].transform.position) < angle)) //Recalculate this for target aquisition
                    {
                        for (int x = 0; x < barrels.Count; x++)
                        {
                            transform.parent.GetComponent<Rigidbody>().AddForceAtPosition(-transform.forward*50, transform.position);
                            barrels[x].GetComponent<ParticleSystem>().Emit(bulletBurst);
                            flash.enabled = true;
                            StartCoroutine("GunFlash");
                            smoke.Emit(100);
                            //Debug.Log("firing");
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
    /*
    void OnTriggerStay(Collider other) //The old method of tracking
    {
        //Debug.Log("Hit");
        
        if ((other.gameObject.CompareTag("Enemy") && !isEnemy) || (other.gameObject.CompareTag("Friend") && isEnemy))//Detect, track and fire at enemy
        {
            //Debug.Log("Detected");
            if (!hasTarget)//There is something in here that causes tremendous lag whenever a ship aquires a target.
            {
                hasTarget = true;
                target = other.gameObject;
            }
            if (hasTarget && target == null)
            {
                hasTarget = false;
            }
        }
        if (hasTarget)
        {
            Vector3 targetPos = target.transform.position - transform.position;
            Quaternion rot = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(targetPos), rotSpeed * Time.deltaTime);
            transform.rotation = rot;
            //transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z); //Lock turret to an axis, doesn't work yet.
            if (Time.time > nextFire)
            {
                GameObject shot = Instantiate(bullet, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.Euler(transform.rotation.eulerAngles.x + Random.Range(-inaccuracy, inaccuracy), transform.rotation.eulerAngles.y + Random.Range(-inaccuracy, inaccuracy), transform.rotation.eulerAngles.z + Random.Range(-inaccuracy, inaccuracy))) as GameObject;

                Rigidbody srb = shot.GetComponent<Rigidbody>();
                srb.velocity = transform.parent.GetComponent<Rigidbody>().velocity;
                srb.AddRelativeForce(Vector3.forward * 6000);
                shot.GetComponent<BulletScript>().damage = damage;
                if (other.gameObject.CompareTag("Friend") && isEnemy)
                    shot.GetComponent<BulletScript>().isEnemy = true;
                else
                    shot.GetComponent<BulletScript>().isEnemy = false;
                shot.transform.localScale += new Vector3(bulletScale, bulletScale, bulletScale * 10);
                nextFire = Time.time + fireRate; //Determines fire rate
            }
        }
    }
    */
}