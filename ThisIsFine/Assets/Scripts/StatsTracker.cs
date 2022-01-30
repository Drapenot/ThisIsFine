using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsTracker : MonoBehaviour
{
    public static int _animalsSurvived;
    public static int _animalsDied;
    private BurningAnimalSpawner _spawner;
    private float _totalTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        //DontDestroyOnLoad(this.gameObject);
        _spawner = FindObjectOfType<BurningAnimalSpawner>();
    }

	private void Update()
	{
        //Debug.Log("saved Animals: " + _animalsSurvived);
        //Debug.Log("died Animals: " + _animalsDied);
        if(_animalsDied > 25)
		{
            Debug.Log("Game Over! " + _totalTime);
		}
        else
		{
            _totalTime += Time.deltaTime;
		}

	}
}
