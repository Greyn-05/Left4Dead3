using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface OpenDoor
{
    void OpenThisDoor();
}
public class DoorInfo : MonoBehaviour , OpenDoor
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
