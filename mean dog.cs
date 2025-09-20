using UnityEngine;
using System.Collections;
public class EnemyChase: MonoBehaviour
{
        public GameObject player;      
    public float moveSpeed = 4f;  
    public bool isStopped = false;
    public int damage = 10;
    private SpriteRenderer sr;
    public float speed = 3f;
   
    public Rigidbody2D rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (!isStopped && player != null)
        {
           
            Vector2 direction = (player.transform.position - transform.position).normalized;
            transform.position += (Vector3)direction * moveSpeed * Time.deltaTime;
        }
        if (player == null) return;


        Vector2 moveDir = (player.transform.position - transform.position).normalized;
        rb.linearVelocity = new Vector2(moveDir.x * speed, rb.linearVelocity.y);

        if (rb.linearVelocity.x > 0.01f)
            sr.flipX = false; 
        else if (rb.linearVelocity.x < -0.01f)
            sr.flipX = true;
    }
   public void HearSound()
    {
        if (!isStopped)
            StartCoroutine(StopMomentarily());

    }
    IEnumerator StopMomentarily()
    {
        isStopped = true;
        Debug.Log("Enemy stopped because of noise");

        yield return new WaitForSeconds(2f);
        isStopped = false;
        Debug.Log("Enemy resumes chasing");
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }
        }
    }

}