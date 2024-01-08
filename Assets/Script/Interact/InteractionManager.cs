using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public interface IInteractable
{
    string GetInteractPrompt();
    void OnInteract();
}
public interface IOpenDoor
{
    void OpenThisDoor();
}

public class InteractionManager : MonoBehaviour
{
    public float checkRate = 0.05f;
    private float lastCheckTime;
    public float maxCheckDistance;
    public LayerMask layerMask;

    private GameObject curInteractGameobject;
    private IInteractable curInteractable;
    private IOpenDoor currentDoor;

    public Text interactText;
    private Camera _camera;

    public void Initialize()
    {
        _camera = PlayerManager.Instance.m_cameraManager.GetCamera1();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Time.time - lastCheckTime > checkRate)
        {
            lastCheckTime = Time.time;

            Ray ray = _camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, maxCheckDistance, layerMask))
            {
                if (hit.collider.gameObject != curInteractGameobject)
                {
                    curInteractGameobject = hit.collider.gameObject;
                    if (curInteractGameobject.TryGetComponent<IInteractable>(out curInteractable))
                    {
                        SetPromptText();
                    }
                    else if (curInteractGameobject.TryGetComponent<IOpenDoor>(out currentDoor))
                    {
                        SetDoorOpenTxt();
                    }
                    
                }
            }
            else
            {
                curInteractGameobject = null;
                curInteractable = null;
                currentDoor = null;
                interactText.gameObject.SetActive(false);
            }
        }
    }

    private void SetPromptText()
    {
        interactText.gameObject.SetActive(true);
        interactText.text = string.Format("<b>[E]</b> {0}", curInteractable.GetInteractPrompt());
    }
    private void SetDoorOpenTxt()
    {
        interactText.gameObject.SetActive(true);
        interactText.text = "<b>[E]</b> 문 열기";
    }

    public void OnInteractInput(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.phase == InputActionPhase.Started && curInteractable != null)
        {
            curInteractable.OnInteract();
            curInteractGameobject = null;
            curInteractable = null;
            interactText.gameObject.SetActive(false);
        }
    }
    public void OpenDoorInput(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.phase == InputActionPhase.Started && currentDoor != null)
        {
            currentDoor.OpenThisDoor();
            curInteractGameobject = null;
            currentDoor = null;
            interactText.gameObject.SetActive(false);
        }
    }
}