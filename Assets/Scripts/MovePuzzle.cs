using UnityEngine;

public class MovePuzzle : MonoBehaviour
{
	private void OnMouseDown()
	{
		if (GlobalData.IsSuccess)
			return;

		var renderer = gameObject.GetComponent<Renderer>();
		var emptyPuzzle = GameObject.Find(GlobalData.EmptyPuzzleName);

		if (renderer.transform.position.x == emptyPuzzle.transform.position.x &&
			(renderer.transform.position.z == emptyPuzzle.transform.position.z - 1 || renderer.transform.position.z == emptyPuzzle.transform.position.z + 1))
		{
			MovePuzzles(renderer, emptyPuzzle);
		}
		else if (renderer.transform.position.z == emptyPuzzle.transform.position.z &&
			(renderer.transform.position.x == emptyPuzzle.transform.position.x - 1 || renderer.transform.position.x == emptyPuzzle.transform.position.x + 1))
		{
			MovePuzzles(renderer, emptyPuzzle);
		}
	}

	private void MovePuzzles(Renderer renderer, GameObject emptyPuzzle)
	{
		var tempPos = renderer.transform.position;
		renderer.transform.position = emptyPuzzle.transform.position;
		emptyPuzzle.transform.position = tempPos;

		var validationResult = Validate();
		if (validationResult)
		{
			GlobalData.IsSuccess = true;
			GlobalData.Stopwatch.Stop();
		}
	}

	private bool Validate()
	{
		var puzzles = GameObject.FindGameObjectsWithTag(GlobalData.PuzzleTag);

		foreach (var puzzle in puzzles)
		{
			var movePuzzle = puzzle.GetComponent<MovePuzzle>();
			if (movePuzzle.InitPosition != movePuzzle.transform.position)
				return false;
		}

		return true;
	}

	public string PuzzleName { get; set; }
	public Vector3 InitPosition { get; set; }
}
