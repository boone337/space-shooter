using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{

    public int grad_A = 90;
    public int grad_B = 80;
    public int grad_C = 70;
    public int grad_F = 60;

    public int Average_grad;

    // Start is called before the first frame update
    void Start()
    {

        Average_grad = (grad_A + grad_B + grad_C + grad_F) / 4;



    }

    // Update is called once per frame
    void Update()
    {
      /*  if (grad_A >= 90)
        {
            Debug.Log(" You made an A!");

        }
        else if(grad_B >= 80)
          {
            Debug.Log("You made a B!");

          }

        else if (grad_C >= 70)
        {

            Debug.Log("You made a C!");

        }
        else if (grad_F >= 60)
        {
            Debug.Log("You FAILED!!!!");

        }


        */

    }
}
