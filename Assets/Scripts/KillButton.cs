using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SecondButtonTrigger : MonoBehaviour
{
    //Tag GameObject jugador
    public string playerTag = "Player";

    //Bandera para ver si se eliminaron todos los esqueletos
    private bool allSkeletonsEliminated = false;

    private void OnTriggerEnter(Collider other)
    {
        // Revisar si jugador colisionó con el botón
        if (other.CompareTag(playerTag))
        {
            //Destruir un esqueleto al azar
            DestroyRandomSkeleton();
        }
    }

    private void DestroyRandomSkeleton()
    {
        //Obtener la lista de esqueletos que existen
        List<GameObject> targets = ButtonTrigger.activeSkeletons;

        //Si no hay esqueletos o si ya se eliminaron, regresar
        if (targets.Count == 0)
        {
            return;
        }
        if (allSkeletonsEliminated)
        {
            return;
        }

        //Elegir un esqueleto al azar y eliminarlo
        int randomIndex = Random.Range(0, targets.Count);
        GameObject skeletonToDestroy = targets[randomIndex];
        targets.RemoveAt(randomIndex);
        Destroy(skeletonToDestroy);

        //Si se eliminaron todos los esqueletos, jugador gana
        if (targets.Count <= 0 && !allSkeletonsEliminated)
        {
            SceneManager.LoadScene("Win");
            allSkeletonsEliminated = true;
        }
    }
}

