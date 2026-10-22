using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{

    public Dictionary<string, int> inventory = new Dictionary<string, int>();

    public int respawnAreaNum = 0;

    [SerializeField] private GameObject inventoryUIText;
    [SerializeField] private GameObject inventoryUIDisplay;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
}
