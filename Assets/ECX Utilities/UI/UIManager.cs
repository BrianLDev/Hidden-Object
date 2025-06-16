using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using EcxUtilities;

public class UIManager : Singleton<UIManager> {
	[SerializeField] private Canvas mainMenuCanvas;
	[SerializeField] private Canvas gameHudCanvas;
	[SerializeField] private Canvas optionsMenuCanvas;
	[SerializeField] private Canvas gameOverScreenCanvas;
	private List<Canvas> canvasList;
	private Canvas currentCanvas;
	private bool isOptionsUIVisible = false;

	private void Awake() {
		// Populate canvasList with all avaialble Canvases
		if (canvasList == null)
			canvasList = new List<Canvas>();
		if (mainMenuCanvas) canvasList.Add(mainMenuCanvas);
		if (gameHudCanvas) canvasList.Add(gameHudCanvas);
		if (optionsMenuCanvas) canvasList.Add(optionsMenuCanvas);
		if (gameOverScreenCanvas) canvasList.Add(gameOverScreenCanvas);
		if (SceneManager.GetActiveScene() == SceneManager.GetSceneByBuildIndex(0))
			ActivateCanvas(mainMenuCanvas);
		if (SceneManager.GetActiveScene() == SceneManager.GetSceneByBuildIndex(1))
			ActivateCanvas(gameHudCanvas);
	}

	// TODO: IMPROVE THESE SCENE LOADERS SO THEY'RE NOT DEPENDENT ON THE BUILD NUMBER
	public void LoadMainMenu() {
		SceneManager.LoadScene(0);
		ActivateCanvas(mainMenuCanvas);
	}

	public void LoadGame() {
		SceneManager.LoadScene(1);
		ActivateCanvas(gameHudCanvas);
	}

	public void LoadGameOverUI() {
		Canvas canvas = gameOverScreenCanvas;
		float delay = 1.5f;
		object[] parameters = new object[2] { canvas, delay };
		StartCoroutine("ShowUIWithDelay", parameters);
	}

	private IEnumerator ShowUIWithDelay(object[] parameters) {
		yield return new WaitForSeconds((float)parameters[1]);
		ActivateCanvas((Canvas)parameters[0]);
		yield return null;
	}

	public void ToggleOptionsMenu() {
		isOptionsUIVisible = !isOptionsUIVisible;
		optionsMenuCanvas.gameObject.SetActive(isOptionsUIVisible);
		currentCanvas.gameObject.SetActive(!isOptionsUIVisible);
	}

	private void DisableAllCanvases() {
		foreach (Canvas canvas in canvasList)
			canvas.gameObject.SetActive(false);
	}

	private void ActivateCanvas(Canvas canvas) {
		DisableAllCanvases();
		currentCanvas = canvas;
		currentCanvas.gameObject.SetActive(true);
	}

	public void ReloadScene() {
		Scene scene = SceneManager.GetActiveScene();
		SceneManager.LoadScene(scene.name);
	}

	public void ExitGame() {
		// TODO: ADD AN "ARE YOU SURE" MESSAGE
		#if UNITY_EDITOR
			UnityEditor.EditorApplication.isPlaying = false;
		#else
			Application.Quit();
		#endif
	}

	// USE THE METHODS BELOW BY ATTACHING THIS SCRIPT TO AN EVENT TRIGGER ON YOUR UI BUTTONS
	public static void PlayMouseOver() {
		float pitchRangeUI = AudioManager.PitchRangeUI;
		float pitch = Random.Range(1 - pitchRangeUI / 2, 1 + pitchRangeUI / 2);
		AudioManager.Instance.PlayClip(AudioManager.Instance.UISfxManager.mouseOver, AudioCategory.UI, 1, pitch);
	}
	public static void PlayButtonClick() {
		float pitchRangeUI = AudioManager.PitchRangeUI;
		float pitch = Random.Range(1 - pitchRangeUI / 2, 1 + pitchRangeUI / 2);
		AudioManager.Instance.PlayClip(AudioManager.Instance.UISfxManager.buttonClick, AudioCategory.UI, 1, pitch);
	}
	public static void PlayErrorSound() {
		float pitchRangeUI = AudioManager.PitchRangeUI;
		float pitch = Random.Range(1 - pitchRangeUI / 2, 1 + pitchRangeUI / 2);
		AudioManager.Instance.PlayClip(AudioManager.Instance.UISfxManager.errorSound, AudioCategory.UI, 1, pitch);
	}
}