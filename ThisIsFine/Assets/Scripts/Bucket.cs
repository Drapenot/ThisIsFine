using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bucket : MonoBehaviour
{

public bool insideWaterTrigger = false;
public bool bucketEmpty = true;

public bool clicked = false;

public GameObject waterPlane;
public GameObject waterParticle;


    // Start is called before the first frame update
    void Start()
    {
        
    }


    // Update is called once per frame
    void Update()
    {

        if (Input.GetButtonDown("Fire1") && bucketEmpty && insideWaterTrigger)
        {
            bucketEmpty = false;
            print("bucket filled");
            waterPlane.SetActive(true);
        
        }

        if (Input.GetButtonDown("Fire1") && !bucketEmpty && !insideWaterTrigger)
        {
            clicked = true;
            bucketEmpty = true;
            print("bucket empty");
            waterPlane.SetActive(false);
            waterParticle.GetComponent<ParticleSystem>().Play();
            clicked = false;
        }
    
    }

    void OnTriggerEnter(Collider other)
    {

        if(other.gameObject.layer == 4)
        {   
            insideWaterTrigger = true;
            print(insideWaterTrigger);
        }
    }

    void OnTriggerExit(Collider other)
    {

        if(other.gameObject.layer == 4)
        {   
            insideWaterTrigger = false;
            print(insideWaterTrigger);
        }
    }


}
