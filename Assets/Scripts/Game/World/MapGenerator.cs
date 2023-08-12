using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Metadata;

public class MapGenerator : MonoBehaviour
{
    [Header("Listas")]
    [SerializeField] GameObject[] patronMapPrefab;
    [SerializeField] Dictionary<int, List<PatronController>> patronDictionary = new Dictionary<int, List<PatronController>>();
    [SerializeField] List<PatronPrefabData> childObjects = new List<PatronPrefabData>();

    [Header("Variables")]
    [SerializeField] float waitTimeRaycast = 1;

    [Header("Componentes")]
    [SerializeField] PatronController lastPatron;
    [SerializeField] GameObject parentPatron;
    [SerializeField] GameObject mapStartPrefab;

    private void Start() {
        InstantiateAllPrefabs();
        StartCoroutine(ContinuousRaycast());

        for(int i =0; i<mapStartPrefab.transform.childCount; i++) {
            PatronPrefabData patronData = new PatronPrefabData();
            patronData.patron = mapStartPrefab.transform.GetChild(i).gameObject.GetComponent<PatronController>();
            patronData.initialPos = mapStartPrefab.transform.GetChild(i).gameObject.transform.localPosition;
            childObjects.Add(patronData);
        }

        GameManager.Instance.OnGameStart += ResetMap;
        GameManager.Instance.OnGameExit += ResetMap;
    }

    private IEnumerator ContinuousRaycast() {
        Ray ray = new Ray(transform.position, -transform.up);
        RaycastHit hit;

        // Realizar el Raycast y pintar el rayo en la escena
        if (Physics.Raycast(ray, out hit, Mathf.Infinity)) {

        }
        else {
            // El Raycast no colisionó con ningún objeto
            if(GameManager.Instance.gameState == GameState.Game) InstantiatePatronMap();
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
        patron.SetNewPosition(new Vector3(2.38f, -0.2799928f - 3.16f, lastPatron.gameObject.transform.position.z + lastPatron.plartformScale.localScale.z));

        lastPatron = patron;
    }

    public void ResetMap() {
        DisableAllPrefabsInDictionary();
        PatronPrefabData lastPatronData = new PatronPrefabData();
        foreach (PatronPrefabData child in childObjects) {
            child.patron.SetNewPositionZ(child.initialPos.z);
            child.patron.EnablePatron();
            lastPatronData = child;
        }
        lastPatron = lastPatronData.patron;
    }

    void DisableAllPrefabsInDictionary() {
        foreach (var key in patronDictionary.Keys) {
            List<PatronController> gameObjectsList = patronDictionary[key];
            foreach (PatronController obj in gameObjectsList) {
                obj.DisablePatron();
            }
        }
    }
}
