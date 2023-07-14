using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingMissle : MonoBehaviour
{

    [SerializeField]
    private float _speed = 10.0f;

    [SerializeField]
    private float _rotateSpeed = 400f;

    private Transform _target;

    private Rigidbody2D rb;

    private AudioSource _audioSource;


    // Start is called before the first frame update
    void Start()
    {
        Enemy _enemy = transform.GetComponent<Enemy>();

        rb = GetComponent<Rigidbody2D>();

         _target = GameObject.FindGameObjectWithTag("enemy").transform;

        transform.Translate(Vector3.up * _speed * Time.deltaTime);

        _audioSource = GetComponent<AudioSource>();

        if (_audioSource == null)
        {
            Debug.LogError("AudioSource is null");
        }
    }

    // Update is called once per frame
     
    void FixedUpdate()
    {
      
        if (_target == null) { transform.Translate(Vector3.up * _speed * Time.deltaTime); }

        else
        {
            Vector2 direction = (Vector2)_target.position - rb.position;

            direction.Normalize();

            float rotateAmount = Vector3.Cross(direction, transform.up).z; //right left

            rb.angularVelocity = -rotateAmount * _rotateSpeed; // spelling spelling spelling!!! 

            rb.velocity = transform.up * _speed;

            _audioSource.Play();
        }
        
        if (transform.position.y > 10f)
        {

            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);

            }
            Destroy(this.gameObject);

        } 

        if (transform.position.x > 17f)
        {

            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }
            Destroy(this.gameObject);
        }

        else if(transform.position.x<-17f && transform.parent != null)
        { 
            
           
                
         }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Hit" + other.transform.name);  //transform is for the root, name is for the who hit the enemy

        if (other.tag == "enemy")
        {
            Debug.Log(" homing hit enemy");
           
            //other.transform.GetComponent<player>().Damage();  To be able to null check the above method is better
            if ("enemy" != null)
            {
                Destroy(this.gameObject);

            }
        }
    }

}
