using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMat : MonoBehaviour
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
            case Type.Door:
                    mesh = GetComponent<MeshRenderer>();
                break;
            case Type.Window:
                mesh = transform.GetChild(2).GetComponent<MeshRenderer>();
                break;
            default:
                break;
        }
        var materials = mesh.materials;
        Debug.Log(materials.Length + " " + id);
        materials[id] = mats[Random.Range(0, mats.Count)];
        mesh.materials = materials;
    }

}
