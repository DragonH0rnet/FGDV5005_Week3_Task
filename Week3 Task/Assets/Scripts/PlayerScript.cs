using System.Collections.Generic;
using StarterAssets;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{

    public Dictionary<string, int> inventory = new Dictionary<string, int>();

    public int respawnAreaNum = 0;

    [SerializeField] private GameObject inventoryUIText;
    [SerializeField] private GameObject inventoryUIDisplay;

    [SerializeField] private float pushDrag = 5f;

    private CharacterController controller;
    private ThirdPersonController controllerScript;
    private Vector3 impactVelocity = Vector3.zero;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        controller = GetComponent<CharacterController>();
        controllerScript = this.GetComponent<ThirdPersonController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (impactVelocity.magnitude > 0.2f)
        {
            controllerScript.canMove = false;  
            controller.Move(impactVelocity * Time.deltaTime);
        }
        else
        {
            controllerScript.canMove = true;
        }

        if (impactVelocity.magnitude > 0f)
        {
            impactVelocity = Vector3.Lerp(impactVelocity, Vector3.zero, pushDrag * Time.deltaTime);
        }
    }

    public void UpdateInventory()
    {
        if (inventory.Count == 0)
        {
            inventoryUIText.SetActive(false);
        }
        else
        {
            inventoryUIText.SetActive(true);
            string invText = "";
            foreach (var item in inventory)
            {
                invText += $"{item.Key}: x{item.Value} \n ";
            }
            inventoryUIDisplay.GetComponent<TextMeshProUGUI>().text = invText;
        }
    }

    public void hitPlayer(Vector3 force)
    {
        impactVelocity = force;
    }
}
