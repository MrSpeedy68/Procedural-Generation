using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager.UI;
using UnityEngine;

public static class Noise
{
    public static float[,] GenerateNoiseMap(int mapWidth, int mapHeight, int seed, float scale, int octaves, float persistance, float lacunarity, Vector2 offset) //Handles the Generation of the noise map
    {
        float[,] noiseMap = new float[mapWidth, mapHeight]; //Makes a new 2D array of the map height nad width

        System.Random prng = new System.Random(seed); //Generates a random seed
        Vector2[] octavesOffsets = new Vector2[octaves];
        for(int i = 0; i < octaves; i++)
        {
            float offsetX = prng.Next(-100000, 100000) + offset.x;
            float offsetY = prng.Next(-100000, 100000) + offset.y;
            octavesOffsets[i] = new Vector2(offsetX, offsetY);
        }

        if(scale <= 0) //Make sure that scale is not 0 as it would be dividing by 0
        {
            scale = 0.0001f;
        }

        float maxNoiseHeight = float.MinValue;
        float minNoiseHeight = float.MaxValue;

        float halfWidth = mapWidth / 2f;
        float halfHeight = mapHeight / 2f;

        for(int y = 0;y < mapHeight;y++)
        {
            for(int x = 0;x < mapWidth; x++)
            {
                float amplitude = 1;
                float frequency = 1;
                float noiseHeight = 0;

                for (int i = 0; i < octaves; i++)
                {
                    float sampleX = (x-halfWidth) / scale * frequency + octavesOffsets[i].x; //The higher the frequency the further apart the sample points will be whcih means the height values will change more rapidly
                    float sampleY = (y-halfHeight) / scale * frequency + octavesOffsets[i].y; //Choosing sample points

                    float perlinValue = Mathf.PerlinNoise(sampleX, sampleY) * 2 - 1; //Generate perlin noise with the x and y values , * 2-1 is to have negative height
                    noiseHeight += perlinValue * amplitude;

                    amplitude *= persistance; //Decreases each octave
                    frequency *= lacunarity; //Increases each octave
                }

                if(noiseHeight > maxNoiseHeight)
                {
                    maxNoiseHeight = noiseHeight;
                }
                else if(noiseHeight < minNoiseHeight)
                {
                    minNoiseHeight = noiseHeight;
                }

                noiseMap[x, y] = noiseHeight;
            }
        }

        for (int y = 0; y < mapHeight; y++)
        {
            for (int x = 0; x < mapWidth; x++)
            {
                noiseMap[x, y] = Mathf.InverseLerp(minNoiseHeight, maxNoiseHeight, noiseMap[x, y]);
            }
        }

                return noiseMap;
    }

}
