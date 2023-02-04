using UnityEngine;
using UnityEngine.SceneManagement;

public class HUDController : MonoBehaviour
{
    private GameObject _menuPanel;
    private SimPlant _simPlant;

    private void Awake()
    {
        _menuPanel = GameObject.Find("MenuPanel");
        _simPlant = GameObject.Find("Map").GetComponent<SimPlant>();
        _menuPanel.SetActive(false);
    }

    public void ShowMenu()
    {
        Debug.Log("Show menu");
        _menuPanel.SetActive(true);
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("StartScene");
    }

    public void Resume()
    {
        _menuPanel.SetActive(false);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
