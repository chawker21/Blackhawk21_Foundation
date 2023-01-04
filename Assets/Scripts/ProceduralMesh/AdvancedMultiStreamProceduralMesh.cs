
using Unity.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using Unity.Mathematics;

using static Unity.Mathematics.math;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class AdvancedMultiStreamProceduralMesh : MonoBehaviour
{

	// This function is called when the object is enabled
	void OnEnable()
	{
		// Initialize variables for the number of vertex attributes, vertices, and triangle indices
		int vertexAttributeCount = 4;
		int vertexCount = 4;
		int triangleIndexCount = 6;

		// Allocate an array of Mesh.MeshData objects and get the first element
		Mesh.MeshDataArray meshDataArray = Mesh.AllocateWritableMeshData(1);
		Mesh.MeshData meshData = meshDataArray[0];

		// Create a temporary NativeArray of VertexAttributeDescriptor objects
		var vertexAttributes = new NativeArray<VertexAttributeDescriptor>(
			vertexAttributeCount, Allocator.Temp, NativeArrayOptions.UninitializedMemory
		);

		// Set the attributes of the first vertex attribute
		vertexAttributes[0] = new VertexAttributeDescriptor(dimension: 3);

		// Set the attributes of the second vertex attribute
		vertexAttributes[1] = new VertexAttributeDescriptor(
			VertexAttribute.Normal, dimension: 3, stream: 1
		);

		// Set the attributes of the third vertex attribute
		vertexAttributes[2] = new VertexAttributeDescriptor(
			VertexAttribute.Tangent, VertexAttributeFormat.Float16, 4, 2
		);

		// Set the attributes of the fourth vertex attribute
		vertexAttributes[3] = new VertexAttributeDescriptor(
			VertexAttribute.TexCoord0, VertexAttributeFormat.Float16, 2, 3
		);

		// Set the vertex buffer parameters for the mesh data using the vertex attribute array
		meshData.SetVertexBufferParams(vertexCount, vertexAttributes);

		// Dispose of the temporary vertex attribute array
		vertexAttributes.Dispose();

		// Get the position data for the vertices of the mesh
		NativeArray<float3> positions = meshData.GetVertexData<float3>();

		// Set the position of the first vertex
		positions[0] = 0f;

		// Set the position of the second vertex
		positions[1] = right();

		// Set the position of the third vertex
		positions[2] = up();

		// Set the position of the fourth vertex
		positions[3] = float3(1f, 1f, 0f);

		// Get the normal data for the vertices of the mesh
		NativeArray<float3> normals = meshData.GetVertexData<float3>(1);

		// Set the normal of each vertex to be back
		normals[0] = normals[1] = normals[2] = normals[3] = back();

		// Initialize half variables for 0 and 1

		// Initialize half variables for 0 and 1
		half h0 = half(0f), h1 = half(1f);

// Get the tangent data for the vertices of the mesh
		NativeArray<half4> tangents = meshData.GetVertexData<half4>(2);

// Set the tangent of each vertex to (h1, h0, h0, half(-1f))
		tangents[0] = tangents[1] = tangents[2] = tangents[3] = half4(h1, h0, h0, half(-1f));

// Get the texture coordinate data for the vertices of the mesh
		NativeArray<half2> texCoords = meshData.GetVertexData<half2>(3);

// Set the texture coordinate of the first vertex
		texCoords[0] = h0;

// Set the texture coordinate of the second vertex
		texCoords[1] = half2(h1, h0);

// Set the texture coordinate of the third vertex
		texCoords[2] = half2(h0, h1);

// Set the texture coordinate of the fourth vertex
		texCoords[3] = h1;

// Set the index buffer parameters for the mesh data to have triangleIndexCount indices of type UInt16
		meshData.SetIndexBufferParams(triangleIndexCount, IndexFormat.UInt16);

// Get the triangle index data for the mesh
		NativeArray<ushort> triangleIndices = meshData.GetIndexData<ushort>();

// Set the indices of the first triangle
		triangleIndices[0] = 0;
		triangleIndices[1] = 2;
		triangleIndices[2] = 1;

// Set the indices of the second triangle
		triangleIndices[3] = 1;
		triangleIndices[4] = 2;
		triangleIndices[5] = 3;

// Create a bounds object with center (0.5, 0.5) and size (1, 1)
		var bounds = new Bounds(new Vector3(0.5f, 0.5f), new Vector3(1f, 1f));

// Set the number of submeshes in the mesh data to 1
		meshData.subMeshCount = 1;

// Set the submesh of the mesh data to have triangleIndexCount indices, bounds, and vertexCount vertices
// and specify that the bounds should not be recalculated
		meshData.SetSubMesh(0, new SubMeshDescriptor(0, triangleIndexCount)
		{
			bounds = bounds,
			vertexCount = vertexCount
		}, MeshUpdateFlags.DontRecalculateBounds);

// Create a mesh object with the specified bounds and name
		var mesh = new Mesh
		{
			bounds = bounds,
			name = "Procedural Mesh"
		};

// Apply the mesh data to the mesh object and dispose of the mesh data array
		Mesh.ApplyAndDisposeWritableMeshData(meshDataArray, mesh);

// Set the mesh of the MeshFilter component to the created mesh object
		GetComponent<MeshFilter>().mesh = mesh;
	}
}