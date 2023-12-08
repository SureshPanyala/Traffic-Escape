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
    Quaternion rot;
    Rigidbody rb;
   
    private void Start()
    {
        anim = GetComponent<SplineAnimate>();
        rb = GetComponent<Rigidbody>();
        Currentspline = anim.Container;
        rot = transform.localRotation;
        // GetComponent<VehicleObstacleDetector>().enabled = true;
    }
    private void OnMouseDown()
    {
        //if (GameManager.instance.isCarMoving)
          //  return; 

        //GameManager.instance.isCarMoving = true;
 
        randomXPosition = UnityEngine.Random.Range(-4, 4);
        playerpos = this.transform.position;
        Debug.Log(playerpos + "Player picked up!");

        switch (GameManager.instance.currentPower)
        {
            case GameManager.PowerType.helicopter:
                //isHelicopterON= true;
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
        rb.isKinematic = false;
        anim.Restart(true);
    }
    private void OnCollisionEnter(Collision collision)
    {
        var otherCar = collision.gameObject.GetComponent<SplineAnimate>();
        if (otherCar)
        {
            carCollided?.Invoke();
            Destroy(carDriveSound,1f);
            directionMarkImage.SetActive(true);
            HitEffect hitEffect = Instantiate(EffectsManager.instance.hitEffect);
            hitEffect.transform.position = collision.contacts[0].point;
            var controller = collision.gameObject.GetComponent<VehicleController>();
            controller.DoShake();
            anim.Pause();
            StartCoroutine(ReverseAnimation());
        }
    }
    public void DoShake()
    {
        ToggleAnim(false);
        transform.DOShakeRotation(duration, strength, vibrato,
            randomness, fadeOut, ShakeRandomnessMode.Harmonic).OnComplete(()=>ToggleAnim(true));
    }
    void ToggleAnim(bool val)
    {
        anim.enabled = val;
    }

    private void OnTriggerExit(Collider other)
    {
        carDriveSound.GetComponent<AudioSource>().Stop();
       // GameManager.instance.isCarMoving = false;
        DisableCollider();
        gameObject.SetActive(false);
        Debug.Log("Past the Boundary score++");
        WinController.Instance.FinishedCars++;
    }
    
    private IEnumerator ReverseAnimation()
    {
        float duration = anim.ElapsedTime;
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime * 0.5f;
            anim.ElapsedTime -= Time.deltaTime * 0.5f;
            yield return null;
        }
        rb.isKinematic = true;
        //GameManager.instance.isCarMoving = false;
        Debug.Log("Vehicle stoped moving");
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
            directionMarkImage.GetComponent<SpriteRenderer>().sprite = SymbolManager.GetSymbol(SymbolsType);
            GameManager.instance.currentPower = GameManager.PowerType.none;
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
        }
    }
}