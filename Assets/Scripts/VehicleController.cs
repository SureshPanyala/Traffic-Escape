using UnityEngine;
using UnityEngine.Splines;
using UnityEngine.UI;
using System.Collections;
using System;
using DG.Tweening;

public class VehicleController : MonoBehaviour
{
    public static event Action carCollided;
    public GameObject directionMarkImage;
    SplineAnimate anim;
    [SerializeField] private float duration = 0.5f;
    [SerializeField] private Vector3 strength;
    [SerializeField] private int vibrato;
    [SerializeField] private float randomness;
    [SerializeField] private bool snapping;
    [SerializeField] private bool fadeOut;
    [SerializeField] private object randomnessMode;
    [SerializeField] SymbolManager.SymbolsEnum SymbolsType;
    GameManager.PowerType powerType;
    public float pickupDistance = 1f;
    private Vector3 playerpos;
    private bool isPickedUp = false;
    private bool isHelicopterON = false;
    private int randomXPosition = 3;
    GameObject carDriveSound;
    
    private void Start()
    {
        anim = GetComponent<SplineAnimate>();
        Currentspline = anim.Container;
       // GetComponent<VehicleObstacleDetector>().enabled = true;
    }
    private void OnMouseDown()
    {
        GameManager.instance.ResetHint();
        GameManager.instance.CheckEligibleVehiclesForSignback();
        if (GameManager.instance.isCarMoving == true)
        {
            Debug.Log("CarMoving");
            return;
        }
        randomXPosition = UnityEngine.Random.Range(-4, 4);
        playerpos = this.transform.position;
        Debug.Log(playerpos + "Player picked up!");

        switch (GameManager.instance.currentPower)
        {
            case GameManager.PowerType.helicopter:
                isHelicopterON= true;
                MoveHelicopter();
                // Do something for helicopter power type
                break;
            case GameManager.PowerType.switchdirection:
                SwitchSpline();
                // Do something for switchdirection power type
                break;
            case GameManager.PowerType.none:
                MoveVechile();
                // Do something for none power type
                break;
        }
    }
    private void MoveVechile()
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
            directionMarkImage.SetActive(false);
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
            directionMarkImage.SetActive(true);
            HitEffect hitEffect = Instantiate(EffectsManager.instance.hitEffect);
            hitEffect.transform.position = collision.contacts[0].point;
            var controller = collision.gameObject.GetComponent<VehicleController>();
            StartCoroutine(controller.DoShake());
            anim.Pause();
            GameManager.instance.CallTakeDamage();
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
        Destroy(carDriveSound,1f);
        DisableCollider();
        Debug.Log("Past the Boundary score++");
        WinController.Instance.FinishedCars++;
        SplineContainer maincontainer = anim.Container;
        maincontainer.gameObject.SetActive(false);
        GameManager.instance.isCarMoving = false;
        gameObject.SetActive(false);

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
    [SerializeField]
    SplineContainer Currentspline;
    public void SwitchSpline()
    {
        GameManager.instance.SwitchCountNumber();
        // Toggle between splineRoute and changeSplineRoute
        if (anim.Container == Currentspline)
        {
            anim.Container.gameObject.GetComponent<VehicleSpawner>().SwitchSpline();

        }
        else
        {
            anim.Container = Currentspline;
            directionMarkImage.GetComponent<SpriteRenderer>().sprite = SymbolManager.GetSymbol(SymbolsType);
            GameManager.instance.currentPower = GameManager.PowerType.none;
        }
    }

    public void MoveHelicopter()
    {
        GameManager.instance.isCarMoving = true;
        GameManager.instance.HelicopterCountNumber(); ;
        GameManager.instance.helicopterObject.transform.DOMove(playerpos, 3f, false)
            .OnComplete(MovementComplete);
       
    }
    void MovementComplete()
    {
        this.transform.tag = "Player";
        this.transform.SetParent(GameManager.instance.helicopterObject.transform);
        GameManager.instance.helicopterObject.transform.DOMove(GenerateRandomSkyPosition(), 3f, false).OnComplete(HeliCopterReachedBack);
        GameManager.instance.helicopterObject.transform.DORotate(new Vector3(0, 0, 30f), 1f);
        // Code to be executed after the movement is complete
        Debug.Log("Helicopter movement complete!");
        // Add your additional code here...
    }

    void HeliCopterReachedBack()
    {
        foreach (Transform child in GameManager.instance.helicopterObject.transform)
        {
            if (child.tag == "Player")
                // Deactivate the child object
                child.gameObject.SetActive(false);
        }
        // Additional actions, if any
        GameManager.instance.currentPower = GameManager.PowerType.none;
        //isHelicopterON = false;
        WinController.Instance.FinishedCars++;
        GameManager.instance.isCarMoving = false;
        GameManager.instance.isHelicopterModeOn = false;
        SplineContainer splinecontainer = anim.Container;
        splinecontainer.gameObject.SetActive(false);
    }
    Vector3 GenerateRandomSkyPosition()
    {
        // Customize this method based on your sky position requirements
        float randomX = UnityEngine.Random.Range(-10f, 10f);
        float randomY = UnityEngine.Random.Range(20f, 30f);
        float randomZ = UnityEngine.Random.Range(-10f, 10f);

        return new Vector3(randomX, randomY, randomZ);
    }

}