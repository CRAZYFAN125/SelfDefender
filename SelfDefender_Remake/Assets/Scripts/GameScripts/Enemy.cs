using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform target;
    public float speed = 15f;
    public float damage = 10.5f;
    public float health = 15;
    //private bool isDamaged = false;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Baza").transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, target.position) <= 0.2f)
        {
            //if (!isDamaged)
            //{
                BazaScript.instance.Damage(damage);
            //    isDamaged = true;
            //}
            Destroy(gameObject);
            return;
        }
    }

    public void Hit(float amount)
    {
        health -= amount;
        if (health<=0f)
        {
            Destroy(gameObject);
        }
    }
}
