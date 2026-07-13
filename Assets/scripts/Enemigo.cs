using UnityEngine;

public class Enemigo : MonoBehaviour
{
    [Header("Ataque")]
    public int dano = 1;
    public float alcanceDeDisparo = 15f;
    public float cadencia = 2f; // Dispara cada 2 segundos
    public AudioClip sonidoDisparo;
    
    private float proximoDisparo = 0f;
    private Transform jugador;
    private AudioSource fuente;

    void Start()
    {
        // Buscar al jugador en la escena
        PrimeraPersona p = FindObjectOfType<PrimeraPersona>();
        if (p != null) jugador = p.transform;

        // Obtener o agregar componente de audio
        fuente = GetComponent<AudioSource>();
        if (fuente == null) fuente = gameObject.AddComponent<AudioSource>();
    }

    void Update()
    {
        if (jugador != null)
        {
            // Mirar hacia el jugador (opcional, pero útil para que rote apuntando)
            transform.LookAt(new Vector3(jugador.position.x, transform.position.y, jugador.position.z));

            float distancia = Vector3.Distance(transform.position, jugador.position);
            
            // Si está dentro del alcance y ya pasó el tiempo para disparar
            if (distancia <= alcanceDeDisparo && Time.time >= proximoDisparo)
            {
                proximoDisparo = Time.time + cadencia;
                DispararAlJugador();
            }
        }
    }

    void DispararAlJugador()
    {
        if (sonidoDisparo != null) fuente.PlayOneShot(sonidoDisparo);

        // Apuntamos directo a la posición del jugador
        Vector3 origen = transform.position + Vector3.up * 1f; 
        Vector3 destino = jugador.position; 
        Vector3 direccion = (destino - origen).normalized;

        RaycastHit[] impactos = Physics.RaycastAll(origen, direccion, alcanceDeDisparo);
        bool golpeoJugador = false;

        foreach (RaycastHit hit in impactos)
        {
            // Buscamos el componente Vida en el objeto que golpeó, en sus padres o en sus hijos
            Vida v = hit.collider.GetComponentInParent<Vida>();
            if (v == null) v = hit.collider.GetComponentInChildren<Vida>();

            if (v != null && v.esJugador)
            {
                v.RecibirDano(dano);
                Debug.Log("¡El enemigo te ha disparado y te quitó " + dano + " de vida!");
                golpeoJugador = true;
                break; 
            }
        }

        if (!golpeoJugador)
        {
            Debug.Log("El enemigo disparó pero el rayo pasó por encima o no encontró el componente Vida en el jugador.");
        }
    }
}
