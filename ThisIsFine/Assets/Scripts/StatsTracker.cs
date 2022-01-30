using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsTracker : MonoBehaviour
{
    public static int _animalsSurvived;
    public static int _animalsDied;
    private BurningAnimalSpawner _spawner;
    public static float _totalTime = 0;
    private GameOverScreen _gameOverScreen;

    private static StatsTracker _instance;
    public static StatsTracker Instance
	{
        get
		{
            return _instance;
		}
	}

    public static void DestroyInstance()
	{
        if(_instance != null)
		{
            _animalsSurvived = 0;
            _animalsDied = 0;
            _totalTime = 0;
            Destroy(_instance.gameObject);
		}
	}

    // Start is called before the first frame update
    void Start()
    {
        if(_instance == null)
		{
            _instance = this;
		}
        else
		{
            Destroy(gameObject);
		}

        DontDestroyOnLoad(this.gameObject);
        _spawner = FindObjectOfType<BurningAnimalSpawner>();
        //_gameOverScreen = FindObjectOfType<GameOverScreen>(true);
        //_gameOverScreen.gameObject.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
    }

	private void Update()
	{
        //Debug.Log("saved Animals: " + _animalsSurvived);
        //Debug.Log("died Animals: " + _animalsDied);
        if(_animalsDied > 25)
		{
            UnityEngine.SceneManagement.SceneManager.LoadScene(2);
            //Debug.Log("Game Over! " + _totalTime);
            //if(_gameOverScreen.gameObject.activeInHierarchy)
			//{
            //    return;
			//}
            //_gameOverScreen.gameObject.SetActive(true);
            //_gameOverScreen.SetTexts(_animalsSurvived, _animalsDied, _totalTime);
            //_spawner.StopSpawning();
            //
            //var input = FindObjectOfType<StarterAssets.StarterAssetsInputs>();
            //input.cursorInputForLook = false;
            //input.cursorLocked = false;
            Cursor.visible = true;
        }
        else
		{
            _totalTime += Time.deltaTime;
		}

	}
}
