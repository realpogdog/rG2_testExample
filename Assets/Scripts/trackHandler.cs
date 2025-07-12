using UnityEngine;

public class trackHandler : MonoBehaviour
{
    public GameObject playerCharacter;
    public float moveSpeed;
    
     Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    
    void Update()
    {
        rb.position =  Vector2.MoveTowards(transform.position, playerCharacter.transform.position, moveSpeed * Time.deltaTime);
    }
}
