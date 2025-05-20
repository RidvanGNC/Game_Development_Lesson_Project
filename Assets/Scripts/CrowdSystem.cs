using UnityEngine;

public class CrowdSystem : MonoBehaviour
{
    [Header("Eleements")]
    [SerializeField] private PlayerAnimatior playerAnimatior;
    [SerializeField] private Transform runnerParent;
    [SerializeField] private GameObject runnerPrefab;

    [Header("Setting")]
    [SerializeField] private float radius;
    [SerializeField] private float angel;
    void Start()
    {

    }

    void FixedUpdate()
    {
        if (!GameManager.instance.isGameState())
            return;

        PlaceRunner();

        if(runnerParent.childCount <= 0)
        {
            GameManager.instance.SetGameState(GameManager.GameState.GameOver);
        }
    }

    private void PlaceRunner()
    {
        for (int i = 0; i < runnerParent.childCount; i++)
        {
            Vector3 childLocalPosition = PlayerRunnerLocalPosition(i);
            runnerParent.GetChild(i).localPosition = childLocalPosition;
        }
    }

    private Vector3 PlayerRunnerLocalPosition(int index)
    {
        float x = radius * Mathf.Sqrt(index) * Mathf.Cos(Mathf.Deg2Rad * index * angel);
        float z = radius * Mathf.Sqrt(index) * Mathf.Sin(Mathf.Deg2Rad * index * angel);
        return new Vector3(x, 0, z);
    }

    public float GetCrowdRadius()
    {
        return radius * Mathf.Sqrt(runnerParent.childCount);
    }

    public void ApplyBonus(BonusType bonusType, int bonusAmount)
    {
        switch (bonusType)
        {
            case BonusType.Addiction:
                AddRunners(bonusAmount);
                break;

            case BonusType.Production:
                int runnerToAdd = (runnerParent.childCount * bonusAmount) - runnerParent.childCount;
                AddRunners(runnerToAdd);
                break;

            case BonusType.Difference:
                RemoveRunners(bonusAmount);
                break;

            case BonusType.Division:
                int runnerToRemove = runnerParent.childCount - (runnerParent.childCount / bonusAmount);
                RemoveRunners(runnerToRemove);
                break;
        }
    }

    private void AddRunners(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            Instantiate(runnerPrefab, runnerParent);
            playerAnimatior.Run();
        }
    }

    public void RemoveRunners(int amount)
    {
        if(amount >= runnerParent.childCount)
        {
            amount = runnerParent.childCount;
        }
        int runnerAmount = runnerParent.childCount;

        for (int i = runnerAmount - 1; i >= runnerAmount - amount; i--)
        {
            Transform runnerToDestroy = runnerParent.GetChild(i);
            runnerToDestroy.SetParent(null);
            Destroy(runnerToDestroy.gameObject);
        }
    }
}
