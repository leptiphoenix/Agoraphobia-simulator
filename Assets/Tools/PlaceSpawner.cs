using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class PlaceSpawner : EditorWindow
{
    Vector2 scrollPos;
    string objectBaseName = "spawned_";
    int objectID = 1, nbObject = 1, spawnMaxDistance = 40, spawnMinRange = 0;
    GameObject objectToSpawn;
    GameObject objectParent;

    float distParant = 0f;
    float minScaleFactor = 1f;
    float maxScaleFactor = 1f;


    [MenuItem("My Tools/PlaceSpawnerWindow")]
    public static void ShowWindow()
    {
        GetWindow(typeof(PlaceSpawner));
    }

    private void OnGUI()
    {
        scrollPos = GUILayout.BeginScrollView(scrollPos);
        GUILayout.Label("Spawn New Object", EditorStyles.boldLabel);


        objectToSpawn = EditorGUILayout.ObjectField("Prefab to spawn", objectToSpawn, typeof(GameObject), false) as GameObject;
        objectParent = EditorGUILayout.ObjectField("Parent in scene", objectParent, typeof(GameObject), true) as GameObject;

        GUILayout.Label("Spawn Options", EditorStyles.boldLabel);

        objectBaseName = EditorGUILayout.TextField("New Object Name Prefix", objectBaseName);
        nbObject = EditorGUILayout.IntField("Spawn count", nbObject);
        spawnMaxDistance = EditorGUILayout.IntField("Maximum spawn distance", spawnMaxDistance);
        spawnMinRange = EditorGUILayout.IntField("Minimum spawn range (Donut)", spawnMinRange);
        minScaleFactor = EditorGUILayout.FloatField("Minimum scale factor", minScaleFactor);
        maxScaleFactor = EditorGUILayout.FloatField("Maximum scale factor", maxScaleFactor);

        if (GUILayout.Button("Spawn " + nbObject + " Object"))
        {
            SpawnObject(nbObject);
        }
        if (GUILayout.Button("Spawn " + nbObject + " Object in donut"))
        {
            SpawnObjectDonut(nbObject);
        }
        if (GUILayout.Button("Delete all prefab of parent"))
        {
            DeletePrefab();
        }
        if (GUILayout.Button("Clear Id"))
        {
            objectID = 0;
            Debug.Log("Id Cleared : " + objectID);
        }

        GUILayout.EndScrollView();
    }

    private void SpawnObject(int nbObject)
    {
        if (nbObject == 0)
        {
            nbObject = 1;
        }
        if (objectToSpawn == null)
        {
            Debug.LogError("Error: Assigner un object à spawned");
        }
        if (objectBaseName == string.Empty)
        {
            objectBaseName = "prefab_";
        }

        for (int i = 0; i < nbObject; i++)
        {
            Vector2 spawnCircle = (Random.insideUnitCircle * spawnMaxDistance);
            Vector3 spawnPos = new Vector3(spawnCircle.x + objectParent.transform.position.x, 0f + objectParent.transform.position.y, spawnCircle.y + objectParent.transform.position.z);
            GameObject newObject = Instantiate(objectToSpawn, spawnPos, Quaternion.identity) as GameObject;
            newObject.transform.SetParent(objectParent.transform);
            newObject.name = objectBaseName + objectID;
            newObject.transform.localScale = Vector3.one * Random.Range(minScaleFactor, maxScaleFactor);
            newObject.tag = "Prefab";
            objectID++;
        }

        Debug.Log("Spawned " + nbObject + " objects for a total of " + objectID + " spawned objects !");

    }
    private void SpawnObjectDonut(int nbObject)
    {
        if (nbObject == 0)
        {
            nbObject = 1;
        }
        if (objectToSpawn == null)
        {
            Debug.LogError("Error: Assigner un object à spawned");
        }
        if (objectBaseName == string.Empty)
        {
            objectBaseName = "prefab_";
        }

        for (int i = 0; i < nbObject; i++)
        {
            Vector2 spawnCircle = (Random.insideUnitCircle * spawnMaxDistance);
            Vector3 spawnPos = new Vector3(spawnCircle.x + objectParent.transform.position.x, 0f + objectParent.transform.position.y, spawnCircle.y + objectParent.transform.position.z);
            while (Vector3.Distance(objectParent.transform.position, spawnPos) < spawnMinRange)
            {
                spawnCircle = (Random.insideUnitCircle * spawnMaxDistance);
                spawnPos = new Vector3(spawnCircle.x + objectParent.transform.position.x, 0f + objectParent.transform.position.y, spawnCircle.y + objectParent.transform.position.z);
            }
            GameObject newObject = Instantiate(objectToSpawn, spawnPos, Quaternion.identity) as GameObject;
            newObject.transform.SetParent(objectParent.transform);
            newObject.name = objectBaseName + objectID;
            newObject.transform.localScale = Vector3.one * Random.Range(minScaleFactor, maxScaleFactor);
            newObject.tag = "Prefab";
            objectID++;
        }

        Debug.Log("Spawned " + nbObject + " objects for a total of " + objectID + " spawned objects !");
    }
    private void DeletePrefab()
    {
        Debug.Log(objectParent.transform.childCount + " Object deleted from parent");
        int countPrefab = 0;
        foreach (Transform child in objectParent.transform)
        {
            if (child.tag == "Prefab")
            {
                countPrefab++;
            }
        }
        countPrefab = objectParent.transform.childCount - countPrefab;
        while (objectParent.transform.childCount != countPrefab)
        {
            foreach (Transform child in objectParent.transform)
            {
                if (child.tag == "Prefab")
                {
                    GameObject.DestroyImmediate(child.gameObject);
                }
            }
        }
    }


}


