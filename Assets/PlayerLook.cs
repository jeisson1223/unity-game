using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    [SerializeField] float mouseSense = 100f; // Asegúrate de que este valor no sea cero
    [SerializeField] Transform player, playerArms;

    float xAxisClamp = 0;

    void Update()
    {
        Cursor.lockState = CursorLockMode.Locked;

        float rotateX = Input.GetAxis("Mouse X") * mouseSense;
        float rotateY = Input.GetAxis("Mouse Y") * mouseSense;

        // Verifica los valores de entrada
        Debug.Log("rotateX: " + rotateX + ", rotateY: " + rotateY);

        //xAxisClamp -= rotateY; // Cambiar a rotateY aquí para limitar la rotación vertical

        Vector3 rotPlayerArms = playerArms.rotation.eulerAngles;
        Vector3 rotPlayer = player.rotation.eulerAngles;

        rotPlayerArms.x -= rotateY;
        rotPlayerArms.z = 0; 
        rotPlayer.y += rotateX;

        // Limitar la rotación vertical
        if (xAxisClamp > 90)
        {
            xAxisClamp = 90;
            rotPlayerArms.x = 90;
        }
        else if (xAxisClamp < -90)
        {
            xAxisClamp = -90;
            rotPlayerArms.x = 270;
        }

        playerArms.rotation = Quaternion.Euler(rotPlayerArms);
        player.rotation = Quaternion.Euler(rotPlayer);
    }
}
