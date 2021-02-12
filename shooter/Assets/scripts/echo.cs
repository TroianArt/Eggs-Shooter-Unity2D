using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class echo : MonoBehaviour
{
    private float timeBtwSpawns;
    public float startTimeBtwSpawns;
    public GameObject echoimage;



    // Update is called once per frame
    void Update()
    {




        if (timeBtwSpawns <= 0)
        {
            
            Instantiate(echoimage, transform.position, Quaternion.identity);
            timeBtwSpawns = startTimeBtwSpawns;
            
            
        }
        else
        {
            timeBtwSpawns -= Time.deltaTime;
        }

    }
}

