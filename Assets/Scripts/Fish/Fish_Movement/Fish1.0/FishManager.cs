using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishManager : MonoBehaviour
{
    const int threadGroupSize = 1024;

    public FishSettings settings;
    public ComputeShader compute;//A Compute Shader is a Shader Stage that is used entirely for computing arbitrary information
    Fish_IA[] tabFish;
    void Start()
    {
        //on remplis le tableau avec tous les poissons trouvés.
        tabFish = FindObjectsOfType<Fish_IA>();

        foreach (Fish_IA f in tabFish)
        {
            f.Initialize(settings, null);
        }

    }

    void Update()
    {
        if(tabFish!=null)
        {
            int numFish = tabFish.Length;
            var fishData = new FishData[numFish]; // on créer une structure de data pour gérer les poissons par la suite 

            for (int i = 0; i < tabFish.Length; i++) // on replis les information de la struct.
            {
                fishData[i].position = tabFish[i].position;
                fishData[i].direction = tabFish[i].forward;
            }
            var fishBuffer = new ComputeBuffer(numFish, FishData.Size);
            fishBuffer.SetData(fishData);

            compute.SetBuffer(0, "fish", fishBuffer);
            compute.SetInt("numFish", tabFish.Length);
            compute.SetFloat("viewRadius", settings.perceptionRadius);
            compute.SetFloat("avoidRadius", settings.avoidanceRadius);

            int threadGroups = Mathf.CeilToInt(numFish / (float)threadGroupSize);
            compute.Dispatch(0, threadGroups, 1, 1);

            fishBuffer.GetData(fishData);

            for (int i = 0; i < tabFish.Length; i++)
            {
                tabFish[i].averageFlockHeading = fishData[i].flockHeading;
                tabFish[i].centreOfFlockMates = fishData[i].flockCentre;
                tabFish[i].averageAvoidanceHeading = fishData[i].avoidanceHeading;
                tabFish[i].numPerceivedFlockMates = fishData[i].numFlockmates;

                tabFish[i].UpdateFish();
            }

            fishBuffer.Release();
        }
    }


    public struct FishData
    {
        public Vector3 position;
        public Vector3 direction;

        public Vector3 flockHeading;
        public Vector3 flockCentre;
        public Vector3 avoidanceHeading;
        public int numFlockmates;

        public static int Size
        {
            get
            {
                return sizeof(float) * 3 * 5 + sizeof(int);
            }
        }
    }
}
