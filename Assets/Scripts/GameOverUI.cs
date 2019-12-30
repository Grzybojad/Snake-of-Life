using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class GameOverUI : MonoBehaviour
{
	public GameObject firstOption;

	private void OnEnable()
	{
		EventSystem.current.SetSelectedGameObject( firstOption );
	}

	public void PlayAgain()
	{
		SceneManager.LoadScene( 1 );
	}

	public void MainMenu()
	{
		SceneManager.LoadScene( 0 );
	}
}
