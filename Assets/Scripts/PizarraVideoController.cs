using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Video;

public class PizarraVideoController : MonoBehaviour
{
    [SerializeField] private VideoPlayer video;
    private RectTransform _rectransform;
    void Awake()
    {
        _rectransform = GetComponent<RectTransform>();

    }
    // Start is called before the first frame update
    void Start()
    {
        LoadVideo();
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

    private void LoadVideo()
    {
        int idcontenido = PlayerPrefs.GetInt("varglobal_idContenido");
        //string temp = Application.dataPath + "/Sprites/Clases/Contenido/"+idcontenido+"/video.mp4";
        string relativePath = "https://virtual.espinosa.edu.pe/proyecto/espinosa3d/Contenido/" + idcontenido + "/video.mp4";

        print("temp_contenido_videoclases:"+ relativePath);
        //if (video.url == temp) return;
        video.url = relativePath;
        video.Prepare();
        video.Play();
        video.loopPointReached += CheckOver;
        
    }

    void CheckOver(VideoPlayer vp)
    {
        Debug.Log("Acumular puntos");
        CerrarVideo();
        //GameManager.Instance.CloseVideoClases();
    }

    public void CerrarVideo()
    {
        int idContenidoUltimoVistoCurso = PlayerPrefs.GetInt("varglobal_idContenidoUltimoVistoCurso");
        int id_contenidoactual = PlayerPrefs.GetInt("varglobal_idContenido");
        print("idContenidoUltimoVistoCurso:"+ idContenidoUltimoVistoCurso+"-id_cursoactual:"+ id_contenidoactual);
        if (id_contenidoactual >= idContenidoUltimoVistoCurso)
        {
            print("ENTRAAAA A ACTUALIZAAAAAR");
            GameManager.Instance.actualizar_contenidoultimovistocurso(id_contenidoactual);
        }
        
        
        DestroyImmediate(gameObject);
    }
}
