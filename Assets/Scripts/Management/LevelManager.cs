using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private TouchManager touch;
    [SerializeField] private CharacterManager character;

    private void OnEnable()
    {
        touch.OnMove += character.Move;
        touch.OnJump += character.Jump;
    }

    private void OnDisable()
    {
        touch.OnMove -= character.Move;
        touch.OnJump -= character.Jump;
    }
}
