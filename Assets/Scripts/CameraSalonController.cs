using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CameraSalonController : MonoBehaviour
{
    private Transform _transform;
    [SerializeField] private GameObject _camTarget;
    [SerializeField] private GameObject _canvasPizarra;
    [SerializeField] bool active = true;
    [SerializeField] GameObject prefabButton;
    private GameObject _gameManager;

    // Start is called before the first frame update
    void Start()
    {
        _transform = transform;
        _gameManager = GameObject.Find("GameManager");
        StartCoroutine(Waiting(3,true));
        //active = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!active) return;
        _transform.position = Vector3.MoveTowards(_transform.position, _camTarget.transform.position, 5 * Time.deltaTime);
        if (Vector3.Distance(_transform.position, _camTarget.transform.position) <= 5 && active )
        {
            //_transform.rotation = Quaternion.Euler(0f, 90f, 0f);
            //Debug.Log("QUIEEETO, ESPERAR 3 SEGUNDOS PARA ABRIR CANVAS");
            abrirCanvas();
            print("Entra");
            _transform.position = _camTarget.transform.position;

        } 

    }
    

    IEnumerator Waiting(int time, bool fgactive)
    {   
        yield return new WaitForSeconds(time);
        active = fgactive;        
    }

  

    void abrirCanvas()
    {
        //Debug.Log("Entra a abrircanvas");
        //StartCoroutine(Waiting(3,false));       
        //Debug.Log("Activa pizarra");
        active = false;
        _canvasPizarra.SetActive(true);
        GameManager.Instance.PunteroVisiblePizarra();
        ObtenerCursos2Pizarra();
    }


    void ObtenerCursos2Pizarra()
    {
        print("grado:"+ PlayerPrefs.GetInt("varglobal_grado"));
        print("nivel:" + PlayerPrefs.GetString("varglobal_nivel"));
        //ControlAcceso.singleton.GetCursos(PlayerPrefs.GetInt("varglobal_grado"), PlayerPrefs.GetString("varglobal_nivel"));
        //ControlAcceso.singleton.GetCursos(2, "Primaria");
        /*for (int i = 0; i < GameManager.Instance.cursos.Length ; i++)
        {
            print(GameManager.Instance.cursos[i]);
        }*/
        StartCoroutine(cursos(PlayerPrefs.GetInt("varglobal_grado"), PlayerPrefs.GetString("varglobal_nivel")));

    }

    



    IEnumerator cursos(int grado, string nivel)
    {

        WWW conneccion = new WWW("https://virtual.espinosa.edu.pe/proyecto/espinosa3d/cursos.php?grado=" + grado + "&nivel=" + nivel);
        yield return (conneccion);
        if (conneccion.text == "401")
        {
            print("Grado y nivel incorrectos");
            //textError.text = "Usuario incorrecto";
            //MostrarError("Usuario incorrecto");
        }
        else
        {
            
            print("substring:"+conneccion.text.Substring(0, conneccion.text.Length-1));
            string[] nDatos = conneccion.text.Substring(0, conneccion.text.Length - 1).Split("-");
            GameManager.Instance.cursos = nDatos;
            RectTransform _rectransform;
            int j = 0;
            for (float i = 0; i< nDatos.Length; i++)
            {
                
                GameObject objectButton = Instantiate(prefabButton);
                objectButton.transform.parent = _canvasPizarra.transform;
                _rectransform = objectButton.GetComponent<RectTransform>();
                _rectransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Bottom, 0, 0);
                _rectransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, 0, 0);
                _rectransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, 0, 0);
                _rectransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Right, 0, 0);

                _rectransform.anchorMin = new Vector2(0.2f, (0.7f - (i/10)));
                _rectransform.anchorMax = new Vector2(0.8f, (0.8f - (i/10)));
                objectButton.transform.GetChild(0).GetComponent<Text>().text = nDatos[j].ToString();
                objectButton.transform.GetChild(0).GetComponent<Text>().resizeTextForBestFit = true;
                objectButton.transform.GetChild(0).GetComponent<Text>().font.name = "Arial";
                //objectButton.GetComponent<Button>().onClick.AddListener(() => GameManager.Instance.abrirCurso(nDatos[j].ToString().Split("|")[0]));
                int id_curso = int.Parse(nDatos[j].Split("|")[0]);
                print("ID_CURSO:" + id_curso);
                objectButton.GetComponent<Button>().onClick.AddListener(() => _gameManager.GetComponent<GameManager>().abrirCurso(id_curso));
                //objectButton.GetComponent<Button>().onClick.AddListener(TaskOnClick);
                //print(objectButton.transform.GetChild(0).GetComponent<Text>().text);
                
                    
                j++;
            }

            

        }

    }


}
