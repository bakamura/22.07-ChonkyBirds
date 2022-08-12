using UnityEngine;

public static class MenuFunctions {

    public static void FadeCanvasGroup(CanvasGroup canvasGroup, float fadeAmount) {
        canvasGroup.alpha = fadeAmount;
        bool open = fadeAmount >= 1; //
        canvasGroup.interactable = open;
        canvasGroup.blocksRaycasts = open;
    }

}
