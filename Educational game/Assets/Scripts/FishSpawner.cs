using UnityEngine;

public class FishSpawner : MonoBehaviour
{
    public float spawnInterval = 2f;
    public float minY = -3f, maxY = 3f;
    private float timer;

    [Range(0f, 1f)]
    public float puzzleLetterChance = 0.8f; // 80% of spawns will be from target word

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            SpawnFish();
            timer = 0f;
        }
    }

    void SpawnFish()
    {
        // Get fish from pool
        GameObject fish = ObjectPooler.Instance.GetPooledFish();

        // Random Y position
        float yPos = Random.Range(minY, maxY);

        // Random direction (left or right)
        int dir = Random.value > 0.5f ? 1 : -1;

        // Init fish
        FishSwim3D swim = fish.GetComponent<FishSwim3D>();
        swim.Init(dir, yPos);

        // Pick a letter
        char letter;
        if (Random.value <= puzzleLetterChance && WordPuzzleManager.Instance != null)
        {
            // Choose from target word
            string target = WordPuzzleManager.Instance.GetTargetWord();
            letter = target[Random.Range(0, target.Length)];
        }
        else
        {
            // Random distractor A–Z
            letter = (char)('A' + Random.Range(0, 26));
        }

        fish.GetComponent<FishLetter>().SetLetter(letter);
    }
}
