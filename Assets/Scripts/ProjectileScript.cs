using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    private bool collided;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Collidable" && !collided)
        {
            collided = true;
            Destroy(gameObject);
        }
    }
}
