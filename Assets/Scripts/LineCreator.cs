
using UnityEngine;

public class LineCreator : MonoBehaviour
{

	public PuzzleCreator puzzle1;

	

	public GameObject linePrefab;

	Line activeLine;

	void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			GameObject lineGO = Instantiate(linePrefab);
			activeLine = lineGO.GetComponent<Line>();
		}

		if (Input.GetMouseButtonUp(0))
		{
			activeLine = null;
		}

		if (activeLine != null)
		{
			Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			activeLine.UpdateLine(mousePos);
		}

	}

}