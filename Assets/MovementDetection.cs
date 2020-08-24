using UnityEngine;

public class MovementDetection : MonoBehaviour
{
    public int maxHealth = 100; // max amount of health the player has
    public int currentHealth; // just setting the health

    [SerializeField] int dammge;/*so the enemy choses how much damge the 
    player gets like a strong enemy does more damge*/
    [SerializeField] HealthBar healthbar; // connecting the helth bar
    [SerializeField] float angleIncreasespeed = (2 * Mathf.PI) / 5; //how fast the angle of the zombie's movement changes. In rads.

    [SerializeField] float radius = 5f; //radius of the circle's movement
    [SerializeField] float viewRadius = 10f; //how far the zombie sees.
    [SerializeField] float detectionAngle = 90f; //the angle of the zombie's view
    float angle; //current angle of zombie's movement

    float x; //current x
    float z; //current z

    float startingX; //startingX and startingZ are the center of the circle. These are needed because we constantly calculate the location based on the angle.
    float startingZ;

    float randomAngleToMove; //next angle which the zombie must travel on the circle.
    float multiplier = -1;//this determines the direction that the zombie moves on the circle. If positive the angle increases, if negative it decreases, causing movement on the circle.

    void Start()
    {
        Settinghealth();
        startSetPosition();

        randomAngleToMove = Random.Range(0, 2 * Mathf.PI);
        startSetDirection();
    }

    private void Settinghealth()// in the start of exsection it sets the player health
    {
        currentHealth = maxHealth;
        healthbar.SetMaxHealth(maxHealth);
    }

    void startSetDirection()//sets the starting direction of the enemy
    {
        if (randomAngleToMove > Mathf.PI)
        {
            multiplier = 1;
            transform.Rotate(0, 180, 0);
        }
        else
        {
            multiplier = -1;
        }
    }

    void startSetPosition() //sets the starting position. Basically moves the zombie from the center on a position on the cricle.
    {
        angle = 0f;// transform.rotation.eulerAngles.y;// * 180 / Mathf.PI;
        transform.position = new Vector3(transform.position.x + radius, transform.position.y, transform.position.z);
        startingX = transform.position.x;
        startingZ = transform.position.z;
    }


    void Update()
    {
        //float currentRotationAngle = transform.rotation.eulerAngles.y;
        movement();
        detection();
    }

    void movement()//controlls the enemys movement
    {

        angle = angle + multiplier * angleIncreasespeed * Time.deltaTime;
        setPositionAndRotation();
        resetAngle();
        findNewRandomAngle();
        setDirection();
    }

    void resetAngle() //for conventience, when the angle becomes more that 2π or less than 0, it resets it to the equivalent value between 0 and 2π.
    {
        if (angle > 2 * Mathf.PI)
        {
            angle = angle - 2 * Mathf.PI;
        }
        else if (angle < 0)
        {
            angle = angle + 2 * Mathf.PI;
        }
    }

    void findNewRandomAngle()//finds a random angle to start moving from
    {
        if (randomAngleToMove <= 0)
        {
            randomAngleToMove = Random.Range(0, 2 * Mathf.PI);
        }
    }

    void setDirection() //this sets the direction that the zombie looks towards. Depends on the direction that the zombie moves on the circle. (clockwise or counter-clockwise)
    {// It sets the facing direction after the next movement point is decided
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
    }

    void setPositionAndRotation()//moves the zombie, and rotates it as it is moving on the circle
    {//this sets the position in general while moving.

        randomAngleToMove -= angleIncreasespeed * Time.deltaTime;
        x = startingX + Mathf.Cos(angle) * radius;
        z = startingZ + Mathf.Sin(angle) * radius;
        transform.position = new Vector3(x - radius, transform.position.y, z);
        transform.Rotate(0, (-multiplier * angleIncreasespeed * Time.deltaTime) * 180 / Mathf.PI, 0);
    }

    void detection()//this is the starting of the detction of the player if you remvoe this the other things bellow wont work
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");//this determines the tag that the detected object must have
        if ((player.transform.position - transform.position).magnitude < viewRadius)
        {
            SettingValues(player);
        }
    }

    private void SettingValues(GameObject player)//here we determine if the player is within the viewing angle.
    {
        Vector3 currentDirection = transform.position - transform.forward;
        if (Mathf.Abs(Vector3.Angle(currentDirection - transform.position, player.transform.position - transform.position)) <= (detectionAngle / 2)) 
        {
            RaycastingToSeePlayer(player);
        }
    }

    private void RaycastingToSeePlayer(GameObject player)//here we raycast to see if there is an obstacle between the zombie and the player. Currently there is no layerMask. We can add later if e.g. we 
    {//prefer obstacles to belong to a specific layer (i.e. if not all coliders are obstacles)
        RaycastHit target;
        if (Physics.Raycast(transform.position, player.transform.position - transform.position, out target, viewRadius))
        {
            Detection(target);
        }
    }

    private void Detection(RaycastHit target)//we set what happens when the player gets detectid
    {
        if (target.collider != null)
        {
            //may have to change later because of the use of tags
            if (target.transform.tag == "Player")//checking if the detected collider is of tag "Player" or it is something else.
            {
                TakeDammge();
            }
        }
    }

    void TakeDammge()/*gets called by a messege from the enemy and this exsecutes the takeing dammge part you can add more that might happen when the player gets attacked or gets detected*/
    {
        currentHealth -= dammge;
        healthbar.SetHealth(currentHealth);
    }
}
