using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameManager {

    private static ProgressData _progressData = SaveSystem.Load(SaveSystem.DataType.Progress) as ProgressData;
    public static ProgressData ProgressData { get { return _progressData; } set { _progressData = value; } }
    private static SettingsData _settingsData = SaveSystem.Load(SaveSystem.DataType.Settings) as SettingsData;
    public static SettingsData SettingsData { get { return _settingsData; } set { _settingsData = value; } }

}
