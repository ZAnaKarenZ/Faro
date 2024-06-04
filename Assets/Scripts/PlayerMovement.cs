using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(CharacterController))]

public class PlayerMovement : MonoBehaviour

{
    // Singleton instance
    public static PlayerMovement instance;

    // Controller variables
    [SerializeField] private Camera playerCamera;
    [SerializeField] private float walkSpeed = 4f;
    [SerializeField] private float jumpPower = 7f;
    private float gravity = 10f;
    [SerializeField] private float lookSpeed = 2f;
    private float lookXLimit = 45f;
    private Vector3 moveDirection = Vector3.zero;
    private float rotationX = 0;
    private CharacterController characterController;

    // Collider
    private Collider c;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);
        float currentSpeedX = walkSpeed * Input.GetAxis("Vertical");
        float currentSpeedY = walkSpeed * Input.GetAxis("Horizontal");
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * currentSpeedX) + (right * currentSpeedY);

        if (Input.GetButton("Jump") && characterController.isGrounded)
        {
            moveDirection.y = jumpPower;
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }

        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        characterController.Move(moveDirection * Time.deltaTime);

        rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
        rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
        playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
    }

    void OnCollisionEnter(Collision c)
    {
        //Collision es un objeto que contiene información sobre la colisión: objetos involucrados, puntos de contacto, fuerzas involucradas
        print("COLLISION ENTER" + c.gameObject.layer + " " + c.gameObject.tag);
        if(c.gameObject.layer == 6)
        {
            print("COLLISION CON BOTON");
        }
    }

    void OnCollisionStay(Collision c)
    {
        print("COLLISION STAY");
    }

    void OnCollisionExit(Collision c)
    {
        print("COLLISION EXIT");
    }

    //TRIGGERS
    //Detecta colisión sin reacción física

    void onTriggerEnter(Collider c)
    {
        print("TRIGGER ENTER");
    }

    void onTriggerStay(Collider c)
    {
        print("TRIGGER STAY");
    }

    void onTriggerExit(Collider c)
    {
        print("TRIGGER EXIT");
    }

}

