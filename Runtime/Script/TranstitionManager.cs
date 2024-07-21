using UnityEngine;

namespace KenTank.GameManager {
public class TranstitionManager : MonoBehaviour
{
    [SerializeField] Transtition transition;
    public static Transtition _transition;

    public void Initialize()
    {
        _transition = Instantiate(transition, GameManager.instance.transtitionManager.transform);
        _transition.gameObject.SetActive(false);
    }

    public void Close() 
    {
        _transition.gameObject.SetActive(true);
    }

    public void Open()
    {
        _transition.Ready(true);
    }

    public bool waiting 
    {
        get {
            return _transition.waiting;
        }
        set {
            _transition.waiting = value;
        }
    }
}}
