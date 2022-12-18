using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    void OnCollisionEnter(Collision other)
    {
        switch (other.gameObject.tag)
        {
            case "Friendly":
                break;

            case "Finish":
                break;

            case "Fuel":
                break;

            default:
                break;
        }
    }
}
