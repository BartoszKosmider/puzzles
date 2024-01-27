using System.Collections.Generic;
using System.Linq;
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
		var puzzles = GameObject.FindGameObjectsWithTag(GlobalData.PuzzleTag).ToList();
		var random = new System.Random();
		var indexToMove = 15;
		for (int i = 0; i < 1000; i++)
		{
			indexToMove = positions.FindIndex(x => x == 15);
			var puzzle1 = puzzles.FirstOrDefault(p => p.name == GetPuzzleName(15));
			var newIndex = Test[indexToMove].OrderBy(x => random.Next()).FirstOrDefault();

			var puzzlePosition = positions[newIndex];
			var puzzle2 = puzzles.FirstOrDefault(p => p.name == GetPuzzleName(puzzlePosition));

			var temp = positions[indexToMove];
			positions[indexToMove] = positions[newIndex];
			positions[newIndex] = temp;

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

	private Dictionary<int, List<int>> Test = new Dictionary<int, List<int>>
	{
		{ 0, new List<int>() { 1, 4 } },
		{ 1, new List<int>() { 0, 2, 5 } },
		{ 2, new List<int>() { 1, 3, 6 } },
		{ 3, new List<int>() { 2, 7 } },
		{ 4, new List<int>() { 0, 5, 8 } },
		{ 5, new List<int>() { 1, 4, 6, 9 } },
		{ 6, new List<int>() { 2, 5, 7, 10 } },
		{ 7, new List<int>() { 3, 6, 11 } },
		{ 8, new List<int>() { 4, 9, 12 } },
		{ 9, new List<int>() { 5, 8, 10, 13 } },
		{ 10, new List<int>() { 6, 9, 11, 14 } },
		{ 11, new List<int>() { 7, 10, 15 } },
		{ 12, new List<int>() { 8, 13 } },
		{ 13, new List<int>() { 9, 12, 14 } },
		{ 14, new List<int>() { 10, 13, 15 } },
		{ 15, new List<int>() { 11, 14 } },
	};

	private List<int> positions = new List<int>()
	{
		0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15
	};
}
