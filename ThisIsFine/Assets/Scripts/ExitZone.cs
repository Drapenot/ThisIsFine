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
		bool isInParent = false;
		bool isInChild = false;
		var burnable = other.GetComponent<ICanBeSetOnFire>();
		if (burnable == null)
		{
			burnable = other.GetComponentInParent<ICanBeSetOnFire>();
			isInParent = true;
		}
		if (burnable == null)
		{
			burnable = other.GetComponentInChildren<ICanBeSetOnFire>();
			isInChild = true;
		}
		if (burnable != null && !burnable.IsBurning() && burnable is BurningAnimal)
		{
			//if (!isInChild && !isInParent)
			//{
				Destroy((burnable as BurningAnimal).gameObject);
			//}
			StatsTracker._animalsSurvived++;

		}
	}

	public Vector3 GetPosition()
	{
		if (_collider != null)
		{
			return _collider.bounds.center;
		}

		return transform.position;
	}
}
