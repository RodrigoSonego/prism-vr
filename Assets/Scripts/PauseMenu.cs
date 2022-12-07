using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    void Awake()
    {
        gameObject.SetActive(true);

        HideCanvas();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGameAndShowCanvas();
        }
    }

    public void _ContinueGame()
    {
        HideCanvas();

        Level.instance.Unpause();
    }

    public void _ReturnToMenu()
    {
        Level.instance.LoadMenuScene();
    }

    private void PauseGameAndShowCanvas()
    {
        Level.instance.Pause();
        ShowCanvas();
    }

    private void ShowCanvas()
    {
        var graphics = GetComponentsInChildren<Graphic>();

        if (graphics == null) { return; }

        foreach (var graphic in graphics)
        {
            graphic.enabled = true;
        }
    }

    private void HideCanvas()
    {
        var graphics = GetComponentsInChildren<Graphic>();

        if (graphics == null) { return; }

        foreach (var graphic in graphics)
        {
            graphic.gameObject.SetActive(true);
            graphic.enabled = false;
        }
    }
}
