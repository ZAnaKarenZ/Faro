using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonTrigger : MonoBehaviour
{
    // Tag del GameObject jugador
    public string playerTag = "Player";

    // Tag de los objetos a activar y desactivar
    public string targetTag = "Skeleton";

     //Bandera para ver si este primer botón ya fue presionado
    private bool isFirstButtonPushed = false;

    //Referencia al segundo botón
    public GameObject secondButton;

    //Creación de corrutinas
    private IEnumerator corrutinaE;
    private Coroutine corrutinaC;

    void Start(){

        //Definir corrutinaE
        corrutinaE = CrearEsqueleto();
    }

    private void OnTriggerEnter(Collider other)
    {
        // Revisar si el jugador es quien entró a la zona del trigger
        if (other.CompareTag(playerTag) && isFirstButtonPushed == false)
        {
            //Iniciar la creación de esqueletos en la corrutina
            corrutinaC = StartCoroutine(corrutinaE);

            //Bandera indica que este primer botón ya fue presionado
            isFirstButtonPushed = true;

            //Permitir la funcionalidad del segundo botón
            if (secondButton != null)
            {
                secondButton.SetActive(true);
            }
        }
    }

    //Corrutina que crea un nuevo esqueleto cada 4 segundos
    IEnumerator CrearEsqueleto()
    {
        while(true)
        {
            // Find all GameObjects with the specified tag
            GameObject[] targets = GameObject.FindGameObjectsWithTag(targetTag);
            foreach (GameObject target in targets)
            {
                MeshRenderer renderer = target.GetComponent<MeshRenderer>();
                if (renderer != null)
                {
                    renderer.enabled = true;
                }
            }
            yield return new WaitForSeconds(4);
        }
    }
}
