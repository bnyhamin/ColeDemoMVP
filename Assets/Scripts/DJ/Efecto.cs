using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Efecto : MonoBehaviour
{
    [SerializeField] private Camera Cam = null;    
    private bool activeButton = false;
    private Transform _transform = null;
    private Vector3 _initialPosition = Vector3.zero;
    private Vector3 _endPosition = Vector3.zero;
    private Material colorOff;
    [SerializeField] private Material colorOn;

    private void Awake()
    {

        _transform = transform;
        _initialPosition = _transform.position;
        _endPosition = _initialPosition;
        _endPosition.y = _endPosition.y - 0.1f;
        colorOff = GetComponent<MeshRenderer>().material;


    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 50))
            {
                if (hit.collider.gameObject == gameObject /*&& activeButton*/)
                {
                    ButtonPress();
                }
            }
        }
    }

    void ButtonPress()
    {
        GetComponent<MeshRenderer>().material = colorOn;
        StartCoroutine(ChangePosition("in"));
        

        /*if (!activeButton)
        {
            goFondo.GetComponent<MeshRenderer>().material = colorOn;
            activeButton = true;
            StartCoroutine(ChangePosition("in"));
        }
        else
        {
            goFondo.GetComponent<MeshRenderer>().material = colorOff;
            activeButton = false;
            StartCoroutine(ChangePosition("out"));
        }*/
    }

    private IEnumerator ChangePosition(string direction)
    {
        Vector3 initPosition = _transform.position;
        float currentTime = 0;
        float TotalTime = 0.5f;
        activeButton = false;

        Vector3 targetPosition = (direction == "in") ? _endPosition  : _initialPosition;

        while (currentTime < TotalTime)
        {
            
            float percent = currentTime * TotalTime;
            transform.position = Vector3.Lerp(initPosition, targetPosition, percent);
            currentTime += Time.deltaTime;
            yield return null;
        }
        //
        if (direction == "in") StartCoroutine(ChangePosition("out"));
        else
        {
            activeButton = true;
            GetComponent<MeshRenderer>().material = colorOff;
        }
            

    }
}
