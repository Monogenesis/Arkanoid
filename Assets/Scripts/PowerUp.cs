using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    private Vector3 velocity;
    public float dropSpeed = 5;
    void Start()
    {
        velocity = new Vector3(0, 0, dropSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position -= velocity * Time.deltaTime;
    }
}
