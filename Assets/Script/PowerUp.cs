using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
    
{
    private float _speed = 3.0f;

    //ID for Powerups
    
    [SerializeField] //0= Triple Shot 1= Speed 2= Shield
    private int _powerupID;

    [SerializeField]
    private AudioSource _audioSource;

    [SerializeField]
    private AudioClip  _audioClip;

    private Animator _anim;

    private GameObject _player;

    private bool isMagnetActive = false;
    

    // Start is called before the first frame update
    void Start()
    {

        _player = GameObject.FindGameObjectWithTag("Player");

       _anim = GetComponent<Animator>();

       if (_anim == null)
       {
           Debug.LogError("_anim is  null");

       }

        

        // _audioSource = GetComponent<AudioSource>(); not needed with audio clip
        //transform.position = new Vector3(0, 11.0f, 0);  This code here will conflict with spawn manager
        //Used by itself is ok, but used with spawn manager random x axis will cause it to stay at 0.  


        // if (_audioSource == null)
        //{
        // Debug.LogError("The AudioSource is Null.");  "with  audio clip this is not needed"

        //}
        //if player is not equal to not there 
        //player is there

    }

    // Update is called once per frame
    void Update()
    {
        //move down at a speed of 3 
        //when we leave the screen, destroy this object
          
        transform.Translate( Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y < -11.0f)

        {
            Destroy(this.gameObject);

        }

       if  (Input.GetKeyDown(KeyCode.C) )    // this was the best and simplest way to do the magnet

        {
            isMagnetActive = true;          // when c is not pressed conditions remain false automatically
        }

        if (isMagnetActive == true)        // when is pressed conditions change from false to true and initiates Magnet()
        
        {

            Magnet();

            //isMagnetActive = true;
        }

       // Magnet();
    }



     private void Magnet()
      {
          
         transform.position = Vector3.Lerp( this.transform.position , _player.transform.position, 1f * Time.deltaTime);

     }

    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
           
        {
            Player player = other.transform.GetComponent<Player>();
            if (player != null)
                //commmunicate with Player Script with power up script
                //handle to the component I want
                //assign the handle to the component



                AudioSource.PlayClipAtPoint(_audioClip,transform.position);
            {

                switch (_powerupID)
                {

                    case 0:
                        player.TripleShotActive();
                        Debug.Log("TripleShotCollected");

                        break;
                    case 1:
                        player.SpeedBoostActive();
                        Debug.Log("Collected Speed Boost");

                        break;
                    case 2:
                        player.ShieldActive();
                        Debug.Log("Shields Collected");

                        break;
                    case 3:
                        player.AmmoSupply();
                        Debug.Log("Elmo Collected");

                        break;

                    case 4:
                        player.OneUp();
                        Debug.Log("Life Collected!");
                        break;

                    case 5:
                        player.Bomb();
                        Debug.Log("Bomb Collected!");
                        break;

                    case 6:
                        player.RedBomb();
                        Debug.Log("Player Damage Collected");
                       _anim.SetTrigger("bomb death");   // Ask Thomas why the anim set trigger does not work here/ works in the player script instead                   
                        break;

                    case 7: 
                        player.HomingWeapon();
                        Debug.Log("Homing Missile Collected");
                        break;

                }

                Destroy(this.gameObject, 1.0f);
                
            }

        }

      

    }


    //OnTriggerCollision
    //Only be collected by the player (Hint:use tags)

    /*  if (_powerupID == 0)          clausing out the whole scipt with (forward slash star)
      {
         // player.TripleShotActive();

      }

      //else if (_powerupID == 1)
      {
          //Debug.Log("Collected Speed Boost!");
      }

      //else if (_powerupID == 2)
      {

          //Debug.Log("Shields Collected");

      } */        //using star forward slash to close out the clause instead of using // all the way down 


    // above if else statements can be used to activate different powerups.  
    //after too many power ups it becomes messy so use the switch statement to clean things up.  
    //if powerUp is 0
    // player.TripleShotActive();  
    //else if powerUp is 1
    //play speed powerup
    //else if powerup is 2
    //shields powerup


}
