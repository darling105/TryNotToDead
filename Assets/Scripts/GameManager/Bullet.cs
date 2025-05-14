using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] public float damage;
    [SerializeField] public int per;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Init(float damage, int per, Vector3 direcion)
    {
        this.damage = damage;
        this.per = per;

        if (per >= 0)
        {
            rb.linearVelocity = direcion * 15f;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Enemy") || per == -100)
            return;

        per--;

        if (per < 0)
        {
            rb.linearVelocity = Vector2.zero;
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(!other.CompareTag("Area")|| per == -100)
            return;
        
        gameObject.SetActive(false);
    }
}
