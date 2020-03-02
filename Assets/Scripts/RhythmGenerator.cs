using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Spawn random types of enemies based off the bpm of the audio.
/// </summary>
public class RhythmGenerator : MonoBehaviour
{
    [SerializeField] AudioPeer audioPeer;
    [SerializeField] GameObject rhythmPrefab;
    [SerializeField] GameObject dragPrefab;
    [SerializeField] List<Transform> lanes;

    [SerializeField] Material leftMaterial;
    [SerializeField] Material rightMaterial;
    [SerializeField] Material dragMaterial;

    int spawnCount = 0;

    int numOfSpheres = 10;
    int currentSphere = 0;
    int currentDragSphere = 0;

    List<GameObject> spherePool = new List<GameObject>();
    List<GameObject> dragPool = new List<GameObject>();

    enum SpawnChoices
    {
        Nothing,
        Quarter,
        TwoAtOnce,
        PullTogether
    };

    SpawnChoices currentSpawn;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize Object Pools
        for (int i = 0; i < numOfSpheres; i++)
        {
            CreateAndAddToPool(rhythmPrefab, spherePool, i);
            CreateAndAddToPool(dragPrefab, dragPool, i);
        }
    }

    /// <summary>
    /// Instantiate object and add it to appropriate object pool.
    /// </summary>
    /// <param name="prefab"></param>
    /// <param name="pool"></param>
    /// <param name="num"></param>
    void CreateAndAddToPool(GameObject prefab, List<GameObject> pool, int num)
    {
        GameObject go = GameObject.Instantiate(prefab);
        go.SetActive(false);
        go.name = prefab.name + num.ToString();
        pool.Add(go);
    }

    // Update is called once per frame
    void Update()
    {
        // Don't spawn anything if the song is close to finishing.
        if (AudioPeer.mainSong.time > AudioPeer.mainSong.clip.length - BPM.beatInterval * 8)
            return;

        // Every 2 beats choose a spawn option and execute it.
        if (BPM.beatFull && BPM.beatCountFull % 2 == 0)
        {
            currentSpawn = ChooseSpawnOption();

            if (currentSpawn == SpawnChoices.Nothing)
                return;
            else if (currentSpawn == SpawnChoices.Quarter)
                SpawnSphere(Random.Range(0, 2), Random.Range(0, lanes.Count), spherePool, currentSphere);
            else if (currentSpawn == SpawnChoices.PullTogether)
            {
                SpawnSphere(2, Random.Range(0, lanes.Count / 2), dragPool, currentDragSphere);
                SpawnSphere(2, Random.Range(lanes.Count / 2, lanes.Count), dragPool, currentDragSphere);
            }
            else if (currentSpawn == SpawnChoices.TwoAtOnce)
            {
                SpawnSphere(0, Random.Range(0, lanes.Count / 2), spherePool, currentSphere);
                SpawnSphere(1, Random.Range(lanes.Count / 2, lanes.Count), spherePool, currentSphere);
            }
        }

        if (spawnCount >= int.MaxValue)
            spawnCount = 0;
    }

    /// <summary>
    /// Activate the current sphere in sphere pool. Also set the position and material accordingly.
    /// </summary>
    /// <param name="prefab"></param>
    /// <param name="leftOrRight"></param>
    /// <param name="lane"></param>
    void SpawnSphere(int sphereType, int lane, List<GameObject> pool, int poolIdx)
    {
        GameObject go = pool[poolIdx % pool.Count];
        go.transform.position = lanes[lane].position;
        go.transform.rotation = transform.rotation;


        if (sphereType == 2) // Configure for draggable sphere
        {
            go.GetComponent<DancingBalls>().material = dragMaterial;
            currentDragSphere++;
        }
        else if (sphereType == 1) // Configure for 'Right' sphere
        {
            go.GetComponent<DancingBalls>().material = rightMaterial;
            go.GetComponent<DestroyOnAirTap>().hand = "Right";
            go.GetComponent<DestroyOnAirTap>().rightMaterial = rightMaterial;
            currentSphere++;
        }
        else if (sphereType == 0) // Configure for 'Left' sphere
        {
            go.GetComponent<DancingBalls>().material = leftMaterial;
            go.GetComponent<DestroyOnAirTap>().hand = "Left";
            go.GetComponent<DestroyOnAirTap>().leftMaterial = leftMaterial;
            currentSphere++;
        }

        go.SetActive(true);
        go.GetComponent<RhythmMove>().enabled = true;
        go.GetComponent<Collider>().enabled = true;
    }

/*    void SpawnDragSphere(int lane)
    {
        GameObject go = dragPool[currentDragSphere % dragPool.Count];
        go.transform.position = lanes[lane].position;
        go.transform.rotation = transform.rotation;
        go.GetComponent<DancingBalls>().material = dragMaterial;
        go.GetComponent<RhythmMove>().enabled = true;
        currentDragSphere++;
        go.SetActive(true);
    }*/

    /// <summary>
    /// Randomly select a spawn choice from spawn choices enum.
    /// </summary>
    /// <returns></returns>
    SpawnChoices ChooseSpawnOption()
    {
        if (audioPeer.amplitudeBuffer < .5f)
            return RollDice(new List<int>() { 40, 85, 90 });
        return RollDice(new List<int>() { 20, 75, 90 });
    }


    /// <summary>
    /// Chooses a random spawn choice based off threshold weights passed in.
    /// TODO: Currently hardcoding weights and selections. Might want to make this more modular.
    /// </summary>
    /// <param name="thresholds"></param>
    /// <returns></returns>
    SpawnChoices RollDice(List<int> thresholds)
    {
        int roll = Random.Range(0, 100);

        if (roll > 0 && roll <= thresholds[0])
            return SpawnChoices.Nothing;
        else if (roll <= thresholds[1])
            return SpawnChoices.Quarter;
        else if (roll <= thresholds[2])
            return SpawnChoices.TwoAtOnce;
        return SpawnChoices.PullTogether;
    }
}
