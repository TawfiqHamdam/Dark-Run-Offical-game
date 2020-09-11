using UnityEngine;

//everything in this script is soposued to be a reference and all things here are made to be modifyed
public class RandomDrops : MonoBehaviour
{/* LegendaryStick
    RareStick
    commonStick*/

    [SerializeField] int[] Table =
    {
        600,//you can add more values in the inspector
        300,
        100 
    };
    [SerializeField] int total;
    [SerializeField] int randomNumber;

    void Start()
    {
        foreach(var Item in Table)
        {
            total += Item;
        }

        randomNumber = Random.Range(0, total);

        foreach (var Weight in Table)
        {
            if (randomNumber <= Weight)
            {
                //here you would spwan the stick or whatever
                print("You get the " + Weight);
            }
            else
            {
                randomNumber -= Weight;
            }
        }
    }
    
    void Update()
    {

    }
}
