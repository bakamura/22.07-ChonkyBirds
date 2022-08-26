using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsData {

    private float _musicVol = 0.5f;
    public float MusicVol { get { return _musicVol; } set { _musicVol = value; } }
    private float _sfxVol = 0.5f;
    public float SfxVol { get { return _sfxVol; } set { _sfxVol = value; } }

}