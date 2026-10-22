using UnityEngine;

public class MovePlatform : MonoBehaviour
{

    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float xDistance = 0f;
    [SerializeField] private float yDistance = 0f;
    [SerializeField] private float zDistance = 0f;

    private Vector3 startPosition;
    private Vector3 endPosition;

    private bool movingToEnd = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPosition = transform.position;
        endPosition = startPosition + new Vector3(xDistance, yDistance, zDistance);
    }

    // Update is called once per frame
    void Update()
    {
        if (movingToEnd)
        {
            transform.position = Vector3.MoveTowards(transform.position, endPosition, moveSpeed * Time.deltaTime);
            if ( Vector3.Distance(transform.position, endPosition) < 0.1f)
            {
                movingToEnd = false;
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, startPosition, moveSpeed * Time.deltaTime);
            if (Vector3.Distance(transform.position, startPosition) < 0.1f)
            {
                movingToEnd = true;
            }
        }

        Physics.SyncTransforms();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.SetParent(transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.SetParent(null);
        }
    }

}
