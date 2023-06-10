using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cartman : MonoBehaviour
{
    [SerializeField]
    private AudioSource _audioSource;

    private Player _player;


    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();

        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

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


            _audioSource.Play();


        }

        if (other.tag == "laser")
        {
            Destroy(other.gameObject);
            if (_player != null)
            {
                _player.AddScore(10);
            }

            _audioSource.Play();



        }
    }
}
