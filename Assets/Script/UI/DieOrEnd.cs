using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SlimUI.ModernMenu;
using UnityEngine.SceneManagement;

public class DieOrEnd : MonoBehaviour
{
    public enum Theme {custom1, custom2, custom3, custom4};
    
    [Header("THEME SETTINGS")]
    public Theme theme;
    private int themeIndex;
    public ThemedUIData themeController;

    public GameObject exitMenu;

    [Header("SFX")]
    [Tooltip("The GameObject holding the Audio Source component for the HOVER SOUND")]
    public AudioSource hoverSound;
    [Tooltip("The GameObject holding the Audio Source component for the AUDIO SLIDER")]
    public AudioSource sliderSound;
    [Tooltip("The GameObject holding the Audio Source component for the SWOOSH SOUND when switching to the Settings Screen")]
    public AudioSource swooshSound;

    public void LoadScene()
	{
        SceneManager.LoadScene("MainScene"); // 메인 씬으로
	}
}
