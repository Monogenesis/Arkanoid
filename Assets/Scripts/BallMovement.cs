using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{

    public static ArrayList allActiveBalls = new ArrayList();
    public GameObject ballPrefab;
    public float maxX = 12;
    public float maxZ = 8;
    private Vector3 velocity;
    public Transform playField;
    private Vector3 startPosition;

    private void Awake()
    {
        allActiveBalls.Add(this);
    }
    private void Start()
    {

        startPosition = transform.position;
        velocity = new Vector3(0, 0, -maxX);
    }


   

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Paddle"))
        {
            float maxDist = other.transform.localScale.x * 1 * 0.5f + transform.localScale.x * 1 * 0.5f;
            float dist = transform.position.x - other.transform.position.x;
            float nDist = dist / maxDist;

            velocity = new Vector3(nDist * maxX, velocity.y, -velocity.z);
        }
        else if (other.CompareTag("Block"))
        {
            Block collidingBlock = other.gameObject.GetComponent<Block>();
            collidingBlock.hitByBall();

            float otherWidth = other.transform.localScale.x;
            float otherHeight = other.transform.localScale.z;

            if (transform.position.x < other.transform.position.x - otherWidth * 0.5f || transform.position.x > other.transform.position.x + otherWidth * 0.5f)
            {
                if (transform.position.z < other.transform.position.z + otherHeight * 0.5f)
                {
                    // Edge collision
                    velocity = new Vector3(-velocity.x, velocity.y, -velocity.z);
                }
                else if (transform.position.z > other.transform.position.z - otherHeight * 0.5f)
                {
                    // Edge collision
                    velocity = new Vector3(-velocity.x, velocity.y, -velocity.z);
                }
                else
                {
                    // Side collision
                    velocity = new Vector3(velocity.x, velocity.y, -velocity.z);
                }
            }
            else
            {
                // The rest must be bottom and top side collsion ????
                velocity = new Vector3(velocity.x, velocity.y, -velocity.z);
            }
            

        }
        else if (other.CompareTag("TopWall"))
        {
            velocity = new Vector3(velocity.x, velocity.y, -velocity.z);
        }
        else if (other.CompareTag("SideWall"))
        {
            velocity = new Vector3(-velocity.x, velocity.y, velocity.z);
        }
        else if (other.CompareTag("DeadZone"))
        {
           
        
            allActiveBalls.Remove(this);
            if (allActiveBalls.Count <= 0)
            {
                Controller.GameController.Lives--;
            }
            Instantiate(ballPrefab, startPosition, Quaternion.identity);
            Destroy(gameObject);
            //transform.position = startPosition;
            //velocity = new Vector3(0, 0, -maxX);
        }
        //GetComponent<AudioSource>().Play();
    }


    private void Update()
    {
        transform.position += velocity * Time.deltaTime;
    }
}
