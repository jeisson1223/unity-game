using UnityEngine;

public class Bullet : MonoBehaviour
{
    float speed = 20f; // Velocidad inicial
    Vector3 direction;

    public void setDirection(Vector3 dir)
    {
        direction = dir.normalized; // Asegúrate de que la dirección esté normalizada
    }

    void Start()
    {
       
    }

    void FixedUpdate()
    {
        transform.position += direction * speed * Time.deltaTime;

        // Puedes agregar lógica para detectar colisiones
        Collider[] targets = Physics.OverlapSphere(transform.position, 1);
        foreach (var item in targets)
        {
            if (item.CompareTag("Enemy"))
            {
                Destroy(item.gameObject); // Destruye al enemigo
                Destroy(gameObject); // Destruye la bala
            }
        }
    }
}
