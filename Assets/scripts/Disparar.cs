using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Disparar : MonoBehaviour
{
    [Header("Configuración del Arma")]
    public Camera camara;
    public int dano = 2;
    public float alcance = 100f;
    public float cadencia = 5f;
    public AudioClip sonidoDisparo;
    public GameObject muzzle;

    [Header("Sistema de Munición")]
    public int municionMaxima = 10;
    public float tiempoRecarga = 2f;
    public Text textoMunicion; // UI Text para mostrar la munición
    
    private int municionActual;
    private bool estaRecargando = false;

    private AudioSource fuente;
    private float proximo = 0f;

    void Start()
    {
        fuente = GetComponent<AudioSource>();
        if (fuente == null) fuente = gameObject.AddComponent<AudioSource>();
        
        if (muzzle != null) muzzle.SetActive(false);

        // Iniciar con el cargador lleno al principio del juego
        municionActual = municionMaxima;
        ActualizarTexto();
    }

    void OnEnable()
    {
        // Al volver a equipar esta arma
        estaRecargando = false; // Cancelar recarga pendiente si se cambió de arma
        if (muzzle != null) muzzle.SetActive(false);
        ActualizarTexto(); // Mostrar las balas de esta arma específica en la UI
    }

    void Update()
    {
        // Si está recargando, no se puede hacer nada más
        if (estaRecargando)
            return;

        // Recargar si presiona la tecla R o si no hay balas y se intenta disparar
        if (Input.GetKeyDown(KeyCode.R) || (municionActual <= 0 && Input.GetMouseButtonDown(0)))
        {
            if (municionActual < municionMaxima) // Solo recargar si le faltan balas
            {
                StartCoroutine(Recargar());
                return;
            }
        }

        // Disparar si hace clic, ya pasó la cadencia y tiene balas
        if (Input.GetMouseButtonDown(0) && Time.time >= proximo && municionActual > 0)
        {
           proximo = Time.time + cadencia;
           Disparo(); 
        }    
    }

    IEnumerator Recargar()
    {
        estaRecargando = true;
        
        if (textoMunicion != null) 
            textoMunicion.text = "Recargando...";
            
        Debug.Log("Recargando arma...");

        // Espera el tiempo de recarga
        yield return new WaitForSeconds(tiempoRecarga);

        municionActual = municionMaxima;
        estaRecargando = false;
        ActualizarTexto();
        Debug.Log("¡Arma recargada!");
    }

    void Disparo()
    {
        municionActual--; // Restar una bala
        ActualizarTexto();

        if (sonidoDisparo != null) fuente.PlayOneShot(sonidoDisparo);
        if (muzzle != null) 
        { 
            muzzle.SetActive(true);
            Invoke("ApagarMuzzle", 0.05f);
        }

        Ray ray = camara.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        if (Physics.Raycast(ray, out RaycastHit hit, alcance))
        {
            Vida v = hit.collider.GetComponentInParent<Vida>();
            if (v != null) v.RecibirDano(dano);
        }
    }

    void ActualizarTexto()
    {
        if (textoMunicion != null)
        {
            textoMunicion.text = "Balas: " + municionActual + " / " + municionMaxima;
        }
    }

    void ApagarMuzzle()
    {
        if (muzzle != null)
        {
            muzzle.SetActive(false);
        }
    }
}
