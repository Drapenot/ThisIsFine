using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitZones : MonoBehaviour
{
    private static ExitZones _instance;
    public static ExitZones Instance
	{
        get
		{
            return _instance;
		}
	}

	List<ExitZone> _allExitZones = new List<ExitZone>();

	private void Awake()
	{
		if(_instance == null)
		{
			_instance = this;
		}

		if(_instance != null && _instance != this)
		{
			Destroy(gameObject);
		}
	}

	private void Start()
	{
		foreach(var exitZone in transform.GetComponentsInChildren<ExitZone>())
		{
			_allExitZones.Add(exitZone);
		}
	}

	public ExitZone GetClosestExitZone(Vector3 position)
	{
		ExitZone closestExitZone = null;
		var closestExitZoneDistance = float.PositiveInfinity;

		foreach(var exitZone in _allExitZones)
		{
			if((exitZone.transform.position - transform.position).sqrMagnitude < closestExitZoneDistance)
			{
				closestExitZone = exitZone;
				closestExitZoneDistance = (exitZone.transform.position - transform.position).sqrMagnitude;
			}
		}

		return closestExitZone;
	}
}
