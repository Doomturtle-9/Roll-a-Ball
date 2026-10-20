using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;

    private int count;
    private int movementCount;

    private float movementX;
    private float movementY;

    public float speed = 10;

    public TextMeshProUGUI countText;

    public GameObject winTextObject;

    public TextMeshProUGUI movementText;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        count = 0;
        movementCount = 0;

        SetCountText();

        SetMoveCountText();

        winTextObject.SetActive(false);
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        
        movementY = movementVector.y;
        movementCount++;
        SetMoveCountText();
    }

    void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        
        rb.AddForce(movement * speed);
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);

            count = count + 1;

            
            SetCountText();
        }
    }

    void SetMoveCountText()
    {
        movementText.text = "Movements made: " + movementCount.ToString();
    }
    
    void SetCountText()
    {
        
        countText.text = "Count: " + count.ToString();

        

        
        if (count >= 8)
        {
            
            winTextObject.SetActive(true);

            
            Destroy(GameObject.FindGameObjectWithTag("Enemy"));
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            
            Destroy(gameObject);

            
            winTextObject.gameObject.SetActive(true);
            winTextObject.GetComponent<TextMeshProUGUI>().text = "You Lose!";

        }

    }


}