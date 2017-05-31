using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class NoiseMapGen {
    //Public Method
    public static float[,] GenerateNoiseMap(int mapWidth, int mapHeight, float a, float b, float c, float d)
    {
        float[,] noiseMap = new float[mapWidth, mapHeight];
        int stepSize = mapWidth - 1;
        float max = 3f;
        float min = -3f;
        float maxHeight = 10;

        noiseMap = setUpArray(noiseMap, a, b, c, d, mapWidth-1, mapHeight-1);

        while (stepSize>1)
        {
            noiseMap = squareStep(noiseMap, mapWidth, mapHeight, stepSize, max, min);
            noiseMap = diamondStep(noiseMap, mapWidth, mapHeight, stepSize, max, min);
            if (stepSize == 1)
                return noiseMap;
            stepSize = stepSize / 2;
            /*if (max != 1)
                max = max-1;*/
            max = max / 2;
            min = min/2;
        }
        return noiseMap;
    }

    //Private Methods
    private static float[,] setUpArray(float[,] dt, float a, float b, float c, float d, int width, int height)
    {
        float[,] mapArray = dt;


        //set up map corners (Seed)
        mapArray[0, 0] = a;
        mapArray[0, height] = b;
        mapArray[width, 0] = c;
        mapArray[width, height] = d;

        return mapArray;
    }
    private static float[,] squareStep(float[,] dt, int mapWidth, int mapHeight, int stepSize, float m, float mini)
    {
        float[,] data = dt;
        float average = 0;
        float max = m;
        float min = mini;
        float temp = Random.Range(min, max);
        float final;

        for (int x = 0; x < mapHeight;)
        {
            for (int y = 0; y < mapWidth;)
            {
                if (x + stepSize < mapHeight && y + stepSize < mapWidth)
                {
                    average = (data[x, y] + data[x + stepSize,y] + data[x ,y + stepSize] + data[x + stepSize,y + stepSize]) / 4;
                    final = average + temp;
                    if (average + temp > 20)
                        final = 20;
                    data[x + stepSize / 2, y + stepSize / 2] = final;
                    temp = Random.Range(min, max);
                }
                y = y + stepSize;
            }
            x = x + stepSize;
        }

        return data;
    }
    private static float[,] diamondStep(float[,] dt, int mapWidth, int mapHeight, int stepSize, float m, float mini)
    {
        float[,] data = dt;
        float average = 0;
        float max = m;
        float min = mini;
        float temp = Random.Range(min, max);
        float final;

        for (int x = 0; x < mapHeight;)
        {
            for (int y = 0; y < mapWidth;)
            {
                if (x + stepSize < mapHeight)
                {
                    //temp = ran.nextInt(5);
                    if (y - stepSize >= 0 && y + stepSize < mapWidth)
                    {
                        average = (data[x, y] + data[x + stepSize / 2, y + stepSize / 2] + data[x, y + stepSize] + data[x, y - stepSize]) / 4;
                        final = average + temp;
                        if (final > 20)
                            final = 20;
                        data[x + stepSize / 2, y] = final;
                        temp = Random.Range(min, max);
                    }
                    else if (y - stepSize >= 0)
                    {
                        average = (data[x, y] + data[x + stepSize, y] + data[x + stepSize / 2, y - stepSize / 2]) / 3;
                        final = average + temp;
                        if (final > 20)
                            final = 20;
                        data[x + stepSize / 2, y] = final;
                        temp = Random.Range(min, max);
                    }
                    else if (y + stepSize < mapWidth)
                    {
                        average = (data[x, y] + data[x + stepSize, y] + data[x + stepSize / 2, y + stepSize / 2]) / 3;
                        final = average + temp;
                        if (final > 20)
                            final = 20;
                        data[x + stepSize / 2, y] = final;
                        temp = Random.Range(min, max);
                    }
                }
                if (y + stepSize < mapWidth)
                {
                    //temp = ran.nextInt(5);
                    if (x - stepSize >= 0 && x + stepSize < mapHeight)
                    {
                        average = (data[x, y] + data[x, y + stepSize] + data[x + stepSize / 2, y + stepSize / 2] + data[x - stepSize / 2, y + stepSize / 2]) / 4;
                        final = average + temp;
                        if (final > 20)
                            final = 20;
                        data[x, y + stepSize / 2] = final;
                        temp = Random.Range(min, max);
                    }
                    else if (x - stepSize >= 0)
                    {
                        average = (data[x, y] + data[x, y + stepSize] + data[x - stepSize / 2, y + stepSize / 2]) / 3;
                        final = average + temp;
                        if (final > 20)
                            final = 20;
                        data[x, y + stepSize / 2] = final;
                        temp = Random.Range(min, max);
                    }
                    else if (x + stepSize < mapHeight)
                    {
                        average = (data[x, y] + data[x, y + stepSize] + data[x + stepSize / 2, y + stepSize / 2]) / 3;
                        final = average + temp;
                        if (final > 20)
                            final = 20;
                        data[x, y + stepSize / 2] = final;
                        temp = Random.Range(min, max);
                    }
                }
                y = y + stepSize;
            }
            x = x + stepSize;
        }
        return data;
    }
}
