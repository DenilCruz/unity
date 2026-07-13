using UnityEngine;
using System.Collections;

public class SpawnerEnemigos : MonoBehaviour
{
    [Header("Configuración del Spawner")]
    public GameObject enemigoPrefab; // Aquí arrastras tu enemigo desde la carpeta prefabs
    public int cantidadASpawnear = 5; // Cuántos enemigos quieres que salgan en total
    public float tiempoEntreSpawns = 100.5f; // Segundos de espera entre cada aparición

    void Start()
    {
        // Al empezar el juego, iniciamos el proceso de spawn si pusimos un prefab
        if (enemigoPrefab != null)
        {
            StartCoroutine(SpawnearEnemigos());
        }
        else
        {
            Debug.LogWarning("¡Al Spawner le falta el prefab del enemigo!");
        }
    }

    IEnumerator SpawnearEnemigos()
    {
        // Repetimos este bloque de código según la cantidad que elegiste
        for (int i = 0; i < cantidadASpawnear; i++)
        {
            // Creamos un clon del enemigo exactamente en la posición de este Spawner
            Instantiate(enemigoPrefab, transform.position, transform.rotation);
            
            // Esperamos unos segundos antes de sacar al siguiente enemigo
            yield return new WaitForSeconds(tiempoEntreSpawns);
        }
    }
}
