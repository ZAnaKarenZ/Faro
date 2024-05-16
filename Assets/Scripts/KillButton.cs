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

    // Number of skeletons to deactivate
    public int numberOfSkeletons = 3;

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
        // Find all GameObjects with the specified tag
        GameObject[] targets = GameObject.FindGameObjectsWithTag(targetTag);

        // If there are no more skeletons, return
        if (targets.Length == 0)
        {
            return;
        }

        // If all skeletons are already eliminated, return
        if (allSkeletonsEliminated)
        {
            return;
        }

        // Ensure we don't select the same skeleton twice
        int randomIndex;
        do
        {
            randomIndex = Random.Range(0, targets.Length);
        } while (deactivatedIndices.Contains(randomIndex));

        // Deactivate the Mesh Renderer of the selected target object
        MeshRenderer renderer = targets[randomIndex].GetComponent<MeshRenderer>();
        if (renderer != null)
        {
            renderer.enabled = false;
        }

        // Add the index to the list of deactivated indices
        deactivatedIndices.Add(randomIndex);

        // Decrease the count of remaining skeletons
        numberOfSkeletons--;

        // Check if all skeletons are eliminated and print a message
        if (numberOfSkeletons <= 0 && !allSkeletonsEliminated)
        {
            SceneManager.LoadScene("Win");
            allSkeletonsEliminated = true;
        }
    }
}

