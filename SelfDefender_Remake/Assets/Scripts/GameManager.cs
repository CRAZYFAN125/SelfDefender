using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using Crazy;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("MainProps:")]
    public static GameManager instance;
    public Transform cam;
    public float moveSpeed = 14f;
    Vector2 moveVector;
    public GameObject ShopPanel;
    [Header("SecondaryProps:")]
    [SerializeField] private Text Money;
    public int MoneyCount = 100;
    public bool isShopOpen { get; private set; } = false;
    private void Awake()
    {
        if (instance!=null)
        {
            Destroy(this);
            return;
        }
        instance = this;
    }
    private void FixedUpdate()
    {
        cam.position += new Vector3(moveVector.x, moveVector.y)*moveSpeed*Time.fixedDeltaTime;
        Money.text = $"{MoneyCount} Coins";
    }

    public void Move(InputAction.CallbackContext callback)
    {
        if (callback.performed)
        {
            moveVector = callback.ReadValue<Vector2>();
        }
        if (callback.canceled)
        {
            moveVector = new Vector2(0, 0);
        }
    }
    public void OpenOrCloseShop(InputAction.CallbackContext callback)
    {
        if (callback.performed)
        {
            isShopOpen = !isShopOpen;
            ShopPanel.SetActive(isShopOpen);
            if (isShopOpen)
            {
                Time.timeScale = .1f;
            }
            else
            {
                Time.timeScale = 1f;
            }
        }
    }
}
