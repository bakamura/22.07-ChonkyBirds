using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    [Header("Canvases & Info")]

#pragma warning disable 414
    [SerializeField] private CanvasGroup _mainMenu = null;
    [SerializeField] private CanvasGroup _creditsMenu = null;
    [SerializeField] private CanvasGroup _settingsMenu = null;
    [SerializeField] private CanvasGroup _languageSelectorMenu = null;
    [SerializeField] private CanvasGroup _shopMenu = null;
    [SerializeField] private CanvasGroup _worldSelectorMenu = null;
    [SerializeField] private CanvasGroup _levelSelectorMenu = null;
#pragma warning restore 414
    [Tooltip("Duration of each fade (In/Out)")]
    [SerializeField] private float _fadeDuration = 0.25f;
    [SerializeField] private float _sleepTimeMidFade = 0.5f;

    [Header("Camera")]

    [SerializeField] private Vector3 _mainCamPos = Vector3.zero;
    [SerializeField] private Vector3 _mainCamAng = Vector3.zero;
    [Space(10)]
    [SerializeField] private Vector3 _creditCamPos = Vector3.zero;
    [SerializeField] private Vector3 _creditCamAng = Vector3.zero;
    [SerializeField] private MeshRenderer[] _bgMesh = null;

    [Header("Internal")]

    private Transform _cam = null;
    private CanvasGroup _currentMenu = null;
    private WaitForSeconds _sleepMidFades = null;
    private WaitForSeconds _sleepTransitionDuration;

    private void Awake() {
        _cam = Camera.main.transform;
        _currentMenu = _mainMenu;
        _sleepMidFades = new WaitForSeconds(_sleepTimeMidFade);
        _sleepTransitionDuration = new WaitForSeconds(2 * _fadeDuration + _sleepTimeMidFade);

        // Set level star displays in each level in each world
        // Set settings to what's saved
    }

    #region CanvasHandling

    public void SwitchCanvas(CanvasGroup canvasToOpen) {
        StartCoroutine(TransitionCanvas(canvasToOpen));
    }

    private IEnumerator TransitionCanvas(CanvasGroup canvasToOpen) {
        float currentFade = 1;
        while (currentFade > 0) {
            currentFade -= Time.deltaTime / _fadeDuration;
            MenuFunctions.FadeCanvasGroup(_currentMenu, currentFade);

            yield return null;
        }
        currentFade = 0;

        yield return _sleepMidFades;

        while (currentFade < 1) {
            currentFade += Time.deltaTime / _fadeDuration;
            MenuFunctions.FadeCanvasGroup(canvasToOpen, currentFade);

            yield return null;
        }
        _currentMenu = canvasToOpen;
    }

    public void SwitchCanvasInstant(CanvasGroup canvasToOpen) {
        MenuFunctions.FadeCanvasGroup(canvasToOpen, 1);
        if (canvasToOpen == _languageSelectorMenu) {
            _currentMenu = _languageSelectorMenu;
            return;
        }
        MenuFunctions.FadeCanvasGroup(_currentMenu, 0);
        _currentMenu = canvasToOpen;
    }

    public void SwitchCameraPos() {
        StartCoroutine(TransitionCamera());
    }

    private IEnumerator TransitionCamera() {
        Vector3 newPos = _currentMenu == _mainMenu ? _creditCamPos : _mainCamPos;
        Quaternion newAng = Quaternion.Euler(_currentMenu == _mainMenu ? _creditCamAng : _mainCamAng);

        float currentFade = 0;
        float transitionDuration = 2 * _fadeDuration + _sleepTimeMidFade;
        Vector3 startPos = _cam.position;
        Quaternion startAng = _cam.rotation;
        while (currentFade < 1) {
            currentFade += Time.deltaTime / transitionDuration;
            _cam.position = Vector3.Lerp(startPos, newPos, currentFade);
            _cam.rotation = Quaternion.Lerp(startAng, newAng, currentFade);
            yield return null;
        }
    }

    public void SwitchModelVisibility(bool visible) {
        if (visible) foreach (MeshRenderer renderer in _bgMesh) renderer.enabled = true;
        else StartCoroutine(DisableBgVisibility());
    }

    private IEnumerator DisableBgVisibility() {
        yield return _sleepTransitionDuration;

        foreach(MeshRenderer renderer in _bgMesh) renderer.enabled = false;
    }

    #endregion

    public void EnterLevel(int levelToLoad) {
        SceneManager.LoadScene(levelToLoad); //
    }

}
