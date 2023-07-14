using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement2 : MonoBehaviour
   
{
    private int _movementID;

    public float rotationModifier;

    //[SerializeField]
   // private Vector3 targetPosition;

    private Player _player;

    [SerializeField]
    private float _speed = 3f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {       
            transform.Translate(Vector3.left * _speed * Time.deltaTime);

            if (transform.position.x < -13.6f)
            {
                float randomY = Random.Range(5f, -5f); //assign to random Y axis  alien will appear at 12f random Y axis

                transform.position = new Vector3(12f, randomY, 0);
            }
        
        switch (_movementID)
        {
            case 0:

                Vector3 vectorToTarget = _player.transform.position - transform.position;  //calc direction toward target orignal code was to use target
                                                                                           //  in this case I had to reference the player position through hard code.  
                                                                                           //could not drag and drop player into target box through hierarch
                float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg - rotationModifier;  //calc he angle towards the target in degrees 
                                                                                                                   //without minus rotationModifier it will not work. 

                Quaternion Q = Quaternion.AngleAxis(angle, Vector3.forward); // create a quaternion that represents the rotation towards the target

                transform.rotation = Quaternion.Slerp(transform.rotation, Q, _speed * Time.deltaTime); //rotate towards target using a smooth slerp or lerp

                break;

            case 1:


                break;





        }








    }  


    



}
