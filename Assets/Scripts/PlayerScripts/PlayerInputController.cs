using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : MonoBehaviour
{
    private PlayerInput playerInput;
    private TableManager tableManager;

    private static string MOVEMENT_UI = "MovementUI";
    private static string BUYCARD_UI = "BuyCardUI";
    private static string HOUSE_BUILD_UI = "HouseBuildUI";

    private void Start()
    {
        this.tableManager = FindObjectOfType<TableManager>();
        this.playerInput = GetComponent<PlayerInput>();
        BuyInterface.onCreation += changeBuyInterfaceMap;
        HouseBuildInterface.onCreation += changeHouseBuildInterfaceMap;
        MovementInterface.onCreation += changeMovementInterfaceMap;


    }
    #region MovementUICode
    public void onSkipTurn(InputAction.CallbackContext callbackContext)
    {
        if (!isActivePlayer())
            return;
        if (!callbackContext.performed)
            return;
        FindObjectOfType<MovementInterface>().OnNextPlayerButton();
    }

    public void onThrowDice(InputAction.CallbackContext callbackContext)
    {
        if (!isActivePlayer())
            return;
        if (!callbackContext.performed)
            return;
        FindObjectOfType<MovementInterface>().OnStartMovementButton();
    }

    public void onTradeInterface(InputAction.CallbackContext callbackContext)
    {
        Debug.Log("@TODO Trade Interface");
    }

    public void onBuyStructures(InputAction.CallbackContext callbackContext)
    {
        if (!isActivePlayer())
            return;
        if (!callbackContext.performed)
            return;
        FindObjectOfType<MovementInterface>().OnBuyHouseButton();
    }
    #endregion

    #region BuycardUI
    public void onBuyCard(InputAction.CallbackContext callbackContext)
    {
        if (!isActivePlayer())
            return;
        if (!callbackContext.performed)
            return;
        FindObjectOfType<BuyInterface>().onBuyButton();
    }

    public void onSkipBuyCard(InputAction.CallbackContext callbackContext)
    {
        if (!isActivePlayer())
            return;
        if (!callbackContext.performed)
            return;
        FindObjectOfType<BuyInterface>().OnSkipButton();
    }
    #endregion

    #region StateMap status
    private void changeBuyInterfaceMap()
    {
        if (!isActivePlayer())
            return;
        playerInput.SwitchCurrentActionMap(BUYCARD_UI);
    }
    private void changeHouseBuildInterfaceMap()
    {
        if (!isActivePlayer())
            return;
        playerInput.SwitchCurrentActionMap(HOUSE_BUILD_UI);
    }
    private void changeMovementInterfaceMap()
    {
        if (!isActivePlayer())
            return;
        playerInput.SwitchCurrentActionMap(MOVEMENT_UI);
    }
    #endregion


    private bool isActivePlayer()
    {
        return tableManager.getActivePlayer().getId() == this.GetComponent<PlayerController>().getId();
    }
    
    private void Update()
    {
       //if(isActivePlayer())
       //     Debug.Log("Active player:" + tableManager.getActivePlayer().getId() + "Action map:" + playerInput.currentActionMap);
    }
}
