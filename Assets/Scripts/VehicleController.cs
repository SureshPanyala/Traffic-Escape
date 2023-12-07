using UnityEngine;
using UnityEngine.Splines;
using UnityEngine.UI;
using System.Collections;
using System;
using DG.Tweening;

public class VehicleController : MonoBehaviour
{
    public static event Action carCollided;
    public Image directionMarkImage;
    SplineAnimate anim;
    [SerializeField] private float duration = 0.5f;
    [SerializeField] private Vector3 strength;
    [SerializeField] private int vibrato;
    [SerializeField] private float randomness;
    [SerializeField] private bool snapping;
    [SerializeField] private bool fadeOut;
    [SerializeField] private object randomnessMode;

    AudioSource carDriveSound;
    private void Start()
    {
        anim = GetComponent<SplineAnimate>();
       // GetComponent<VehicleObstacleDetector>().enabled = true;
    }
    private void OnMouseDown()
    {
        if (UIManager.Instance.SettingsPanel.activeSelf)
        {
            UIManager.Instance.PanelFadeOut(UIManager.Instance.SettingsPanel, 1);
        }
        UIManager.Instance.touchTutorial?.SetActive(false);
        carDriveSound = SoundManager.instance.PlaySound(sound: SoundManager.Sounds.CarDrive);
        DriveVehicle();
        CheckForCollitions();
    }
    void CheckForCollitions()
    {
        if (anim.Container.gameObject.GetComponent<ObstacleDetector>().CheckIfPathIsClear())
        {
            directionMarkImage.enabled = false;
            GetComponent<VehicleController>().enabled = false;
        }
    }
    void DisableCollider()
    {
        GetComponent<BoxCollider>().enabled = false;
    }
    private void DriveVehicle()
    {
        GetComponent<Rigidbody>().isKinematic = false;
        anim.Restart(true);
    }
    private void OnCollisionEnter(Collision collision)
    {
        var otherCar = collision.gameObject.GetComponent<SplineAnimate>();
        if (otherCar)
        {
            carCollided?.Invoke();
            Destroy(carDriveSound);
            directionMarkImage.enabled = true;
            HitEffect hitEffect = Instantiate(EffectsManager.instance.hitEffect);
            hitEffect.transform.position = collision.contacts[0].point;
            var controller = collision.gameObject.GetComponent<VehicleController>();
            StartCoroutine(controller.DoShake());
            anim.Pause();
            StartCoroutine(ReverseAnimation());
        }
    }
    public IEnumerator DoShake()
    {
        anim.enabled = false;
        transform.DOShakeRotation(duration, strength, vibrato,
            randomness, fadeOut, ShakeRandomnessMode.Harmonic);
        yield return new WaitForSeconds(duration);
        anim.enabled = true;
    }

    private void OnTriggerExit(Collider other)
    {
        carDriveSound.Stop();
        DisableCollider();
        Debug.Log("Past the Boundary score++");
        WinController.Instance.FinishedCars++;
    }
    private IEnumerator ReverseAnimation()
    {
        float duration = anim.ElapsedTime;
        float elapsedTime = 0.001f;

        while (elapsedTime < duration)
        {
            anim.ElapsedTime -= Time.deltaTime;
            elapsedTime += Time.deltaTime;
            yield return null; // Wait for the next frame
        }
    }
}