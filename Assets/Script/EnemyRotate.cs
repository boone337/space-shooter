using System.Collections;
using System.Collections.Generic;
//using UnityEditorInternal;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UIElements;

public class EnemyRotate : MonoBehaviour
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
      
       transform.position =  Vector3.MoveTowards(transform.position, targetPosition,  _speed * Time.deltaTime); // get object to move to TARGET AREA
                                                                                                                //transform.Translate( targetPosition * _speed * Time.deltaTime);
        
        if (transform.position.y == 4f)

        {
           // float randomX = Random.Range(-12f, 12f); //assign to random Y axis  alien will appear at 12f random Y axis

            //transform.Translate( new Vector3(randomX, 4f, 0));
        }


        if (_player != null)  //then if target object is not null rotate

        { 

            Vector3 vectorToTarget = _player.transform.position - transform.position;  //calc direction toward target orignal code was to use target
                         //  in this case I had to reference the player position through hard code.  
                         //could not drag and drop player into target box through hierarchy


            float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg - rotationModifier;  //calc the angle towards the target in degrees 
                                                                                           //without minus rotationModifier it will not work. 

            Quaternion Q = Quaternion.AngleAxis(angle, Vector3.forward); // create a quaternion that represents the rotation towards the target

            transform.rotation = Quaternion.Slerp(transform.rotation,  Q ,  _speed * Time.deltaTime); //rotate towards target using a smooth slerp or lerp

        }

      

    }

   

        
    



}



        














    


