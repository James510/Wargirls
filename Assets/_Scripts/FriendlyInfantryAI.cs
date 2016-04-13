using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FriendlyInfantryAI : MonoBehaviour
{
    public float speed;
    public float range;
    public float fireRate;
    public GameObject[] enemies;
    private bool engage;
    private float nextFire;
    void Start()
    {
        nextFire = Time.time;
    }
    void Update()
    {
        transform.Translate(Vector3.right * speed/10);
        for(int i=0;i<enemies.Length;i++)
        {
            if ((Vector3.Distance(enemies[i].transform.position, transform.position) < range) && Time.time > nextFire)
            {
                transform.GetChild(0).GetComponent<ParticleSystem>().Emit(1);
                nextFire = Time.time + fireRate;
            }
        }
    }
    void SetList(GameObject[] other)
    {
        enemies = other;
    }
}