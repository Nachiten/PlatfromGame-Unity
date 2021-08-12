﻿using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class ManejarMenu : MonoBehaviour
{
    #region Variables

    // Flag de ya asigne las variables
    static bool variablesAsignadas = false;

    // GameObjects
    static GameObject menu, opciones, creditos, panelMenu;

    // Manager de las animaciones
    static LeanTweenManager tweenManager;

    // Textos varios
    static TMP_Text textoBoton;

    // Flags varios
    bool menuActivo = false, opcionesActivas = false, creditosActivos = false;

    // Strings utilizados
    string continuar = "CONTINUAR", comenzar = "COMENZAR";

    // Index de escena actual
    int index;

    #endregion

    /* -------------------------------------------------------------------------------- */

    #region FuncionStart

    private void Awake()
    {
        if (variablesAsignadas)
            return;

        try
        {
            menu = GameObject.Find("Menu");
            panelMenu = GameObject.Find("PanelMenu");
            opciones = GameObject.Find("MenuOpciones");
            creditos = GameObject.Find("MenuCreditos");

            textoBoton = GameObject.Find("TextoBotonComenzar").GetComponent<TMP_Text>();

            tweenManager = GameObject.Find("Canvas Menu").GetComponent<LeanTweenManager>();

            if (menu == null) {
                throw new Exception("menu");
            }
            if (panelMenu == null)
            {
                throw new Exception("panelMenu");
            }
            if (opciones == null)
            {
                throw new Exception("opciones");
            }
            if (creditos == null)
            {
                throw new Exception("creditos");
            }
            if (textoBoton == null)
            {
                throw new Exception("textoBoton");
            }
            if (tweenManager == null)
            {
                throw new Exception("tweenManager");
            }

            variablesAsignadas = true;
        }
        catch (Exception e)
        {
            Debug.LogError("[ManejarMenu] Error al asignar variable: " + e.Message);
        }
    }

    /* -------------------------------------------------------------------------------- */

    void Start()
    {
        index = SceneManager.GetActiveScene().buildIndex;

        // No estoy en el menu principal
        if (index != 0)
        {
            textoBoton.text = continuar;

            // Oculto todo de una patada pq se esta mostrando la pantalla de carga
            menu.SetActive(false);
            panelMenu.SetActive(false);
        }
        // Estoy en el menu principal
        else
        {
            textoBoton.text = comenzar;

            menu.SetActive(true);
            menuActivo = true;
        }

        opciones.SetActive(false);
        creditos.SetActive(false);
    }

    #endregion

    /* -------------------------------------------------------------------------------- */

    #region FuncionUpdate

    void Update()
    {
        index = SceneManager.GetActiveScene().buildIndex;

        if (index == 0) return;

        bool animacionEnEjecucion = GameObject.Find("Canvas Menu").GetComponent<LeanTweenManager>().animacionEnEjecucion;

        if (Input.GetKeyDown("escape") && !animacionEnEjecucion)
            manejarMenu();
    }

    #endregion

    /* -------------------------------------------------------------------------------- */

    public void manejarMenu() 
    {
        menuActivo = !menuActivo;

        if (menuActivo)
        {
            //Debug.Log("[ManejarMenu] Abriendo menu.");
            menu.SetActive(true);
            tweenManager.abrirMenu();
        }
        else 
        {
            //Debug.Log("[ManejarMenu] Cerrando menu.");
            tweenManager.cerrarMenu();
        }

        if (opcionesActivas) 
        {
            tweenManager.cerrarOpciones();
            opcionesActivas = false;
        }
    }

    /* -------------------------------------------------------------------------------- */

    public void manejarOpciones()
    {
        opcionesActivas = !opcionesActivas;

        if (opcionesActivas) 
        { 
            tweenManager.abrirOpciones();
        }
        else
        { 
            tweenManager.cerrarOpciones();
        }
    }

    /* -------------------------------------------------------------------------------- */
    public void manejarCreditos() 
    {
        creditosActivos = !creditosActivos;

        if (creditosActivos)
            tweenManager.abrirCreditos();
        
        else
            tweenManager.cerrarCreditos();
        
    }
}
