using UnityEngine;
using Random = UnityEngine.Random;

public class CollectionPositionAssign : MonoBehaviour
{
    [SerializeField] private Transform collectionParent;
    [SerializeField] private Transform[] collectionSpawnPoints;
    [SerializeField] private GameObject collectionPrefab;
    [SerializeField] private int spawnCollectionNum = 3;
    
    private void Awake()
    {
        CollectionSpawn();
    }

    private void CollectionSpawn()
    {
        ShuffleCollectionSpawnPoints();
        Transform[] spawnPoints = GetCollectionSpawnPoints(spawnCollectionNum);

        foreach (var spawnPoint in spawnPoints)
        {
            Instantiate(collectionPrefab, spawnPoint.position, Quaternion.identity, collectionParent);
        }
    }

    private void ShuffleCollectionSpawnPoints()
    {
        for (int i = 0; i < collectionSpawnPoints.Length - 1; i++)
        {
            int randomIndex = Random.Range(i, collectionSpawnPoints.Length);
            (collectionSpawnPoints[i], collectionSpawnPoints[randomIndex]) =
                (collectionSpawnPoints[randomIndex], collectionSpawnPoints[i]);
        }
    }

    private Transform[] GetCollectionSpawnPoints(int numRequired)
    {
        Transform[] result = new Transform[numRequired];
        int numChoose = numRequired;

        for (int numLeft = collectionSpawnPoints.Length; numLeft > 0; numLeft--)
        {
            float prob = (float)numChoose / numLeft;
            if (Random.value <= prob)
            {
                numChoose--;
                result[numChoose] = collectionSpawnPoints[numLeft - 1];
                if (numChoose == 0)
                {
                    break;
                }
            }
        }

        return result;
    }
}