using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform rifleStart;
    [SerializeField] private Text HpText;

    [SerializeField] private GameObject GameOver;
    [SerializeField] private GameObject Victory;

    public float bulletSpeed = 20f; // Velocidad del proyectil
    public float health = 100;
    public float moveSpeed = 5f; // Velocidad de movimiento
    private CharacterController characterController;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        
    }

    public void ChangeHealth(int hp)
    {
        health += hp;
        if (health > 100)
        {
            health = 100;
        }
        else if (health <= 0)
        {
            Lost();
        }
        HpText.text = health.ToString();
    }

    public void Win()
    {
        Victory.SetActive(true);
        Destroy(GetComponent<PlayerLook>());
        Cursor.lockState = CursorLockMode.None;
    }

    public void Lost()
    {
        GameOver.SetActive(true);
        Destroy(GetComponent<PlayerLook>());
        Cursor.lockState = CursorLockMode.None;
    }

    void Update()

    {
        if (Input.GetMouseButtonDown(0)) // Clic izquierdo
        {
            // Crear un nuevo proyectil
            GameObject projectile = Instantiate(bullet, rifleStart.position, rifleStart.rotation);
            
            // Añadir fuerza al proyectil para que se mueva
            Rigidbody rb = projectile.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddForce(rifleStart.forward * bulletSpeed, ForceMode.Impulse);
            }
        }   
        MovePlayer(); // Llama al método MovePlayer para mover al jugador.

        if (Input.GetMouseButtonDown(0))
        {
            GameObject buf = Instantiate(bullet);
            buf.transform.position = rifleStart.position;
            buf.GetComponent<Bullet>().setDirection(transform.forward);
            buf.transform.rotation = transform.rotation;
        }

        if (Input.GetMouseButtonDown(1))
        {
            Collider[] tar = Physics.OverlapSphere(transform.position, 2);
            foreach (var item in tar)
            {
                if (item.tag == "Enemy")
                {
                    Destroy(item.gameObject);
                }
            }
        }

        Collider[] targets = Physics.OverlapSphere(transform.position, 3);
        foreach (var item in targets)
        {
            if (item.tag == "Heal")
            {
                ChangeHealth(50);
                Destroy(item.gameObject);
            }
            if (item.tag == "Finish")
            {
                Win();
            }
            if (item.tag == "Enemy")
            {
                Lost();
            }
        }
    }

    private void MovePlayer()
    {
        float horizontal = Input.GetAxis("Horizontal"); // A y D o flechas izquierda y derecha
        float vertical = Input.GetAxis("Vertical"); // W y S o flechas arriba y abajo

        Vector3 move = transform.right * horizontal + transform.forward * vertical; // Crea un vector de movimiento

        if (move.magnitude > 0) // Solo mover si hay entrada
        {
            characterController.Move(move * moveSpeed * Time.deltaTime); // Mueve el jugador
        }
    }
}
