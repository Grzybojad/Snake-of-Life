using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour
{
	public GameObject firstOption;

	private void OnEnable()
	{
		EventSystem.current.SetSelectedGameObject( null );
		StartCoroutine( "SetFirstSelected" );
	}

	IEnumerator SetFirstSelected()
	{
		// Wait for 1 frame
		yield return new WaitForEndOfFrame();

		EventSystem.current.SetSelectedGameObject( firstOption );
	}

	public void PlayGame()
	{
		SceneManager.LoadScene( 1 );
	}

	public void QuitGame()
	{
		Debug.Log( "Exit " );
		Application.Quit();
	}
}
