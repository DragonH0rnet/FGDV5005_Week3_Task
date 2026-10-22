using UnityEngine;

public class Pickup : MonoBehaviour
{

    [SerializeField] private string itemName;
    [SerializeField] private int spinSpeed;
    [SerializeField] private bool rotateX;
    [SerializeField] private bool rotateY;
    [SerializeField] private bool rotateZ;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (rotateX)
        {
            transform.Rotate(Vector3.right * spinSpeed * Time.deltaTime);
        }
        if (rotateY)
        {
            transform.Rotate(Vector3.up * spinSpeed * Time.deltaTime);
        }
        if (rotateZ)
        {
            transform.Rotate(Vector3.forward * spinSpeed * Time.deltaTime);
        }
    }

  private void OnTriggerEnter(Collider other)
  {
        if (other.CompareTag("Player"))
        {
            PlayerScript playerScript = other.GetComponent<PlayerScript>();
            if (playerScript.inventory.ContainsKey(itemName))
            {
                playerScript.inventory[itemName]++;
            }

            else
            {
                playerScript.inventory.Add(itemName, 1);
            }
            playerScript.UpdateInventory();
            Debug.Log($"Picked up {itemName}");
            Destroy(gameObject);
        }
  }
}
