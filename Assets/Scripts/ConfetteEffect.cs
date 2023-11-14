using UnityEngine;

public class ConfetteEffect : MonoBehaviour
{

    public static ConfetteEffect Instance;
    [SerializeField] ParticleSystem[] _particleSystem;
    private void Awake()
    {
        Instance = this;
    }
    public void PlayConfetteEffect()
    {
        foreach(ParticleSystem ps in _particleSystem)
        {
            ps.Play();
        }
    }
}
