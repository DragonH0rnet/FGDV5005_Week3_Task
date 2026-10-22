using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{

    [SerializeField] private bool isOpen = false;
    [SerializeField] private bool unlockable = false;
    [SerializeField] private bool locked = false;
    [SerializeField] private bool resets = false;
    [SerializeField] private string keyName = "Key";
    [SerializeField] private float openSpeed = 2.0f;
    [SerializeField] private GameObject lockDisplay;

    [SerializeField] private float closedAngle = 0f;
    [SerializeField] private float openAngle = 90f;
    [SerializeField] private float positiveRotation = 1f;

    private float rotationAngle = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isOpen && rotationAngle < openAngle)
        {
            //Debug.Log($"Closing door, {this.transform.localEulerAngles.z} + target {closedAngle}");
            this.transform.Rotate(Vector3.forward * (positiveRotation * openSpeed) * Time.deltaTime);
            rotationAngle += (positiveRotation * openSpeed) * Time.deltaTime;
        }
        else if(!isOpen && rotationAngle > closedAngle)
        {
            //Debug.Log($"Closing door, {this.transform.localEulerAngles.z} + target {closedAngle}");
            this.transform.Rotate(Vector3.forward * -(positiveRotation * openSpeed) * Time.deltaTime);
            rotationAngle -= (positiveRotation * openSpeed) * Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (unlockable && locked)
            {
                PlayerScript playerScript = other.GetComponent<PlayerScript>();
                Dictionary<string, int> inventory = playerScript.inventory;
                if (inventory.ContainsKey(keyName))
                {
                    locked = false;
                    isOpen = true;

                    if (inventory[keyName] > 1)
                    {
                        inventory[keyName]--;
                    }
                    else
                    {
                        inventory.Remove(keyName);
                    }
                    playerScript.UpdateInventory();
                    Debug.Log("Door unlocked and opened.");
                }
                else
                {
                    lockDisplay.SetActive(true);
                    Debug.Log("Door is locked. You need the " + keyName + " to unlock it.");
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        lockDisplay.SetActive(false);
    }
}
