using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class MoveScript : MonoBehaviour
{
    public Rigidbody2D myRigidBody;
    public float speed;
    public float jumpStrength;
    public LogicScript logic;
    public Sprite happySprite;
    public Sprite deadSprite;
    private SpriteRenderer spriteRenderer;
    private Sprite defaultSprite;
    private bool canJump = true;
    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();

        spriteRenderer = GetComponent<SpriteRenderer>();
        defaultSprite = spriteRenderer.sprite;
    }

    // movement of soot sprite (can only jump once before hitting the ground)
    void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow) == true) {
            Vector3 movement = new Vector3(10, 0, 0);
            transform.Translate(movement * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.LeftArrow) == true) {
            Vector3 movement = new Vector3(-10, 0, 0);
            transform.Translate(movement * speed * Time.deltaTime);
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) && canJump){
            myRigidBody.velocity = Vector2.up * jumpStrength;
            canJump = false;
        }
    }

    // handle collisions
    private void OnTriggerEnter2D(Collider2D collision) {

        if (collision.gameObject.tag == "Coal" || collision.gameObject.tag == "Fire") {
            Debug.Log("Game Over");
            spriteRenderer.sprite = deadSprite;
            logic.gameOver();

        } else if (collision.gameObject.tag == "Candy") {
            Debug.Log("Increase Score");
            spriteRenderer.sprite = happySprite;
            Invoke("RevertSprite", 0.5f);
            Destroy(collision.gameObject);
            logic.addScore();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {

        spriteRenderer = GetComponent<SpriteRenderer>();

        if (collision.gameObject.tag == "Ground") {
            canJump = true;
        }
    }

    // to make the sprite go back to default after changing expressions
    private void RevertSprite() {
        spriteRenderer.sprite = defaultSprite;
    }
}
