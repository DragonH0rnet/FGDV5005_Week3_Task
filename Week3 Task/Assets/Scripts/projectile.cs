using Unity.VisualScripting;
using UnityEngine;

public class projectile : MonoBehaviour
{

    [SerializeField] private float pushForce;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Trigger");
            
            Vector3 pushDirection = this.transform.forward * -1;
            pushDirection.y = 0; // Keep it on a flat plane so the player doesn't fly into the sky or floor
            pushDirection.Normalize();
            
            other.gameObject.GetComponent<PlayerScript>().hitPlayer(pushDirection * pushForce);
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        Destroy(this.gameObject);
    }
}
