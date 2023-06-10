using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLaser2 : MonoBehaviour

{
    [SerializeField]
    private float _speed = 10.0f;

    [SerializeField]
    private bool isEnemyLaser = false;

    private Player _player;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();

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

        if (transform.position.y < -12f)
        {
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }

            Destroy(this.gameObject);

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
    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.tag == "Player" && isEnemyLaser == true)
        {

            if (_player != null)
            {
                Destroy(this.gameObject);
            }

        }



    }

}






