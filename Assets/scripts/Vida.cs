using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Vida : MonoBehaviour
{
    [Header("Configuración de Vida")]
    public int vidaMax = 3;
    public bool esJugador = false;
    
    [Header("UI del Jugador")]
    public Text textoVida; // Arrastrar el texto de vida aquí
    public GameObject panelGameOver; 

    [Header("Feedback de Daño (Solo Jugador)")]
    public Image imagenDano; 
    public Color colorDano = new Color(1f, 0f, 0f, 0.5f); 
    public float velocidadDesvanecimiento = 5f;

    [Header("Sonidos")]
    public AudioClip sonidoRecibirDano;
    private AudioSource fuenteAudio;

    private int vidaActual;

    void Start()
    {
        vidaActual = vidaMax;
        ActualizarTextoVida();
        
        Time.timeScale = 1f; 
        
        if (panelGameOver != null)
        {
            panelGameOver.SetActive(false);
        }

        if (imagenDano != null)
        {
            imagenDano.color = Color.clear;
        }

        fuenteAudio = GetComponent<AudioSource>();
        if (fuenteAudio == null) fuenteAudio = gameObject.AddComponent<AudioSource>();
    }

    void Update()
    {
        if (imagenDano != null && imagenDano.color != Color.clear)
        {
            imagenDano.color = Color.Lerp(imagenDano.color, Color.clear, velocidadDesvanecimiento * Time.deltaTime);
        }
    }

    public void RecibirDano(int cantidad)
    {
        vidaActual -= cantidad;
        ActualizarTextoVida();
        
        if (sonidoRecibirDano != null && fuenteAudio != null)
        {
            fuenteAudio.PlayOneShot(sonidoRecibirDano);
        }
        
        if (esJugador && imagenDano != null && vidaActual > 0)
        {
            imagenDano.color = colorDano;
        }

        if (vidaActual <= 0) Morir();
    }

    public void Curar(int cantidad)
    {
        vidaActual += cantidad;
        if (vidaActual > vidaMax)
        {
            vidaActual = vidaMax;
        }
        ActualizarTextoVida();
    }

    void Morir()
    {
        if (esJugador)
        {
            if (panelGameOver != null)
            {
                panelGameOver.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                Time.timeScale = 0f;
            }
            else
            {
                Reintentar();
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Reintentar()
    {
        Time.timeScale = 1f; 
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); 
    }

    public int VidaActual()
    {
        return vidaActual;
    }

    void ActualizarTextoVida()
    {
        // Solo actualizamos el texto si este objeto es el jugador y le pusimos un Texto
        if (esJugador && textoVida != null)
        {
            textoVida.text = "Vida: " + vidaActual + " / " + vidaMax;
        }
    }
}
