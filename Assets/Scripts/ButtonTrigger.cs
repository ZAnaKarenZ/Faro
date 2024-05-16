using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonTrigger : MonoBehaviour
{
    // Tag or layer name of the player GameObject
    public string playerTag = "Player";

    // Tag of the objects you want to activate/deactivate
    public string targetTag = "Skeleton";

     // Flag to track whether the first button is pushed
    private bool isFirstButtonPushed = false;

    // Reference to the second button GameObject
    public GameObject secondButton;

    private void OnTriggerEnter(Collider other)
    {
        // Check if the collider entering the trigger zone is the player
        if (other.CompareTag(playerTag) && isFirstButtonPushed == false)
        {
            // Find all GameObjects with the specified tag
            GameObject[] targets = GameObject.FindGameObjectsWithTag(targetTag);

            // Activate the Mesh Renderers of all target objects
            foreach (GameObject target in targets)
            {
                MeshRenderer renderer = target.GetComponent<MeshRenderer>();
                if (renderer != null)
                {
                    renderer.enabled = true;
                }
            }

            // Set the flag to true
            isFirstButtonPushed = true;

            // Enable the second button GameObject
            if (secondButton != null)
            {
                secondButton.SetActive(true);
            }
        }
    }
}
