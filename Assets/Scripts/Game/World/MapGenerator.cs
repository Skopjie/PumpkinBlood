using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] GameObject[] patronMapPrefab;
    [SerializeField] Dictionary<int, List<PatronController>> patronDictionary = new Dictionary<int, List<PatronController>>();

    [Header("Variables")]
    [SerializeField] float waitTimeRaycast = 1;

    [SerializeField] PatronController lastPatron;
    [SerializeField] GameObject parentPatron;

    private void Start() {
        InstantiateAllPrefabs();
        StartCoroutine(ContinuousRaycast());
    }

    private IEnumerator ContinuousRaycast() {
        Ray ray = new Ray(transform.position, -transform.up);
        RaycastHit hit;

        // Realizar el Raycast y pintar el rayo en la escena
        if (Physics.Raycast(ray, out hit, Mathf.Infinity)) {
            // Aquí puedes agregar el código que deseas ejecutar cuando el Raycast golpea algo.
            Debug.Log("Raycast hit: " + hit.collider.gameObject.name);
        }
        else {
            // El Raycast no colisionó con ningún objeto
            InstantiatePatronMap();
        }

        // Pintar el rayo en la escena
        Debug.DrawRay(ray.origin, ray.direction * 100f, Color.red, 1f);

        // Esperar el tiempo definido antes de realizar el próximo Raycast
        yield return new WaitForSeconds (waitTimeRaycast);
        StartCoroutine(ContinuousRaycast());
    }

    void InstantiateAllPrefabs() {
        for(int i = 0; i<patronMapPrefab.Length; i++) {
            PatronController mapPatron = Instantiate(patronMapPrefab[i], parentPatron.transform).GetComponent<PatronController>();
            mapPatron.SetEnablePatron(false);
            patronDictionary[i] = new List<PatronController> { mapPatron };
        }
    }

    PatronController CreateNewPatron(int indexPatron) {
        PatronController mapPatron = Instantiate(patronMapPrefab[indexPatron], parentPatron.transform).GetComponent<PatronController>();
        mapPatron.SetEnablePatron(false);
        patronDictionary[indexPatron].Add(mapPatron);
        return mapPatron;
    }

    PatronController GetRandomPatronMap() {
        int indexRandomMap = Random.Range(0, patronMapPrefab.Length);
        foreach(PatronController patron in patronDictionary[indexRandomMap]) {
            if(patron.IsActive() == false) {
                return patron;
            }
        }
        return CreateNewPatron(indexRandomMap);
    }

    void InstantiatePatronMap() {
        PatronController patron = GetRandomPatronMap();
        patron.SetEnablePatron(true);
        patron.SetNewPosition(new Vector3(0, 2.75f -3.16f, lastPatron.gameObject.transform.position.z + lastPatron.plartformScale.localScale.z));

        lastPatron = patron;
    }
}
