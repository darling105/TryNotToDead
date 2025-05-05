using System;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public bool isLeft;
    public SpriteRenderer sr;

    SpriteRenderer player;

    Vector3 rightHandPosition = new Vector3(0.35f, -0.15f, 0);
    Vector3 reverseRightHandPosition = new Vector3(-0.15f, -0.15f, 0);
    Quaternion leftHandRotation = Quaternion.Euler(0, 0, -35);
    Quaternion reverseLeftHandRotation = Quaternion.Euler(0, 0, -135);

    private void Awake()
    {
        player = GetComponentsInParent<SpriteRenderer>()[1];
    }

    private void LateUpdate()
    {
        bool isReverse = player.flipX;

        if (isLeft)
        {
            transform.localRotation = isReverse ? reverseLeftHandRotation : leftHandRotation;
            sr.flipY = isReverse;
            sr.sortingOrder = isReverse ? 4 : 6;
        }
        else
        {
            transform.localPosition = isReverse ? reverseRightHandPosition : rightHandPosition;
            sr.flipX = isReverse;
            sr.sortingOrder = isReverse ? 6 : 4;
        }
    }
}
