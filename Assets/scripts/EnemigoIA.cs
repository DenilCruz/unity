using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemigoIA : MonoBehaviour
{
    [Tooltip("El objetivo al que el enemigo va a perseguir. Si está vacío, buscará al jugador automáticamente.")]
    public Transform objetivo;
    
    private NavMeshAgent navMeshAgent;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();

        // Si no se asignó el objetivo desde el Inspector, intenta encontrar al jugador en la escena
        if (objetivo == null)
        {
            // Busca al jugador por su script PrimeraPersona
            PrimeraPersona jugador = FindObjectOfType<PrimeraPersona>();
            if (jugador != null)
            {
                objetivo = jugador.transform;
            }
            else
            {
                Debug.LogWarning("EnemigoIA: No se encontró al jugador en la escena.");
            }
        }
    }

    void Update()
    {
        // Si hay un objetivo definido, actualizamos el destino del NavMeshAgent para que lo persiga
        if (objetivo != null)
        {
            navMeshAgent.SetDestination(objetivo.position);
        }
    }
}
