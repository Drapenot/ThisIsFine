using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    public Text SavedAnimalsText;
    public Text DeadAnimalsText;
    public Text TimeText;

	private void Start()
	{
        SetTexts(StatsTracker._animalsSurvived, StatsTracker._animalsDied, StatsTracker._totalTime);
        StatsTracker.DestroyInstance();
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
	}

	public void SetTexts(int savedAnimals, int deadAnimals, float time)
	{
        SavedAnimalsText.text = "You managed to save " + savedAnimals + " animals,";
        DeadAnimalsText.text = "while letting " + deadAnimals + " helpless creatures die.";
        TimeText.text = "This deed took you " + time + " seconds.";
	}

    public void MainMenuButton()
	{
        SceneManager.LoadScene(0);
    }
}
