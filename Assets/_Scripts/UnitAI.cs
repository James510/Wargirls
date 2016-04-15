using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class UnitAI : MonoBehaviour
{
    /*public int hp;
    public int damage;
    public float speed;
    public float range;
    public float fireRate;
    public GameObject[] enemies;
    public bool isEnemy;
    private bool engage;
    private float nextFire;
    void Start()
    {
        if (isEnemy)
            transform.tag = "Enemy";
        nextFire = Time.time;
    }
    void Update()
    {

        transform.Translate(Vector3.right * speed / 10);


        for (int i = 0; i < enemies.Length; i++)
        {
            if ((Vector3.Distance(enemies[i].transform.position, transform.position) < range) && Time.time > nextFire)
            {
                transform.GetChild(0).GetComponent<ParticleSystem>().Emit(1);
                nextFire = Time.time + fireRate;
            }
        }
        if (hp < 0)
        Destroy(this.gameObject);
    }
    void SetList(GameObject[] other)
    {
        enemies = other;
    }
    void OnParticleCollision(GameObject other)
    {
        Debug.Log("Hit");
        hp -= other.transform.parent.GetComponent<UnitAI>().damage;
    }*/

    public bool selected = false;
    public int hp;
    public float speed = 5.0f;
    public float maxDistance = 1000;
    public List<GameObject> turrets;
    public GameObject[] enemies;
    public GameObject unitManager;
    public GameObject explosionFX;
    public GameObject deathExplosionFX;
    public bool isEnemy;
    private bool hasTarget = false;
    private bool isAlive = true;
    private GameObject target;
    private Image healthBar;
    private int originalHP;

    // Update is called once per frame
    void Start()
    {
        originalHP = hp;
        unitManager = GameObject.FindGameObjectWithTag("UnitManager");
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
            child.gameObject.layer = 10;
            if (child.tag == "Turret")
            {
                turrets.Add(child.gameObject);
                turretTemp++;
            }
            if (child.name == "Canvas")
            {
                foreach (Transform health in child.transform)
                {
                    if (health.name == "HP")
                    {
                        healthBar = health.GetComponent<Image>();
                    } 
                }
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
        if (speed < 0)
            speed = 0;
        if (isAlive)
        {
            if (hp < 1)
            {
                Instantiate(deathExplosionFX, transform.position, transform.rotation);
                foreach (Transform child in transform)
                {
                    child.SendMessage("Deactivate");
                }
                isAlive = false;
            }
        }
    }

    void OnParticleCollision(GameObject other)
    {
        if (isAlive)
        {
            hp -= other.transform.parent.GetComponent<TurretScript>().damage;
            //Instantiate(explosionFX, new Vector3(transform.position.x + Random.Range(-4.0f, 4.0f), transform.position.y + Random.Range(-2.0f, 2.0f), transform.position.z + Random.Range(-4.0f, 4.0f)), transform.rotation);
        }
    }
}