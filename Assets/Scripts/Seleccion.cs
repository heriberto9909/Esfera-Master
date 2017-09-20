using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Seleccion : MonoBehaviour {

    public int tipoJuego;

    public static Seleccion seleccion;

    /*private void Awake()
    {
        if (seleccion == null)
        {
            seleccion = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (seleccion = this)
        {
            Destroy(gameObject);
        }
        // DontDestroyOnLoad(gameObject);

    }*/

    public void Button()
    {
        tipoJuego = 2;
        SceneManager.LoadScene("EsferaSC");
    }

    public void Button2()
    {
        tipoJuego = 0;
        SceneManager.LoadScene("EsferaZS");
    }

    public void Button3()
    {
        tipoJuego = 1;
        SceneManager.LoadScene("EsferaCZ");
    }

    public void Button4()
    {
        tipoJuego = 0;
        SceneManager.LoadScene("EsferaMixta");
    }
}
