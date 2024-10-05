using UnityEngine;

public class LookAtToObject : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private bool toCamera = true;

    private Transform _cameraTransform;

    private void OnEnable()
    {
        _cameraTransform = Camera.main.transform;
    }

    private void Update()
    {
        transform.LookAt(toCamera ? _cameraTransform : target);
    }
}