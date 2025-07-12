using UnityEngine;

public class playerMovement : MonoBehaviour
{
    public Vector2 InputVector;

    public float moveSpeed = 5f;

    Rigidbody2D rb;

    void Awake()
    {
       rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        InputVector.x = Input.GetAxisRaw("Horizontal");
        InputVector.y = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate()
    {
        Vector2 nextVec = InputVector.normalized * moveSpeed  *  Time.fixedDeltaTime;
        rb.MovePosition(rb.position + nextVec);
    }
}
