using UnityEngine;
using UnityEngine.Splines;
using UnityEngine.UI;
using System.Collections;
using System;
using DG.Tweening;

public class VehicleController : MonoBehaviour
{
    public enum VehicleType
    {
        Car,
        Aeroplane,
        Boat
    }

    public VehicleType vehicleType;
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
<<<<<<< Updated upstream

    AudioSource carDriveSound;
=======
    public SymbolManager.SymbolsEnum SymbolsType;
    GameManager.PowerType powerType;
    public float pickupDistance = 1f;
    private Vector3 playerpos;
    private bool isPickedUp = false;
    private bool isHelicopterON = false;
    private int randomXPosition = 3;
    GameObject carDriveSound;
    private Rigidbody rb;

    [Header("VEHICLE FX")] public ParticleSystem boatStartWaterRipple;
    public GameObject boatMovingFx;
>>>>>>> Stashed changes
    private void Start()
    {
        anim = GetComponent<SplineAnimate>();
       // GetComponent<VehicleObstacleDetector>().enabled = true;
    }
    private void OnMouseDown()
    {
<<<<<<< Updated upstream
=======
        //GameManager.instance.ResetHint();
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
>>>>>>> Stashed changes
        if (UIManager.Instance.SettingsPanel.activeSelf)
        {
            UIManager.Instance.PanelFadeOut(UIManager.Instance.SettingsPanel, 1);
        }
        UIManager.Instance.touchTutorial?.SetActive(false);
        carDriveSound = SoundManager.instance.PlaySound(sound: SoundManager.Sounds.CarDrive);
        DriveVehicle();
       if(gameObject.name.Contains("Boat")) MoveFx();
        CheckForCollitions();
    }
    void CheckForCollitions()
    {
        if (anim.Container.gameObject.GetComponent<ObstacleDetector>().CheckIfPathIsClear())
        {
            directionMarkImage.enabled = false;
            GetComponent<VehicleController>().enabled = false;
            GameManager.instance.isCarMoving = false;
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

    void MoveFx()
    {
        boatStartWaterRipple.Play();
        boatMovingFx.SetActive(true);
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
            if(controller && controller.anim.ElapsedTime <= 0)
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
<<<<<<< Updated upstream
        carDriveSound.Stop();
        DisableCollider();
        Debug.Log("Past the Boundary score++");
        WinController.Instance.FinishedCars++;
        SplineContainer splineContainer = anim.Container; 
        splineContainer.gameObject.SetActive(false);
        GameManager.instance.isCarMoving = false;
        anim.Container = null;
        gameObject.SetActive(false);
        
        //if the vehicle non-car
        DisappearEffect();
    }

    private void DisappearEffect()
    {
        
=======
        if(other.name.Contains("WinCubes")){
            Destroy(carDriveSound, 1f);
            DisableCollider();
            Debug.Log("Past the Boundary score++");
            WinController.Instance.FinishedCars++;
            SplineContainer maincontainer = anim.Container;
            maincontainer.gameObject.SetActive(false);
            GameManager.instance.isCarMoving = false;
            gameObject.SetActive(false);
        }
>>>>>>> Stashed changes
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name.Contains("Person"))
        {
            anim.Pause();
            GameManager.instance.CallTakeDamage();
            StartCoroutine(ReverseAnimation());
        }
    }

    private IEnumerator ReverseAnimation()
    {
        float duration = anim.ElapsedTime;
        float elapsedTime = 0.001f;

        while (elapsedTime < duration)
        {
<<<<<<< Updated upstream
            anim.ElapsedTime -= Time.deltaTime;
            elapsedTime += Time.deltaTime;
            yield return null; // Wait for the next frame
=======
            elapsedTime += Time.deltaTime * 0.5f;
            anim.ElapsedTime -= Time.deltaTime * 0.5f;
            yield return null;
        }
        rb.isKinematic = true;
<<<<<<< Updated upstream
        GameManager.instance.isCarMoving = false;
        Debug.Log("Vehicle stoped moving");
=======
        if(gameObject.name.Contains("Boat")) boatMovingFx.SetActive(false);
>>>>>>> Stashed changes
    }
    [SerializeField]
    SplineContainer Currentspline;
    public void SwitchSpline()
    {
        // Toggle between splineRoute and changeSplineRoute
        if (anim.Container == Currentspline)
        {
            anim.Container.gameObject.GetComponent<VehicleSpawner>().SwitchSpline();
        }
        else
        {
            anim.Container = Currentspline;
            directionMarkImage.sprite = SymbolManager.GetSymbol(SymbolsType);
            GameManager.instance.currentPower = GameManager.PowerType.none;
            GameManager.instance.isCarMoving = false;
        }
    }
    void Update()
    {
        if (isHelicopterON && !isPickedUp)
        {
            // Check for mouse button down to initiate helicopter movement
            MoveHelicopterToPlayer();
        }
        else if(isHelicopterON && isPickedUp)
        {
            MoveHelicopterToSpecificPosition(new Vector3(randomXPosition, 25f, 1f));
        }
    }
    void MoveHelicopterToPlayer()
    {
        Vector3 randomSkyPosition = GenerateRandomSkyPosition();
        Vector3 playerPosition = this.transform.position;
        float step = 10f * Time.deltaTime;
        GameManager.instance.helicopterObject.transform.position = Vector3.MoveTowards(GameManager.instance.helicopterObject.transform.position, playerpos, step);
        float distanceToPlayer = Vector3.Distance(GameManager.instance.helicopterObject.transform.position, playerpos);
        Debug.Log(distanceToPlayer +"Player picked up!");
        if (distanceToPlayer < pickupDistance)
        {
            isPickedUp = true;
            this.transform.tag = "Player";
            this.transform.SetParent(GameManager.instance.helicopterObject.transform);
            Debug.Log("Player picked up!");
        }
    }

    public void MoveHelicopter()
    {
        GameManager.instance.isCarMoving = true;
        GameManager.instance.helicopterObject.transform.DOMove(playerpos, 3f, false)
            .OnComplete(MovementComplete);
       
    }
    void MovementComplete()
    {
        this.transform.tag = "Player";
        this.transform.SetParent(GameManager.instance.helicopterObject.transform);
        GameManager.instance.helicopterObject.transform.DOMove(GenerateRandomSkyPosition(), 3f, false).OnComplete(HeliCopterReachedBack);
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
    }
    Vector3 GenerateRandomSkyPosition()
    {
        // Customize this method based on your sky position requirements
        float randomX = UnityEngine.Random.Range(-10f, 10f);
        float randomY = UnityEngine.Random.Range(20f, 30f);
        float randomZ = UnityEngine.Random.Range(-10f, 10f);

        return new Vector3(randomX, randomY, randomZ);
    }
    void MoveHelicopterToSpecificPosition(Vector3 targetPosition)
    {
        float step = 10f * Time.deltaTime;
        GameManager.instance.helicopterObject.transform.position = Vector3.MoveTowards(GameManager.instance.helicopterObject.transform.position, targetPosition, step);

        // If the helicopter has reached the target position, you might want to perform additional actions
        if (GameManager.instance.helicopterObject.transform.position == targetPosition)
        {
            foreach (Transform child in GameManager.instance.helicopterObject.transform)
            {
                if(child.tag == "Player")
                // Deactivate the child object
                child.gameObject.SetActive(false);
            }
            // Additional actions, if any
            GameManager.instance.currentPower = GameManager.PowerType.none;
            isHelicopterON = false;
            WinController.Instance.FinishedCars++;
>>>>>>> Stashed changes
        }
    }
}