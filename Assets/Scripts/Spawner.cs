using UnityEngine;
using System.Collections.Generic;

public class Spawner : MonoBehaviour
{
    [Header("Referencias")]
    public List<GameObject> Prefabs;
    public Transform leftupPoint;
    public Transform rightdownPoint;

    [Header("Estadísticas")]
    public float timeBtwSpawn = 1.5f;
    public int totalKilledEnemies = 0;
    public int currentKilledEnemies = 0;
    public int maxKilledEnemies = 10;
    public int enemiesToAdd = 5;
    public int enemiesToSpawn = 1;
    public float speedIncreaseFactor = 0.9f; // Factor para reducir intervalo de spawn
    public float gameDuration = 300f; // Duración de la partida (5 minutos)

    [Header("Progreso")]
    public float progress; // Progreso de la partida (0 a 1)

    private float timer = 0f;
    private float elapsedTime = 0f;

    private Vector3 leftupPosition;
    private Vector3 rightdownPosition;

    void Start()
    {
        leftupPosition = leftupPoint.position;
        rightdownPosition = rightdownPoint.position;

        Debug.Log($"Posiciones iniciales - leftupPosition.y = {leftupPosition.y}, rightdownPosition.y = {rightdownPosition.y}");
    }

    void Update()
    {
        // Actualizar el tiempo transcurrido y el progreso
        elapsedTime += Time.deltaTime;
        progress = elapsedTime / gameDuration;

        // Terminar la partida si se supera la duración
        if (elapsedTime >= gameDuration)
        {
            EndGame();
            return;
        }

        // Manejo del spawn
        if (timer < timeBtwSpawn)
        {
            timer += Time.deltaTime;
        }
        else
        {
            timer = 0f;
            for (int i = 0; i < enemiesToSpawn; i++)
            {
                float x = Random.Range(leftupPoint.position.x, rightdownPoint.position.x);
                float y = Random.Range(rightdownPoint.position.y, leftupPoint.position.y);

                int prefab = Random.Range(0, Prefabs.Count);

                Instantiate(Prefabs[prefab], new Vector3(x, y, transform.position.z), Quaternion.Euler(0, 0, 180));
                Debug.Log($"Instanciado enemigo en: x = {x}, y = {y}");
            }
        }

        // Incremento progresivo de dificultad
        if (elapsedTime % 30f < Time.deltaTime) // Cada 30 segundos
        {
            timeBtwSpawn *= speedIncreaseFactor; // Reducir tiempo entre spawns
            Debug.Log($"Dificultad incrementada, nuevo intervalo de spawn: {timeBtwSpawn}");
        }
    }

    public void AddKilledEnemies()
    {
        totalKilledEnemies++;
        currentKilledEnemies++;

        if (currentKilledEnemies >= maxKilledEnemies)
        {
            currentKilledEnemies = 0;
            maxKilledEnemies += enemiesToAdd;
            enemiesToSpawn++;
        }
    }

    private void EndGame()
    {
        Debug.Log("¡Juego terminado!");
        // Aquí puedes añadir lógica para finalizar la partida
        // Por ejemplo, mostrar la pantalla de resultados o reiniciar la escena
        enabled = false; // Detener este script
    }
}
/*
 public Slider progressBar;

void Update()
{
    progressBar.value = progress;
}
 */