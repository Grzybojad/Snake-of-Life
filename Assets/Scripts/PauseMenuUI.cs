using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class PauseMenuUI : MonoBehaviour
{
	public GameObject firstOption;

	private void OnEnable()
	{
		EventSystem.current.SetSelectedGameObject( firstOption );
	}

	public void ResumeGame()
	{
		Time.timeScale = 1;
	}

	public void MainMenu()
	{
		SceneManager.LoadScene( 0 );
	}
}
