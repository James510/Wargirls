using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UnitAI : MonoBehaviour
{
    public int hp;
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
        //Debug.Log("Hit");
        hp -= other.transform.parent.GetComponent<UnitAI>().damage;
    }

}