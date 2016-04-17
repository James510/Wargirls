using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class UnitAI : MonoBehaviour
{
    public bool selected = false;
    public int hp;
    public float speed = 5.0f,reverseSpeed = 4.0f;
    public float maxDistance = 1000;
    public List<GameObject> turrets;
    public GameObject[] enemies;
    public Sprite icon;
    public GameObject explosionFX;
    public GameObject deathExplosionFX;
    public GameObject smokeFX;
    public bool isEnemy;
    private bool hasTarget = false;
    private bool isAlive = true;
    private GameObject unitManager;
    private GameObject gameController;
    private GameObject target;
    private Image healthBar;
    private int originalHP;
    private bool forwards, backwards, retreat;
    private bool dying = false;
    public List<GameObject> wheels;
    private GameObject smoke;

    void Start()
    {
        originalHP = hp;
        unitManager = GameObject.FindGameObjectWithTag("UnitManager");
        gameController = GameObject.FindGameObjectWithTag("GameController");
        StartCoroutine("TargetAquisition", Random.Range(0.1f, 0.8f));
        if (isEnemy)
        {
            transform.tag = "Enemy";
            gameObject.layer = 9;
            foreach (Transform child in transform)
            {
                if (child.gameObject.tag != "GUINon")
                {
                    if (child.tag == "Turret")
                    {
                        child.GetComponent<TurretScript>().isEnemy = true;
                    }
                }
            }
        }
        int turretTemp = 0;
        foreach (Transform child in transform)
        {
            //child.gameObject.layer = 10;
            if (child.tag == "Turret")
            {
                turrets.Add(child.gameObject);
                turretTemp++;
            }
            if (child.name == "Canvas")
            {
                foreach (Transform obj in child.transform)
                {
                    if (obj.name == "HP")
                    {
                        healthBar = obj.GetComponent<Image>();
                    }
                }
            }
            if (child.tag == "Wheels")
            {
                wheels.Add(child.gameObject);
            }
        }

    }

    IEnumerator TargetAquisition(float offset)
    {
        if (isAlive)
        {
            if (enemies.Length > 0)
            {
                //if (!hasTarget)
                //{
                target = enemies[0];
                for (int x = 0; x < enemies.Length; x++)
                {
                    if (Vector3.Distance(transform.position, enemies[x].transform.position) < Vector3.Distance(transform.position, target.transform.position))
                        target = enemies[x];
                }
                foreach (Transform child in transform)
                {
                    if (Vector3.Distance(transform.position, target.transform.position) < 500 && child.tag == "Turret")//set vector3 distance as a local variable instead
                    {
                        //Debug.Log("Firing");
                        hasTarget = true;
                        for (int y = 0; y < turrets.Count; y++)
                            turrets[y].SendMessage("SetTarget", target);
                    }
                    /*if (Vector3.Distance(transform.position, target.transform.position) < 1000 && child.tag == "Turret")
                    {
                        hasTarget = true;
                        for (int y = 0; y < turrets.Count; y++)
                            turrets[y].SendMessage("SetTarget", target);
                    }*/
                }
                //}
                if (hasTarget && target == null)
                {
                    hasTarget = false;
                }
            }
            yield return new WaitForSeconds(2.0f + offset);
            StartCoroutine("TargetAquisition", offset);

        }
    }


    void EnemyList(GameObject[] list)
    {
        enemies = list;
    }

    void Update()
    {
        healthBar.rectTransform.sizeDelta = new Vector2(2.9f * hp/originalHP, 0.4f);
        if (isAlive)
        {
            if (hp < 1)
            {
                isAlive = false;
                if(dying==false)
                {
                    smoke = Instantiate(smokeFX, transform.position, Quaternion.Euler(-90.0f, 0.0f, 0.0f)) as GameObject;
                    smoke.transform.SetParent(transform);
                    GetComponent<Rigidbody>().AddExplosionForce(10000, new Vector3(transform.position.x, transform.position.y - 1, transform.position.z), 10);
                    dying = true;
                }
                smoke.transform.rotation = Quaternion.Euler(-90.0f, 0.0f, 0.0f);
            }
        }
        if (forwards)
            GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * speed);
        if (backwards)
            GetComponent<Rigidbody>().AddRelativeForce(-Vector3.forward * reverseSpeed);
        GetComponent<Rigidbody>().velocity = new Vector3(Mathf.Clamp(GetComponent<Rigidbody>().velocity.x, -5, 5), Mathf.Clamp(GetComponent<Rigidbody>().velocity.y, -5, 5), Mathf.Clamp(GetComponent<Rigidbody>().velocity.z, -5, 5));
    }

    void OnParticleCollision(GameObject other)
    {
        if (isAlive)
        {
            hp -= other.transform.parent.GetComponent<TurretScript>().damage;
            //Instantiate(explosionFX, new Vector3(transform.position.x + Random.Range(-4.0f, 4.0f), transform.position.y + Random.Range(-2.0f, 2.0f), transform.position.z + Random.Range(-4.0f, 4.0f)), transform.rotation);
        }
    }

    void Damage(int dam)
    {
        hp -= dam;
    }

    void OnMouseDown()
    {
        gameController.SendMessage("SelectUnit", gameObject);
    }

    void GoForwards()
    {
        forwards = true;
        backwards = false;
        retreat = false;
        for(int i=0;i<wheels.Count;i++)
        {
            HingeJoint hinge = wheels[i].GetComponent<HingeJoint>();
            JointMotor motor = hinge.motor;
            motor.freeSpin = true;
        }
    }

    void GoBackwards()
    {
        forwards = false;
        backwards = true;
        retreat = false;
        for (int i = 0; i < wheels.Count; i++)
        {
            HingeJoint hinge = wheels[i].GetComponent<HingeJoint>();
            JointMotor motor = hinge.motor;
            motor.freeSpin = true;
        }
    }

    void GoRetreat()
    {
        forwards = false;
        backwards = false;
        retreat = true;
        for (int i = 0; i < wheels.Count; i++)
        {
            HingeJoint hinge = wheels[i].GetComponent<HingeJoint>();
            JointMotor motor = hinge.motor;
            motor.freeSpin = true;
        }
    }

    void GoStop()
    {
        forwards = false;
        backwards = false;
        retreat = false;
        for (int i = 0; i < wheels.Count; i++)
        {
            HingeJoint hinge = wheels[i].GetComponent<HingeJoint>();
            JointMotor motor = hinge.motor;
            motor.freeSpin = false;
        }
    }
}