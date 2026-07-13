using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class Meta : MonoBehaviour
{
    [Header("UI y Victoria")]
    public Text textoEnemigos; 
    public string nombreSiguienteNivel; 
    
    [Header("Sonido")]
    public AudioClip sonidoVictoria;
    private AudioSource fuenteAudio;

    private int cantidadEnemigos;
    private bool yaGano = false;

    void Start()
    {
        fuenteAudio = GetComponent<AudioSource>();
        if (fuenteAudio == null) fuenteAudio = gameObject.AddComponent<AudioSource>();
    }

    void Update()
    {
        // Cuenta todos los objetos en la escena que tengan el script "Enemigo"
        cantidadEnemigos = FindObjectsOfType<Enemigo>().Length;

        // Actualiza el texto en la pantalla
        if (textoEnemigos != null)
        {
            textoEnemigos.text = "Enemigos restantes: " + cantidadEnemigos;
        }
    }

    // Se activa cuando el jugador entra en la zona de la Meta
    void OnTriggerEnter(Collider other)
    {
        // Comprobamos si el que tocó la Meta fue el Jugador
        if (other.GetComponent<PrimeraPersona>() != null)
        {
            if (cantidadEnemigos <= 0 && !yaGano)
            {
                yaGano = true; // Para que no se active varias veces
                Debug.Log("¡Victoria! Has eliminado a todos y llegaste a la meta.");
                
                if (sonidoVictoria != null) 
                    fuenteAudio.PlayOneShot(sonidoVictoria);
                
                // Iniciamos la corrutina para esperar a que termine el sonido
                StartCoroutine(EsperarYCambiarNivel());
            }
            else if (!yaGano)
            {
                Debug.Log("Aún no puedes ganar. Te faltan eliminar " + cantidadEnemigos + " enemigos.");
            }
        }
    }

    IEnumerator EsperarYCambiarNivel()
    {
        // Esperamos el tiempo que dura el sonido (o 2 segundos si no hay sonido)
        float espera = sonidoVictoria != null ? sonidoVictoria.length : 2f;
        yield return new WaitForSeconds(espera);

        // Cargar la siguiente escena (o reiniciar si no hay nombre)
        if (string.IsNullOrEmpty(nombreSiguienteNivel))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else
        {
            SceneManager.LoadScene(nombreSiguienteNivel);
        }
    }
}
