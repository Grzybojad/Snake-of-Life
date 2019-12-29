using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuUI : MonoBehaviour
{
	public void ResumeGame()
	{
		Time.timeScale = 1;
	}

	public void MainMenu()
	{
		SceneManager.LoadScene( 0 );
	}
}
