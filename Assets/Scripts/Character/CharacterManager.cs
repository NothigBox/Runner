using UnityEngine;

[RequireComponent(typeof(CharacterMovement))]
public class CharacterManager : MonoBehaviour
{
    CharacterMovement movement;

    private void Awake()
    {
        movement = GetComponent<CharacterMovement>();
    }

    public void Move(Vector2 direction)
    {
        movement.MoveSideways(direction.x);
    }

    public void Jump()
    {
        movement.Jump();
    }
}
