using UnityEngine;

public class NewMonoBehaviourScript1 : MonoBehaviour
{
    public Rigidbody2D rb;
    public float jump = 5f;
    private bool onGround = true;
    public float speed = 4f;
    public KeyCode noiseKey = KeyCode.X;
    public AudioSource bark; 
    public float cooldownTime = 5f;     
    public float cooldownTimer = 0f;
   
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        float xInput = Input.GetAxis("Horizontal");
        float yInput = Input.GetAxis("Vertical");

        if (Mathf.Abs(xInput) > 0)
        {
            rb.linearVelocity = new Vector2(xInput * speed, rb.linearVelocity.y);
        }

        if (Mathf.Abs(yInput) > 0)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, yInput * speed);
        }
        if (Input.GetKeyDown(KeyCode.Space) && onGround)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jump);
            onGround = false;
        }
        if (cooldownTimer > 0)
        {
            cooldownTimer -= Time.deltaTime;
        }

        
        if (Input.GetKeyDown(noiseKey) && cooldownTimer <= 0f)
        {
            MakeNoise();
            cooldownTimer = cooldownTime; // reset cooldown
        }
        void MakeNoise()
        {

            if (Input.GetKeyDown(noiseKey))
            {
                // Play the sound
                if (bark != null)
                    bark.Play();
            }
            // Example: tell the enemy
            GameObject meanDog = GameObject.FindWithTag("Mean Dog");
            if (meanDog != null)
            {
                meanDog.GetComponent<EnemyChase>().HearSound();
            }
            Debug.Log("Player made a noise!");
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            onGround = true;
        }
    }
 
}
