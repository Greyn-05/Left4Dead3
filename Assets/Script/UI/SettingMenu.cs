using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using SlimUI.ModernMenu;

public class SettingMenu : MonoBehaviour
{
     public enum Theme {custom1, custom2, custom3, custom4};
    
    [Header("THEME SETTINGS")]
    public Theme theme;
    private int themeIndex;
    public ThemedUIData themeController;

    public GameObject exitMenu;

    [Header("PANELS")]
    [Tooltip("The UI Panel parenting all sub menus")]
    public GameObject SettingCanvas;
    [Tooltip("The UI Panel that holds the CONTROLS window tab")]
    public GameObject PanelControls;
    [Tooltip("The UI Panel that holds the VIDEO window tab")]
    public GameObject PanelVideo;
    [Tooltip("The UI Panel that holds the GAME window tab")]
    public GameObject PanelGame;
    [Tooltip("The UI Panel that holds the KEY BINDINGS window tab")]
	public GameObject PanelCrossHair;
	[Tooltip("The UI Panel that holds the CROSSHAIR tab")]
    public GameObject PanelKeyBindings;
    [Tooltip("The UI Sub-Panel under KEY BINDINGS for MOVEMENT")]
    public GameObject PanelMovement;
    [Tooltip("The UI Sub-Panel under KEY BINDINGS for COMBAT")]
    public GameObject PanelCombat;
    [Tooltip("The UI Sub-Panel under KEY BINDINGS for GENERAL")]
    public GameObject PanelGeneral;
        

    // highlights in settings screen
    [Header("SETTINGS SCREEN")]
    [Tooltip("Highlight Image for when GAME Tab is selected in Settings")]
    public GameObject lineGame;
    [Tooltip("Highlight Image for when VIDEO Tab is selected in Settings")]
    public GameObject lineVideo;
    [Tooltip("Highlight Image for when CONTROLS Tab is selected in Settings")]
    public GameObject lineControls;
    [Tooltip("Highlight Image for when KEY BINDINGS Tab is selected in Settings")]
	public GameObject lineCrossHair;
	[Tooltip("Highlight Image for when CROSSHAIR Tab is selected in Settings")]
    public GameObject lineKeyBindings;
    [Tooltip("Highlight Image for when MOVEMENT Sub-Tab is selected in KEY BINDINGS")]
    public GameObject lineExit;
    [Tooltip("Highlight Image for when EXIT Tab is selected in Settings")]
    public GameObject lineMovement;
    [Tooltip("Highlight Image for when COMBAT Sub-Tab is selected in KEY BINDINGS")]
    public GameObject lineCombat;
    [Tooltip("Highlight Image for when GENERAL Sub-Tab is selected in KEY BINDINGS")]
    public GameObject lineGeneral;


	[Header("SFX")]
    [Tooltip("The GameObject holding the Audio Source component for the HOVER SOUND")]
    public AudioSource hoverSound;
    [Tooltip("The GameObject holding the Audio Source component for the AUDIO SLIDER")]
    public AudioSource sliderSound;
    [Tooltip("The GameObject holding the Audio Source component for the SWOOSH SOUND when switching to the Settings Screen")]
    public AudioSource swooshSound;

	public GameObject[] lineCrossHairs;

	void Start()
	{
        SetThemeColors();
        exitMenu.SetActive(false);
	}

	void SetThemeColors()
	{
		switch (theme)
		{
			case Theme.custom1:
				themeController.currentColor = themeController.custom1.graphic1;
				themeController.textColor = themeController.custom1.text1;
				themeIndex = 0;
				break;
			case Theme.custom2:
				themeController.currentColor = themeController.custom2.graphic2;
				themeController.textColor = themeController.custom2.text2;
				themeIndex = 1;
				break;
			case Theme.custom3:
				themeController.currentColor = themeController.custom3.graphic3;
				themeController.textColor = themeController.custom3.text3;
				themeIndex = 2;
				break;
			case Theme.custom4:
				themeController.currentColor = themeController.custom4.graphic4;
				themeController.textColor = themeController.custom4.text4;
				themeIndex = 3;
				break;
			default:
				Debug.Log("Invalid theme selected.");
				break;
			}
	}

	public void ReturnMenu()
	{
        exitMenu.SetActive(false);
        GamePanel();
	}

    public void ReturnGame()
    {
        // 다시 게임 화면으로
    }

	void DisablePanels()
	{
		PanelControls.SetActive(false);
		PanelVideo.SetActive(false);
		PanelGame.SetActive(false);
		PanelCrossHair.SetActive(false);
		PanelKeyBindings.SetActive(false);

		lineGame.SetActive(false);
		lineControls.SetActive(false);
		lineVideo.SetActive(false);
		lineKeyBindings.SetActive(false);
		lineCrossHair.SetActive(false);
        lineExit.SetActive(false);

		PanelMovement.SetActive(false);
		lineMovement.SetActive(false);
		PanelCombat.SetActive(false);
		lineCombat.SetActive(false);
		PanelGeneral.SetActive(false);
		lineGeneral.SetActive(false);
	}

	public void GamePanel()
	{
		DisablePanels();
		PanelGame.SetActive(true);
		lineGame.SetActive(true);
	}

	public void VideoPanel()
	{
		DisablePanels();
		PanelVideo.SetActive(true);
		lineVideo.SetActive(true);
	}

	public void ControlsPanel()
	{
		DisablePanels();
		PanelControls.SetActive(true);
		lineControls.SetActive(true);
	}

	public void CrossHairPanel()
	{
		DisablePanels();
		PanelCrossHair.SetActive(true);
		lineCrossHair.SetActive(true);
	}

	public void KeyBindingsPanel()
	{
		DisablePanels();
		MovementPanel();
		PanelKeyBindings.SetActive(true);
		lineKeyBindings.SetActive(true);
	}

	public void MovementPanel()
	{
		DisablePanels();
		PanelKeyBindings.SetActive(true);
		PanelMovement.SetActive(true);
		lineMovement.SetActive(true);
	}

	public void CombatPanel()
	{
		DisablePanels();
		PanelKeyBindings.SetActive(true);
		PanelCombat.SetActive(true);
		lineCombat.SetActive(true);
	}

	public void GeneralPanel()
	{
		DisablePanels();
		PanelKeyBindings.SetActive(true);
		PanelGeneral.SetActive(true);
		lineGeneral.SetActive(true);
	}

	public void PlayHover()
	{
		hoverSound.Play();
	}

	public void PlaySFXHover()
	{
		sliderSound.Play();
	}

	public void PlaySwoosh()
	{
		swooshSound.Play();
	}

	// Are You Sure - Quit Panel Pop Up
	public void AreYouSure()
	{
        DisablePanels();
		exitMenu.SetActive(true);
	}

    public void LoadScene()
	{
        SceneManager.LoadScene("Seyeon_Start"); // 씬 이름 추후 수정 필
	}

	public GameObject GetLineCrossHairObject(int index)
	{
		if (index >= 0 && index < lineCrossHairs.Length)
		{
			return lineCrossHairs[index];
		}

		return null;
	}

	// public void QuitGame()
	// {
	// 	#if UNITY_EDITOR
	// 		UnityEditor.EditorApplication.isPlaying = false;
	// 	#else
	// 		Application.Quit();
	// 	#endif
	// }

	// // Load Bar synching animation
	// IEnumerator LoadAsynchronously(string sceneName)
	// { // scene name is just the name of the current scene being loaded
	// 	AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
	// 	operation.allowSceneActivation = false;
	// 	mainCanvas.SetActive(false);
	// 	loadingMenu.SetActive(true);

	// 	while (!operation.isDone)
	// 	{
	// 		float progress = Mathf.Clamp01(operation.progress / .95f);
	// 		loadingBar.value = progress;

	// 		if (operation.progress >= 0.9f && waitForInput)
	// 		{
	// 			loadPromptText.text = "Press " + userPromptKey.ToString().ToUpper() + " to continue";
	// 			loadingBar.value = 1;

	// 			if (Input.GetKeyDown(userPromptKey))
	// 			{
	// 				operation.allowSceneActivation = true;
	// 			}
    //         }
	// 		else if (operation.progress >= 0.9f && !waitForInput)
	// 		{
	// 			operation.allowSceneActivation = true;
	// 		}

	// 		yield return null;
	// 	}
	// }
}
