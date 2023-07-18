using UnityEngine;

public class PlayerSalon : MonoBehaviour
{
    public static PlayerSalon Instance;

    [SerializeField] private float _velocity = 5f;
    //[SerializeField] private GameObject _camPlayer;

    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] int score = 0;
    [SerializeField] private Animator _anim;
    private Transform _transform;
    public bool fgmouse = true;
    //[SerializeField] private Joystick JoystickLeft = null;
    //[SerializeField] private Joystick JoystickRight = null;
    //private bool _useJoystick = true;
    private Vector3 _move = Vector3.zero;
    private Vector3 _moveLook = Vector3.zero;

    public int Score
    {
        get => score;
        set
        {
            score = value;
            //UIManager.Instance.UpdateUIScore(score);

        }
    }

    private void Awake()
    {
        _transform = transform;
        _rigidbody = GetComponent<Rigidbody>();
#if UNITY_EDITOR || UNITY_STANDALONE_WIN
        Cursor.lockState = CursorLockMode.Locked;
#endif

        if (Instance == null)
        {
            Instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
        //_camPlayer.SetActive(true);
        _anim = GetComponent<Animator>();
        _anim.SetInteger("estado", 3);
#if UNITY_EDITOR || UNITY_STANDALONE_WIN
        //JoystickLeft.gameObject.SetActive(false);
        //JoystickRight.gameObject.SetActive(false);
        //_useJoystick = false;
#endif
    }

    // Update is called once per frame
    void Update()
    {
        //Move();
        

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {

            _anim.SetInteger("estado", 2);
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            _anim.SetInteger("estado", 1);
        }
        else if (Input.GetKey(KeyCode.LeftControl))
        {
            _anim.SetInteger("estado", 3);
        }
    }

    private void Move()
    {
#if UNITY_EDITOR || UNITY_STANDALONE_WIN
        _move.x = Input.GetAxisRaw("Horizontal");
        _move.z = Input.GetAxisRaw("Vertical");
#endif
        /*if (_useJoystick)
        {

            _move.x = JoystickLeft.Horizontal;
            _move.z = JoystickLeft.Vertical;

        }*/
        if (_move.x != 0 || _move.z != 0)
        {
            Vector3 movimiento = _transform.right * _move.x + _transform.forward * _move.z;
            _transform.position += movimiento * Time.deltaTime * _velocity;
        }


        if (fgmouse)
        {
#if UNITY_EDITOR || UNITY_STANDALONE_WIN
            if (Cursor.lockState != CursorLockMode.Locked)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
            _moveLook.x = Input.GetAxis("Mouse X");
            _moveLook.y = Input.GetAxis("Mouse Y");

#endif
            /*if (_useJoystick)
            {
                _moveLook.x = JoystickRight.Horizontal * 1.5f;
                _moveLook.y = JoystickRight.Vertical * 1.5f;
            }*/

            _transform.Rotate(Vector3.up * _moveLook.x * _velocity);
            //_camPlayer.transform.Rotate(Vector3.left * _moveLook.y * _velocity);



        }
        else
        {
#if UNITY_EDITOR || UNITY_STANDALONE_WIN
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
#endif
        }





    }

    public void TakePoint(int point)
    {
        Score = Score - point;
        //GameManager.Instance.UpdateUIScore(Score);


    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "ColliderAula1")
        {
            GameManager.Instance.showLivingInformation(true, other.name);
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.name == "ColliderAula1")
        {
            GameManager.Instance.showLivingInformation(false, other.name);
        }
    }
}
