using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementDetection : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    private int dammge;/*so the enemy choses how much damge the 
    player gets like a strong enemy does more damge*/

    [SerializeField] HealthBar healthbar;
    [SerializeField] float angleIncreasespeed = (2 * Mathf.PI) / 5;
    [SerializeField] float radius = 5f; //radius of the circle's movement
    [SerializeField] float viewRadius = 10f;
    [SerializeField] float detectionAngle = 10f;
    float angle;
    float x;
    float z;
    float startingX; //startingX and startingZ are the center of the circle.
    float startingZ;

    float randomAngleToMove;

    float multiplier = -1;
    void Start()
    {
        currentHealth = maxHealth;
        healthbar.SetMaxHealth(maxHealth);

        angle = 0f;// transform.rotation.eulerAngles.y;// * 180 / Mathf.PI;
        transform.position = new Vector3(transform.position.x + radius, transform.position.y, transform.position.z);
        startingX = transform.position.x;
        startingZ = transform.position.z;

        randomAngleToMove = Random.Range(0, 2 * Mathf.PI);
        if(randomAngleToMove> Mathf.PI)
        {
            multiplier = 1;
            transform.Rotate(0, 180, 0);
        }
        else
        {
            multiplier = -1;
        }
    }

    void Update()
    {
        //float currentRotationAngle = transform.rotation.eulerAngles.y;
        movement();
        detection();
    }

    void movement()
    {
        
        angle = angle + multiplier * angleIncreasespeed * Time.deltaTime;
        randomAngleToMove -= angleIncreasespeed * Time.deltaTime;
        x = startingX + Mathf.Cos(angle) * radius;
        z = startingZ + Mathf.Sin(angle) * radius;
        transform.position = new Vector3(x - radius, transform.position.y, z);
        transform.Rotate(0, (-multiplier * angleIncreasespeed * Time.deltaTime)*180/ Mathf.PI, 0);
        //print(angle);
        if(angle > 2 * Mathf.PI)
        {
            angle = angle - 2 * Mathf.PI;
        }
        else if(angle < 0)
        {
            angle = angle + 2 * Mathf.PI;
        }
        if (randomAngleToMove <= 0)
        {
            randomAngleToMove = Random.Range(0, 2 * Mathf.PI);
        }

        if (randomAngleToMove > Mathf.PI)
        {
            if (multiplier < 0)
            {
                transform.Rotate(0, 180, 0);
            }
            multiplier = 1;
        }
        else
        {
            if (multiplier > 0)
            {
                transform.Rotate(0, 180, 0);
            }
            multiplier = -1;
        }


        /*if((multiplier > 0 && randomAngleToMove - angle >0 0)|| (multiplier < 0 && randomAngleToMove - angle < 0))
        {

            randomAngleToMove = Random.Range(0.01, 2 * Mathf.PI); //new random point

        }

        if (randomAngleToMove > Mathf.PI)
        {
            multiplier = 1;
        }
        else
        {
            multiplier = -1;
        }*/
    }

    void detection()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if((player.transform.position - transform.position).magnitude < viewRadius)
        {
            Vector3 currentDirection = transform.position - transform.forward;
            /*print(currentDirection);
            print(transform.position);
            print("angle: " + Mathf.Abs(Vector3.Angle(currentDirection - transform.position, player.transform.position - transform.position)));
            print("detection angle: " + (detectionAngle / 2));
            print(Mathf.Abs(Vector3.Angle(currentDirection, player.transform.position)) <= (detectionAngle / 2));*/
            if (Mathf.Abs(Vector3.Angle(currentDirection - transform.position, player.transform.position - transform.position))<= (detectionAngle / 2))
            {
                RaycastHit target;
                if(Physics.Raycast(transform.position, player.transform.position - transform.position, out target, viewRadius))
                {
                    if (target.collider != null)
                    {
                        //may have to change later because of the use of tags
                        if (target.transform.tag == "Player")
                        {
                            Attack();
                        }
                    }
                }
            }
        }
    }
    void Attack()
    {
        TakeDammge(5);
    }

    void TakeDammge(int dammge)//gets called by a messege from the enemy
    {
        currentHealth -= dammge;
        healthbar.SetHealth(currentHealth);
    }
}
