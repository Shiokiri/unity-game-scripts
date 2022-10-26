using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
	public Button newGameButton;
	public Button continueGameButton;
	public Button settingsButton;
	public Button quitGameButton;

	private void Awake()
	{
		Cursor.lockState = CursorLockMode.None;

		newGameButton.onClick.AddListener(NewGame);
		continueGameButton.onClick.AddListener(ContinueGame);
		quitGameButton.onClick.AddListener(QuitGame);
	}

	void NewGame()
	{
		Cursor.lockState = CursorLockMode.Locked;
		PlayerPrefs.DeleteAll();
		SceneController.Instance.TransitionToFirstLevel();
	}

	void ContinueGame()
	{
		Cursor.lockState = CursorLockMode.Locked;
		SceneController.Instance.TransitionToLoadGame();
	}

	void QuitGame()
	{
		Application.Quit();
	}
}
