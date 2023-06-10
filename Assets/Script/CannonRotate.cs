using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonRotate : MonoBehaviour
{
    


    [SerializeField]
    private Vector3 targetPosition; //original plan was to use this , drag and drop player in target



    // public float smoothTime = 0.5f;

    //public float rotationSpeed = 5f; // speed to rotate towards target

    //Vector3 velocity;

    [SerializeField]
    private float _speed = 3.0f;

    public float rotationModifier;

    private Player _player;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {

        // Calculate the direction from the enemy to the player
        Vector3 directionToPlayer = _player.transform.position ;

        // Calculate the rotation needed to look at the player
        Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);

        // Smoothly rotate the enemy towards the player's direction
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * 3);

        Vector3 targ = _player.transform.position;
        targ.z = 0f;

        Vector3 objectPos = transform.position;
        targ.x = targ.x - objectPos.x;
        targ.y = targ.y - objectPos.y;

        float angle = Mathf.Atan2(targ.y, targ.x) * Mathf.Rad2Deg-rotationModifier;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        /*  if (_player != null)  //then if target object is not null rotate

          {

              Vector3 vectorToTarget = _player.transform.position - transform.position;  //calc direction toward target orignal code was to use target
                                                                                         //  in this case I had to reference the player position through hard code.  
                                                                                         //could not drag and drop player into target box through hierarchy
                                                                                         // Calculate the rotation needed to look at the player
              Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);


              float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg - rotationModifier;  //calc the angle towards the target in degrees 
                                                                                                                 //without minus rotationModifier it will not work. 

              Quaternion Q = Quaternion.AngleAxis(angle, Vector3.forward); // create a quaternion that represents the rotation towards the target

              transform.rotation = Quaternion.Slerp(transform.rotation, Q, _speed * Time.deltaTime); //rotate towards target using a smooth slerp or lerp

          }  */


    }
}
