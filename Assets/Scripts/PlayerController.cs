using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    [Header("Setting")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private int roadWidth;
    [SerializeField] private bool canMove;
    [SerializeField] private PlayerAnimatior playerAnimatior;
    [Header("Control")]
    [SerializeField] private float slidSpeed;
    private Vector3 clickScreenPosition;
    private Vector3 clickPlayerPosition;
    [SerializeField] private CrowdSystem crowdSystem;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        GameManager.OnGameStateChanged += GameStateChangeCallback;
    }

    void OnDestroy()
    {
        GameManager.OnGameStateChanged -= GameStateChangeCallback;
    }

    private void FixedUpdate()
    {
        if (canMove)
        {
            MoveSpeedForward();
            ManageControl();
        }
    }

    private void GameStateChangeCallback(GameManager.GameState gameState)
    {
        if(gameState == GameManager.GameState.Game)
        {
            StartMoving();
        }
        else if (gameState == GameManager.GameState.GameOver)
        {
            StopMoving();
        }
        else if (gameState == GameManager.GameState.LevelComplete)
        {
            StopMoving();
        }
    }

    public void StartMoving()
    {
        canMove = true;
        playerAnimatior.Run();
    }

    public void StopMoving()
    {
        canMove = false;
        playerAnimatior.Idle();
    }

    private void MoveSpeedForward()
    {
        transform.position += Vector3.forward * moveSpeed * Time.deltaTime;
    }

    private void ManageControl()
    {
        if (Input.GetMouseButtonDown(0))
        {
            clickScreenPosition = Input.mousePosition;
            clickPlayerPosition = transform.position;
        }
        else if (Input.GetMouseButton(0))
        {
            float xScreenDifference = Input.mousePosition.x - clickScreenPosition.x;

            xScreenDifference /= Screen.width;
            xScreenDifference *= slidSpeed;

            Vector3 position = transform.position;
            position.x = clickPlayerPosition.x + xScreenDifference;

            position.x = Mathf.Clamp(position.x, -roadWidth / 2 + crowdSystem.GetCrowdRadius(), roadWidth / 2 - crowdSystem.GetCrowdRadius());

            transform.position = position;

            //transform.position = clickPlayerPosition + Vector3.right * xScreenDifference;
        }
    }
}
