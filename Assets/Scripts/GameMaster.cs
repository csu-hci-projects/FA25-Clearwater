using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    private static readonly WaitForSeconds waitFor2point5Seconds = new(2.5f);
    private HashSet<PuzzleBrain> puzzleBrains;
    private bool allDone = false;

    void Start()
    {
        puzzleBrains = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None).OfType<PuzzleBrain>().ToHashSet();

        StartCoroutine(CheckBrainsLoop());
    }

    private IEnumerator CheckBrainsLoop()
    {
        while (true)
        {
            yield return waitFor2point5Seconds;

            if (allDone) break;  // and do something interesting probably

            bool doneIfTrue = true;
            foreach (PuzzleBrain brain in puzzleBrains)
            {
                if (!brain.CheckCompletion()) doneIfTrue = false;
            }
            if (doneIfTrue) allDone = true;
        }
    }
}
