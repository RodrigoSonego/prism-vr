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
    }

    public void LoadNextLevel()
    {
		DisablePrisms();
		if (levelIndex == numberOfLevels) { return; }

		StartCoroutine(LoadAfterSeconds());
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
