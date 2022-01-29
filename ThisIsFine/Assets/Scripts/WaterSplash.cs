using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterSplash : MonoBehaviour
{

    public Bucket bucketRef;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {

        var canBeSetOnFire = other.GetComponent<ICanBeSetOnFire>();

        if(canBeSetOnFire != null && canBeSetOnFire.IsBurning() && bucketRef.clicked == true) 
        {   
        canBeSetOnFire.Extinguish();
        print("Machallesnass");
        }
    }


}
