using UnityEngine;

namespace KenTank.GameManager {
public class Transtition : MonoBehaviour
{
    [SerializeField] Animator animator;

    public bool waiting = true;

    public void Ready(bool value)
    {
        animator.SetBool("Ready", value);
        waiting = false;
    }

    public void Off() 
    {
        //gameObject.SetActive(false);
        Destroy(gameObject);
    }

    void OnEnable()
    {
        animator.SetBool("Ready", false);
        waiting = true;
    }

    public void FinishWaiting() 
    {
        waiting = false;
    }
}}
