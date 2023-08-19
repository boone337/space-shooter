using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class RandomLoot : MonoBehaviour
{
    public int[] table = //{ 60, 30, 10 }; or can do as shown below
    {
    60,
    30,
    10,
    };
    //most frequent down to rarity
    public int total;
    public int RandomNumber;
    public List<GameObject> Loot;
    // Start is called before the first frame update
    void Start()
    {
        //tally total weight
        //draw a random number between  and the total weight (100)
        // ex.  randomNumber = 49 

        // is 49 <= 60   .... sword A 

        // randomNumber = 61 .... is 61 <= 60 ...no ... so 61-60..... 1<= 30 ,, yes 

        //ex2 randomNumber = 92..92<=60? no so 92-60 = 32..32<=30?  no so 32-30 =2
        // 2<+10? yes thats your weapon.  

      /*  foreach(var weight in table)
        {
            if (RandomNumber <= weight)
            {
                //award Item. 
                Debug.Log("+ award the weight");
            }

            else
            {
                RandomNumber -= weight;
            }
        }
         */
        //now we compare the random number <= to current weight?

    }

    // Update is called once per frame
    void Update()
    {
        foreach (var item in table)
        {
            total += item;
        }

        RandomNumber = Random.Range(0, total);


        for (int i = 0; i < table.Length; i++)
        {
            if (RandomNumber <= table[i])
            {
                Loot[i].SetActive(true);
                return;
            }
            else
            {
                RandomNumber -= table[i];
            }

        }
    }
}
