using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu (fileName = "New Puzzle", menuName = "Create Puzzle Asset")]
public class PuzzleCreator : ScriptableObject
{
    public new string name = "Puzzle";
    public int noOfRows = 2;
    public int noOfColumns = 2;
    
    public Vector2 puzzleStart;
    public Vector2 puzzleEnd;
    public List<Vector2> puzzleGrid;

    public bool IsValid(int[,] puzzleArray)
    {
        // Check if all the puzzle solved in a correct way
        return false;
    }


}
