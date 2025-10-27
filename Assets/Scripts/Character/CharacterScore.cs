using UnityEngine;

public class CharacterScore : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Obstacle") == true)
        {
            gameObject.SetActive(false);
        }
    }
}
