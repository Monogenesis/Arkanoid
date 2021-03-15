using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    public GameObject ballPrefab;
    public float speed = 10;
    public Transform playField;
    public float maxXScale = 7.0f;
    private float minXScale;
    public float paddleSizeTime;
    public GameObject portalPrefab;
    private void Start()
    {
        minXScale = transform.localScale.x;
    }

    void Update()
    {
        float maxMapWidth = ((playField.localScale.x * 10) * 0.5f) - (transform.localScale.x * 0.5f);
        float nextPos = transform.position.x + (Input.GetAxis("Horizontal") * Time.deltaTime * speed);
        transform.position = new Vector3(Mathf.Clamp(nextPos, -maxMapWidth, maxMapWidth), transform.position.y,transform.position.z);


        // Paddle size power up
        paddleSizeTime -= Time.deltaTime;

        if(paddleSizeTime > 0)
        {
            transform.localScale = new Vector3(Mathf.Lerp(transform.localScale.x,maxXScale,0.05f), transform.localScale.y, transform.localScale.z);
        }
        else
        {
            transform.localScale = new Vector3(Mathf.Lerp(transform.localScale.x, minXScale, 0.05f), transform.localScale.y, transform.localScale.z);
        }
    }

 
    private void paddleSizeBoost()
    {
        paddleSizeTime = 10.0f;
    }

    private void triggerMultiBall()
    {
        Vector3 spawnPos = new Vector3(transform.position.x, transform.position.y, transform.position.z + transform.localScale.z * 0.5f);
        Instantiate(ballPrefab, spawnPos, Quaternion.identity);
    }

    public void createPortal(GameObject target)
    {
        
        Instantiate(portalPrefab, new Vector3(target.transform.position.x, 1.2f, target.transform.position.z), Quaternion.identity);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PowerUpSize"))
        {
            paddleSizeBoost();
            Destroy(other.gameObject);
        }
        if (other.CompareTag("PowerUpMultiball"))
        {
            triggerMultiBall();
            Destroy(other.gameObject);
        }
        if (other.CompareTag("PowerUpPortal"))
        {
            Debug.Log("Portal hit");
            createPortal(other.gameObject);
            Destroy(other.gameObject);
        }
    }
}
