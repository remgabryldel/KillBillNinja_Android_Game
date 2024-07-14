using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public GameObject[] fruitPrefabs;

    public GameObject bombPrefab;

    [Range(0f, 1f)]
    public float bombChance = 0.05f;

    public float maxTempovita = 5f;

    public float minSpawnDelay = 0.15f;
    public float maxSpawnDelay = 1.5f;

    public float minForza = 18f;    
    public float maxForza = 22f;

    public float minAngolo = -25f;
    public float maxAngolo = 25f;

    private Collider spawnArea;

    private void Awake()
    {
        spawnArea = GetComponent<Collider>();
    }

    private void OnEnable()
    {
        StartCoroutine(Spawn());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private IEnumerator Spawn(){

        yield return new WaitForSeconds(2f);

        while (enabled){
            GameObject prefab = fruitPrefabs[Random.Range(0, fruitPrefabs.Length)];

            if(Random.value < bombChance){
                prefab = bombPrefab;
            }

            Vector3 posizione = new Vector3();
            posizione.x = Random.Range(spawnArea.bounds.min.x, spawnArea.bounds.max.x);
            posizione.y = Random.Range(spawnArea.bounds.min.y, spawnArea.bounds.max.y);
            posizione.z = Random.Range(spawnArea.bounds.min.z, spawnArea.bounds.max.z);

            Quaternion rotazione = Quaternion.Euler(0f, 0f, Random.Range(minAngolo, maxAngolo));

            GameObject frutto = Instantiate(prefab, posizione, rotazione);
            Destroy(frutto, maxTempovita);

            float forza = Random.Range(minForza, maxForza);
            frutto.GetComponent<Rigidbody>().AddForce(frutto.transform.up * forza, ForceMode.Impulse);


            yield return new WaitForSeconds(Random.Range(minSpawnDelay, maxSpawnDelay));
        }

    }

}
