using UnityEngine;

public class CreatePuzzles : MonoBehaviour
{
	private int textureWidth;
	private int textureHeight;

	private void Start()
	{
		SetTextureIfNecessary();
		InitializePuzzles();
		ChangePuzzlesPositions();
		DestroyTemporaryObject();
		GlobalData.Stopwatch.Restart();
	}

	private void ChangePuzzlesPositions()
	{
		var puzzles = GameObject.FindGameObjectsWithTag(GlobalData.PuzzleTag);
		var random = new System.Random();
		for (int i = 0; i < 100; i++)
		{
			var random1 = random.Next(0, GlobalData.CubesPerAxis * GlobalData.CubesPerAxis);
			var random2 = random.Next(0, GlobalData.CubesPerAxis * GlobalData.CubesPerAxis);

			var puzzle1 = puzzles[random1];
			var puzzle2 = puzzles[random2];
			var tempPos = puzzle1.transform.position;
			puzzle1.transform.position = puzzle2.transform.position;
			puzzle2.transform.position = tempPos;
		}
	}

	private void SetTextureIfNecessary()
	{
		if (GlobalData.ActiveTexture != null)
			return;

		var renderer = gameObject.GetComponent<Renderer>();
		GlobalData.ActiveTexture = (Texture2D)renderer.sharedMaterial.mainTexture;
	}

	private void InitializePuzzles()
	{
		var index = 0;
		for (int x = 0; x < GlobalData.CubesPerAxis; x++)
			for (int z = 0; z < GlobalData.CubesPerAxis; z++)
			{
				CreateSinglePuzzle(x, z, index);
				index++;
			}
	}

	private void CreateSinglePuzzle(int xCoord, int zCoord, int index)
	{
		var coordinates = new Vector3(-xCoord, 2, -zCoord);
		var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
		cube.name = GetPuzzleName(index);

		var rd = cube.GetComponent<Renderer>();
		rd.material = GetComponent<Renderer>().material;
		rd.tag = GlobalData.PuzzleTag;
		cube.transform.localScale = transform.localScale / GlobalData.CubesPerAxis;
		// set position
		var firstCube = transform.position - transform.localScale / 2 + cube.transform.localScale / 2;
		cube.transform.position = firstCube + Vector3.Scale(coordinates, cube.transform.localScale);
		cube.transform.position = new Vector3(cube.transform.position.x, 1, cube.transform.position.z);

		if (xCoord + 1 == GlobalData.CubesPerAxis && zCoord + 1 == GlobalData.CubesPerAxis)
			SetEmptyTexture(rd);
		else
			SetTexture(rd, xCoord, zCoord);

		cube.AddComponent<MovePuzzle>();
		var movePuzzle = cube.GetComponent<MovePuzzle>();
		movePuzzle.PuzzleName = GetPuzzleName(index);
		movePuzzle.InitPosition = cube.transform.position;
	}

	private void DestroyTemporaryObject()
	{
		var test = GameObject.Find("Puzzles");
		Destroy(test);
	}

	private void SetEmptyTexture(Renderer renderer)
	{
		var newTexture = new Texture2D(textureWidth, textureHeight);
		renderer.material.mainTexture = newTexture;
	}

	private void SetTexture(Renderer rd, int xCoord, int zCoord)
	{
		//var texture = (Texture2D)rd.sharedMaterial.mainTexture;
		var texture = GlobalData.ActiveTexture;
		textureWidth = texture.width / GlobalData.CubesPerAxis;
		textureHeight = texture.height / GlobalData.CubesPerAxis;

		var x = xCoord * textureWidth;
		var y = zCoord * textureHeight;
		var pixels = texture.GetPixels(x, y, textureWidth, textureHeight, 0);
		var newTexture = new Texture2D(textureWidth, textureHeight);
		newTexture.SetPixels(pixels);
		newTexture.Apply();

		rd.material.mainTexture = newTexture;
	}

	private string GetPuzzleName(int index)
	{
		return $"Puzzle:{index}";
	}
}
