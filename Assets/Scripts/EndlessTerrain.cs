using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndlessTerrain : MonoBehaviour
{

    public const float maxViewDistance = 900;
    public Transform viewer;

    public static Vector2 viewerPosition;
    int chunkSize;
    int chunksVisableInViewDistance;

    Dictionary<Vector2, TerrainChunk> terrainChunkDictionary = new Dictionary<Vector2, TerrainChunk>();
    List<TerrainChunk> terrainChunksVisableLastUpdate = new List<TerrainChunk>();

    private void Start()
    {
        chunkSize = MapGenerator.mapChunkSize - 1;
        chunksVisableInViewDistance = Mathf.RoundToInt(maxViewDistance / chunkSize);
    }

    private void Update()
    {
        viewerPosition = new Vector2(viewer.position.x, viewer.position.z);
        UpdateVisableChunks();
    }

    void UpdateVisableChunks()
    {
        for(int i = 0; i < terrainChunksVisableLastUpdate.Count; i++)
        {
            terrainChunksVisableLastUpdate[i].SetVisable(false);
        }
        terrainChunksVisableLastUpdate.Clear();


        int currentChunkCoordX = Mathf.RoundToInt(viewerPosition.x / chunkSize);
        int currentChunkCoordY = Mathf.RoundToInt(viewerPosition.y / chunkSize);

        for(int yOffset = -chunksVisableInViewDistance; yOffset <= chunksVisableInViewDistance; yOffset++)
        {
            for (int xOffset = -chunksVisableInViewDistance; xOffset <= chunksVisableInViewDistance; xOffset++)
            {
                Vector2 viewedChunkCoord = new Vector2(currentChunkCoordX + xOffset, currentChunkCoordY + yOffset);

                if(terrainChunkDictionary.ContainsKey (viewedChunkCoord))
                {
                    terrainChunkDictionary[viewedChunkCoord].UpdateTerrainChunk();
                    if (terrainChunkDictionary[viewedChunkCoord].IsVisable())
                    {
                        terrainChunksVisableLastUpdate.Add(terrainChunkDictionary[viewedChunkCoord]);
                    }
                }
                else
                {
                    terrainChunkDictionary.Add(viewedChunkCoord, new TerrainChunk(viewedChunkCoord, chunkSize, transform));
                }
            }
        }
    }

    public class TerrainChunk
    {
        GameObject meshObject;
        Vector2 position;
        Bounds bounds;


        public TerrainChunk(Vector2 coord, int size, Transform parent)
        {
            position = coord * size;
            bounds = new Bounds(position, Vector2.one * size);
            Vector3 positionV3 = new Vector3(position.x, 0, position.y);

            meshObject = GameObject.CreatePrimitive(PrimitiveType.Plane);
            meshObject.transform.position = positionV3;
            meshObject.transform.localScale = Vector3.one * size / 10f;
            meshObject.transform.parent = parent;
            SetVisable(false);
        }

        public void UpdateTerrainChunk()
        {
            float viewerDstFromNearestEdge = bounds.SqrDistance(viewerPosition);
            bool visible = viewerDstFromNearestEdge <= maxViewDistance;
            SetVisable(visible);
        }

        public void SetVisable(bool visible)
        {
            meshObject.SetActive(visible);
        }

        public bool IsVisable()
        {
            return meshObject.activeSelf;
        }
    }
}
