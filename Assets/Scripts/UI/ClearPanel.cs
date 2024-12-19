using UnityEngine;
using UnityEngine.UI;

public class ClearPanel : MonoBehaviour
{
    private LocalManager manager;

    private CanvasGroup canvas;
    public Button ExitButton;

    private void Awake()
    {
        manager = FindAnyObjectByType<LocalManager>();
        canvas = GetComponent<CanvasGroup>();
    }

    private void Start()
    {
        manager.onGameEnd += OnActive;
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
