using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class WaterMeshGenerator : MonoBehaviour
{
    public int gridSize = 100; // Số ô vuông trên một cạnh
    public float cellSize = 1f; // Kích thước mỗi ô

    void Start()
    {
        MeshFilter meshFilter = GetComponent<MeshFilter>();
        Mesh mesh = new Mesh();
        mesh.name = "WaterMesh";

        Vector3[] vertices = new Vector3[(gridSize + 1) * (gridSize + 1)];
        int[] triangles = new int[gridSize * gridSize * 6];

        // 1. Tạo các đỉnh (Vertices)
        for (int i = 0, z = 0; z <= gridSize; z++)
        {
            for (int x = 0; x <= gridSize; x++)
            {
                vertices[i] = new Vector3(x * cellSize, 0, z * cellSize);
                i++;
            }
        }

        // 2. Tạo các tam giác (Triangles)
        int vert = 0;
        int tris = 0;
        for (int z = 0; z < gridSize; z++)
        {
            for (int x = 0; x < gridSize; x++)
            {
                triangles[tris + 0] = vert + 0;
                triangles[tris + 1] = vert + gridSize + 1;
                triangles[tris + 2] = vert + 1;
                triangles[tris + 3] = vert + 1;
                triangles[tris + 4] = vert + gridSize + 1;
                triangles[tris + 5] = vert + gridSize + 2;

                vert++;
                tris += 6;
            }
            vert++;
        }

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals(); // Quan trọng để đổ bóng chính xác
        meshFilter.mesh = mesh;
    }
}
