using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed, digSpeed;
    [SerializeField]
    private int energy;
    private int maxEnergy;
    [SerializeField]
    private int digStrength = 1;
    private float gridSize = 1;

    private ActionState currentState = ActionState.Idle;

    [SerializeField]
    private Animator animator;
    [SerializeField]
    private AnimationClip moveClip;

    private WorldManager worldManager;
    private PlayerSpriteManager spriteManager;
    private PlayerAudioController audioManager;

    private PlayerUpgrader playerUpgrader;

    //For Debugging
    private int originalEnergy, originalDigStrength;
    private float originalMoveSpeed, originalDigSpeed;

    public static Action onSurface, onEnterShop, onExitShop, onOpenMenu;
    public static Action<PlayerUpgrade> onFailedUpgrade, onSuccessfulUpgrade;
    public static Action<int, int> onChangeEnergy;

    private void Start()
    {
        SetDigSpeed(digSpeed);
        maxEnergy = energy;

        animator = GetComponent<Animator>();
        animator.SetFloat("MoveSpeed", 1 / moveSpeed);
        animator.SetFloat("DigSpeed", 1 / digSpeed);

        worldManager = FindFirstObjectByType<WorldManager>();

        spriteManager = GetComponent<PlayerSpriteManager>();

        audioManager = GetComponent<PlayerAudioController>();

        playerUpgrader = GetComponent<PlayerUpgrader>();
        playerUpgrader.Init(this);

        originalDigStrength = digStrength;
        originalEnergy = energy;
        originalDigSpeed = digSpeed;
        originalMoveSpeed = moveSpeed;
    }

    private void OnEnable()
    {
        ShopButtons.onCloseShopUI += ExitMenu;
    }
    private void OnDisable()
    {
        ShopButtons.onCloseShopUI -= ExitMenu;
    }

    private void Update()
    {
        if (currentState == ActionState.InMenu)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                ExitMenu();
            }
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            OpenMenu();
        }
    }

    private void OpenMenu()
    {
        //Add menu
        currentState = ActionState.InMenu;
        onOpenMenu?.Invoke();
        //Load Main Menu
        SceneManager.LoadScene(0);
    }

    private void FixedUpdate()
    {
        if (currentState != ActionState.Idle)
        {
            return;
        }

        Vector2 input = GetMovementInput();
        if (input == Vector2.zero)
        {
            return;
        }

        spriteManager.SetSpriteDirection(input);
        if (CheckForBlock(input))
        {
            if(energy > 0)
            {
                StartCoroutine(Dig(input));
            }
            else
            {
                //No energy
            }
        }
        else if(GetInWorldBounds((Vector2)transform.position + input))
        {
            StartCoroutine(Move(input));
        }
    }

    private Vector2 GetMovementInput()
    {
        if (Input.GetKey(KeyCode.W)) return Vector2.up;
        if (Input.GetKey(KeyCode.S)) return Vector2.down;
        if (Input.GetKey(KeyCode.D)) return Vector2.right;
        if (Input.GetKey(KeyCode.A)) return Vector2.left;
        return Vector2.zero;
    }

    private IEnumerator Move(Vector2 moveDir)
    {
        Vector2 startPosition = transform.position;
        Vector2 endPosition = startPosition + (moveDir * gridSize);
        
        currentState = ActionState.Moving;
        animator.SetBool("isMoving", true);

        float elapsedTime = 0;
        while (elapsedTime < moveSpeed)
        {
            elapsedTime += Time.deltaTime;
            float percent = elapsedTime / moveSpeed;
            transform.position = Vector2.Lerp(startPosition, endPosition, percent);
            yield return null;
        }

        transform.position = endPosition;
        if(moveDir == Vector2.up)
        {
            if(transform.position.y == worldManager.worldHeight)
            {
                Surface();
            }
        }

        currentState = ActionState.Idle;
        animator.SetBool("isMoving", false);

        Vector2Int pos = Vector2ToVectorInt(endPosition);
        if(worldManager.GetTileTypeAt(pos) == GridBlockType.Shop)
        {
            onEnterShop?.Invoke();
            worldManager.EnterShopAt(pos);
            currentState = ActionState.InMenu;
        }
    }

    private IEnumerator Dig(Vector2 moveDir)
    {
        currentState = ActionState.Digging;
        animator.SetBool("isDigging", true);

        Vector2 pos = (Vector2)transform.position + moveDir;
        Vector2Int targetPos = Vector2ToVectorInt(pos);

        SetEnergy(energy - 1);
        PlayDrillAudio();

        float elapsedTime = 0;
        while (elapsedTime < digSpeed)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        //Attack block
        GameObject targetBlock = worldManager.mapGrid.GetCell(targetPos);
        targetBlock.GetComponent<Tile>().Dig(digStrength);

        currentState = ActionState.Idle;
        animator.SetBool("isDigging", false);
    }

    private bool CheckForBlock(Vector2 dir)
    {
        Vector2 endPos = (Vector2)transform.position + dir;
        Vector2Int targetPos = Vector2ToVectorInt(endPos);

        GameObject targetBlock = worldManager.mapGrid.GetCell(targetPos);
        if (targetBlock)
        {
            return true;
        }
        return false;
    }

    public void ExitMenu()
    {
        currentState = ActionState.Idle;
        onExitShop?.Invoke();
    }

    private void Surface()
    {
        onSurface?.Invoke();
        SetEnergy(maxEnergy);
    }
    
    private void SetEnergy(int newEnergy)
    {
        energy = newEnergy;
        onChangeEnergy?.Invoke(energy, maxEnergy);
    }

    private void SetDigSpeed(float speed)
    {
        if (speed == 0)
        {
            speed = 0.00001f;
        }
        animator.SetFloat("DigSpeed", 1 / speed);
        digSpeed = speed;
    }

    private void SetDigStrength(int newStrength)
    {
        digStrength = newStrength;
    }

    private void SetMoveSpeed(float speed)
    {
        if (speed == 0)
        {
            speed = 0.00001f;
        }
        animator.SetFloat("MoveSpeed", 1 / speed);
        moveSpeed = speed;
    }

    private bool GetInWorldBounds(Vector2 pos)
    {
        if (!worldManager)
        {
            return true;
        }
        Vector2 worldSize = new Vector2(worldManager.worldWidth, worldManager.worldHeight);
        if(pos.x < 0 || pos.y < 0)
            return false;
        if(pos.x >= worldSize.x || pos.y > worldSize.y)
        {
            return false;
        }
        return true;
    }

    public void UpgradeDrill()
    {
        SetDigStrength(digStrength + 2);
    }

    public void UpgradeEnergy()
    {
        maxEnergy += 5;
        SetEnergy(maxEnergy);
    }

    public void UpgradeSpeed()
    {
        SetDigSpeed(digSpeed * 0.9f);
        SetMoveSpeed(moveSpeed * 0.9f);
    }

    public void UpgradeReset()
    {
        maxEnergy = originalEnergy;
        SetEnergy(maxEnergy);

        SetDigStrength(originalDigStrength);

        SetMoveSpeed(originalMoveSpeed);
        SetDigSpeed(originalDigSpeed);
    }
    
    public void FailedUpgrade(PlayerUpgrade upgrade)
    {
        onFailedUpgrade?.Invoke(upgrade);
    }

    public void SuccessfulUpgrade(PlayerUpgrade upgrade)
    {
        onSuccessfulUpgrade?.Invoke(upgrade);
    }

    private Vector2Int Vector2ToVectorInt(Vector2 vector)
    {
        return new Vector2Int(Mathf.RoundToInt(vector.x), Mathf.RoundToInt(vector.y));
    }

    private void PlayDrillAudio()
    {
        audioManager.PlayDrillAudio();
    }

    public void PickUpItemAnimation(InventoryItemSO item)
    {
        spriteManager?.PickUpAnimation(item);
    }
}