using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameUI : MonoBehaviour {


    [Header("Canvases & Info")]

    [SerializeField] private CanvasGroup _ui = null;
    [SerializeField] private CanvasGroup _pauseMenu = null;
    [SerializeField] private CanvasGroup _settingsMenu = null;
    [SerializeField] private CanvasGroup _levelEndMenu = null;

    private CanvasGroup _currentCanvas = null;

#if UNITY_STANDALONE_WIN
    [Header("Pause")]

    [SerializeField] private KeyCode _pauseKey = KeyCode.Escape;

    private void Update() {
        if (Input.GetKeyDown(_pauseKey)) {
            if (_ui.interactable) SwitchCanvasInstant(_pauseMenu);
            else SwitchCanvasInstant(_ui);
        }

    }
#endif

    private void Awake() {
        _currentCanvas = _ui;
    }

    public void SwitchCanvasInstant(CanvasGroup canvasToOpen) {
        // Start Animation for each menu except main?
        MenuFunctions.FadeCanvasGroup(canvasToOpen, 1);
        MenuFunctions.FadeCanvasGroup(_currentCanvas, 0);
        _currentCanvas = canvasToOpen;
        if (_currentCanvas == _ui) Time.timeScale = 1;
        else Time.timeScale = 0;
    }

    public void RestartLevel() {
        // Set every item in the scene to be reusable
    }

    public void EnterLevel(int levelToLoad) {
        SceneManager.LoadScene(levelToLoad); // Set in a way to return to the corresponding world's Menu
        Time.timeScale = 1; // Prevent weird behaviour time related
    }

}
