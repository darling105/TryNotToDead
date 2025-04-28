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

        if (per > -1)
        {
            rb.linearVelocity = direcion * 15f;
        }
    }
}