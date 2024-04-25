using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    [SerializeField]
    private PlayerInputManager _playerInputManager;


    private void OnEnable()
    {
        _playerInputManager.onPlayerJoined += OnPlayerJoin;
    }

    private void OnPlayerJoin(PlayerInput playerInput)
    {
        StartCoroutine(WaitTo_SetupPlayer(playerInput));
    }

    IEnumerator WaitTo_SetupPlayer(PlayerInput playerInput)
    {
        yield return new WaitForEndOfFrame();
        Debug.Log("OnPlayerJoin");
        //PlayerController.SetupPlayerDetail(playerInput.playerIndex);
    }
}
