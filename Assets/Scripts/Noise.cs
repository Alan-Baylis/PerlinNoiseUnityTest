using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//There won't be multiple instances of this class, so we will make it static.
public static class Noise{

    //method to generate the noise map
    //return values between 0 and 1
    //seed = if we want the same map, just get the same seed.
    public static float[,] GenerateNoiseMap(int mapWidth, int mapHeight, int seed, float scale, int octaves, float persistance, float lacunarity, Vector2 offset)
    {
        float[,] noiseMap = new float[mapWidth, mapHeight];

        System.Random prng = new System.Random(seed);
        Vector2[] octaveOffsets = new Vector2[octaves];
        for(int i = 0; i < octaves; i++)
        {
            float offsetX = prng.Next(-100000, 100000) + offset.x; //dont use values that are too high, or it'll return the same values over and over for mathf.perlin
            float offsetY = prng.Next(-100000, 100000) + offset.y;
            octaveOffsets[i] = new Vector2(offsetX, offsetY);
        }

        if(scale <= 0)
        {
            scale = 0.0001f;
        }

        float maxNoiseHeight = float.MinValue;
        float minNoiseHeight = float.MaxValue;

        float halfWidth = mapWidth / 2f;
        float halfHeight = mapHeight / 2f;

        for(int y=0; y<mapHeight; y++)
        {
            for(int x=0; x<mapWidth; x++)
            {

                float amplitude = 1;
                float frequency = 1;
                float noiseHeight = 0;

                for(int i=0; i < octaves; i++)
                {
                    float sampleX = (x - halfWidth) / scale * frequency + octaveOffsets[i].x; //get non integer values. makesure scale is not 0 or error 
                    float sampleY = (y - halfHeight) / scale * frequency + octaveOffsets[i].y; //higher the frequency, furthre apart the samples will be, meaning the height values will change more rapidly

                    float perlinValue = Mathf.PerlinNoise(sampleX, sampleY) * 2 - 1; //-1 to 1, more interesting heights
                    noiseHeight += perlinValue * amplitude;

                    amplitude *= persistance; //decreases each octave?
                    frequency *= lacunarity; //frequency increases each octave
                    
                }

                if(noiseHeight > maxNoiseHeight)
                {
                    maxNoiseHeight = noiseHeight;
                }else if(noiseHeight < minNoiseHeight)
                {
                    minNoiseHeight = noiseHeight; //get the min and max noise heights. the range of our noise map values
                }

                noiseMap[x, y] = noiseHeight;

            }
        }

        for (int y = 0; y < mapHeight; y++) //normalize noise map.
        {
            for (int x = 0; x < mapWidth; x++)
            {
                noiseMap[x, y] = Mathf.InverseLerp(minNoiseHeight, maxNoiseHeight, noiseMap[x, y]); 

            }
        }



        return noiseMap;

    }

}
