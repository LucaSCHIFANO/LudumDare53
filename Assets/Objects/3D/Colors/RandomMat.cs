using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class RandomMat : MonoBehaviour // This script is use to change a material randomly
{
    [SerializeField] private int id;
    [SerializeField] private List<Material> mats = new List<Material>();
    private MeshRenderer mesh;

    [SerializeField] Type type;

    enum Type
    {
        Door,
        Window,
    }

    private void Awake()
    {
        switch (type) 
        {
            case Type.Door: // For the doors, the mesh is directly on the gameObject
                mesh = GetComponent<MeshRenderer>();
                break;
            case Type.Window:// But for the windows, the mesh is on the 3rd child
                mesh = transform.GetChild(2).GetComponent<MeshRenderer>();
                break;
            default:
                break;
        }

        var materials = mesh.materials; // to change materials you have to copy the entier list of materials...
        Debug.Log(materials.Length + " " + id);
        materials[id] = mats[Random.Range(0, mats.Count)]; // .. change what you want
        mesh.materials = materials; // ... and reasign the new list

    }

}
