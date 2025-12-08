using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    private static readonly WaitForSeconds waitFor2point5Seconds = new(2.5f);
    private HashSet<PuzzleBrain> puzzleBrains;
    private bool allDone = false;

    public static bool playerHasWrench = false;

    [SerializeField] private GameObject celebrate;

    void Start()
    {
        celebrate.SetActive(false);

        puzzleBrains = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None)
            .OfType<PuzzleBrain>()
            .ToHashSet();

        StartCoroutine(CheckBrainsLoop());
    }

    private IEnumerator CheckBrainsLoop()
    {
        while (!allDone)
        {
            yield return waitFor2point5Seconds;

            bool doneIfTrue = true;
            foreach (PuzzleBrain brain in puzzleBrains)
            {
                if (!brain.CheckCompletion()) doneIfTrue = false;
            }

            if (doneIfTrue)
            {
                allDone = true;
                celebrate.SetActive(true);
            }
        }
    }

    public void PlayerObtainWrench()
    {
        playerHasWrench = true;
    }
}
