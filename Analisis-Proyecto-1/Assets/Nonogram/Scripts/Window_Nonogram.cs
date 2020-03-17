using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;
using CodeMonkey.Utils;
using CodeMonkey;

public class Window_Nonogram : MonoBehaviour
{

    private RectTransform nonogramContainer;
    [SerializeField] private TextBox textBox;
    [SerializeField] private ErrorMessage errorMessage;
    [SerializeField] private Sprite cVaciaSprite;
    [SerializeField] private Sprite cRellenaSprite;
    private TextWriter archivo;
    private TextReader lectorArchivo;
    private String nombreArchivo;
    private int X;
    private int Y;
    List<int[]> Filas = new List<int[]>();
    List<int[]> Columnas = new List<int[]>();

    int[,] matriz;

    private void Awake(){
        nonogramContainer = transform.Find("nonogramContainer").GetComponent<RectTransform>();
        //textBox = transform.Find("TextBox").GetComponent<TextBox>();
        //creartxt();
        //leertxt();
        //nonogram miNonogram = new nonogram(X, Y, nonogramContainer, cVaciaSprite, cRellenaSprite);
        //dibujarCuadriculas(new Vector2(200,200));
        //dibujarNonogram(24,38);//max actual(con valores anclados 24x38)
    }

    // Start is called before the first frame update
    void Start()
    {
        //llama al text box
        textBox.Show((string inputText) => {
            CMDebug.TextPopupMouse(inputText);
            nombreArchivo = inputText;
            //prueba para comprobar que se obtiene el dato correctamente
            Debug.Log(nombreArchivo + ".txt");
            leertxt(nombreArchivo + ".txt");
            matriz = new int[Filas.Count,Columnas.Count];
            rellenarMatriz();
            nonogram miNonogram = new nonogram(matriz, X, Y, nonogramContainer, cVaciaSprite, cRellenaSprite);
        });

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void rellenarMatriz(){
        for(int i = 0; i < Filas.Count; i++){
            rellenarFila(i, Filas[i]);
        }
    }

    private void rellenarFila(int pFila, int[] pCasillas){
        int porPintar = 0;
        int columna = 0;
        int contador = 0;
        int totalCols = Columnas.Count;
        if(casillasNecesarias(pCasillas) > totalCols){
            //terminar programa
            Debug.Log("No se puede resolver");
            errorMessage.Show();
            //System.Environment.Exit(0); esto crashea unity
        }else{
            for(int i = 0; i < pCasillas.Length; i++){
                porPintar = pCasillas[i];
                for(int j = 0; j < porPintar; j++){
                    matriz[pFila,columna] = 1;
                    columna++;
                }
                if(columna < totalCols){
                    matriz[pFila,columna] = 2;
                    columna++;
                }
            }
            while(columna < totalCols){
                matriz[pFila,columna] = 2;
                columna++;
            }
        }
    }

    private int casillasNecesarias(int[] pCasillas){
        int contador = 0;
        int totalPintadas = 0;
        while(contador < pCasillas.Length){
            totalPintadas += pCasillas[contador];
            contador++;
        }
        return totalPintadas+contador-1;

    }


    /*private void rellenarMatriz(){
        for(int i = 0; i < Filas.Count; i++){
            rellenarFila(0);
        }
        for(int j = 0; j < Columnas.Count; j++){
            rellenarColumna(j);
        }
    }

    rellenarFila(int pFila){
        int[] resFilaActual = Filas[pFila];
        List<int> casillasPintadas = new List<int>();
        for(int i = 0; i < Columnas.Count; i++){
            int contador = 0;
            if(matriz[pFila,i] == 1){
                while (matriz[pFila,i] == 1){
                    contador++;
                    i++;
                }
                casillasPintadas.Add(contador);
            }
        }
        if(casillasPintadas.Count > resFilaActual.Length){

        }
    }

    rellenarColumna(int pColumna){

    }*/

    /*private void rellenarMatriz(){
        //int[,] matriz = new int[Filas.Count,Columnas.Count];
        int[] filaActual;       //lista de restrcciones de la fila actual
        int[] columnaActual;    //lista de restrcciones de la columna actual
        for(int i = 0; i < Filas.Count; i++){
            filaActual = Filas[i];
            int restriccionActual;
            for(int j = 0; j < filaActual.Length; j++){
                int contador = 0;
                restriccionActual = filaActual[j];
                while(contador < restriccionActual){
                    rellenarColumna();
                }
            }
        }
    }

    private void rellenarColumna(){

    }*/

    /*private void rellenarMatriz(List<int[]> pFilas, List<int[]> pColumnas){
        int[] filaActual;
        int restriccionActual;
        int[,] matriz = new int[X,Y];
        for(int i = 0; i < pFilas.Count; i++){
            filaActual = pFilas[i];
            int contador = 0;
            while (contador < filaActual.Length)
            {
                restriccionActual = filaActual[contador];
                rellenarFila(matriz, i, restriccionActual);
            }
        }
    }

    private void rellenarFila(int [,] matriz, int pFila, int casillasPintadas){
        int contador = 0;
        while(matriz[pFila,contador] != 0){
            while (matriz[pFila,contador] == 1){
                contador++;
            }
            contador++;
            if(contador+casillasPintadas-1 >= matriz.Length(0)){
                Debug.Log("Imposible resolver");
            }
        }
        for (int i = 0; i < casillasPintadas; i++)
        {
            matriz[pFila,contador+i] = 1;
            rellenarColumna(matriz, pFila, contador+i);
        }

    }

    private void rellenarColumna(int [,] matriz, int pFila, int pColumna){
        int contador = 0;

        if(contador+casillasPintadas >= matriz.Length(1)){
                Debug.Log("Imposible resolver");
            }

    }*/

    private void creartxt(){
        //crear un archivo con el nombre y extencion asignados
        archivo = new StreamWriter("archivo.txt");

        string mensaje = "prueba";
        //escribir en un archivo
        archivo.WriteLine(mensaje);

        //cerrar un archivo
        archivo.Close();
    }

    private void leertxt(string pArchivo)
    {
        //abrir en modo solo lectura?
        try
        {
            lectorArchivo = new StreamReader(pArchivo);
            //escribir en la consola de Unity

            String linea = lectorArchivo.ReadLine();           //
            linea = linea.Replace(" ", "");                   // Lee y guarda
            int separador = linea.IndexOf(",");              // el largo y ancho
            X = int.Parse(linea.Substring(0, separador));    // del Nonogram
            Y = int.Parse(linea.Substring(separador + 1));     //
            Debug.Log("X: " + X + " Y: " + Y);

            bool FilaColumna = false;
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
                        int[] myInt = Array.ConvertAll(separadas, int.Parse);
                        if (FilaColumna)
                        {
                            Filas.Add(myInt);//Añadir a la lista de filas
                            //Debug.Log("Fila: " + separadas.Length);
                        }
                        else
                        {
                            Columnas.Add(myInt);//Añadir a la lista de columnas
                            //Debug.Log("Columnas: " + separadas.Length);
                        }
                    }
                }

            } while (linea != null);
        }
        catch
        {
            Debug.Log("ERROR");
        }

        imprimirFilasColumnas();
        //cerrar archivo
        lectorArchivo.Close();
    }

    private void imprimirFilasColumnas()
    {
        Debug.Log("Filas");
        for (int i = 0; i < Filas.Count; i++)
        {
            for (int j = 0; j < Filas[i].Length; j++)
            {
                Debug.Log("Fila " + (i + 1) + " " + Filas[i][j]);
            }
        }
        Debug.Log("Columnas");
        for (int i = 0; i < Columnas.Count; i++)
        {
            for (int j = 0; j < Columnas[i].Length; j++)
            {
                Debug.Log("Columna " + (i + 1) + " " + Columnas[i][j]);
            }
        }
    }
    /*private void dibujarCuadriculas(Vector2 filaColumna){
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
    }*/

    private class nonogram{

        private RectTransform nonogramContainer;
        private Sprite cVaciaSprite;
        private Sprite cRellenaSprite;
        private int[,] matriz;
        
        public nonogram(int[,] pMatriz, int filas, int columnas, RectTransform pContainer, Sprite pVacia, Sprite pRellena){
            this.nonogramContainer = pContainer;
            this.cVaciaSprite = pVacia;
            this.cRellenaSprite = pRellena;
            matriz = pMatriz;
            dibujarNonogram(filas,columnas);
        }

        private void dibujarCuadriculas(Vector2 filaColumna, bool pVacia){
            GameObject cuadricula = new GameObject("casilla",typeof(Image));
            cuadricula.transform.SetParent(nonogramContainer, false);
            if(pVacia){
                cuadricula.GetComponent<Image>().sprite = cVaciaSprite;    
            }else{
                cuadricula.GetComponent<Image>().sprite = cRellenaSprite;
            }
            RectTransform rectTransform = cuadricula.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = filaColumna;
            rectTransform.sizeDelta = new Vector2(25,25);
            rectTransform.anchorMin = new Vector2(0,0);
            rectTransform.anchorMax = new Vector2(0,0);
        }

        private void dibujarNonogram(int filas, int columnas){
            float nonogramHeight = nonogramContainer.sizeDelta.y;
            float yMaximun = 25f;
            float xSize = 25f;
            bool tipoCasilla;
            for(int i = 0; i < filas; i++){
                for (int j = 0; j < columnas; j++){
                    float yPosition = nonogramHeight-xSize - (i * xSize);
                    float xPosition = xSize + (j/yMaximun)*nonogramHeight;
                    tipoCasilla = matriz[i,j] != 1;
                    dibujarCuadriculas(new Vector2(xPosition,yPosition), tipoCasilla);
                }
            }
        }
    }
}
