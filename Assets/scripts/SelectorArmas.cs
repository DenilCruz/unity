using UnityEngine;

public class SelectorArmas : MonoBehaviour
{
    [Header("Lista de Armas")]
    public GameObject[] armas; // Arrastra aquí los objetos 3D de tus armas (Pistola, Escopeta, etc.)
    
    private int indiceArmaActual = 0;

    void Start()
    {
        // Al empezar, nos aseguramos de activar solo la primera arma de la lista
        SeleccionarArma();
    }

    void Update()
    {
        // Si el jugador presiona la tecla T, cambia de arma
        if (Input.GetKeyDown(KeyCode.T))
        {
            // Avanzamos al siguiente índice de la lista
            indiceArmaActual++;
            
            // Si llegamos al final de la lista, volvemos a la primera arma (índice 0)
            if (indiceArmaActual >= armas.Length)
            {
                indiceArmaActual = 0;
            }
            
            // Aplicamos el cambio
            SeleccionarArma();
        }
    }

    void SeleccionarArma()
    {
        // Recorremos toda nuestra lista de armas
        for (int i = 0; i < armas.Length; i++)
        {
            if (armas[i] != null)
            {
                // Si el índice coincide con el arma actual, la activamos. Si no, la desactivamos.
                armas[i].SetActive(i == indiceArmaActual);
            }
        }
    }
}
