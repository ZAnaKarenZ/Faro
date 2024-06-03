using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting;

[Preserve]
interface IInteractable
{
    public void Interact();
}


public class Interactor : MonoBehaviour
{
    public Transform InteractorSource;
    public float InteractRange;
    public Canvas interactionIcon;

    void Start()
    {
        if (interactionIcon != null)
        {
            interactionIcon.enabled=false;
        }
    }

    private void Update()
{
    Ray r = new Ray(InteractorSource.position, InteractorSource.forward);
    Debug.DrawRay(InteractorSource.position, InteractorSource.forward * InteractRange, Color.red); // Visualize the ray

    if (Physics.Raycast(r, out RaycastHit hitInfo, InteractRange))
    {
        Debug.Log("Hit: " + hitInfo.collider.gameObject.name);

        if (hitInfo.collider.gameObject.TryGetComponent(out IInteractable interactObject))
        {
            Debug.Log("Interactable Object Found: " + hitInfo.collider.gameObject.name);

            if (interactionIcon != null)
            {
                interactionIcon.enabled = true;
            }

            if (Input.GetKeyDown(KeyCode.X))
            {
                interactObject.Interact();
            }

            return;
        }
    }

    if (interactionIcon != null)
    {
        interactionIcon.enabled = false;
    }
}

}
