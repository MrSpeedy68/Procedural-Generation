using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TextureGenerator //Create a texture out of a 1D colour map
{
    public static Texture2D TextureFromColourMap(Color[] colourMap, int width, int height)
    {
        Texture2D texture = new Texture2D(width, height);
        texture.filterMode = FilterMode.Point;
        texture.wrapMode = TextureWrapMode.Clamp;
        texture.SetPixels(colourMap);
        texture.Apply();
        return texture;
    }

    public static Texture2D TextureFromHeightMap(float[,] heightMap)
    {
        int width = heightMap.GetLength(0); //Grabs the first dimension 0 which is x
        int height = heightMap.GetLength(1); //Grabs the second dimension 1 which is y

        Texture2D texture = new Texture2D(width, height); //Makes a new 2D texture of height and width

        //Set color of all the pixels by generating an array of colors and setting them to the pixels
        Color[] colourMap = new Color[width * height];
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                colourMap[y * width + x] = Color.Lerp(Color.black, Color.white, heightMap[x, y]); //Set pixel color between black and white % range is the same as noiseMap
            }
        }
        return TextureFromColourMap(colourMap, width, height);
    }
}
