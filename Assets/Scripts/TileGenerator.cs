using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class TileGenerator : MonoBehaviour
{
    [SerializeField] NavMeshSurface navMeshSurface;

    public GameObject tilePrefab; // The tile prefab to instantiate
    public Transform cameraTransform; // Reference to the camera
    public int tilesAhead = 5; // Number of tiles to keep ahead of the camera in both directions
    public int tilesBehind = 2; // Number of tiles to keep behind the camera
    public float tileSize = 10f; // Size of a single tile

    private Vector2Int currentCenterTile; // Keeps track of the current center of the grid
    private HashSet<Vector2Int> activeTiles = new HashSet<Vector2Int>(); // Tracks active tiles
    private Dictionary<Vector2Int, GameObject> tileObjects = new Dictionary<Vector2Int, GameObject>(); // Maps grid positions to tile GameObjects

    void Start()
    {
        // Initialize the grid based on the camera's starting position
        UpdateCenterTile();
        GenerateInitialGrid();
    }

    void Update()
    {
        // Check if the camera has moved enough to require updating the grid
        Vector2Int newCenterTile = GetTileIndex(cameraTransform.position);
        if (newCenterTile != currentCenterTile)
        {
            currentCenterTile = newCenterTile;
            UpdateGrid();
            //Debug.Log("BuildNavmesh");
            //navMeshSurface.BuildNavMesh();

            //navMeshSurface.UpdateNavMesh(navMeshSurface.navMeshData);
        }
    }

    private void GenerateInitialGrid()
    {
        for (int x = -tilesBehind; x <= tilesAhead; x++)
        {
            for (int z = -tilesBehind; z <= tilesAhead; z++)
            {
                Vector2Int tilePos = currentCenterTile + new Vector2Int(x, z);
                SpawnTile(tilePos);
            }
        }
        //Debug.Log("BuildNavmesh");
        //navMeshSurface.BuildNavMesh();
    }

    private void UpdateGrid()
    {
        // Spawn new tiles and remove out-of-range tiles
        for (int x = -tilesBehind; x <= tilesAhead; x++)
        {
            for (int z = -tilesBehind; z <= tilesAhead; z++)
            {
                Vector2Int tilePos = currentCenterTile + new Vector2Int(x, z);

                if (!activeTiles.Contains(tilePos))
                {
                    SpawnTile(tilePos);
                }
            }
        }

        List<Vector2Int> tilesToRemove = new List<Vector2Int>();
        foreach (var tile in activeTiles)
        {
            if (Mathf.Abs(tile.x - currentCenterTile.x) > tilesAhead || Mathf.Abs(tile.y - currentCenterTile.y) > tilesAhead)
            {
                tilesToRemove.Add(tile);
            }
        }

        foreach (var tile in tilesToRemove)
        {
            RemoveTile(tile);
        }
    }

    private Vector2Int GetTileIndex(Vector3 position)
    {
        int x = Mathf.FloorToInt(position.x / tileSize);
        int z = Mathf.FloorToInt(position.z / tileSize);
        return new Vector2Int(x, z);
    }

    private void SpawnTile(Vector2Int tilePos)
    {
        Vector3 spawnPosition = new Vector3(tilePos.x * tileSize, 0, tilePos.y * tileSize);
        GameObject newTile = Instantiate(tilePrefab, spawnPosition, Quaternion.identity);
        activeTiles.Add(tilePos);
        tileObjects[tilePos] = newTile;
    }

    private void RemoveTile(Vector2Int tilePos)
    {
        if (tileObjects.TryGetValue(tilePos, out GameObject tile))
        {
            Destroy(tile);
            tileObjects.Remove(tilePos);
        }

        activeTiles.Remove(tilePos);
    }

    private void UpdateCenterTile()
    {
        currentCenterTile = GetTileIndex(cameraTransform.position);
    }
}
