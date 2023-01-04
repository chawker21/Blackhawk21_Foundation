using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//RequireComponent will add these components to the object that this script is attached to.
[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class SimpleProceduralMesh : MonoBehaviour
{

    void OnEnable()
    {
        //Adds name to the mesh filter in the component section in play mode
        var mesh = new Mesh
        {
            name = "Procedural Mesh"
        };
        
        //Assign name to the mesh property of the meshFilter Component
        GetComponent<MeshFilter>().mesh = mesh;
    }
    
    
}
