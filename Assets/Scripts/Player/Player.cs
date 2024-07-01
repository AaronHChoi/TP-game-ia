using System.Collections.Generic;
using System.Collections;
using UnityEngine;
public class Player : MonoBehaviour, IPlayerModel
{
    public float speed;
    public float turnSmoothTime = 0.1f;
    public float playerValue;
    public GameObject player;
    private float turnSmoothVelocity;
    private Rigidbody rb;
    Transform cam;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        cam = Camera.main.transform;
    }
    public virtual void Move(Vector3 dir)
    {
        if (dir.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            rb.velocity = moveDir.normalized * speed;
        }
        else
        {
            rb.velocity = Vector3.zero; // Si la dirección es menor que 0.1f, detener el movimiento.
        }
    }

    public virtual void LookDir(Vector3 dir)
    {
        if (dir.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Food"))
        {
            // Get the value of the other object
            float otherValue = collision.gameObject.GetComponent<FoodSize>().currentSize;

            FoodSize foodSize = collision.gameObject.GetComponent<FoodSize>();

            // Compare values
            if (playerValue >= otherValue)
            {
                playerValue += foodSize.currentSize / 10;
                player.transform.localScale = new Vector3(playerValue, playerValue, playerValue);
                collision.gameObject.SetActive(false);
            }
            else if (playerValue < otherValue)
            {
                GameManager.Instance.EndGame("Lose");
            }
        }
    }
}
