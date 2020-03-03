using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Window_Nonogram : MonoBehaviour
{

    private RectTransform nonogramContainer;
    private TextWriter archivo;
    private TextReader lectorArchivo;

    private void Awake(){
        nonogramContainer = transform.Find("nonogramContainer").GetComponent<RectTransform>();
        //creartxt();
        leertxt();
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

    private void leertxt(){
        //abrir en modo solo lectura?
        lectorArchivo = new StreamReader("archivo.txt");
        //escribir en la consola de Unity
        Debug.Log(lectorArchivo.ReadLine());//lee una linea linea
        Debug.Log(lectorArchivo.ReadLine());//cada vez que se llama se lee la linea siguiente
        Debug.Log(lectorArchivo.ReadToEnd());//lee lodo el archivo, si se leyeron lineas con anterioridad empezara en la linea siguiente a la ultima leida
        //si no hay nada escrito, o no resta nada por leer, retorna "" o vacio. No lo se bien
        //cerrar archivo
        lectorArchivo.Close();
    }
}
