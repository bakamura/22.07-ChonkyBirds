using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameUI : MonoBehaviour {


    [Header("Canvases & Info")]

    [SerializeField] private CanvasGroup _ui = null;
    [SerializeField] private CanvasGroup _pauseMenu = null;
    [SerializeField] private CanvasGroup _settingsMenu = null;


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

}
