using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenuSettings : MonoBehaviour
{
    [SerializeField] private GameObject settingsMenu;
   public void LoadMainGame(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void enableSettings()
    {
        bool On = settingsMenu.activeSelf;
        settingsMenu.SetActive(!On);
    }
}
