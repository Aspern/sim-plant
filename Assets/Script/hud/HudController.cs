using Script.hud;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class HudController : MonoBehaviour
{
    public const string HudControllerComponentName = "HUD";

    [Header("UI")] [Tooltip("menu panel component.")]
    public GameObject menuPanel;

    public ActionMenu ActionMenu { get; private set; }
    public Compass Compass { get; private set; }

    public PlantCounter PlantCounter { get; set; }

    private void Awake()
    {
        ActionMenu = GameObject.Find(ActionMenu.ActionMenuComponentName).GetComponent<ActionMenu>();
        Compass = GameObject.Find(Compass.CompassComponentName).GetComponent<Compass>();
        PlantCounter = GameObject.Find(PlantCounter.PlantCounterComponentName).GetComponent<PlantCounter>();
    }

    public void ShowMenu()
    {
        menuPanel.SetActive(true);
    }

    public void CloseMenu()
    {
        menuPanel.SetActive(false);
    }

    public void BackToHomeScreen()
    {
        SceneManager.LoadScene("StartScene");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}