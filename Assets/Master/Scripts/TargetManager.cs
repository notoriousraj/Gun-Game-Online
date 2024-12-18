using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetManager : MonoBehaviour
{
    public List<TargetBehaviour> targets;
    public float randomInterval = 3f;

    private void Start()
    {
        GameManager.Instance.OnGameModeChanged += OnGameModeChanged;
    }

    private void OnGameModeChanged(bool isPlaying)
    {
        if (isPlaying)
        {
            StartCoroutine(RandomlyActivateTargets());
        }
        else
        {
            StopCoroutine(RandomlyActivateTargets());
        }
    }

    // Coroutine to randomly select targets and trigger the 'Get Up' animation
    private IEnumerator RandomlyActivateTargets()
    {
        while (true)
        {
            yield return new WaitForSeconds(randomInterval);

            // Pick a random target from the list
            int randomIndex = Random.Range(0, targets.Count);
            TargetBehaviour randomTarget = targets[randomIndex];
            // Debug.Log("Random target: " + randomTarget.name);

            // Trigger the 'Get Up' animation on the random target
            randomTarget.GetUp(); // TargetBehaviour will handle if it's already up
        }
    }
}
