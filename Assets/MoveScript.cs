using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveScript : MonoBehaviour
{
    public Rigidbody2D myRigidBody;
    public float speed;
    // Start is called before the first frame update
    public float jumpStrength;
    public LogicScript logic;
    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
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

        if (Input.GetKeyDown(KeyCode.Space) == true) {
            myRigidBody.velocity = Vector2.up * jumpStrength;
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("CandyCoal")) {
            Debug.Log("Game Over");
            logic.gameOver();
        }
    }
}
