using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class Window_Nonogram : MonoBehaviour
{

    private RectTransform nonogramContainer;
    [SerializeField] private Sprite cVaciaSprite;
    [SerializeField] private Sprite cRellenaSprite;
    private TextWriter archivo;
    private TextReader lectorArchivo;

    private void Awake(){
        nonogramContainer = transform.Find("nonogramContainer").GetComponent<RectTransform>();
        //creartxt();
        leertxt();
        //dibujarCuadriculas(new Vector2(200,200));
        dibujarNonogram(24,38);//max actual(con valores anclados 24x38)
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

    private void dibujarCuadriculas(Vector2 filaColumna){
        GameObject cuadriculaVacia = new GameObject("cVacia",typeof(Image));
        cuadriculaVacia.transform.SetParent(nonogramContainer, false);
        cuadriculaVacia.GetComponent<Image>().sprite = cVaciaSprite;
        RectTransform rectTransform = cuadriculaVacia.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = filaColumna;
        rectTransform.sizeDelta = new Vector2(25,25);
        rectTransform.anchorMin = new Vector2(0,0);
        rectTransform.anchorMax = new Vector2(0,0);
    }

    private void dibujarNonogram(int filas, int columnas){
        float nonogramHeight = nonogramContainer.sizeDelta.y;
        float yMaximun = 25f;
        float xSize = 25f;
        for(int i = 0; i < columnas; i++){
            for (int j = 0; j < filas; j++){
                float xPosition = xSize + (i * xSize);
                float yPosition = xSize + (j/yMaximun)*nonogramHeight;
                dibujarCuadriculas(new Vector2(xPosition,yPosition));
            }
        }
    }
}
