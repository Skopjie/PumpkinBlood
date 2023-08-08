using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ResourceGenerator : MonoBehaviour
{
    // la herramienta pilla los elepementos y los crea antes de tiempo de ejecucion herramienta
    [SerializeField] GameObject prefab;
    [SerializeField] float rangeBtwPrefab;

    [SerializeField] Vector3 startRotation;
    [SerializeField] Vector3 endRotation;

    [SerializeField] Vector3 startScale;
    [SerializeField] Vector3 endScale;

    List<GameObject> prefabs = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        InstantiatePrefabs();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InstantiatePrefabs() {
        for(int i = 0; i < this.gameObject.transform.childCount; i++) {
            prefabs.Add(this.gameObject.transform.GetChild(i).gameObject);
        }


        foreach (GameObject prefab in prefabs) {
            prefab.transform.localScale = new Vector3(Random.Range(startScale.x,endScale.x), Random.Range(startScale.y, endScale.y), Random.Range(startScale.z, endScale.z));
            prefab.transform.eulerAngles = new Vector3(Random.Range(startRotation.x, endRotation.x), Random.Range(startRotation.y, endRotation.y), Random.Range(startRotation.z, endRotation.z));
        }
    }
}
