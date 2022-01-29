using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurningAnimalSpawner : MonoBehaviour
{
    public List<BurningAnimalWithCuteness> animalList = new List<BurningAnimalWithCuteness>();
    private float _totalWeight = 50;

    // Start is called before the first frame update
    void Start()
    {
        _totalWeight = 0;
        foreach(var animal in animalList)
		{
            _totalWeight += animal.cuteness;
		}

        GetComponent<MeshRenderer>().enabled = false;
        InvokeRepeating("SpawnRandomAnimal", 0, 3f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SpawnRandomAnimal()
	{
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

        var offset = new Vector3(Random.Range(0, transform.localScale.x / 2f), 0, Random.Range(0, transform.localScale.z / 2f));
        offset.x *= Mathf.Sign(Random.Range(-1, 1));
        offset.y *= Mathf.Sign(Random.Range(-1, 1));
        spawnPosition += offset;

        GameObject.Instantiate(selectedAnimal, spawnPosition, Quaternion.identity);

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
