using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public interface IInteractable
{
    string GetInteractPrompt();
    void OnInteract();
}
public interface IOpenDoor
{
    bool IsOpen { get; set; }

    void OpenThisDoor();
    void CloseThisDoor();
}

public class InteractionManager : MonoBehaviour
{
    public float checkRate = 0.05f;
    private float lastCheckTime;
    public float maxCheckDistance;
    public LayerMask layerMask;

    private GameObject curInteractGameobject;
    //private IInteractable curInteractable;
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

            Debug.DrawRay(_camera.transform.position, _camera.transform.forward*maxCheckDistance, Color.green);

            if (Physics.Raycast(ray, out hit, maxCheckDistance, layerMask))
            {
                if (hit.collider.gameObject != curInteractGameobject)
                {
                    curInteractGameobject = hit.collider.gameObject;
                    //if (curInteractGameobject.TryGetComponent<IInteractable>(out curInteractable))
                    //{
                    //    SetPromptText();
                    //}
                    if (curInteractGameobject.TryGetComponent<IOpenDoor>(out currentDoor))
                    {
                        SetDoorOpenTxt();
                    }
                    else if(curInteractGameobject.tag == "BasementDoor")
                    {
                        SetBasementDoorTxt();
                    }

                }
            }
            else
            {
                curInteractGameobject = null;
                //curInteractable = null;
                currentDoor = null;
                interactText.gameObject.SetActive(false);
            }
        }
    }

    private void SetBasementDoorTxt()
    {
        interactText.gameObject.SetActive(true);
        interactText.text = "<b>[5]</b> 지하 연구실로 이동하기";
    }

    //private void SetPromptText()
    //{
    //    interactText.gameObject.SetActive(true);
    //    interactText.text = string.Format("<b>[E]</b> {0}", curInteractable.GetInteractPrompt());
    //}
    private void SetDoorOpenTxt()
    {
        interactText.gameObject.SetActive(true);
        if (!currentDoor.IsOpen)
        {
            interactText.text = "<b>[E]</b> 문 열기";
        }
        else
        {
            interactText.text = "<b>[E]</b> 문 닫기";
        }
    }

    public void OnBasementDoorInput(InputAction.CallbackContext callbackContext)
    {
        if (curInteractGameobject.CompareTag("BasementDoor") && callbackContext.phase == InputActionPhase.Started)
        {
            curInteractGameobject = null;
            interactText.gameObject.SetActive(false);
            SceneManager.LoadScene("BasementLab"); //연구실로 이동
        }
    }
    //public void OnInteractInput(InputAction.CallbackContext callbackContext)    
    //{
    //    if (callbackContext.phase == InputActionPhase.Started && curInteractable != null)
    //    {
    //        curInteractable.OnInteract();
    //        curInteractGameobject = null;
    //        curInteractable = null;
    //        interactText.gameObject.SetActive(false);
    //    }
    //}
    public void OpenDoorInput(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.phase == InputActionPhase.Started && currentDoor != null)
        {
            if (!currentDoor.IsOpen)
            {
                currentDoor.OpenThisDoor();
            }
            else
            {
                currentDoor.CloseThisDoor();
            }
            curInteractGameobject = null;
            currentDoor = null;
            interactText.gameObject.SetActive(false);
        }
    }
}