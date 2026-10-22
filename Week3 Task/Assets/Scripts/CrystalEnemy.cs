using UnityEngine;

public class CrystalEnemy : MonoBehaviour
{

    [SerializeField] private float secondaryRotSpeed = 10f;
    [SerializeField] private float secondaryRotSpeedAggro = 45f;
    [SerializeField] private GameObject secondaryObj;

    [SerializeField] private GameObject player;

    [SerializeField] private GameObject projectile;

    [SerializeField] private Transform firePoint;

    [SerializeField] private float fireRate = 1f;
    [SerializeField] private float fireRange = 30f;
    [SerializeField] private float fireSpeed = 20f;
    private float currentFireCool = 0f;

    private bool aggroed = false;

    private float resetAggroTime = 3f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerTarget = player.transform.position + new Vector3(0f, 1.2f, 0f);
        float distanceToPlayer = Vector3.Distance(transform.position, playerTarget);
        if (distanceToPlayer <= fireRange)
        {
            RaycastHit hit;
            int layerToIgnore = LayerMask.NameToLayer("Platform");
            int layerMask = ~(1 << layerToIgnore);
            if (Physics.Raycast(transform.position, (playerTarget - transform.position).normalized, out hit, fireRange, layerMask))
            {
                Debug.DrawLine(transform.position, hit.point, Color.red);
                if (hit.collider.gameObject == player)
                {
                    this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation, Quaternion.LookRotation(playerTarget - this.transform.position), Time.deltaTime * 100f);
                    if (currentFireCool <= 0f && aggroed)
                    {
                        FireProjectile();
                        currentFireCool = fireRate;
                    }
                    aggroed = true;
                }
                else
                {
                    aggroed = false;
                }
            }     
        }
        else
        {
            aggroed = false;
        }

        if (!aggroed)
        {
            if (resetAggroTime > 0f)
            {
                secondaryObj.transform.Rotate(Vector3.forward * secondaryRotSpeedAggro * Time.deltaTime);
                resetAggroTime -= Time.deltaTime;
            }
            else
            {
                secondaryObj.transform.Rotate(Vector3.forward * secondaryRotSpeed * Time.deltaTime);
                this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation, Quaternion.Euler(-89.9f, 0f, 0f), Time.deltaTime * 30f);
            }
        }
        else
        {
            secondaryObj.transform.Rotate(Vector3.forward * secondaryRotSpeedAggro * Time.deltaTime);
            resetAggroTime = 3f;
        }

        if (currentFireCool > 0f)
        {
            currentFireCool -= Time.deltaTime;
        }
    }

    private void FireProjectile()
    {
        // Instantiate the projectile at the fire point's position and rotation
        GameObject newProjectile = Instantiate(projectile, firePoint.position, firePoint.transform.rotation);
        // Add force to the projectile to make it move forward
        Rigidbody rb = newProjectile.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddForce(this.transform.forward * fireSpeed, ForceMode.Impulse);
        }
    }
}
