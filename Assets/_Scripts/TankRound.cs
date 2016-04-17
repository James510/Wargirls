using UnityEngine;
using System.Collections;

public class TankRound : MonoBehaviour
{
    public bool isHE,isAP;
    public int damage=0,explosiveDmg=0;
    public GameObject sparkFx;
    public bool isEnemy;
    private bool hit=false;

    void Start()
    {
        if (isEnemy)
            gameObject.layer = 12;
        else
            gameObject.layer = 13;
    }

	void Update ()
    {
        transform.LookAt(transform.position + GetComponent<Rigidbody>().velocity);
    }

    void OnCollisionEnter(Collision other)
    {
        if(hit==false)
        {
            int rand = Random.Range(0, 1);
            if (other.gameObject.GetComponent<UnitAI>() != null)
            {
                other.gameObject.SendMessage("Damage", damage);
                GameObject sparks = Instantiate(sparkFx, transform.position, transform.rotation) as GameObject;
                sparks.GetComponent<ParticleSystem>().Emit(100);
                if (rand == 1)
                {
                    other.gameObject.SendMessage("Damage", damage * 2); //critical
                    sparks.GetComponent<ParticleSystem>().Emit(200);
                    Destroy(gameObject);
                }
                hit = true;
            }
            StartCoroutine("Detonate");
        }
    }   

    IEnumerator Detonate()
    {
        if (isHE)
        {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, 5);
            for (int i = 0; i < hitColliders.Length; i++)
            {
                if (hitColliders[i].GetComponent<Rigidbody>() != null)
                    hitColliders[i].GetComponent<Rigidbody>().AddExplosionForce(200, transform.position, 5);
                if (hitColliders[i].GetComponent<UnitAI>() != null)
                    hitColliders[i].SendMessage("Damage", damage);
            }
        }
        if(isAP)
        {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, 1);
            for (int i = 0; i < hitColliders.Length; i++)
            {
                if (hitColliders[i].GetComponent<Rigidbody>() != null)
                    hitColliders[i].GetComponent<Rigidbody>().AddExplosionForce(500, transform.position, 1);
                if (hitColliders[i].GetComponent<UnitAI>() != null)
                    hitColliders[i].SendMessage("Damage", damage);
            }
        }    
        yield return new WaitForSeconds(0.1f);
        Destroy(gameObject);
    }
}
