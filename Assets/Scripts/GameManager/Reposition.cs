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


        switch (transform.tag)
        {
            case "Ground":
                float diffX = playerPosition.x - myPosition.x;
                float diffY = playerPosition.y - myPosition.y;
                float xDirection = diffX < 0 ? -1 : 1;
                float yDirection = diffY < 0 ? -1 : 1;
                diffX = Mathf.Abs(diffX);
                diffY = Mathf.Abs(diffY);

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
                    Vector3 playerDistance = playerPosition - myPosition;
                    Vector3 randomDirection = new Vector3(Random.Range(-3, 3), Random.Range(-3, 3), 0);
                    transform.Translate(randomDirection + playerDistance * 2);
                }
                break;
        }
    }
}
