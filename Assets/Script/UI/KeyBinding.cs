// using System.Collections;
// using System.Collections.Generic;
// using System.Net.Http.Headers;
// using UnityEngine;
// using UnityEngine.InputSystem;

// public class KeyBinding : MonoBehaviour
// {
//     [SerializeField] private InputActionReference moveAction = null;
//     [SerializeField] private InputActionReference interactAction = null;
//     [SerializeField] private InputActionReference shootAction = null;
//     [SerializeField] private InputActionReference reloadAction = null;
//     [SerializeField] private InputActionReference jumpAction = null;

//     private InputActionRebindingExtensions.RebindingOperation rebindingOperation;

//     private const string RebindsKey = "rebinds";

//     private void Start()
//     {
//         string rebinds = PlayerPrefs.GetString(RebindsKey, string.Empty);

//         if (string.IsNullOrEmpty(rebinds))
//         {
//             return;
//         }

//         PlayerController.PlayerInput.actions.LoadBinding
//     }
// }
