using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PracticaController : MonoBehaviour
{
    [SerializeField] private GameObject[] partes;
    private RectTransform _rectransform;
    //[SerializeField] private GameObject buttonAgain;
    //[SerializeField] private GameObject buttonContinue;
    public int respuestasExamenCorrectas = 0;
    public int respuestasExamenIncorrectas = 0;
    public Text txtCantCorrectas;
    public Text txtCantInorrectas;

    private void Awake()
    {
        _rectransform = GetComponent<RectTransform>();
    }
    // Start is called before the first frame update
    void Start()
    {
        _rectransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Bottom, 0, 0);
        _rectransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, 0, 0);
        _rectransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, 0, 0);
        _rectransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Right, 0, 0);

        _rectransform.anchorMin = new Vector2(0, 0);
        _rectransform.anchorMax = new Vector2(1, 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void valida(string texto)
    {
        // int parte, int respEsperada, int respObtenida
        int parte = int.Parse(texto.Split('|')[0]);
        int respEsperada = int.Parse(texto.Split('|')[1]);
        int respObtenida = int.Parse(texto.Split('|')[2]);
        print("parte:" + parte + " de partes:" + partes.Length);

        if (respEsperada == respObtenida)
        {
            //GameManager.Instance.respuestasExamenCorrectas++;
            respuestasExamenCorrectas++;
        }
        else
        {
            //GameManager.Instance.respuestasExamenIncorrectas++;
            respuestasExamenIncorrectas++;
        }
        partes[parte].gameObject.SetActive(false);
        partes[parte + 1].gameObject.SetActive(true);
        if (parte + 1 == partes.Length - 1)
        {
            //GameManager.Instance.UpdateTextCantidades();
            UpdateTextCantidades();
            //txtCantCorrectas.text = respuestasExamenCorrectas.ToString();
            //txtCantInorrectas.text = respuestasExamenIncorrectas.ToString();
            //GameManager.Instance.txtCantCorrectas.text = GameManager.Instance.respuestasExamenCorrectas.ToString();
            //GameManager.Instance.txtCantInorrectas.text = GameManager.Instance.respuestasExamenIncorrectas.ToString();




        }
    }

    public void volver()
    {
        //GameManager.Instance.respuestasExamenCorrectas = 0;
        //GameManager.Instance.respuestasExamenIncorrectas = 0;
        //GameManager.Instance.txtCantCorrectas.text = string.Empty;
        //GameManager.Instance.txtCantInorrectas.text = string.Empty;
        respuestasExamenCorrectas = 0;
        respuestasExamenIncorrectas = 0;
        txtCantCorrectas.text = string.Empty;
        txtCantInorrectas.text = string.Empty;
        partes[0].gameObject.SetActive(true);
        partes[partes.Length - 1].gameObject.SetActive(false);
    }

    public void continuar()
    {
        //gameObject.SetActive(false);
        //GameManager.Instance.CloseExamen();        
        CerrarMaterial();
    }

    public void salir()
    {
        gameObject.SetActive(false);
        GameManager.Instance.CloseMaterial(3);
    }

    public void CerrarMaterial()
    {
        int idContenidoUltimoVistoCurso = PlayerPrefs.GetInt("varglobal_idContenidoUltimoVistoCurso");
        int id_materialactual = PlayerPrefs.GetInt("varglobal_idContenido");
        print("idContenidoUltimoVistoCurso:" + idContenidoUltimoVistoCurso + "-id_materialactual:" + id_materialactual);
        if (id_materialactual >= idContenidoUltimoVistoCurso)
        {
            print("ENTRAAAA A ACTUALIZAAAAAR");
            GameManager.Instance.actualizar_contenidoultimovistocurso(id_materialactual);
        }

        GameManager.Instance.CloseMaterial(3);
        DestroyImmediate(gameObject);
        //StartCoroutine(waitClose());
        
    }

    /*IEnumerator waitClose()
    {
        yield return new WaitForSeconds(1);
        DestroyImmediate(gameObject);
    }*/

    public void UpdateTextCantidades()
    {
        print("Resp correctas:" + respuestasExamenCorrectas.ToString());
        print("Resp INcorrectas:" + respuestasExamenIncorrectas.ToString());
        txtCantCorrectas.text = respuestasExamenCorrectas.ToString();
        txtCantInorrectas.text = respuestasExamenIncorrectas.ToString();
    }
}
