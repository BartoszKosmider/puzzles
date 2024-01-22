using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
	public void PlayGame()
	{
		SceneManager.LoadScene(1);
	}

	public void QuitGame()
	{
		Application.Quit();
	}

	public void ChangeTexture(Texture2D texture)
	{
		GlobalData.ActiveTexture = texture;
	}
}
