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


    // Start is called before the first frame update
    void Start()
    {

       // _audioSource = GetComponent<AudioSource>(); not needed with audio clip
        //transform.position = new Vector3(0, 11.0f, 0);  This code here will conflict with spawn manager
        //Used by itself is ok, but used with spawn manager random x axis will cause it to stay at 0.  


       // if (_audioSource == null)
        //{
           // Debug.LogError("The AudioSource is Null.");  "with  audio clip this is not needed"

        //}

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
       

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
           

        {
           
            //commmunicate with Player Script with power up script
            //handle to the component I want
            //assign the handle to the component

            Player player = other.transform.GetComponent<Player>();
            if (player != null)  //if player is not equal to not there 
                                 //player is there

                AudioSource.PlayClipAtPoint(_audioClip,transform.position);
            {
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

                switch(_powerupID)
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


                }


            }
            Destroy(this.gameObject);

        }



    }


    //OnTriggerCollision
    //Only be collected by the player (Hint:use tags)



}
