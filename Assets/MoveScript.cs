using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class MoveScript : MonoBehaviour
{
    public Rigidbody2D myRigidBody;
    public float speed;
    // Start is called before the first frame update
    public float jumpStrength;
    public LogicScript logic;
    public Sprite happySprite;
    public Sprite deadSprite;
    private SpriteRenderer spriteRenderer;
    private Sprite defaultSprite;
    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();

        spriteRenderer = GetComponent<SpriteRenderer>();
        defaultSprite = spriteRenderer.sprite;;
    }

    // Update is called once per frame
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

        if (Input.GetKeyDown(KeyCode.UpArrow) == true) {
            myRigidBody.velocity = Vector2.up * jumpStrength;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {

        spriteRenderer = GetComponent<SpriteRenderer>();

        switch (collision.gameObject.tag) {
            case "Coal":
                Debug.Log("Game Over");
                spriteRenderer.sprite = deadSprite;
                logic.gameOver();
                break;

            case "Candy":
                Debug.Log("Increase Score");
                spriteRenderer.sprite = happySprite;
                Invoke("RevertSprite", 0.5f);
                Destroy(collision.gameObject);
                logic.addScore();
                break;

            default:
                break;
        }
    }

    private void RevertSprite() {
        spriteRenderer.sprite = defaultSprite;
    }
}
