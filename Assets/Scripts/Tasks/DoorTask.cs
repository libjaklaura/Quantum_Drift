using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DoorTask : Task
{
    public TMP_InputField inputField;
    public string correctCode;
    public Animator doorAnimator;

    private bool isUnlocked = false;
    //private bool playerInside = false;


    void Start()
    {
        doorAnimator = GetComponent<Animator>();
        if (inputField != null)
            inputField.onValueChanged.AddListener(CheckCode);
        else
            isUnlocked = true;
        
    }

    void OnEnable()
    {
        if(isUnlocked)
        {
            doorAnimator.SetTrigger("Open");
        }
    }

    private void CheckCode(string input)
    {
        if (input == correctCode)
        {
            isUnlocked = true;
            Complete();
            OpenDoor();
        }
    }

    void OpenDoor()
    {
        if (doorAnimator != null)
        {
            doorAnimator.SetTrigger("Open");
            GetComponentInChildren<AudioSource>().Play();
        }
    }

    /*
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = true;
            if (isUnlocked)
            {
                OpenDoor();
            }
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
       if (other.CompareTag("Player"))
        {
            playerInside = false;
            if (isUnlocked)
            {
                doorAnimator.SetTrigger("Closed");
            }
        }
    }*/
}
