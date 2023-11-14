using UnityEngine;
using DG.Tweening;


public class CameraShaker : MonoBehaviour
{
    [SerializeField] private Transform _Camera;
    [SerializeField] private float positionStrength;
    [SerializeField] private float rotationStrength;

    private void OnEnable()
    {
        VehicleController.carCollided += CameraShake;
    }

    private void OnDisable()
    {
        VehicleController.carCollided -= CameraShake;
    }
    private void CameraShake()
    {
        _Camera.DOComplete();
        _Camera.DOShakePosition(0.3f,positionStrength, 10, 90);
        _Camera.DOShakeRotation(0.3f,rotationStrength, 10, 90);
    }
}
