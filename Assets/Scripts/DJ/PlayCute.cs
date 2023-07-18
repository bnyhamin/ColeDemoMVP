
using UnityEngine;

public class PlayCute : MonoBehaviour
{
    [SerializeField] private Camera Cam = null;
    [SerializeField] private GameObject goFondo;
    private bool activeButton = false;

    [SerializeField] private Material colorOff;
    [SerializeField] private Material colorOn;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
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
        

        
    }
    void ButtonPress()
    {
        if (!activeButton)
        {
            goFondo.GetComponent<MeshRenderer>().material = colorOn;
            activeButton = true;
        }
        else
        {
            goFondo.GetComponent<MeshRenderer>().material = colorOff;
            activeButton = false;
        }
    }

}
