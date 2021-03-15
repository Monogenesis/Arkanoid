using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public float zSpawnPos;
    private GameObject upperPortal;
    public GameObject lowerPortal;
    public float uptime = 10;

    void Start()
    {
     
        upperPortal = Instantiate(lowerPortal, new Vector3(lowerPortal.transform.position.x, lowerPortal.transform.position.y, zSpawnPos), Quaternion.identity);
        Invoke(nameof(DestroySelf), uptime);
    }

    private void OnDestroy()
    {
        Destroy(upperPortal);
        Destroy(lowerPortal);
    }
    void DestroySelf()
    {
        Destroy(upperPortal);
        Destroy(lowerPortal);
        Destroy(gameObject);
    }
}

