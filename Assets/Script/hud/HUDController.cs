using UnityEngine;

public class HUDController : MonoBehaviour
{
    private GameObject _menuPanel;

    private void Awake()
    {
        _menuPanel = GameObject.Find("MenuPanel");
        _menuPanel.SetActive(false);
    }

    public void ShowMenu()
    {
        Debug.Log("Show menu");
        _menuPanel.SetActive(true);
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
