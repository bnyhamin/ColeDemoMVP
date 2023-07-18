using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EjercicioController : MonoBehaviour
{
    [SerializeField] private GameObject[] partes;

    //[SerializeField] private Text felicitaciones;
    //[SerializeField] private GameObject panelEjercicios;
    private Text felicitaciones;
    private GameObject panelEjercicios;
    private RectTransform _rectransform;

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

        felicitaciones = GameManager.Instance.felicitaciones; //  GameObject.Find("Felicitaciones").GetComponent<Text>()
        panelEjercicios = GameManager.Instance.panelEjercicios;
        felicitaciones.gameObject.SetActive(false);
        clean();
        //felicitaciones = GameObject.Find("Felicitaciones").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void clean()
    {
        print("Limpiandos");
        //felicitaciones.gameObject.SetActive(false);

        /*GameObject[] _cube = FindObjectsOfType<GameObject>();
        for (int i = 0; i < _cube.Length; i++)
        {
            if (_cube[i].name.Contains("Bien"))
            {
                _cube[i].gameObject.SetActive(false);
            }

        }*/

        /*for (int i = 0; i < partes.Length; i++)
        {
            int cant = partes[i].transform.childCount;
            print("Cantidad a limpiar:"+cant);
            for (int j = 0; j < cant; j++)
            {
                print("iterando:" + j+" "+ partes[i].transform.GetChild(j).name);
                if (partes[i].transform.GetChild(j).name.Contains("InputField"))
                {
                    print("entraaaa" + partes[i].transform.GetChild(j).gameObject.transform.childCount);
                    GameObject textrespuesta = partes[i].transform.GetChild(j).gameObject;
                    if(partes[i].transform.GetChild(j).gameObject.transform.childCount == 5)
                    {
                        textrespuesta.transform.GetChild(3).gameObject.SetActive(false);
                        textrespuesta.transform.GetChild(4).gameObject.SetActive(false);
                    }
                    if (partes[i].transform.GetChild(j).gameObject.transform.childCount == 6)
                    {
                        textrespuesta.transform.GetChild(4).gameObject.SetActive(false);
                        textrespuesta.transform.GetChild(5).gameObject.SetActive(false);
                    }
                    //textrespuesta.transform.GetChild(3).gameObject.SetActive(false);
                    //textrespuesta.transform.GetChild(4).gameObject.SetActive(false);
                }
            
            }
        }*/


    }        

    public void corregir(int parte)
    {
        print("parte:" + parte);
        int cant = partes[parte].transform.childCount;
        int j = 0;
        int k = 0;
        //partes[parte].transform.GetChild(i).name.Contains("InputField")
        //txtrespuestas
        for (int i =0; i <cant; i++)
        {
            
            //InputField inputField;
            //            txtrespuestas = partes[parte].transform.GetComponentInChildren<InputField>();
            if (partes[parte].transform.GetChild(i).name.Contains("InputField"))
            {
               

                //print(partes[parte].transform.GetChild(i).name);
                GameObject textrespuesta = partes[parte].transform.GetChild(i).gameObject;
                print("Texto ingresado:" + textrespuesta.GetComponent<InputField>().text);
                print("Comparar con respuesta:" + textrespuesta.transform.GetChild(3).GetComponent<Text>().text);
                //print("Texto ingresado:" + partes[parte].transform.GetChild(i).GetComponent<Text>().text );
                //print("Texto ingresado:" + partes[parte].transform.GetChild(i).GetComponent<Text>().text + "| Comparar con respuesta" + partes[parte].transform.GetChild(i).transform.GetChild(2).GetComponent<Text>().text);

                textrespuesta.transform.GetChild(4).gameObject.SetActive(textrespuesta.GetComponent<InputField>().text == textrespuesta.transform.GetChild(3).GetComponent<Text>().text);
                textrespuesta.transform.GetChild(5).gameObject.SetActive(textrespuesta.GetComponent<InputField>().text != textrespuesta.transform.GetChild(3).GetComponent<Text>().text);

                if (textrespuesta.GetComponent<InputField>().text == textrespuesta.transform.GetChild(3).GetComponent<Text>().text) k++;
                    /*if (textrespuesta.GetComponent<InputField>().text == textrespuesta.transform.GetChild(3).GetComponent<Text>().text)
                    {
                        textrespuesta.transform.GetChild(4).gameObject.SetActive(true);
                        textrespuesta.transform.GetChild(5).gameObject.SetActive(false);
                    }
                    else
                    {
                        textrespuesta.transform.GetChild(4).gameObject.SetActive(false);
                        textrespuesta.transform.GetChild(5).gameObject.SetActive(true);
                    }*/
                    j++;
            }
        }


        if (j == k)
        {
            print("FELICITACIONES!");
            felicitaciones.gameObject.SetActive(true);
            CerrarMaterial();
            
        }

        /*for(int i=0; i< txtrespuestas[parte].Length; i++)
        {
            print("Texto ingresado:"+ txtrespuestas[parte][i].GetComponent<Text>().text + "| Comparar con respuesta"+ txtrespuestas[parte][i].transform.GetChild(2).GetComponent<Text>().text );
        }*/
    }

    public void CerrarMaterial()
    {
        int idContenidoUltimoVistoCurso = PlayerPrefs.GetInt("varglobal_idContenidoUltimoVistoCurso");
        int id_materialactual = PlayerPrefs.GetInt("varglobal_idContenido") ;
        print("idContenidoUltimoVistoCurso:" + idContenidoUltimoVistoCurso + "-id_materialnuevo:" + id_materialactual);
        if (id_materialactual >= idContenidoUltimoVistoCurso)
        {
            print("ENTRAAAA A ACTUALIZAAAAAR");
            GameManager.Instance.actualizar_contenidoultimovistocurso(id_materialactual);
        }

        GameManager.Instance.CloseMaterial(2);
        //StartCoroutine(waitClose());
        DestroyImmediate(gameObject);
        //panelEjercicios.SetActive(false);
        
    }

    /*IEnumerator waitClose()
    {
        yield return new WaitForSeconds(1);
        DestroyImmediate(gameObject);
        panelEjercicios.SetActive(false);
    }*/
}
