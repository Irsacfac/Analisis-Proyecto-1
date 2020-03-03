using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Window_Nonogram : MonoBehaviour
{

    private RectTransform nonogramContainer;
    private TextWriter archivo;

    private void Awake(){
        nonogramContainer = transform.Find("nonogramContainer").GetComponent<RectTransform>();
        creartxt();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void creartxt(){
        //crear un archivo con el nombre y extencion asignados
        archivo = new StreamWriter("archivo.txt");

        string mensaje = "prueba";
        //escribir en un archivo
        archivo.WriteLine(mensaje);

        //cerrar un archivo
        archivo.Close();
    }
}
