using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetBehaviour : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private int pointsForHit = 10; // Points given for hitting this target
    [SerializeField] private float coolDownPeriod = 5f; // Time between hits
    private bool isUp = false; // Track if the target is already "up"
    [SerializeField] private AudioClip hitSound; // Sound to play on hit
    [SerializeField] private AudioClip getUpSound; // Sound to play on get up
    private AudioSource audioSource; // AudioSource component to play sounds
    private void Start()
    {
        // Initialize the audio source
        audioSource = GetComponent<AudioSource>();
        animator.SetTrigger("Hit");
    }

    private void OnCollisionEnter(Collision other)
    {
        if (isUp)
        {
            if (other.gameObject.tag == "Arrow") // Ensure the object hitting the target is tagged as "Arrow"
            {
                Debug.Log("HitAnimation");
                OnHit();
                other.gameObject.SetActive(false); // Deactivate the arrow
            }
        }
    }

    private void OnHit()
    {
        // Play the hit animation
        animator.SetTrigger("Hit");
        isUp = false;

        // Play hit sound
        if (hitSound != null)
        {
            audioSource.PlayOneShot(hitSound);
        }

        // Add points to the player's score
        GameManager.Instance.AddPoints(pointsForHit);

        StopCoroutine("CoolDown");
    }

    // Method to play the 'Get Up' animation (reverse of hit)
    public void GetUp()
    {
        animator.SetTrigger("GetUp");
        isUp = true;

        // Play get up sound
        if (getUpSound != null)
        {
            audioSource.PlayOneShot(getUpSound);
        }

        StopCoroutine("CoolDown");
        StartCoroutine("CoolDown");
    }

    IEnumerator CoolDown()
    {
        yield return new WaitForSeconds(coolDownPeriod);
        animator.SetTrigger("Hit");
        isUp = false;
    }
}
