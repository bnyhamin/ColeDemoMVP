using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class ControlAcceso : MonoBehaviour
{
    public static ControlAcceso singleton;
    public InputField txtUsuario;
    public InputField txtClave;
    public InputField txtUsuario1;
    public InputField txtClave1;
    public InputField txtCorreo;
    public InputField txtDni;
    [SerializeField] private int idUsuario;
    [SerializeField] private string nombreCompleto;
    [SerializeField] private string nombreUsuario;
    [SerializeField] private string correo;
    [SerializeField] private string dni;
    [SerializeField] private int puntaje;
    [SerializeField] private int idPersonaje;
    [SerializeField] private int fg_permiso;
    [SerializeField] private int estado;
    [SerializeField] private int grado;
    [SerializeField] private string nivel;
    [SerializeField] private string contenidos_actuales;
    [SerializeField] private GameObject panelPago;
    [SerializeField] private GameObject panelExpulsion;

    public bool sesionIniciada = false;
    [SerializeField] GameObject panelLogin;
    [SerializeField] GameObject panelRegistrar;
    [SerializeField] Text textError;

   
    // Start is called before the first frame update
    void Start()
    {

    }

    public void iniciarSesion()
    {
        StartCoroutine(Login());

    }

    public void RegistrarUsuario()
    {
        print("Entra a corrutina a registrar usuario");
        StartCoroutine(Registrar());

    }

    public void Registrarpago()
    {
        StartCoroutine(Registrar_pago());
    }

    public void BotonPanelActive(string namePanel)
    {

        Debug.Log(namePanel);
        panelLogin.SetActive(namePanel == "PanelLogin");
        panelRegistrar.SetActive(namePanel == "PanelRegistrar");

    }

    public void cerrarPanel(string name)
    {
        if (name == "panelPago") panelRegistrar.SetActive(false);
        if (name == "panelExpulsion") panelExpulsion.SetActive(false);
    }



    IEnumerator Login()
    {
        WWW conneccion = new WWW("https://virtual.espinosa.edu.pe/proyecto/espinosa3d/login.php?usuario=" + txtUsuario.text + "&clave=" + txtClave.text);
        yield return (conneccion);
        if (conneccion.text == "200")
        {
            print("El usuario si existe");
            StartCoroutine(datos());
        }
        else if (conneccion.text == "401")
        {
            print("Usuario o contrase�a incorrecta");
            //textError.text = "Usuario o contrase�a incorrecta";
            MostrarError("Usuario o contrase�a incorrecta");
        }
        else
        {
            print("Error en la conecci�n con la base de datos");
            //textError.text = "Error en la conecci�n con la base de datos";
            MostrarError("Error en la conecci�n con la base de datos");
        }
    }

    IEnumerator datos()
    {
        WWW conneccion = new WWW("https://virtual.espinosa.edu.pe/proyecto/espinosa3d/datos.php?usuario=" + txtUsuario.text);
        yield return (conneccion);
        if (conneccion.text == "401")
        {
            print("Usuario incorrecto");
            //textError.text = "Usuario incorrecto";
            MostrarError("Usuario incorrecto");
        }
        else
        {
            print(conneccion.text);
            string[] nDatos = conneccion.text.Split("|");
            if (nDatos.Length != 11)
            {
                print("Error en la conecci�n");
                //textError.text = "Error en la conecci�n";
                MostrarError("Error en la conecci�n");
            }
            else
            {

                idUsuario = int.Parse(nDatos[0]);
                nombreCompleto = nDatos[1];
                nombreUsuario = nDatos[2];
                correo = nDatos[3];
                puntaje = int.Parse(nDatos[4]);
                idPersonaje = int.Parse(nDatos[5]);
                fg_permiso = int.Parse(nDatos[6]);
                estado = int.Parse(nDatos[7]);
                grado = int.Parse(nDatos[8]);
                nivel = nDatos[9];
                contenidos_actuales = nDatos[10];
                print("idPersonaje:" + idPersonaje);
                sesionIniciada = true;
                PlayerPrefs.SetInt("varglobal_idUsuario", idUsuario);
                PlayerPrefs.SetString("varglobal_nombreCompleto", nombreCompleto); 
                PlayerPrefs.SetString("varglobal_nombreUsuario", nombreUsuario);
                PlayerPrefs.SetInt("varglobal_puntaje", puntaje);
                PlayerPrefs.SetString("varglobal_correo", correo);
                PlayerPrefs.SetInt("varglobal_idPersonaje", idPersonaje);
                PlayerPrefs.SetInt("varglobal_grado", grado);
                PlayerPrefs.SetString("varglobal_nivel", nivel);
                PlayerPrefs.SetString("varglobal_contenidos_actuales", contenidos_actuales);
                
                if (fg_permiso == 1 && estado == 1)
                {

                    SceneManager.LoadScene("SceneGame");
                    print("Entro a SceneGame");
                }
                else
                {
                    if (fg_permiso == 0)
                    {
                        print("No tiene permiso, debe realizar pago");
                        panelPago.SetActive(true);
                        //textError.text = "No tiene permiso, debe realizar pago";
                        MostrarError("No tiene permiso, debe realizar pago");
                    }

                    if (estado == 0)
                    {
                        print("No tiene permiso, no tiene permiso para entrar");
                        panelExpulsion.SetActive(true);
                        //textError.text = "No tiene permiso, no tiene permiso para entrar";
                        MostrarError("No tiene permiso, no tiene permiso para entrar");
                    }


                }


            }
        }

    }

    IEnumerator Registrar()
    {
        print("Entra a registrar");
        WWW conneccion = new WWW("https://virtual.espinosa.edu.pe/proyecto/espinosa3d/registro.php?usuario=" + txtUsuario1.text + "&correo=" + txtCorreo.text + "&dni=" + txtDni.text + "&clave=" + txtClave1.text);
        yield return (conneccion);
        print("Dato de conneccion:" + conneccion);
        if (conneccion.text == "999")
        {
            print("Correo incorrecto");
            //textError.text = "Usuario incorrecto";
            MostrarError("Ingrese Correo incorrecto");
        } else if (conneccion.text == "402")
        {
            print("Usuario ya existe");
            //textError.text = "Usuario ya existe";
            MostrarError("Usuario ya existe");
        }
        else if (conneccion.text == "401")
        {
            Debug.LogError("Error en la conecci�n con la base de datos");
            panelPago.SetActive(true);
            //textError.text = "Error en la conecci�n con la base de datos";
            MostrarError("Error en la conecci�n con la base de datos");
        }
        else
        {//201
            if (conneccion.text.Contains("|"))
            {
                string[] nDatos = conneccion.text.Split("|");
                if (nDatos[0] == "201")
                {
                    print("registr� correctamente [conneccion.text]:" + nDatos[1]);

                    idUsuario = int.Parse(nDatos[1]);
                    PlayerPrefs.SetInt("varglobal_idUsuario", idUsuario);
                    PlayerPrefs.SetString("varglobal_nombreUsuario", txtUsuario1.text);
                    PlayerPrefs.SetInt("varglobal_puntaje", 0);
                    PlayerPrefs.SetString("varglobal_correo", txtCorreo.text);
                    PlayerPrefs.SetInt("varglobal_idPersonaje", 0);

                    txtUsuario1.text = string.Empty;
                    txtCorreo.text = string.Empty;
                    txtDni.text = string.Empty;
                    txtClave1.text = string.Empty;
                    nombreUsuario = txtUsuario.text;
                    puntaje = int.Parse("0");
                    sesionIniciada = true;

                    SceneManager.LoadScene("SceneGame");
                }
            }
            else
            {
                MostrarError("No obtiene resultado esperado " + conneccion.text);
            }




        }
    }


    IEnumerator Registrar_pago()
    {
        print("idusuario:" + idUsuario);
        WWW conneccion = new WWW("https://virtual.espinosa.edu.pe/proyecto/espinosa3d/registra_pago.php?id_usuario=" + idUsuario);
        yield return (conneccion);
        if (conneccion.text == "201")
        {

            print("pag� correctamente");

            panelPago.SetActive(false);
            SceneManager.LoadScene("SceneGame");
        }
        else
        {
            Debug.LogError("Error en la conecci�n con la base de datos");
            panelPago.SetActive(true);
            MostrarError("Error en la conecci�n con la base de datos");
        }

    }

    // Update is called once per frame
    void Update()
    {

    }

    void MostrarError(string mensaje)
    {
        print("Entra mensaje 1:" + mensaje);

        StartCoroutine(Mostrar_Error(mensaje));
    }

    IEnumerator Mostrar_Error(string mensaje)
    {
        print("Entra mensaje 2:" + mensaje);
        textError.text = mensaje;
        yield return new WaitForSeconds(3f);
        textError.text = "";

    }



    public void GetCursos(int grado, string nivel)
    {
        StartCoroutine(cursos(grado, nivel));
    }



    IEnumerator cursos(int grado, string nivel)
    {

        WWW conneccion = new WWW("https://virtual.espinosa.edu.pe/proyecto/espinosa3d/cursos.php?grado=" + grado + "&nivel="+nivel);
        yield return (conneccion);
        if (conneccion.text == "401")
        {
            print("Grado y nivel incorrectos");
            //textError.text = "Usuario incorrecto";
            MostrarError("Usuario incorrecto");
        }
        else
        {
            print(conneccion.text);
            /*string[] nDatos = conneccion.text.Split("-");
            for(int i=0;i< nDatos.Length ; i++)
            {
                print(nDatos[i]);
                //GameManager.Instance.cursos[i] = nDatos[i];
            }*/
            

        }

    }


}
