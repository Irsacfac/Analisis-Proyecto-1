using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;

public class Window_Nonogram : MonoBehaviour
{

    private RectTransform nonogramContainer;
    [SerializeField] private Sprite cVaciaSprite;
    [SerializeField] private Sprite cRellenaSprite;
    private TextWriter archivo;
    private TextReader lectorArchivo;
    private int X;
    private int Y;

    private void Awake(){
        //nonogramContainer = transform.Find("nonogramContainer").GetComponent<RectTransform>();
        creartxt();
        leertxt();
        //dibujarCuadriculas(new Vector2(200,200));
        //dibujarNonogram(24,38);//max actual(con valores anclados 24x38)
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
        lectorArchivo = new StreamReader("Nonogram1.txt");
        //escribir en la consola de Unity

        String linea=lectorArchivo.ReadLine();           //
        linea = linea.Replace(" ","");                   // Lee y guarda
        int separador = linea.IndexOf(",");              // el largo y ancho
        X = int.Parse(linea.Substring(0, separador));    // del Nonogram
        Y = int.Parse(linea.Substring(separador+1));     //
        Debug.Log("X: "+X+" Y: "+Y);

        bool FilaColumna=false;
        do
        {
            linea = lectorArchivo.ReadLine();
            if (linea != null)
            {
                if (linea.Equals("FILAS"))
                {
                    FilaColumna = true;
                }
                else if (linea.Equals("COLUMNAS"))
                {
                    FilaColumna = false;
                }
                else
                {
                    linea = linea.Replace(" ", "");
                    string[] separadas;
                    separadas = linea.Split(',');
                    if (FilaColumna)
                    {
                        //Añadir a la lista de filas
                        Debug.Log("Fila: " + separadas.Length);
                    }
                    else
                    {
                        //Añadir a la lista de columnas
                        Debug.Log("Columnas: " + separadas.Length);
                    }
                }
            }
            
        } while (linea != null);

        /*Debug.Log(lectorArchivo.ReadLine());//lee una linea linea
        Debug.Log(lectorArchivo.ReadLine());//cada vez que se llama se lee la linea siguiente
        Debug.Log(lectorArchivo.ReadToEnd());//lee lodo el archivo, si se leyeron lineas con anterioridad empezara en la linea siguiente a la ultima leida
        //si no hay nada escrito, o no resta nada por leer, retorna "" o vacio. No lo se bien*/
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
