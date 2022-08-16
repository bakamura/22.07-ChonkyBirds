using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameUI : MonoBehaviour {


    [Header("Canvases & Info")]

    [SerializeField] private CanvasGroup _ui = null;
    [SerializeField] private CanvasGroup _pauseMenu = null;
    [SerializeField] private CanvasGroup _settingsMenu = null;

    private CanvasGroup _currentCanvas = null;

#if UNITY_STANDALONE_WIN
    [Header("Pause")]

    [SerializeField] private KeyCode _pauseKey = KeyCode.Escape;

    private void Update() {
        if (Input.GetKeyDown(_pauseKey)) {
            if (_ui.interactable) {
                MenuFunctions.FadeCanvasGroup(_ui, 0);
                MenuFunctions.FadeCanvasGroup(_pauseMenu, 1);
                MenuFunctions.FadeCanvasGroup(_settingsMenu, 0);

                Time.timeScale = 0; //
            }
            else {
                MenuFunctions.FadeCanvasGroup(_ui, 1);
                MenuFunctions.FadeCanvasGroup(_pauseMenu, 0);

                Time.timeScale = 1; //
            }
        }
    }
#endif

    public void PauseBtn() {
        MenuFunctions.FadeCanvasGroup(_ui, 0);
        MenuFunctions.FadeCanvasGroup(_pauseMenu, 1);
        MenuFunctions.FadeCanvasGroup(_settingsMenu, 0);

        Time.timeScale = 0; //
    }

    public void ResumeBtn() {
        MenuFunctions.FadeCanvasGroup(_ui, 1);
        MenuFunctions.FadeCanvasGroup(_pauseMenu, 0);

        Time.timeScale = 1; //
    }

    public void OpenSettingsMenu(bool open) {
        MenuFunctions.FadeCanvasGroup(_settingsMenu, open ? 1 : 0);

    }

    public void SwitchCanvasInstant(CanvasGroup canvasToOpen) {
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
