using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class Pregunta
{
    public string informacionComplementaria;
}
public class PanelComplementario : MonoBehaviour
{
    //se declaran las propiedades
    public GameObject panelInformacionComplementaria;
    public Text informacionComplementaria;
    public Game game;
    private int nivelPregunta;
    private int preguntaAlAzar;

    public void Continuar()
    {
        panelInformacionComplementaria.SetActive(false);
        game.SelecionarPregunta();
    }
    public void Desplegar()
    {
        nivelPregunta = game.nivelPregunta;
        nivelPregunta--;
        preguntaAlAzar = game.preguntaAlAzar;
        panelInformacionComplementaria.SetActive(true);
        // Cargar la informacion del banco de preguntas con
        // la información complementaria
        informacionComplementaria.text = game.bancoDePreguntas[nivelPregunta].preguntas[preguntaAlAzar].informacionComplementaria;
    }
} 