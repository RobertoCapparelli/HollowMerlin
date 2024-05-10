using UnityEngine;

public enum EnemyMovementType {
    ground,
    fly
}

public interface IEnemyMovement 
{

    void SetMovementSpeed(float movementSpeed);
    void SetInputDirection(Vector2 inputDirection);
    void ReverseInputDirection();
    void Jump();
    void StopMovement();
    void SetJumpForce(float jumpForce);
    void SetFaceDirection(bool value);
    void Hitted(Vector2 hitForce, Vector3 sourcePosition);
    void Die(Vector2 dieForce, Vector3 sourcePosition);
    void Die();
    void Teleport(Vector3 position);
    void SetVerticalMovement(float speed, float amplitude);


    Vector2 InputDirection {
        get;
    }
    bool IsGrounded {
        get;
    }
    bool FaceDirection {
        get;
    }

}
