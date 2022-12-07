using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndGameCanvas : MonoBehaviour
{
    void Start()
    {
        HideCanvas();

        if(Objective.instance != null)
        {
            Objective.instance.OnFullyCharge += PauseGameAndShowCanvas;
        }
    }

    public void _LoadMenuScene()
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

        foreach(var graphic in graphics)
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
            graphic.enabled = false;
        }
    }
}
