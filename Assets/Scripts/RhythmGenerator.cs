using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhythmGenerator : MonoBehaviour
{
    [SerializeField] AudioPeer audioPeer;
    [SerializeField] GameObject rhythmPrefab;
    [SerializeField] GameObject dragPrefab;
    [SerializeField] List<Transform> lanes;

    [SerializeField] Material leftMaterial;
    [SerializeField] Material rightMaterial;
    [SerializeField] Material dragMaterial;

    bool spawnOffbeat;

    int spawnCount = 0;

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
        
    }

    // Update is called once per frame
    void Update()
    {
        // Don't spawn anything if the song is close to finishing.
        if (AudioPeer.mainSong.time > AudioPeer.mainSong.clip.length - BPM.beatInterval * 8)
            return;

        if (spawnOffbeat && BPM.beatD8)
        {
            spawnOffbeat = false;
            SpawnSphere(rhythmPrefab, Random.Range(0, 2), Random.Range(0, lanes.Count));
        }

        if (BPM.beatFull && BPM.beatCountFull % 2 == 0)
        {
            currentSpawn = ChooseSpawnOption();

            if (currentSpawn == SpawnChoices.Nothing)
                return;
            else if (currentSpawn == SpawnChoices.Quarter)
                SpawnSphere(rhythmPrefab, Random.Range(0, 2), Random.Range(0, lanes.Count));
            else if (currentSpawn == SpawnChoices.PullTogether)
            {
                SpawnDragSphere(Random.Range(0, lanes.Count / 2));
                SpawnDragSphere(Random.Range(lanes.Count / 2, lanes.Count));
            }
            else if (currentSpawn == SpawnChoices.TwoAtOnce)
            {
                SpawnSphere(rhythmPrefab, 0, Random.Range(0, lanes.Count / 2));
                SpawnSphere(rhythmPrefab, 1, Random.Range(lanes.Count / 2, lanes.Count));
            }
        }

        if (spawnCount >= int.MaxValue)
            spawnCount = 0;
    }

    void SpawnSphere(GameObject prefab, int leftOrRight, int lane)
    {
        GameObject go = Instantiate(prefab, lanes[lane].position, transform.rotation);

        if (leftOrRight == 1)
        {
            go.GetComponent<DancingBalls>().material = rightMaterial;
            go.GetComponent<DestroyOnAirTap>().hand = "Right";
            go.GetComponent<DestroyOnAirTap>().rightMaterial = rightMaterial;
        }
        else if (leftOrRight == 0)
        {
            go.GetComponent<DancingBalls>().material = leftMaterial;
            go.GetComponent<DestroyOnAirTap>().hand = "Left";
            go.GetComponent<DestroyOnAirTap>().leftMaterial = leftMaterial;
        }

        go.name = "RhythmSphere " + spawnCount.ToString();
        spawnCount++;
    }

    void SpawnDragSphere(int lane)
    {
        GameObject go = Instantiate(dragPrefab, lanes[lane].position, transform.rotation);
        go.GetComponent<DancingBalls>().material = dragMaterial;
        go.name = "DragSphere " + spawnCount.ToString();
        spawnCount++;
    }

    SpawnChoices ChooseSpawnOption()
    {
        if (audioPeer.amplitudeBuffer < .5f)
            return RollDice(new List<int>() { 40, 85, 90 });
        return RollDice(new List<int>() { 20, 75, 90 });
    }

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
