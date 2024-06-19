using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player")]
    public float speed;
    private Rigidbody rb;
    private float x;
    private float z;
    [Header("Gun")]
    public GameObject bullet;
    public Transform spawnPoint;
    public float bulletSpeed;
    public TextMeshProUGUI probabilityText;
    private int p = 100;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        probabilityText.text = p.ToString() + "%";

        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");
        rb.velocity = new Vector3(x * speed, rb.velocity.y, z * speed);

        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }



        Vector3 mouseScreenPosition = Input.mousePosition;
        mouseScreenPosition.z = Camera.main.transform.position.y;
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mouseScreenPosition);

        Vector3 direction = mouseWorldPosition - transform.position;
        direction.y = 0; // Ignore the y-axis (height) to keep it 2D


        Quaternion rotation = Quaternion.LookRotation(direction);


        transform.rotation = rotation;
    }

    void Shoot()
    {
        int r = Random.Range(1, 101);
        if (r <= p)
        {
            var a = Instantiate(bullet, spawnPoint.position, spawnPoint.rotation);
            Rigidbody bulletRb = a.GetComponent<Rigidbody>();
            if (bulletRb != null)
            {
                bulletRb.velocity = spawnPoint.forward * bulletSpeed;
            }
            p--;
        }
        else
        {
            Invoke("Ret", 1f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Recover"))
        {
            Destroy(other.gameObject);
            p = 100;
        }
        if (other.gameObject.CompareTag("Enemy"))
        {
            Invoke("Ret", 1f);
        }
        if (other.gameObject.CompareTag("Death"))
        {
            SceneManager.LoadScene(1);
        }
    }

    void Ret()
    {
        SceneManager.LoadScene(0);
    }
}
