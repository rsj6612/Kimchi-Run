using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Settings")]
    public float JumpForce;
    public bool isInvincible = false;

    [Header("References")]
    public Rigidbody2D rb;
    public Animator animator;
    public BoxCollider2D playerCollider;

    private bool isGrounded = true;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && isGrounded){
            // rb.linearVelocityX = 10;
            // rb.linearVelocityY = 20;
            rb.AddForceY(JumpForce, ForceMode2D.Impulse);
            isGrounded = false;
            animator.SetInteger("State", 1);
        }
    }

    public void KillPlayer(){
        playerCollider.enabled = false;
        animator.enabled = false;
        rb.AddForceY(JumpForce, ForceMode2D.Impulse);
    }
    void Hit(){
        GameManager.Instance.lives -= 1;
    }

    void Heal(){
        GameManager.Instance.lives += Mathf.Min(3, GameManager.Instance.lives + 1); // lives를 3으로 제한함.
    }

    void StartInvincible(){
        isInvincible = true;
        Invoke("StopInvincible", 5f);
    }

    void StopInvincible(){
        isInvincible = false;
    }
    void OnCollisionEnter2D(Collision2D collision){
        if (collision.gameObject.name == "Platform"){
            if(!isGrounded){
                animator.SetInteger("State", 2);
            }
            isGrounded = true;
        }
    }

    void OnTriggerEnter2D(Collider2D other){
        if (other.gameObject.tag == "enemy")
        {
            if (!isInvincible){
                Destroy(other.gameObject);
                Hit();
            }
        }
        else if (other.gameObject.tag=="food")
        {
            Destroy(other.gameObject);
            Heal();
        }
        else if (other.gameObject.tag=="golden")
        {
            Destroy(other.gameObject);
            StartInvincible();
        }
    }
}

