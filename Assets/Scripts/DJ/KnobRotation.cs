using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnobRotation : MonoBehaviour
{
    [SerializeField] private Camera Cam = null;
    public float rotationSpeed = 10f;  // Velocidad de rotación del knob

    private bool isRotating = false;  // Bandera para indicar si el knob está siendo rotado
    private Vector3 lastMousePosition;  // Última posición registrada del mouse


    private void OnMouseDown()
    {
        
        isRotating = true;
        lastMousePosition = Input.mousePosition;
    }

    // Método llamado cuando se suelta el clic del mouse
    private void OnMouseUp()
    {
        
        isRotating = false;
    }

    private void Update()
    {
        /*if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 50))
            {
                if (hit.collider.gameObject == gameObject)
                {
                    ButtonPress();
                }
            }
        }
        else
        {
            isRotating = false;
        }*/



        if (isRotating)
        {
            print("apretado");
            // Calcular la diferencia de posición del mouse desde la última actualización
            Vector3 mouseDelta = Input.mousePosition - lastMousePosition;

            // Calcular la rotación en función de la diferencia de posición
            float rotationAmount = -mouseDelta.y * rotationSpeed * Time.deltaTime;

            // Obtener la rotación actual del knob
            Vector3 currentRotation = transform.localEulerAngles;

            // Aplicar la rotación al knob
            transform.localEulerAngles = new Vector3(currentRotation.x , currentRotation.y + rotationAmount, currentRotation.z);

            // Actualizar la última posición del mouse
            lastMousePosition = Input.mousePosition;
        }
    }

    /*void ButtonPress()
    {
        isRotating = true;
        lastMousePosition = Input.mousePosition;
    }*/

}
