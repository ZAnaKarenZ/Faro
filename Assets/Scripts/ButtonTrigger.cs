using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class ButtonTrigger : MonoBehaviour
{
    // Tag del GameObject jugador
    public string playerTag = "Player";

    // Tag de los objetos a activar y desactivar
    public string targetTag = "Skeleton";

    //Bandera para ver si este primer botón ya fue presionado
    private bool isFirstButtonPushed = false;
    //Bandera para ver si la corrutina se está corriendo
    private bool isCoroutineRunning = false;
    //Bandera para ver si la corrutina está en espera
    private bool isWaiting = false;

    //Referencia al segundo botón
    public GameObject secondButton;

    //Creación de corrutinas
    private IEnumerator corrutinaE;
    private Coroutine corrutinaC;

    //Prefab
    [SerializeField]
    private GameObject _original;
    [SerializeField]
    private Transform _referencia;

    //Rango para offsets al crear esqueletos
    [SerializeField]
    private float offsetRange = 1.0f;
    [SerializeField]
    private float rotationOffsetRange = 360.0f;

    //Lista de esqueletos creados
    public static List<GameObject> activeSkeletons = new List<GameObject>();

    void Start(){
        
        //Verificar nulidad de objeto
        Assert.IsNotNull(_original, "ORIGINAL NO PUEDE SER NULO");
        Assert.IsNotNull(_referencia, "REFERENCIA NO PUEDE SER NULO");

        //Definir corrutinaE
        corrutinaE = CrearEsqueleto();
    }

    private void OnTriggerEnter(Collider other)
    {
        // Revisar si el jugador es quien entró a la zona del trigger
        if (other.CompareTag(playerTag) && isFirstButtonPushed == false)
        {
            //Iniciar la creación de esqueletos en la corrutina
            if (!isCoroutineRunning && !isFirstButtonPushed)
            {
                isCoroutineRunning = true;
                corrutinaC = StartCoroutine(corrutinaE);
            }

            //Bandera indica que este primer botón ya fue presionado
            isFirstButtonPushed = true;

            //Permitir la funcionalidad del segundo botón
            if (secondButton != null)
            {
                secondButton.SetActive(true);
            }

        }
    }

    void Update(){
        //No se podrán crear más de 20 esqueletos a la vez
        if(activeSkeletons.Count >= 20){
            isCoroutineRunning = false;
            StopCoroutine(corrutinaC);
        } 
        //Si hay menos de 20 esqueletos y ya se presionó el botón, seguir creando esqueletos
        else if (activeSkeletons.Count < 20 && isFirstButtonPushed && !isCoroutineRunning && !isWaiting){
            StartCoroutine(RestartCoroutineWithDelay());
        }
    }

    //Temporizador
    IEnumerator RestartCoroutineWithDelay()
    {
        isWaiting = true;
        yield return new WaitForSeconds(2);
        isCoroutineRunning = true;
        corrutinaC = StartCoroutine(corrutinaE);
        isWaiting = false;
    }

    //Corrutina que crea un nuevo esqueleto cada 4 segundos
    IEnumerator CrearEsqueleto()
    {
        while(true)
        {
            //Generar valores aleatorios para que haya un offset en la posición inicial
            Vector3 randomOffset = new Vector3(
                Random.Range(-offsetRange, offsetRange),
                Random.Range(-offsetRange, offsetRange),
                Random.Range(-offsetRange, offsetRange)
            );

            Quaternion randomRotation = Quaternion.Euler(
                _referencia.rotation.eulerAngles.x,
                _referencia.rotation.eulerAngles.y + Random.Range(-rotationOffsetRange, rotationOffsetRange),
                _referencia.rotation.eulerAngles.z
            );

            //Creación dinámica de ahogados / esqueletos
            GameObject newSkeleton = Instantiate(
                _original,
                _referencia.position + randomOffset,
                randomRotation
            );

            //Agregar esqueleto a la lista
            activeSkeletons.Add(newSkeleton);
            
            //Esperar 2 segundos
            yield return new WaitForSeconds(2);
        }
    }
}
