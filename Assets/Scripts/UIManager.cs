using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;    
    [SerializeField] Text scoreText;
    [SerializeField] Text nombe_usuarioText;
    [SerializeField] Text timeText;
    [SerializeField] private Text labelDialog;
   
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    
    void Start()
    {
        //showUITime(false);
    }

    public void UpdateUIScore()
    {

        int puntaje =PlayerPrefs.GetInt("varglobal_puntaje");
        print("Ingresando puntaje:"+ puntaje);
        scoreText.text = puntaje.ToString();
    }

    public void UpdateUINombre(string nombe_usuario)
    {
        nombe_usuarioText.text = nombe_usuario;
    }

    public void UpdateUITime(int newTime)
    {
        timeText.text = newTime.ToString();
    }

    public void ChangeDialog(string texto)
    {
        labelDialog.text = texto;
    }

    /*public void showUITime(bool visible)
    {
        timeText.enabled = visible;
    }*/

    // Update is called once per frame
    void Update()
    {
        
    }
}
