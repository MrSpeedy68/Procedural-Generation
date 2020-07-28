using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager.UI;
using UnityEngine;

public static class Noise
{
    public static float[,] GenerateNoiseMap(int mapWidth, int mapHeight, float scale) //Handles the Generation of the noise map
    {
        float[,] noiseMap = new float[mapWidth, mapHeight]; //Makes a new 2D array of the map height nad width

        if(scale <= 0) //Make sure that scale is not 0 as it would be dividing by 0
        {
            scale = 0.0001f;
        }

        for(int y = 0;y < mapHeight;y++)
        {
            for(int x = 0;x < mapWidth; x++)
            {
                float sampleX = x / scale;
                float sampleY = y / scale;

                float perlinValue = Mathf.PerlinNoise(sampleX, sampleY); //Generate perlin noise with the x and y values
                noiseMap[x, y] = perlinValue;
            }
        }
        return noiseMap;
    }

}
