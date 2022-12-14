using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laser : MonoBehaviour
{


    [SerializeField]
    private float _speed = 8.0f;

    [SerializeField]
    private bool isEnemyLaser = false;



    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {

        if (isEnemyLaser == false)
        {
            MoveUp();

        }

        else
        {
            MoveDown();
        
        }



    }
    void MoveUp()
    {
        transform.Translate(Vector3.up * _speed * Time.deltaTime);

        //if laser is greater than 8 on y then destroy object
        //destroy laser

        if (transform.position.y > 12.0f)
        {
            if (transform.parent != null)
            {

                Destroy(transform.parent.gameObject);
            }


            Destroy(this.gameObject);

        }
    }
        void MoveDown()
        {
            transform.Translate(Vector3.down * _speed * Time.deltaTime);

        

          if (transform.position.y < -12.0f)
          {
            if (transform.parent != null)
            {

                Destroy(transform.parent.gameObject);
            }


            Destroy(this.gameObject);

          }

            
        }
        public void AssignEnemyLaser()
        {

        isEnemyLaser = true;
        }


    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.tag == "Player" && isEnemyLaser == true)
        {

            Player player = other.GetComponent<Player>();

            if (player != null)
            {
                player.Damage();
            }

        }




    }




}


