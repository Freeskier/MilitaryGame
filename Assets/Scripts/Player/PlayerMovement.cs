using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Threading;
using Cinemachine;
using System;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] MouseLook mouseLook;
    public CharacterController controller;
    public CinemachineInputProvider inputProvider;
    public float speed = 12.0f;
    public float gravity = -9.81f;
    private PlayerControl playerControl;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    public Transform startPoint;
    public Mutex mutex;
    private Transform cameraTransform;
    public GameObject hand;
    private Animator animator;

    Vector3 velocity;
    Vector2 mouseInput;
    bool isGrounded;


    void Awake()
    {
        playerControl = new PlayerControl();
        playerControl.Player.MouseX.performed += ctx => mouseInput.x = ctx.ReadValue<float>();
    }
    void Start()
    {
        transform.position = startPoint.position;
        cameraTransform = Camera.main.transform;
        Cursor.lockState = CursorLockMode.Locked;
        Inventory.instance.itemUsed += Inventory_ItemUsed;
        mutex = new Mutex(true, "InventoryAcessMutex");
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        //Sprawdzenie czy postac znajduje sie na podlozu
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = 0.0f;
        }

        Vector2 movement = playerControl.Player.PlayerMovement.ReadValue<Vector2>();
        Vector3 move = new Vector3(movement.x, 0, movement.y);
        move = cameraTransform.forward * move.z + cameraTransform.right * move.x;
        controller.Move(move * Time.fixedDeltaTime * speed);

        velocity.y += gravity * Time.fixedDeltaTime;
        controller.Move(velocity * Time.fixedDeltaTime);

        mouseLook.ReciveInput(mouseInput);

        lockMouseOnKey();

    }


    void lockMouseOnKey()
    {
        if (Keyboard.current.leftCtrlKey.isPressed)
        {
            Cursor.lockState = CursorLockMode.None;
            inputProvider.XYAxis.ToInputAction().Disable();
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            inputProvider.XYAxis.ToInputAction().Enable();
        }
    }

    void OnEnable()
    {
        playerControl.Enable();
    }

    void OnDisable()
    {
        playerControl.Disable();
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        mutex.WaitOne();
        IInventoryItem item = hit.collider.GetComponent<IInventoryItem>();
        if (item != null && hit.collider.enabled)
        {
            hit.collider.enabled = false;
            Inventory.instance.AddItem(item);
        }
        mutex.ReleaseMutex();
    }

    private void Inventory_ItemUsed(object sender, InventoryEventArgs e)
    {
        IInventoryItem item = e.Item;
        GameObject uItem = (item as MonoBehaviour).gameObject;
        int counter = hand.transform.childCount;
        if (counter != 0)
        {
            Transform tmp = hand.transform.GetChild(0);
            tmp.parent = null;
            tmp.gameObject.SetActive(false);
        }
        AttachItemToHand(uItem);
    }
    private void AttachItemToHand(GameObject uItem)
    {
        uItem.SetActive(true);
        uItem.transform.SetParent(hand.transform);
        uItem.transform.position = hand.transform.position;
        uItem.GetComponent<Collider>().enabled = false;
        uItem.GetComponent<Rigidbody>().useGravity = false;
    }
}
