using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cannonScript : MonoBehaviour
{
    [SerializeField]
    private float _fireRate = 0.2f;

    [SerializeField]
    private float _canFire = -1f;

    [SerializeField]
    private float _speed = 1f;


    [SerializeField]
    private GameObject Weapon;

   


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Time.time > _canFire)
        {

            _fireRate = Random.Range(1f, 5f);
            _canFire = Time.time + _fireRate;

            shoot(); 
               
        }
      
        
    }


    public void shoot()
    {
   
          Instantiate(Weapon, transform.position + new Vector3(0f, -1f, 0), transform.rotation);
    }



}






