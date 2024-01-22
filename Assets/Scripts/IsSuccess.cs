using UnityEngine;

public class IsSuccess : MonoBehaviour
{
	private void Start()
	{
		var renderer = GetComponent<Renderer>();
		renderer.enabled = false;
	}

	void Update()
	{
		if (!GlobalData.IsSuccess)
			return;

		var renderer = GetComponent<Renderer>();
		renderer.enabled = true;
	}
}
