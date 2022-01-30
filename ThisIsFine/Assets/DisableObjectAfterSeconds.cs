using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableObjectAfterSeconds : MonoBehaviour
{
    public float _soundDuration;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DisableAfterSeconds());
    }

    IEnumerator DisableAfterSeconds()
	{
        yield return new WaitForSeconds(_soundDuration);
        gameObject.SetActive(false);
	}
}
