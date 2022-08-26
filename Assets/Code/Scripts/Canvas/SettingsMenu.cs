using UnityEngine;
using UnityEngine.Audio;
using TMPro;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour {

    [SerializeField] private AudioMixer _mixer = null;
    [SerializeField] private Slider _musicSlider = null;
    [SerializeField] private TextMeshProUGUI _musicText = null;
    [SerializeField] private Slider _sfxSlider = null;
    [SerializeField] private TextMeshProUGUI _SfxText = null;

    const string _MusicParam = "MUSIC_VOL";
    const string _SfxParam = "SFX_VOL";

    private void Start() {
        SettingsData data = SaveSystem.Load(SaveSystem.DataType.Settings) as SettingsData;
        _musicSlider.value = data.MusicVol;
        _sfxSlider.value = data.SfxVol;
    }

    public void SetMusicVol(float newVol) {
        MenuFunctions.ChangeAudioVolume(_mixer, _MusicParam, newVol);
        _musicText.text = (newVol * 100).ToString("F0");
        GameManager.SettingsData.MusicVol = newVol;
        SaveSystem.Save(SaveSystem.DataType.Settings);
    }
    public void SetSfxVol(float newVol) {
        MenuFunctions.ChangeAudioVolume(_mixer, _SfxParam, newVol);
        _SfxText.text = (newVol * 100).ToString("F0"); // Check to string params
        GameManager.SettingsData.SfxVol = newVol;
        SaveSystem.Save(SaveSystem.DataType.Settings);
    }

    // Do Languge menu

}
