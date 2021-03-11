using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public GameObject increaseSizePowerUp;
    public int hitpoints;
    public int value = 50;
    public Color green;
    public Color red;
    public Color silver;
    public Color gold;
    private float scaleFactor;
    private Vector3 startScale;

    private void Start()
    {
        scaleFactor = 0;
        startScale = transform.localScale;
        updateColor();
    }

    private void Update()
    {
        scaleFactor = Mathf.Lerp(scaleFactor, 1, 0.05f);
        transform.localScale = startScale * scaleFactor;
    }


    public void hitByBall()
    {
        hitpoints--;
        if(hitpoints <= 0)
        {
            Controller.GameController.addScore(value);
            int drop = Random.Range(0, 10);
            if (drop >= 7)
            {
                Instantiate(increaseSizePowerUp, transform.position, Quaternion.identity);
            }
        }
        updateColor();
    }



    private void updateColor()
    {
        switch (hitpoints)
        {
            case 0:
                Destroy(gameObject);
                break;
            case 1:
                gameObject.GetComponent<Renderer>().material.color = green;
                break;
            case 2:
                gameObject.GetComponent<Renderer>().material.color = red;
                break;
            case 3:
                gameObject.GetComponent<Renderer>().material.color = silver;
                break;
            case 4:
                gameObject.GetComponent<Renderer>().material.color = gold;
                break;
            default:
                Destroy(gameObject);
                break;
        }
    }

}
