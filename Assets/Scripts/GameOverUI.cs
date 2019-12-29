using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
	public void PlayAgain()
	{
		SceneManager.LoadScene( 1 );
	}

	public void MainMenu()
	{
		SceneManager.LoadScene( 0 );
	}
}
