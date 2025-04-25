using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Reposition : MonoBehaviour
{
    [SerializeField] private Collider2D cld;

    private void Awake()
    {
        cld = GetComponent<Collider2D>();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Area"))
            return;

        Vector3 playerPosition = GameManager.instance.player.transform.position;
        Vector3 myPosition = transform.position;

        float diffX = Mathf.Abs(playerPosition.x - myPosition.x);
        float diffY = Mathf.Abs(playerPosition.y - myPosition.y);

        Vector3 playerDirection = GameManager.instance.player.movementInput;
        float xDirection = playerDirection.x < 0 ? -1 : 1;
        float yDirection = playerDirection.y < 0 ? -1 : 1;

        switch (transform.tag)
        {
            case "Ground":
                if (diffX > diffY)
                {
                    transform.Translate(Vector3.right * xDirection * 40);
                }
                else if (diffX < diffY)
                {
                    transform.Translate(Vector3.up * yDirection * 40);
                }

                break;
            case "Enemy":
                if (cld.enabled)
                {
                    transform.Translate(playerDirection * 20 +
                                        new Vector3(Random.Range(-3f, 3f), Random.Range(-3f, 3f), 0f));
                }

                break;
            default:
                break;
        }
    }
}