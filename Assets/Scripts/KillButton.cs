using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SecondButtonTrigger : MonoBehaviour
{
    // Tag or layer name of the player GameObject
    public string playerTag = "Player";

    // Tag of the objects you want to deactivate
    public string targetTag = "Skeleton";

    // Flag to track if all skeletons are eliminated
    private bool allSkeletonsEliminated = false;

    // List to store the indices of deactivated skeletons
    private List<int> deactivatedIndices = new List<int>();

    private void OnTriggerEnter(Collider other)
    {
        // Check if the collider entering the trigger zone is the player
        if (other.CompareTag(playerTag))
        {
            // Deactivate a random skeleton's Mesh Renderer
            DeactivateRandomSkeleton();
        }
    }

    private void DeactivateRandomSkeleton()
    {
        // Get the list of active skeletons
        List<GameObject> targets = ButtonTrigger.activeSkeletons;

        // Find all GameObjects with the specified tag
        //GameObject[] targets = GameObject.FindGameObjectsWithTag(targetTag);

        // If there are no more skeletons, return
        if (targets.Count == 0)
        {
            return;
        }

        // If all skeletons are already eliminated, return
        if (allSkeletonsEliminated)
        {
            return;
        }

        // Select a random skeleton
        int randomIndex = Random.Range(0, targets.Count);

        // Destroy the selected skeleton
        GameObject skeletonToDestroy = targets[randomIndex];
        targets.RemoveAt(randomIndex); // Remove from the list before destroying
        Destroy(skeletonToDestroy);

        // Add the index to the list of deactivated indices
        deactivatedIndices.Add(randomIndex);

        // Check if all skeletons are eliminated and print a message
        if (targets.Count <= 0 && !allSkeletonsEliminated)
        {
            SceneManager.LoadScene("Win");
            allSkeletonsEliminated = true;
        }
    }
}

