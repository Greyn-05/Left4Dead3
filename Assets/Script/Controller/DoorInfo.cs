using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInfo : MonoBehaviour, IOpenDoor
{
    private Animator door_Animator;
    private void Awake()
    {
        door_Animator = GetComponent<Animator>();
    }
    public void OpenThisDoor()
    {
        door_Animator.SetTrigger("Open");
    }
}
