using ParadoxNotion;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[HideMonoScript, RequireComponent(typeof(Inventory), typeof(NavMeshAgent))]
public class Character : MonoBehaviour
{
    [SerializeField, Min(0)]
    private int _health;
    [SerializeField, Min(0)]
    private int _stamina;
    [SerializeField, Min(0)]
    private int _hunger;

    public Stat healthMax;
    public Stat staminaMax;
    public Stat hungerMax;

    public int Health
    {
        get => _health;
        set
        {
            if (_health != value)
            {
                _health = Mathf.Clamp(value, 0, healthMax.FinalValue);
                if (_health == 0)
                    Destroy(gameObject);
            }
        }
    }

    public int Stamina
    {
        get => _stamina;
        set
        {
            if (_stamina != value)
                _stamina = Mathf.Clamp(value, 0, staminaMax.FinalValue);
        }
    }

    public int Hunger
    {
        get => _hunger;
        set
        {
            if (_hunger != value)
                _hunger = Mathf.Clamp(value, 0, hungerMax.FinalValue);
        }
    }

    private NavMeshAgent nmAgent;
    [SerializeField, Required]
    private Animator animator;

    private void Awake()
    {
        nmAgent = GetComponent<NavMeshAgent>();
    }

    public bool GoTo(Vector3 position, float distance = 1)
    {
        nmAgent.isStopped = false;
        //TODO: path not valid
        //TODO: animation
        nmAgent.SetDestination(position);

        animator.SetFloat("MoveSpeed", nmAgent.velocity.magnitude);
        Debug.Log(nmAgent.velocity.magnitude);

        if (nmAgent.pathPending)
            return false;

        if (nmAgent.remainingDistance < distance)
        {
            nmAgent.SetDestination(transform.position);

            Quaternion targetRotation = Quaternion.LookRotation(Vector3.ProjectOnPlane(position, Vector3.up) - Vector3.ProjectOnPlane(transform.position, Vector3.up));
            if (Quaternion.Angle(transform.rotation, targetRotation) > 1)
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 1000 * Time.deltaTime);
            }
            else
            {
                return true;
            }
        }

        return false;
    }

    public bool GoTo(Target target)
    {
        return GoTo(target.transform.position, target.stopDistance);
    }

    public void Stop()
    {
        nmAgent.isStopped = true;
        animator.SetFloat("MoveSpeed", 0);
    }
}
