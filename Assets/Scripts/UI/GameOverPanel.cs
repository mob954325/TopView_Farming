using UnityEngine;
using UnityEngine.UI;

public class GameOverPanel : MonoBehaviour
{
    private LocalManager manager;

    private CanvasGroup canvas;

    public Button RestartButton;
    public Button ExitButton;

    private void Awake()
    {
        manager = FindAnyObjectByType<LocalManager>();
        canvas = GetComponent<CanvasGroup>();
    }

    private void Start()
    {
        manager.Player.OnDead += OnActive;
        RestartButton.onClick.AddListener(() => { manager.RestartScene(); });
        ExitButton.onClick.AddListener(() => { manager.ExitScene(); });
        OnDeactive();
    }

    private void OnActive()
    {
        canvas.alpha = 1f;
        canvas.blocksRaycasts = true;
        canvas.interactable = true;
    }

    private void OnDeactive()
    {
        canvas.alpha = 0f;
        canvas.blocksRaycasts = false;
        canvas.interactable = false;
    }
}
