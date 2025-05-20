using TMPro;
using UnityEngine;
public enum BonusType
{
    Addiction, Difference, Production, Division
}

public class DoorController : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private TextMeshPro rightDoorText;
    [SerializeField] private TextMeshPro leftDoorText;
    [SerializeField] private new Collider collider;

    [Header("Setting")]
    [SerializeField] private BonusType rightDoorBonusType;
    [SerializeField] private int rightDoorBonusValue;

    [SerializeField] private BonusType leftDoorBonusType;
    [SerializeField] private int leftDoorBonusValue;
    void Start()
    {
        ConfigureDoors();
    }

    void Update()
    {

    }

    private void ConfigureDoors()
    {
        switch (rightDoorBonusType)
        {
            case BonusType.Addiction:
                rightDoorText.text = "+" + rightDoorBonusValue.ToString();
                break;
            case BonusType.Difference:
                rightDoorText.text = "-" + rightDoorBonusValue.ToString();
                break;
            case BonusType.Production:
                rightDoorText.text = "x" + rightDoorBonusValue.ToString();
                break;
            case BonusType.Division:
                rightDoorText.text = "/" + rightDoorBonusValue.ToString();
                break;
        }

        switch (leftDoorBonusType)
        {
            case BonusType.Addiction:
                leftDoorText.text = "+" + leftDoorBonusValue.ToString();
                break;
            case BonusType.Difference:
                leftDoorText.text = "-" + leftDoorBonusValue.ToString();
                break;
            case BonusType.Production:
                leftDoorText.text = "x" + leftDoorBonusValue.ToString();
                break;
            case BonusType.Division:
                leftDoorText.text = "/" + leftDoorBonusValue.ToString();
                break;
        }
    }

    public int GetBonusAmount(float xPosition)
    {
        if (xPosition > 0)
        {
            return rightDoorBonusValue;
        }
        else
        {
            return leftDoorBonusValue;
        }
    }

    public BonusType GetBonusType(float xPosition)
    {
        if (xPosition > 0)
        {
            return rightDoorBonusType;
        }
        else
        {
            return leftDoorBonusType;
        }
    }

    public void DisableDoorCollider()
    {
        collider.enabled = false;
    }
}
