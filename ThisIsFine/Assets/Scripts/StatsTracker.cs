using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsTracker : MonoBehaviour
{
    public static int _animalsSurvived;
    public static int _animalsDied;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

	private void Update()
	{
        Debug.Log("saved Animals: " + _animalsSurvived);
        Debug.Log("died Animals: " + _animalsDied);
	}
}
