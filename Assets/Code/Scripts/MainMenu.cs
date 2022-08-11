using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour {

    [SerializeField] private CanvasGroup _mainMenu = null;
    [SerializeField] private CanvasGroup _creditsMenu = null;
    [SerializeField] private CanvasGroup _settingsMenu = null;
    [SerializeField] private CanvasGroup _shopMenu = null;
    [SerializeField] private CanvasGroup _worldSelectorMenu = null;
    [SerializeField] private CanvasGroup _levelSelectorMenu = null;
    [SerializeField] private float _sleepTimeMidFade = 0.5f;

    private CanvasGroup _currentMenu = null;
    private WaitForSeconds _sleepMidFades = null;

    private void Awake() {
        _currentMenu = _mainMenu;
        _sleepMidFades = new WaitForSeconds(_sleepTimeMidFade);
    }

    public void SwitchCanvas(CanvasGroup canvasToOpen) {
        StartCoroutine(TransitionCanvas(canvasToOpen));
    }

    private IEnumerator TransitionCanvas(CanvasGroup canvasToOpen) {
        float currentFade = 1;
        while (currentFade > 0) {
            currentFade -= Time.deltaTime; //
            MenuFunctions.FadeCanvasGroup(_currentMenu, currentFade);

            yield return new WaitForEndOfFrame(); //
        }
        currentFade = 0;

        yield return _sleepMidFades;

        while (currentFade < 1) {
            currentFade += Time.deltaTime; //
            MenuFunctions.FadeCanvasGroup(canvasToOpen, currentFade);

            yield return new WaitForEndOfFrame(); //
        }
        _currentMenu = canvasToOpen;
    }

}
