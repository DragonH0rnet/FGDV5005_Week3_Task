using UnityEngine;

public class DeathZone : MonoBehaviour
{

    [SerializeField] private GameObject respawnPoint;

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
        Debug.Log("Player has entered the death zone. Respawning...");
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player has entered the death zone. Respawning...");

            CharacterController cc = other.GetComponent<CharacterController>();
            Animator anim = other.GetComponent<Animator>();

            cc.enabled = false;
            anim.enabled = false;

            other.gameObject.transform.position = respawnPoint.transform.position;

            cc.enabled = true;
            anim.enabled = true;
        }
    }
}
