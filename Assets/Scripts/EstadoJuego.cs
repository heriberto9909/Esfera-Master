using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EstadoJuego : MonoBehaviour {

    public int variable;
    public static EstadoJuego estadoJuego;

    private void Awake()
    {
        if (estadoJuego == null)
        {
            estadoJuego = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (estadoJuego = this)
        {
            Destroy(gameObject);
        }
        // DontDestroyOnLoad(gameObject);

    }

    public void start()
    {
        Seleccion variable1 = GetComponent<Seleccion>();
        variable = variable1.tipoJuego;
        Debug.Log("variable " + variable);
    }
}
