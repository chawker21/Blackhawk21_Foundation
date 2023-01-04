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

        //Creates vertices for single Isosceles right triangle that lies on the xyplane with it's corner 90 degree to the origin
        mesh.vertices = new Vector3[]
        {
            Vector3.zero, Vector3.right, Vector3.up
        };
        
        //add a Vector 3 array to the normals property of the mesh to adjust the normals pointing back instead of the default foward
        //which makes it look like light is coming from behind by default.
        mesh.normals = new Vector3[]
        {
            Vector3.zero, Vector3.back, Vector3.back
        };
        
        
        
        //add UV voordinates to apply texture to mesh

        mesh.uv = new Vector2[]
        {
            Vector2.zero, Vector2.right, Vector2.up
        };
        
        // adjust the mesh tangents to get proper texture lighting from the normal mapping
        mesh.tangents = new Vector4[]
        {
            new Vector4(1f, 0f, 0f, -1f),
            new Vector4(1f, 0f, 0f, -1f),
            new Vector4(1f, 0f, 0f, -1f)
        };

        //single triangle defined with three indices
        mesh.triangles = new int[]
        {
            //default this triangle is only seen by turning it around, 0, 1, 2, is counterclockwise 
            //0, 2, 1 turns the face to the camera.
            //0, 1, 2
            0, 2, 1
        };
        
        //Assign name to the mesh property of the meshFilter Component
        GetComponent<MeshFilter>().mesh = mesh;
    }
    
    
}
