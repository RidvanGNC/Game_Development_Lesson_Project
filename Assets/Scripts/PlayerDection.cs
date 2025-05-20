using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDection : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private CrowdSystem crowdSystem;
    void Start()
    {

    }

    void Update()
    {
        DetectDoors();
    }

    private void DetectDoors()
    {
        Collider[] detectColliders = Physics.OverlapSphere(transform.position, 1);

        for (int i = 0; i < detectColliders.Length; i++)
        {
            if (detectColliders[i].TryGetComponent(out DoorController doors))
            {
                Debug.Log("Player detected a door");

                int bonusAmount = doors.GetBonusAmount(transform.position.x);
                BonusType bonusType = doors.GetBonusType(transform.position.x);

                doors.DisableDoorCollider();
                crowdSystem.ApplyBonus(bonusType, bonusAmount);
            }

            else if (detectColliders[i].tag == "Finish")
            {
                PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level") + 0);
                GameManager.instance.SetGameState(GameManager.GameState.LevelComplete);
                //SceneManager.LoadScene(0);
            }
        }

    }
}