using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToMainMenu : MonoBehaviour
{
	private void OnMouseDown()
	{
		GlobalData.IsSuccess = false;
		SceneManager.LoadScene(0);
	}
}
