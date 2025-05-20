using UnityEngine;

public class ChunkManager : MonoBehaviour
{
    public static ChunkManager instance;
    [Header("Elements")]
    [SerializeField] private LevelManager[] levels;
    private GameObject finishLine;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }
    void Start()
    {
        GenerateLevels();

        finishLine = GameObject.FindWithTag("Finish");
    }

    private void GenerateLevels()
    {
        int currentLevel = GetLevels();
        currentLevel = currentLevel % levels.Length;
        LevelManager level = levels[currentLevel];

        CreateLevels(level.chunks);
    }

    private void CreateLevels(Chunk[] levelChunk)
    {
        Vector3 chunkPosition = Vector3.zero;
        for (int i = 0; i < levelChunk.Length; i++)
        {
            Chunk chunkToCreate = levelChunk[i];

            if (i > 0)
                chunkPosition.z += chunkToCreate.GetLenght() / 2;

            Chunk chunkInstance = Instantiate(chunkToCreate, chunkPosition, Quaternion.identity, transform);
            chunkPosition.z += chunkToCreate.GetLenght() / 2;
        }
    }

    //private void CreateRandomLevel()
    //{
    //    Vector3 chunkPosition = Vector3.zero;
    //    for (int i = 0; i < 5; i++)
    //    {
    //        Chunk chunkToCreate = chunkPrefab[Random.Range(0, chunkPrefab.Length)];

    //        if (i > 0)
    //            chunkPosition.z += chunkToCreate.GetLenght() / 2;

    //        Chunk chunkInstance = Instantiate(chunkToCreate, chunkPosition, Quaternion.identity, transform);
    //        chunkPosition.z += chunkToCreate.GetLenght() / 2;
    //    }
    //}

    public float GetFinishZ()
    {
        return finishLine.transform.position.z;
    }

    public int GetLevels()
    {
        return PlayerPrefs.GetInt("Levels", 0);
    }
}
