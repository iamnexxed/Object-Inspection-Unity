
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public Animator doorAnimator;
    
    bool doorOpened;

    void Start() 
    {
        doorOpened = false;
    }

    void OnTriggerStay(Collider other) 
    {
        if(other.CompareTag("Player") && FieldOfView.instance.visibleTargets.Contains(transform))
        {
            // Debug.Log("FOV has detected : " + FieldOfView.instance.visibleTargets);
            if(!doorOpened)
            {
                // Debug.Log("Press E to unlock the door!");
                // Show UI to press a button on screen
                ActionKeyE.instance.showActionKey = true;
                if (Input.GetButtonDown("Interact"))
                {
                    // Open the door
                    doorAnimator.SetTrigger("open");
                    doorOpened = true;
                }
            }
            else
            {
                ActionKeyE.instance.showActionKey = false;
            }
            
        }
        else
        {
            ActionKeyE.instance.showActionKey = false;
        }
    }

    void OnTriggerExit(Collider other) 
    {
        if (other.CompareTag("Player"))
        {
            ActionKeyE.instance.showActionKey = false;
        }

    }
}
