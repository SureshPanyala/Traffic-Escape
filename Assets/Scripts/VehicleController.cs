using UnityEngine;
using UnityEngine.Splines;
using UnityEngine.UI;
using System.Collections;
using System;
using DG.Tweening;
using System.Linq;
using System.Collections.Generic;

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
    }
    private void DriveVehicle()
    {
        anim.Restart(true);
    }
    private void OnCollisionEnter(Collision collision)
    {
        carCollided?.Invoke();
        Destroy(carDriveSound);
        HitEffect hitEffect = Instantiate(EffectsManager.instance.hitEffect);
        hitEffect.transform.position = collision.contacts[0].point;
        var controller = collision.gameObject.GetComponent<VehicleController>();
        StartCoroutine(controller.DoShake());
        anim.Pause();
        StartCoroutine(ReverseAnimation());
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
        Debug.Log("Past the Boundary score++");
        WinController.Instance.FinishedCars++;
    }
    private IEnumerator ReverseAnimation()
    {
        float duration = anim.ElapsedTime;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {

            anim.ElapsedTime -= Time.deltaTime;
            elapsedTime += Time.deltaTime;
            yield return null; // Wait for the next frame
        }
    }
}