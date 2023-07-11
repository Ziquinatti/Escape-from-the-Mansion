using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle1 : MonoBehaviour
{
    public List<InputFieldGrabber> GrabberInputList;
    public List<string> CorrectYear;

    public bool PuzzleFinished = false;

    public void CheckPuzzleComplete()
    {
        for(int i = 0; i < GrabberInputList.Count; i++)
        {
            if (GrabberInputList[i].inputText != CorrectYear[i])
            {
                PuzzleFinished = false;
                break;
            }
            PuzzleFinished = true;
        }

        if (PuzzleFinished)
        {
            FinishPuzzle();
        }
    }

    private void FinishPuzzle()
    {
        Debug.Log("Puzzle finalizado!!!!");
        gameObject.SetActive(false);
    }
}
