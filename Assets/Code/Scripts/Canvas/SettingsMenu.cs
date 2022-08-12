using UnityEngine;
using UnityEngine.Audio;
using TMPro;

public class SettingsMenu : MonoBehaviour {

    [SerializeField] private AudioMixer _mixer = null;
    [SerializeField] private TextMeshProUGUI _musicText = null;
    [SerializeField] private TextMeshProUGUI _SfxText = null;

    const string _MusicParam = "MUSIC_VOL";
    const string _SfxParam = "SFX_VOL";

    public void SetMusicVol(float newVol) {
        MenuFunctions.ChangeAudioVolume(_mixer, _MusicParam, newVol);
        _musicText.text = (newVol * 100).ToString("F0");
    }
    public void SetSfxVol(float newVol) {
        MenuFunctions.ChangeAudioVolume(_mixer, _SfxParam, newVol);
        _SfxText.text = (newVol * 100).ToString("F0"); // Check to string params
    }

}
