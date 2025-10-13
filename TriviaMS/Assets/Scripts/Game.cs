using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    public Dificultad[] bancoDePreguntas;
    public Text enunciado;
    public Text[] respuesta;
    public int nivelPregunta;
    public int preguntaAlAzar;
    public PanelComplementario panelComplementario; 
    public Button[] btn_respuesta;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public TextAsset jsonDesdeResources;   
    public string jsonFileName = "Preguntas"; 

    void Start()
    {
        // Cargar desde Resources
        CargarBancoDesdeResources(jsonFileName);

        // (Importante) Validar que hay datos para evitar IndexOutOfRange
        if (bancoDePreguntas == null || bancoDePreguntas.Length == 0)
        {
            Debug.LogError("Banco de preguntas vacío. Revisa carga del JSON.");
            return;
        }

        nivelPregunta = 0;
        SelecionarPregunta();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SelecionarPregunta()
    {
        // se elige un indice del arreglo al azar
        preguntaAlAzar = Random.Range(0, bancoDePreguntas[nivelPregunta].preguntas.Length);
        // sacamos el texto del banco de preguntas y
        // lo ponemos en el UI donde se despliega el enunciado.
        enunciado.text = bancoDePreguntas[nivelPregunta].preguntas[preguntaAlAzar].enunciado;
        // cargar los textos de cada boton del UI
        for (int i = 0; i < respuesta.Length; i++)
        {
            respuesta[i].text = bancoDePreguntas[nivelPregunta].preguntas[preguntaAlAzar].respuestas[i].texto;
        }
        //string json = JsonUtility.ToJson(bancoDePreguntas);
        //Debug.Log(json);
    }

    public bool EvaluarPregunta(int respuestaJugador)
    {
        if (respuestaJugador == bancoDePreguntas[nivelPregunta].preguntas[preguntaAlAzar].respuestaCorrecta)
        {
            // reinicio del problema con mayor dificultad
            nivelPregunta++;
            if (nivelPregunta == bancoDePreguntas.Length)
            {
                // desplegar la pantalla de fin de juego ganado!
                SceneManager.LoadScene("Ganar");
            }
            else
            {
                //Desplegar el panel de información complementaria
                //ante una respuesta correcta
                try
                {
                    panelComplementario.Desplegar();
                }
                catch (System.Exception ex)
                {
                    Debug.LogError("Se te olvido configurar el panel de información complementaria: " + ex.Message);
                }
                HabilitarRespuestas();
            }
            return true;
        }
        else
        {
            SceneManager.LoadScene("Perder");
            return false;
        }
    }

    public void HabilitarRespuestas()
    {
        for (int i = 0; i < respuesta.Length; i++)
        {
            try
            {
                btn_respuesta[i].gameObject.SetActive(true);
            }
            catch (System.Exception ex)
            {
                Debug.LogError("Falta configurar los botones");
            }
        }
    }

    public void Respuesta(int respuestaJugador)
    {
        //Microdelay para el sonido
        StartCoroutine(RespuestaConSfxDelay(respuestaJugador));
    }

    private System.Collections.IEnumerator RespuestaConSfxDelay(int r)
    {
        // microdelay para que el clic suene "antes" del cambio de UI
        yield return new WaitForSecondsRealtime(0.05f);
        EvaluarPregunta(r);
    }

    public int PreguntaActual()
    {
        return preguntaAlAzar;
    }

    // ---- Carga desde Resources ----
    void CargarBancoDesdeResources(string nombreSinExtension)
    {
        TextAsset json = jsonDesdeResources != null ? jsonDesdeResources
                                                    : Resources.Load<TextAsset>(nombreSinExtension);
        if (json == null)
        {
            Debug.LogError("No se encontró el JSON en Resources: " + nombreSinExtension);
            return;
        }

        var wrapper = JsonUtility.FromJson<BancoDePreguntas>(json.text);
        if (wrapper == null || wrapper.banco == null)
        {
            Debug.LogError("Formato JSON inválido o wrapper vacío.");
            return;
        }

        bancoDePreguntas = wrapper.banco;
    }
}
