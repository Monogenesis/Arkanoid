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
    public static Vector3 startPosition;
    private bool blockCollisionActive = true;

    private void Awake()
    {
        allActiveBalls.Add(this);
    }
    private void Start()
    {
        if(startPosition == null)
        startPosition = transform.position;
        velocity = new Vector3(0, 0, -maxX);
    }


    void ResetBlockCollision()
    {
        blockCollisionActive = true;
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
        else if (other.CompareTag("Block") && blockCollisionActive)
        {
            blockCollisionActive = false;
            Invoke(nameof(ResetBlockCollision), 0.01f);
            Block collidingBlock = other.gameObject.GetComponent<Block>();
            collidingBlock.hitByBall();

            float otherWidth = other.transform.localScale.x;
            float otherHeight = other.transform.localScale.z;

            bool hitFromAbove = transform.position.z > other.transform.position.z + otherHeight * 0.5f;
            bool hitFromBeneath = transform.position.z < other.transform.position.z - otherHeight * 0.5f;
         
                //Left Side
                if (transform.position.x < other.transform.position.x - otherWidth * 0.5f)
                {
                    if(hitFromAbove)
                    {
                    if (velocity.x >= 0)
                        velocity = new Vector3(-velocity.x, velocity.y, -velocity.z);
                    else
                    {
                        velocity = new Vector3(velocity.x, velocity.y, -velocity.z);
                    }
                    }
                    else if (hitFromBeneath)
                    {
                    if (velocity.x >= 0)
                        velocity = new Vector3(-velocity.x, velocity.y, -velocity.z);
                    else
                    {
                        velocity = new Vector3(velocity.x, velocity.y, -velocity.z);
                    }
                }
                    else
                    {
                        //hit on left side
                        velocity = new Vector3(-velocity.x, velocity.y, velocity.z);
                    }
                }
                // Right Side
                else if(transform.position.x > other.transform.position.x + otherWidth * 0.5f)
                {
                    if (hitFromAbove)
                    {
                    if (velocity.x < 0)
                        velocity = new Vector3(-velocity.x, velocity.y, -velocity.z);
                    else
                    {
                        velocity = new Vector3(velocity.x, velocity.y, -velocity.z);
                    }
                }
                    else if (hitFromBeneath)
                    {
                    if (velocity.x < 0)
                        velocity = new Vector3(-velocity.x, velocity.y, -velocity.z);
                    else
                    {
                        velocity = new Vector3(velocity.x, velocity.y, -velocity.z);
                    }
                }
                else
                {
                        //hit on left side
                        velocity = new Vector3(-velocity.x, velocity.y, velocity.z);
                    }
                }
                // Top or Bottom Side
                else
                {
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
            if(velocity.x >= 0)
            {
                transform.position = new Vector3(other.transform.position.x + other.transform.localScale.x * 2.0f, transform.position.y, transform.position.z);
            }
            else
            {
                transform.position = new Vector3(other.transform.position.x - other.transform.localScale.x * 2.0f, transform.position.y, transform.position.z);
            }
          
        }
        else if (other.CompareTag("DeadZone"))
        {
           
        
            allActiveBalls.Remove(this);
            if (allActiveBalls.Count <= 0)
            {
                Controller.GameController.Lives--;
                     Instantiate(ballPrefab, startPosition, Quaternion.identity);
            }
           
            Destroy(gameObject);
            //transform.position = startPosition;
            //velocity = new Vector3(0, 0, -maxX);
        }else if (other.CompareTag("Portal"))
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, other.gameObject.GetComponent<Portal>().zSpawnPos);
            Destroy(other.gameObject);
        }
        GetComponent<AudioSource>().Play();
    }


    private void Update()
    {
        transform.position += velocity * Time.deltaTime;
    }
}
