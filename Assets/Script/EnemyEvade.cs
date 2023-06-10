using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class EnemyEvade : MonoBehaviour
{
    //private Player _player;

    private Enemy _enemy;


    // Start is called before the first frame update
    void Start()
    {
       _enemy  = GameObject.Find("Enemy").GetComponent<Enemy>();

    }

    // Update is called once per frame
    void Update()
    {


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "laser")
        {

            transform.GetComponentInParent<Enemy>().EvadeLaser();
           // StartCoroutine(DetectCooldown());

        }

    }

  /*  IEnumerator DetectCooldown()
    {
        transform.gameObject.SetActive(false);
        yield return new WaitForSeconds(1);
        transform.gameObject.SetActive(true);
    }  */






}   









