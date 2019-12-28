using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
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
