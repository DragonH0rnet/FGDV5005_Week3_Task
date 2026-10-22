using UnityEngine;

public class RotatePlatform : MonoBehaviour
{

    enum RotationAxis
    {
        X,
        Y,
        Z
    }

    [SerializeField] private RotationAxis rotationAxis = RotationAxis.Y;
    private Vector3 rotationVector;
    [SerializeField] private float rotationSpeed = 50f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (rotationAxis == RotationAxis.X)
        {
            rotationVector = Vector3.right;
        }
        else if (rotationAxis == RotationAxis.Y)
        {
            rotationVector = Vector3.up;
        }
        else if (rotationAxis == RotationAxis.Z)
        {
            rotationVector = Vector3.forward;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(rotationVector * Time.deltaTime * rotationSpeed);
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
