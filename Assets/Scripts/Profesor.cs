using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Profesor : MonoBehaviour
{
    public static Profesor Instance;
        
    private Transform _transform;
    [SerializeField] private GameObject _firstTarget;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private Animator _anim;
    Rigidbody rb;
    // Start is called before the first frame update
    private void Awake()
    {
        _transform = transform;
        _rigidbody = GetComponent<Rigidbody>();
        _anim = GetComponent<Animator>();
        rb = _anim.GetComponent<Rigidbody>();
        _anim.SetInteger("state", 0);

        if (Instance == null)
        {
            Instance = this;
        }
    }
    void Start()
    {
        
        MoverLef();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(Vector3.Distance(_transform.position, _firstTarget.transform.position));
        if (Vector3.Distance(_transform.position, _firstTarget.transform.position) <= 5)
        {
            _transform.rotation = Quaternion.Euler(0f, 90f, 0f);
            _anim.SetInteger("state", 1);
        }
    }

    void MoverLef()
    {
        //_transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.identity , Time.deltaTime );
        //float tiltAroundX = Input.GetAxis("Vertical") * tiltAngle;
        //Quaternion target = Quaternion.Euler(0, 0, 0);

        // Dampen towards the target rotation
        //transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * 5);
        
        _transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        //Vector3 target = _firstTarget;// new Vector3(rb.position.x, rb.position.y, 7.2f);
        //Vector3 nuevaPos = Vector3.MoveTowards(_transform.position, _firstTarget, 5 * Time.deltaTime);
        //rb.MovePosition(nuevaPos);

    }
}
