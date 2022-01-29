using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bucket : MonoBehaviour
{

public bool insideWaterTrigger = false;
public bool bucketEmpty = true;

public GameObject waterPlane;
public GameObject waterParticle;
public GameObject bullet;




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
            bucketEmpty = true;
            print("bucket empty");
            waterPlane.SetActive(false);
            waterParticle.GetComponent<ParticleSystem>().Play();
            var instance = Instantiate(bullet, transform.position + transform.forward * 1.5f, transform.rotation);
            instance.GetComponent<Rigidbody>().AddForce(transform.forward * 200f);
            Destroy(instance, 0.8f);
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
