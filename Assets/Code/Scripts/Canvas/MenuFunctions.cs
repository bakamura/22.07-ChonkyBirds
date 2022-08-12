using UnityEngine;
using UnityEngine.Audio;

public static class MenuFunctions {

    public static void FadeCanvasGroup(CanvasGroup canvasGroup, float fadeAmount) {
        canvasGroup.alpha = fadeAmount;
        bool open = fadeAmount >= 1; //
        canvasGroup.interactable = open;
        canvasGroup.blocksRaycasts = open;
    }

    public static void ChangeAudioVolume(AudioMixer mixer, string name, float newVol) {
        mixer.SetFloat(name, Mathf.Log10(newVol));
    }

}