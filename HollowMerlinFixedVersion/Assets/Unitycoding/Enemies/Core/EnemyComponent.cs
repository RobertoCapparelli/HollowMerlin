using UnityEngine;
using ICode;
using AIV_Metroid_Player;
using UnityEngine.Events;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Collections;

public class EnemyComponent : MonoBehaviour, IDamager, IDamageble
{

    [SerializeField]
    private HealthModule healthModule;
    [SerializeField]
    private EnemyCollider[] meleeColliders;
    [SerializeField]
    private EnemyHittableCollider[] bodyColliders;
    [SerializeField]
    private EnemyMovementType movementType;
    [SerializeField]
    private float bodyDamage;
    [SerializeField]
    private float meleeDamage;
    [SerializeField]
    private DamageContainer damageContainer;
    [SerializeField]
    private bool canBeMoved;
    [SerializeField]
    private float AttackForMakeVulnerable;
    [SerializeField]
    private float timeVulnerable;

    private ICodeBehaviour codeBehaviour;
    private Animator animator;
    private IEnemyMovement movementComponent;

    private float attackCount;


    private void Awake()
    {
        codeBehaviour = GetComponent<ICodeBehaviour>();

        animator = GetComponent<Animator>();

        CreateEnemyMovement();
        foreach (EnemyCollider collider in meleeColliders)
        {
            collider.PlayerHitted += OnPlayerHittedMelee;
        }
        foreach (EnemyCollider collider in bodyColliders)
        {
            collider.PlayerHitted += OnPlayerHittedBody;
        }
        InitializeAI();
        ResetMe();
        healthModule.OnDamageTaken += InternalOnDamageTaken;
        healthModule.OnDeath += InternalOnDeath;
    }

    #region HealthModule

    public bool IsVulnerable
    {
        get;
        set;
    }

    public bool isDead
    {
        get { return healthModule.IsDead; }
    }

    public void ResetHealth()
    {
        healthModule.Reset();
    }

    public void TakeDamage(DamageContainer damage)
    {
        healthModule.TakeDamage(damage);
        Debug.Log($"La vita del nemico ({gameObject.name}) è di: {healthModule.CurrentHP}");

        attackCount++;

        if (isDead) return;

        if (attackCount >= AttackForMakeVulnerable)
        {
            IsVulnerable = true;
            StartCoroutine(VulnerableCoroutine());
        }


        if (!canBeMoved) return;
        Hitted(damage.DamageImpulse, damage.ContactPoint);


    }

    public void InternalOnDamageTaken(DamageContainer container)
    {

        //SetInvulnearble(damageInvTime);
    }

    public void InternalOnDeath()
    {
        //gameObject.SetActive(false);
    }
    #endregion


    public void ResetMe()
    {
        ResetHealth();
    }

    private void OnPlayerHittedMelee(IDamageble player, Vector2 contactPoint)
    {
        InternalSetPlayerDamage(player, contactPoint, meleeDamage);
    }

    private void OnPlayerHittedBody(IDamageble player, Vector2 contactPoint)
    {
        InternalSetPlayerDamage(player, contactPoint, bodyDamage);
    }

    #region Coroutine

    IEnumerator VulnerableCoroutine()
    {
        yield return new WaitForSeconds(timeVulnerable);
        IsVulnerable = false;
    }
    #endregion


    private void InternalSetPlayerDamage(IDamageble player, Vector2 contactPoint, float damage)
    {
        damageContainer.Damage = damage;
        damageContainer.ContactPoint = contactPoint;
        player.TakeDamage(damageContainer);
    }

    #region IEnemyMovement
    private void CreateEnemyMovement()
    {
        switch (movementType)
        {
            case EnemyMovementType.ground:
                movementComponent = gameObject.AddComponent<GroundMovement>();
                break;
            case EnemyMovementType.fly:
                movementComponent = gameObject.AddComponent<FlyMovement>();
                break;
        }
    }

    public void SetMovementSpeed(float movementSpeed)
    {
        movementComponent.SetMovementSpeed(movementSpeed);
    }

    public void SetInputDirection(Vector2 inputDirection)
    {
        movementComponent.SetInputDirection(inputDirection);
    }

    public void ReverseInputDirection()
    {
        movementComponent.ReverseInputDirection();
    }

    public void Jump()
    {
        movementComponent.Jump();
    }

    public void StopMovement()
    {
        movementComponent.StopMovement();
    }

    public void SetJumpForce(float jumpForce)
    {
        movementComponent.SetJumpForce(jumpForce);
    }

    public void SetFaceDirection(bool value)
    {
        movementComponent.SetFaceDirection(value);
    }

    public void Hitted(Vector2 hitForce, Vector3 sourcePosition)
    {
        movementComponent.Hitted(hitForce, sourcePosition);
    }

    public void Die(Vector2 dieForce, Vector3 sourcePosition)
    {
        movementComponent.Die(dieForce, sourcePosition);
    }
    public void Die()
    {
        movementComponent.Die();

        foreach(EnemyHittableCollider collider in bodyColliders)
        {
            collider.Collider.enabled = false;
        }
    }

    public void Teleport(Vector3 position)
    {
        movementComponent.Teleport(position);
    }

    public void SetVerticalMovement(float speed, float amplitude)
    {
        movementComponent.SetVerticalMovement(speed, amplitude);
    }

    public Vector2 InputDirection
    {
        get
        {
            return movementComponent.InputDirection;
        }
    }

    public bool IsGrounded
    {
        get
        {
            return movementComponent.IsGrounded;
        }
    }

    public bool FaceDirection
    {
        get
        {
            return movementComponent.FaceDirection;
        }
    }

    #endregion

    #region ICode
    [SerializeField]
    private StateMachine stateMachine;
    [SerializeField]
    private ExtendedVariable[] stateMachineVariables;

    private const string playerVariableString = "Player";
    private const string startPositionString = "StartPosition";

    private const string hitEventString = "Hit";
    private const string deadEventString = "Dead";

    private void InitializeAI()
    {
        codeBehaviour.stateMachine = stateMachine;
        codeBehaviour.EnableStateMachine();
        codeBehaviour.stateMachine.SetVariable(playerVariableString, Player.Get().gameObject);
        codeBehaviour.stateMachine.SetVariable(startPositionString, transform.position);
        foreach (ExtendedVariable variable in stateMachineVariables)
        {
            codeBehaviour.stateMachine.SetVariable(variable.VariableName, variable.GetValue());
        }
        IAIInit[] aiIniters = GetComponents<IAIInit>();
        foreach (IAIInit initer in aiIniters)
        {
            initer.Init(codeBehaviour);
        }
    }

    public void SendFSMCustomEvent(string eventName)
    {
        codeBehaviour.SendEvent(eventName, null);
    }

    private void SendHitFSMEvent()
    {
        SendFSMCustomEvent(hitEventString);
    }

    private void SendDeadFSMEvent()
    {
        SendFSMCustomEvent(deadEventString);
    }
    #endregion
}
