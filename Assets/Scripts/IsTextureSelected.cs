using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IsTextureSelected : MonoBehaviour
{
	void Update()
	{
		var button = this.gameObject.GetComponent<Button>();
		var textureName = GlobalData.ActiveTexture?.name;
		var buttonNameToSelect = textureName != null ? TextureNameToButtonNameMap[textureName] : null;
		if (string.IsNullOrEmpty(textureName) && button.name == "Image1")
		{
			button.Select();
		}
		else if (buttonNameToSelect != null && button.name == buttonNameToSelect)
		{
			button.Select();
		}
	}

	private Dictionary<string, string> TextureNameToButtonNameMap = new Dictionary<string, string>()
	{
		{ "texture1", "Image1" },
		{ "texture2", "Image2" },
		{ "texture3", "Image3" },
	};
}
