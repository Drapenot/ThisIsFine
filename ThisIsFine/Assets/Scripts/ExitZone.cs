using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitZone : MonoBehaviour
{
	private Collider _collider;

	private void Start()
	{
		_collider = GetComponent<Collider>();
	}
	private void OnTriggerEnter(Collider other)
	{
		var burnable = other.GetComponent<ICanBeSetOnFire>();
		if(burnable != null && !burnable.IsBurning() && burnable is BurningAnimal)
		{
			Destroy((burnable as MonoBehaviour).gameObject);
			StatsTracker._animalsSurvived++;
		}
	}

	public Vector3 GetPosition()
	{
		if(_collider != null)
		{
			return _collider.bounds.center;
		}

		return transform.position;
	}
}
