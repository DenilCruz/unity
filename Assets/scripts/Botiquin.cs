using UnityEngine;

public class Botiquin : MonoBehaviour
{
    [Header("Configuración del Botiquín")]
    public int cantidadCura = 1; // Cuánta vida recupera
    public AudioClip sonidoRecoger; // Sonido al curarse
    
    // Se activa cuando algo entra en el "Trigger" del botiquín
    void OnTriggerEnter(Collider other)
    {
        // Revisamos si fue el jugador quien tocó el botiquín
        if (other.GetComponent<PrimeraPersona>() != null)
        {
            Vida vidaJugador = other.GetComponent<Vida>();
            
            // Si el jugador tiene el script Vida y no tiene la vida al máximo
            if (vidaJugador != null && vidaJugador.VidaActual() < vidaJugador.vidaMax)
            {
                // Curamos al jugador
                vidaJugador.Curar(cantidadCura);
                
                // Reproducimos el sonido de agarrar el botiquín
                if (sonidoRecoger != null)
                {
                    // Usamos PlayClipAtPoint para que el sonido suene aunque el botiquín se destruya
                    AudioSource.PlayClipAtPoint(sonidoRecoger, transform.position);
                }
                
                Debug.Log("¡Botiquín recogido! Vida actual: " + vidaJugador.VidaActual());
                
                // Destruimos el objeto botiquín para que desaparezca
                Destroy(gameObject);
            }
            else
            {
                Debug.Log("El jugador ya tiene la vida al máximo, no puede recoger el botiquín.");
            }
        }
    }
}
