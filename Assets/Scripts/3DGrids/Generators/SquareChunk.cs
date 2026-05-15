using _3DGrids.Configs;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer), typeof(MeshCollider))]
public class SquareChunk : MonoBehaviour
{
    private MeshRenderer meshRenderer;
    private MeshFilter meshFilter;
    private MeshCollider meshCollider;

    public void Build(int chunkX, int chunkZ, SquareWorldConfig config)
    {
        meshFilter = GetComponent<MeshFilter>();
        meshRenderer = GetComponent<MeshRenderer>();
        meshCollider = GetComponent<MeshCollider>();

        // создаем меш
        Mesh mesh = new Mesh();
        mesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;

        int size = config.TilesPerChunk;
        float tile = config.TileSize;

        Vector3[] vertices = new Vector3[(size + 1) * (size + 1)];
        int[] triangles = new int[size * size * 6];
        Vector3[] normals = new Vector3[vertices.Length];
        float[] heights = new float[vertices.Length];

        for (int z = 0; z <= size; z++)
        for (int x = 0; x <= size; x++)
        {
            float worldX = x * tile + chunkX * config.ChunkWorldSize;
            float worldZ = z * tile + chunkZ * config.ChunkWorldSize;

            float h = Mathf.PerlinNoise(
                worldX * config.Noise.NoiseScale,
                worldZ * config.Noise.NoiseScale
            ) * config.Noise.MaxHeight;

            int index = z * (size + 1) + x;
            vertices[index] = new Vector3(x * tile, h, z * tile);
            heights[index] = h;
        }

        int triIndex = 0;
        for (int z = 0; z < size; z++)
        for (int x = 0; x < size; x++)
        {
            int i = z * (size + 1) + x;

            triangles[triIndex++] = i;
            triangles[triIndex++] = i + size + 1;
            triangles[triIndex++] = i + 1;

            triangles[triIndex++] = i + 1;
            triangles[triIndex++] = i + size + 1;
            triangles[triIndex++] = i + size + 2;
        }

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
        normals = mesh.normals;

        meshFilter.mesh = mesh;
        meshCollider.sharedMesh = mesh;

        meshRenderer.materials = new Material[3]
        {
            config.GrassMaterial,
            config.DirtMaterial,
            config.RockMaterial
        };

        ApplyMaterials(mesh, vertices, heights, config);

        SpawnVegetation(vertices, normals, heights, config, chunkX, chunkZ);
        transform.position = new Vector3(chunkX * config.ChunkWorldSize, 0, chunkZ * config.ChunkWorldSize);
    }

    private void ApplyMaterials(Mesh mesh, Vector3[] vertices, float[] heights, SquareWorldConfig config)
    {
        // Unity Standard Shader может использовать multi-material через submeshes
        // Разделим треугольники по материалам
        int size = config.TilesPerChunk;
        int totalTris = size * size * 6;

        // три массива для submesh
        var grassTris = new System.Collections.Generic.List<int>();
        var dirtTris = new System.Collections.Generic.List<int>();
        var rockTris = new System.Collections.Generic.List<int>();

        int triIndex = 0;
        for (int z = 0; z < size; z++)
        for (int x = 0; x < size; x++)
        {
            int i = z * (size + 1) + x;
            int[] currentTris = new int[]
            {
                i, i + size + 1, i + 1,
                i + 1, i + size + 1, i + size + 2
            };

            // средняя высота квадрата
            float avgH = (heights[i] + heights[i+1] + heights[i+size+1] + heights[i+size+2]) / 4f;

            if (avgH < 2f) grassTris.AddRange(currentTris);
            else if (avgH < 6f) dirtTris.AddRange(currentTris);
            else rockTris.AddRange(currentTris);
        }

        mesh.subMeshCount = 3;
        mesh.SetTriangles(grassTris.ToArray(), 0);
        mesh.SetTriangles(dirtTris.ToArray(), 1);
        mesh.SetTriangles(rockTris.ToArray(), 2);
    }

    private void SpawnVegetation(Vector3[] vertices, Vector3[] normals, float[] heights, SquareWorldConfig config, int chunkX, int chunkZ)
    {
        int size = config.TilesPerChunk;

        for (int z = 0; z < size; z++)
        for (int x = 0; x < size; x++)
        {
            int i = z * (size + 1) + x;
            float h = heights[i];

            // вычисляем угол наклона
            float slope = Vector3.Angle(normals[i], Vector3.up);

            // простая рандомизация
            if (Random.value > 0.5f) continue;

            Vector3 pos = vertices[i];

            GameObject prefab = null;
            if (h < 2f && config.Grass.Length > 0) prefab = config.Grass[Random.Range(0, config.Grass.Length)];
            else if (h >= 2f && h < 6f && config.Bushes.Length > 0) prefab = config.Bushes[Random.Range(0, config.Bushes.Length)];
            else if (h >= 6f && config.Trees.Length > 0 && slope < 30f) prefab = config.Trees[Random.Range(0, config.Trees.Length)];

            if (prefab != null)
            {
                GameObject go = Instantiate(prefab, transform);
                go.transform.localPosition = pos;
                go.transform.localRotation = Quaternion.Euler(0, Random.Range(0,360), 0);
                go.transform.localScale *= Random.Range(0.8f, 1.2f);
            }
        }
    }
}