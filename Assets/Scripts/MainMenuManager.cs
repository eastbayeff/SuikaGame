using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{

    [SerializeField] private GameObject _creditsPanel;
    [SerializeField] private GameObject _mainPanel;
    [SerializeField] private GameObject _star;

    void Start()
    {
        Cursor.visible = true;
    }

    public void ButtonQuit()
    {
        Application.Quit();
    }

    public void ButtonStart()
    {
        SceneManager.LoadScene("Main");
    }

    public void ButtonToggleCredits()
    {
        _creditsPanel.SetActive(!_creditsPanel.activeSelf);
        _mainPanel.SetActive(!_mainPanel.activeSelf);
        _star.SetActive(!_star.activeSelf);
    }
}
