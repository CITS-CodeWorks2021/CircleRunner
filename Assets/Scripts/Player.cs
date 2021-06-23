using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    public float jumpForce, moveForce, invulnTime;
    public int startHealth;
    int health;
    public LayerMask groundLayers;

    Vector2 jumpVec;
    bool isGrounded, isPlaying, canHurt;
    float timer_Invuln;
    Animator myAnims;

    public UnityEvent<int> OnHealthUpdate;

    // Start is called before the first frame update
    void Start()
    {
        jumpVec = new Vector2(0, jumpForce);
        myAnims = GetComponent<Animator>();

        if (OnHealthUpdate == null) OnHealthUpdate = new UnityEvent<int>();

        GameLogic.OnStart.AddListener(StartGame);

        // Temp, should be replaced with proper game state
        //StartGame();
    }

    void StartGame()
    {
        transform.position = Vector3.zero;
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        isPlaying = true;
        canHurt = true;
        health = startHealth;
        OnHealthUpdate.Invoke(health);
        GetComponent<SpriteRenderer>().enabled = true;
        myAnims.SetBool("isPlaying", isPlaying);
    }

    void EndGame()
    {
        GameLogic.OnEnd.Invoke();
        // Hide Player
        GetComponent<SpriteRenderer>().enabled = false;
        isPlaying = false;
        StopAllCoroutines();
        myAnims.SetBool("isPlaying", isPlaying);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPlaying) return;
        // To help tweak values in Inspector, can be removed when done
        jumpVec = new Vector2(0, jumpForce); 

        //Am I on the Ground?
        RaycastHit2D groundCheck = Physics2D.Raycast(transform.position, Vector2.down, 0.8f, groundLayers);
        if (groundCheck.collider != null && Mathf.Abs(GetComponent<Rigidbody2D>().velocity.y) < 0.1f)
        {
            isGrounded = true;
        }
        else isGrounded = false;

        

        // Should I JUMP?!
        if (Input.GetAxis("Jump") > 0.1f && isGrounded)
        {
            GetComponent<Rigidbody2D>().AddForce(jumpVec, ForceMode2D.Impulse);
            isGrounded = false;
            myAnims.SetFloat("vertSpeed", 1.0f);
        }

        

        //Should I drop Sharply??
        if (GetComponent<Rigidbody2D>().velocity.y < 0.25f && !isGrounded)
        {
            // at the apex of curve?
            GetComponent<Rigidbody2D>().AddForce(Vector2.down*2, ForceMode2D.Force);
            myAnims.SetFloat("vertSpeed", -1.0f);
        }

        // Horizontal Movement, setting instead of pushing for better "feels"
        if(Mathf.Abs(Input.GetAxis("Horizontal")) > 0.1f)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(
                moveForce * Input.GetAxis("Horizontal"),
                GetComponent<Rigidbody2D>().velocity.y
                );
            if (Input.GetAxis("Horizontal") > 0) myAnims.SetFloat("speed", 1.0f);
            else myAnims.SetFloat("speed", -1.0f);
        }
        else myAnims.SetFloat("speed", 0.0f);

        myAnims.SetBool("isGrounded", isGrounded);


    }

    public void TakeDamage()
    {
        Debug.Log("OUCH!");
        if (!canHurt) return;

        health--;
        OnHealthUpdate.Invoke(health);

        if (health <= 0)
        {
            EndGame();
            return;
        }

        canHurt = false;
        timer_Invuln = invulnTime;
        StartCoroutine(InvulnerableTimer());
        myAnims.SetBool("Invulnerable", true);
    }

    IEnumerator InvulnerableTimer()
    {
        while(timer_Invuln > 0)
        {
            timer_Invuln -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        canHurt = true;
        myAnims.SetBool("Invulnerable", false);
    }

    // For things that absolutely end the game, like a pit of doom sorta thing
    public void LethalHit()
    {
        EndGame();
        transform.position = Vector3.zero;
    }
}
