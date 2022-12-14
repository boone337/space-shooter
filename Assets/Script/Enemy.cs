using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : MonoBehaviour

{
    [SerializeField]
    private float _fireRate = 3.0f;

    [SerializeField]
    private float _canFire = -1;

    [SerializeField]
    private GameObject _laserPrefab;

    [SerializeField]
    private AudioSource _audioSource;

    [SerializeField]
    private float _speed = 4.0f;

    private Player _player;

    //create handle to animator component
    private Animator _anim;


    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();

        _audioSource = GetComponent<AudioSource>();

        //null check player
        //assign component to anim
        if (_player == null)
        {
            Debug.LogError("The Player is Null.");

        }
        _anim = GetComponent<Animator>();

        if (_anim == null)
        {
            Debug.LogError("Animator is Null");
        }
    }

    // Update is called once per frame
    void Update()
    {

        CalculateMovement();

        if (Time.time > _canFire)
        {
            _fireRate = Random.Range(3f, 7F);
            _canFire = Time.time + _fireRate;
            GameObject enemyLaser = Instantiate(_laserPrefab, transform.position, Quaternion.identity);
            laser[] lasers = enemyLaser.GetComponentsInChildren<laser>();

            for (int i = 0; i < lasers.Length; i++)
            {
                lasers[i].AssignEnemyLaser();

            }

        }



    }


    void CalculateMovement()
    {

        //move down at 4 meters per second
        transform.Translate(Vector3.down * _speed * Time.deltaTime);


        //if bottom screen respawn with a new random position

        if (transform.position.y < -9f)
        {
            transform.position = new Vector3(Random.Range(-8f, 8f), 7, 0);

        }




    }

    private void OnTriggerEnter2D(Collider2D other)

    {
        Debug.Log("Hit: " + other.transform.name);

        //if other is player  
        //damage the player 
        //destroy us

        if (other.tag == "Player")
        {  //damage player  
            Player player = other.transform.GetComponent<Player>();

            if (player != null)
            {
                player.Damage();

            }
            //trigger anim to set explosion
            _anim.SetTrigger("On Enemy Death");
            _speed = 0;
            _audioSource.Play();
            Destroy(this.gameObject, 1f);

        }

        if (other.tag == "laser")
        {
            Destroy(other.gameObject);
            if (_player != null)
            {
                _player.AddScore(10);
            }
            //trigger anim 
            _anim.SetTrigger("On Enemy Death");
            _speed = 0;
            _audioSource.Play();

            Destroy(GetComponent<Collider2D>());   //this will cause double explosions even after death
            Destroy(this.gameObject, 1f);



            // Player player = GameObject.Find("Player").GetComponent<Player>();
            //this was mad global at the top so it is not needed every where

            //add 10 to score


        }

        //create method to add 10 to score
        //communicate with ui to add score
        //if other islaser  //destroy us // laser

    }





}








