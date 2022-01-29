using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICanBeSetOnFire 
{
    bool IsBurning();
    void SetOnFire();
    void Extinguish();
    float BurnChance
	{
        get;
	}
}
