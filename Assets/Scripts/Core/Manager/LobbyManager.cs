using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LobbyManager : MonoBehaviour
{
    public Button StartButton;
    public Button ExitButton;

    private void Start()
    {
        if (StartButton != null)
        {
            StartButton.onClick.AddListener(() =>
            {
                SceneManager.LoadScene("Main");
            });
        }

        if(ExitButton != null)
        {
            ExitButton.onClick.AddListener(() =>
            {
                Application.Quit();
            });
        }
    }
}