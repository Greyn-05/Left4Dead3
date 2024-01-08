// using System.Collections;
// using System.Collections.Generic;
// using System.Net.Http.Headers;
// using UnityEngine;
// using UnityEngine.InputSystem;
// using TMPro;
// using System.Linq;
// using System;

// public class KeyBinding : MonoBehaviour
// {
//     [Header("Action References")] // InputActionReference 타입의 jumpAction을 인스펙터에서 설정할 수 있게 함
//     [SerializeField] private InputActionReference moveAction = null;
//     [SerializeField] private InputActionReference interactAction = null;
//     [SerializeField] private InputActionReference reloadAction = null;
//     [SerializeField] private InputActionReference jumpAction = null;

//     [Header("Movement Binding Texts")]
//     [SerializeField] private TMP_Text bindingTextUp = null;
//     [SerializeField] private TMP_Text bindingTextDown = null;
//     [SerializeField] private TMP_Text bindingTextLeft = null;
//     [SerializeField] private TMP_Text bindingTextRight = null;

//     [Header("UI References")]
//     [SerializeField] private TMP_Text interactBindingText = null;
//     [SerializeField] private TMP_Text reloadBindingText = null;
//     [SerializeField] private TMP_Text jumpBindingText = null;

//     [SerializeField] private GameObject startRebindingObject = null;
//     [SerializeField] private GameObject waitingForInputObject = null;

//     private InputActionRebindingExtensions.RebindingOperation rebindingOperation;

//     private void Start()
//     {
//         UpdateBindingDisplay(interactAction, interactBindingText);
//         UpdateBindingDisplay(reloadAction, reloadBindingText);
//         UpdateBindingDisplay(jumpAction, jumpBindingText);
//     }

//     public void StartRebinding(string actionName, string bindingName)
//     {
//         InputAction actionToRebind = null;
//         TMP_Text bindingTextToUpdate = null;

//         switch (actionName)
//         {
//             case "Move":
//                 actionToRebind = moveAction.action;
//                 switch (bindingName)
//                 {
//                     case "Up":
//                         bindingTextToUpdate = bindingTextUp;
//                         break;
//                     case "Down":
//                         bindingTextToUpdate = bindingTextDown;
//                         break;
//                     case "Left":
//                         bindingTextToUpdate = bindingTextLeft;
//                         break;
//                     case "Right":
//                         bindingTextToUpdate = bindingTextRight;
//                         break;
//                 }
//                 break;
//             case "Interact":
//                 actionToRebind = interactAction.action;
//                 bindingTextToUpdate = interactBindingText;
//                 break;
//             case "Reload":
//                 actionToRebind = reloadAction.action;
//                 bindingTextToUpdate = reloadBindingText;
//                 break;
//             case "Jump":
//                 actionToRebind = jumpAction.action;
//                 bindingTextToUpdate = jumpBindingText;
//                 break;
//         }

//         if (actionToRebind != null)
//         {
//             startRebindingObject.SetActive(false);
//             waitingForInputObject.SetActive(true);

//             var bindingIndex = FindBindingIndex(actionToRebind, "<Keyboard>/someKey");

//             if (bindingIndex >= 0)
//             {
//                 actionToRebind.Disable();
//                 actionToRebind.PerformInteractiveRebinding(bindingIndex)
//                 .WithControlsExcluding("Mouse") // 마우스 컨트롤은 제외
//                 .OnMatchWaitForAnother(0.1f) // 입력 매치 후 0.1초 대기
//                 .OnComplete(operation => RebindComplete(operation, bindingTextToUpdate)) // 리바인딩 완료 시 RebindComplete 메서드 호출
//                 .Start(); // 리바인딩 작업 시작
//             }
//         }
//     }

//     private void StartRebindingComposite(InputActionReference actionRef, string bindingName, TMP_Text bindingText)
//     {
//         InputAction action = actionRef.action;

//         int compositeIndex = action.bindings.IndexOf(b => b.isComposite && b.name == bindingName);

//         if (compositeIndex >= 0)
//         {
//             int partBindingIndexUp = compositeIndex + 1;
//             int partBindingIndexDown = compositeIndex + 2;
//             int partBindingIndexLeft = compositeIndex + 3;
//             int partBindingIndexRight = compositeIndex + 4;

//             StartRebindingForPart(action, partBindingIndexUp, "Up", bindingText);
//             StartRebindingForPart(action, partBindingIndexDown, "Down", bindingText);
//             StartRebindingForPart(action, partBindingIndexLeft, "Left", bindingText);
//             StartRebindingForPart(action, partBindingIndexRight, "Right", bindingText);
//         }
//     }

//     private void StartRebindingForPart(InputAction action, int bindingIndex, string partName, TMP_Text bindingText)
//     {
//         if (bindingIndex < 0 || bindingIndex >= action.bindings.Count)
//         {
//             return;
//         }

//         var rebindOperation = action.PerformInteractiveRebinding(bindingIndex)
//             .WithControlsExcluding("Mouse") // 마우스 제외
//             .OnMatchWaitForAnother(0.1f) // 입력 후 0.1초 대기
//             .OnComplete(operation => RebindComplete(action, bindingText, partName))
//             .Start(); // 리바인딩 시작
//     }

//     private void RebindComplete(InputActionRebindingExtensions.RebindingOperation operation, TMP_Text bindingTextToUpdate)
//     {
//         operation.action.Enable();
//         var bindingIndex = operation.selectedControlIndex;
//         var bindingDisplayString = operation.action.GetBindingDisplayString(bindingIndex);
//         bindingTextToUpdate.text = bindingDisplayString;
//         operation.Dispose();
//     }

//     private void UpdateBindingDisplay(InputActionReference actionRef, TMP_Text bindingTextToUpdate)
//     {
//         var action = actionRef.action;
//         var bindingIndex = action.GetBindingIndexForControl(action.controls[0]);
//         var bindingDisPlayString = action.GetBindingDisplayString(bindingIndex, InputBinding.DisplayStringOptions.DontIncludeInteractions);
//         bindingTextToUpdate.text = bindingDisPlayString;

//         int bindingIndexUp = FindBindingIndex(action, "<Keyboard>/w");
//         int bindingIndexDown = FindBindingIndex(action, "<Keyboard>/s");
//         int bindingIndexLeft = FindBindingIndex(action, "<Keyboard>/a");
//         int bindingIndexRight = FindBindingIndex(action, "<Keyboard>/d");

//         UpdateBindingText(bindingTextUp, action, bindingIndexUp);
//         UpdateBindingText(bindingTextDown, action, bindingIndexDown);
//         UpdateBindingText(bindingTextLeft, action, bindingIndexLeft);
//         UpdateBindingText(bindingTextRight, action, bindingIndexRight);
//     }

//     private void UpdateBindingText(TMP_Text bindingText, InputAction action, int bindingIndex)
//     {
//         if (bindingIndex >= 0)
//         {
//             bindingText.text = action.GetBindingDisplayString(bindingIndex);
//         }
//     }

//     private int FindBindingIndex(InputAction action, string bindingPath)
//     {
//         for (int i = 0; i < action.bindings.Count; i++)
//         {
//             if (action.bindings[i].path == bindingPath)
//             {
//                 return i;
//             }
//         }

//         return -1;
//     }
// }
