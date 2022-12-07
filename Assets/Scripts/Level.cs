using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
	[SerializeField] private int levelIndex;


	public static Level instance { get; private set; }


	private const int numberOfLevels = 3;
	private static Prism[] prisms;

    void Start()
    {
		instance = this;

		prisms = GetPrisms();

		Unpause();
    }

	public void Pause()
    {
		Time.timeScale = 0;
		Cursor.lockState = CursorLockMode.None;
	}

    public void Unpause()
    {
		Time.timeScale = 1;
		Cursor.lockState = CursorLockMode.Locked;
    }

    public void LoadNextLevel()
    {
		DisablePrisms();
		if (levelIndex == numberOfLevels) { return; }

		StartCoroutine(LoadAfterSeconds());
    }

	public void LoadMenuScene()
	{
		SceneManager.LoadScene("Menu");
	}

	private Prism[] GetPrisms()
    {
		return transform.GetComponentsInChildren<Prism>();
    }

	private void DisablePrisms()
	{
		foreach (var prism in prisms)
		{
			prism.DisablePrism();
		};
	}

	private IEnumerator LoadAfterSeconds()
    {
		yield return new WaitForSeconds(3);

		SceneManager.LoadScene("lvl " + (levelIndex + 1));
    }
}
