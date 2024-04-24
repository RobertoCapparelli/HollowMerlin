using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FlyEnemyControllerStatus
{
    moving,
    hitted,
    dying,
    dead
}
public class FlyingEnemy : MonoBehaviour, IEnemyMovement
{
    #region Private_Attributes

    private float maxFlyingSpeed = 10;

    protected Rigidbody2D myRigidbody;
    private BoxCollider2D myCollider;

    private float movementSpeed;
    private Vector2 inputDirection;

    private Vector2 preHitVelocity;
    private FlyEnemyControllerStatus status;

    #endregion

    #region Properties

    public float MovementSpeed { get { return movementSpeed; } set { if (value < 0) { movementSpeed = 0; return; } movementSpeed = value; } }
    public Vector2 InputDirection { get { return inputDirection; } }
    public float MaxFlyingSpeed { get { return maxFlyingSpeed; } set { maxFlyingSpeed = value; } }
    public bool FaceDirection { get; private set; } // If applicable for flying enemies
    public bool IsGrounded { get { return false; } }

    #endregion

    #region UnityGameLoop
    private void Awake()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myCollider = GetComponentInChildren<BoxCollider2D>();
        ResetMe();
    }

    private void FixedUpdate()
    {
        switch (status)
        {
            case FlyEnemyControllerStatus.dying:
                Dying();
                break;
        }
        IncreaseFallSpeed();
    }


    #endregion


    #region IEnemyMovement

    public void SetMovementSpeed(float movementSpeed)
    {
        MovementSpeed = movementSpeed;
    }

    public void SetFaceDirection(bool value)
    {
        FaceDirection = value;
    }

    public void SetInputDirection(Vector2 inputDirection)
    {
        this.inputDirection = inputDirection.normalized;

        // Calculate desired velocity based on input direction and max flying speed
        Vector2 desiredVelocity = inputDirection * MaxFlyingSpeed;

        // Apply movement based on current status:
        switch (status)
        {
            case FlyEnemyControllerStatus.moving:
                // Smoothly transition velocity towards desired velocity
                myRigidbody.velocity = Vector2.Lerp(myRigidbody.velocity, desiredVelocity, Time.deltaTime * MovementSpeedModifier);
                break;

            case FlyEnemyControllerStatus.hitted:
                // Apply a hit reaction force (modify as needed)
                myRigidbody.AddForce(inputDirection * HitForce, ForceMode2D.Impulse);
                // Store pre-hit velocity for potential recovery behavior
                preHitVelocity = myRigidbody.velocity;
                break;

            case FlyEnemyControllerStatus.dying:
                // Gradually slow down or apply a dying force
                myRigidbody.velocity = Vector2.Lerp(myRigidbody.velocity, Vector2.zero, Time.deltaTime * SlowdownFactor);
                break;

            case FlyEnemyControllerStatus.dead:
                // Stop movement entirely (optional: handle falling physics)
                myRigidbody.velocity = Vector2.zero;
                break;

            default:
                // Handle unexpected states (optional)
                Debug.LogError("Unexpected FlyEnemyControllerStatus: " + status);
                break;
        }

        // Update facing direction if applicable
        if (FaceDirection)
        {
            ChangeYRotation();
        }
    }

    public void ReverseInputDirection()
    {
        throw new System.NotImplementedException();
    }

    public void Jump()
    {
        throw new System.NotImplementedException();
    }

    public void StopMovement()
    {
        throw new System.NotImplementedException();
    }

    public void SetJumpForce(float jumpForce)
    {
        throw new System.NotImplementedException();
    }

    public void Hitted(Vector2 hitForce, Vector3 sourcePosition)
    {
        throw new System.NotImplementedException();
    }

    public void Die(Vector2 dieForce, Vector3 sourcePosition)
    {
        throw new System.NotImplementedException();
    }

    public void Teleport(Vector3 position)
    {
        throw new System.NotImplementedException();
    }

    #endregion

    public void ResetMe()
    {
        FaceDirection = true;
        status = FlyEnemyControllerStatus.moving;
    }
}
