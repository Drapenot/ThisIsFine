using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurningAnimalSpawner : MonoBehaviour
{
    public List<BurningAnimalWithCuteness> animalList = new List<BurningAnimalWithCuteness>();
    private float _totalWeight = 50;
    bool _canSpawn = true;
    private float _timeSinceLastSpawn = 22f;
    private float _timeToNextSpawn = 25f;
    public float startInterval = 25f;
    public float multiplierPerSpawn = 0.9f;

    // Start is called before the first frame update
    void Start()
    {
        _totalWeight = 0;
        foreach(var animal in animalList)
		{
            _totalWeight += animal.cuteness;
		}

        GetComponent<MeshRenderer>().enabled = false;

        _timeToNextSpawn = startInterval;
        _timeSinceLastSpawn = startInterval - 3;
        //InvokeRepeating("SpawnRandomAnimal", 0, 3f);
    }

    // Update is called once per frame
    void Update()
    {
        _timeSinceLastSpawn += Time.deltaTime;
        if(_timeSinceLastSpawn > _timeToNextSpawn)
		{
            SpawnRandomAnimal();
            _timeSinceLastSpawn = 0;
            _timeToNextSpawn = Mathf.Max(_timeToNextSpawn * multiplierPerSpawn, 1.5f);
		}
    }

    public void StopSpawning()
	{
        _canSpawn = false;
	}

    public void SpawnRandomAnimal()
	{
        if(!_canSpawn)
		{
            return;
		}
        var searchValue = Random.value * _totalWeight;
        var currentTotal = 0f;
        GameObject selectedAnimal = null;

        foreach(var animal in animalList)
		{
            currentTotal += animal.cuteness;

            if (searchValue < currentTotal)
			{
                selectedAnimal = animal.burningAnimal;
                break;
			}          
		}

        var spawnPosition = transform.position;

        var offset = new Vector3(Random.Range(0, transform.localScale.x * 5f), 0, Random.Range(0, transform.localScale.z * 5f));
        offset.x *= Mathf.Sign(Random.Range(-1, 1));
        offset.z *= Mathf.Sign(Random.Range(-1, 1));
        spawnPosition += offset;

        var instance = GameObject.Instantiate(selectedAnimal, spawnPosition, Quaternion.identity);
        instance.layer = selectedAnimal.layer;

	}
}

[System.Serializable]
public class BurningAnimalWithCuteness
{
    [SerializeField]
    public GameObject burningAnimal;

    [SerializeField]
    public float cuteness;
}
