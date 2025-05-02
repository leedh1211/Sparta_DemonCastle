using UI.Stack;
using UnityEngine;

public class TheStack : MonoBehaviour
{
    // Const Value
    private const float BoundSize = 3.5f;     
    private const float MovingBoundsSize = 3f;   
    private const float StackMovingSpeed = 5.0f; 
    private const float BlockMovingSpeed = 3.5f;  
    private const float ErrorMargin = 0.1f;        

    public GameObject originBlock = null;

    private Vector3 prevBlockPosition;
    private Vector3 desiredPosition;
    private Vector3 stackBounds = new Vector2(BoundSize, BoundSize);

    Transform lastBlock = null;
    float blockTransition = 0f;
    float secondaryPosition = 0f;

    int stackCount = 0;
    int score = 0;

    public Color prevColor;
    public Color nextColor;

    bool isMovingX = true;
    private bool isGameOver = true;

    void Start()
    {
        if(originBlock == null)
        {
            Debug.Log("OriginBlock is NULL");
            return;
        }

        prevColor = GetRandomColor();
        nextColor = GetRandomColor();

        prevBlockPosition = Vector3.down;
        Spawn_Block();
    }

    void Update()
    {
        if (isGameOver) return;

        if (Input.GetMouseButtonDown(0))
        {
            if(PlaceBlock())
            {
                Spawn_Block();
            }
            else
            {
                GameOver();
            }
        }

        MoveBlock();
        transform.position = Vector3.Lerp(transform.position, desiredPosition, StackMovingSpeed * Time.deltaTime);
    }

    bool Spawn_Block()
    {
        if(lastBlock != null)
            prevBlockPosition = lastBlock.localPosition;

        GameObject newBlock = Instantiate(originBlock);

        if(newBlock == null)
        {
            Debug.Log("NewBlock Instantiate Failed!");
            return false;
        }

        ColorChange(newBlock);

        Transform newTrans = newBlock.transform;
        newTrans.parent = this.transform;
        newTrans.localPosition = prevBlockPosition + Vector3.up;
        newTrans.localRotation = Quaternion.identity;
        newTrans.localScale = new Vector3(stackBounds.x, 1, stackBounds.y);

        stackCount++;
        desiredPosition = Vector3.down * stackCount;
        blockTransition = 0f;

        lastBlock = newTrans;
        isMovingX = !isMovingX;

        return true;
    }

    Color GetRandomColor()
    {
        return new Color(
            Random.Range(100f, 250f) / 255f,
            Random.Range(100f, 250f) / 255f,
            Random.Range(100f, 250f) / 255f
        );
    }

    void ColorChange(GameObject go)
    {
        Color applyColor = Color.Lerp(prevColor, nextColor, (stackCount % 11) / 10f);

        Renderer rn = go.GetComponent<Renderer>();
        if(rn == null) return;

        rn.material.color = applyColor;
        Camera.main.backgroundColor = applyColor - new Color(0.1f, 0.1f, 0.1f);

        if(applyColor.Equals(nextColor))
        {
            prevColor = nextColor;
            nextColor = GetRandomColor();
        }
    }

    void MoveBlock()
    {
        blockTransition += Time.deltaTime * BlockMovingSpeed;

        float movePosition = Mathf.PingPong(blockTransition, BoundSize) - BoundSize / 2;

        if (isMovingX)
            lastBlock.localPosition = new Vector3(movePosition * MovingBoundsSize, stackCount, secondaryPosition);
        else
            lastBlock.localPosition = new Vector3(secondaryPosition, stackCount, -movePosition * MovingBoundsSize);
    }

    bool PlaceBlock()
    {
        Vector3 lastPosition = lastBlock.localPosition;

        if (isMovingX)
        {
            float deltaX = prevBlockPosition.x - lastPosition.x;
            bool isNegative = deltaX < 0;
            deltaX = Mathf.Abs(deltaX);

            if (deltaX > ErrorMargin)
            {
                stackBounds.x -= deltaX;
                if (stackBounds.x <= 0) return GameOver();

                float middle = (prevBlockPosition.x + lastPosition.x) / 2;
                lastBlock.localScale = new Vector3(stackBounds.x, 1, stackBounds.y);
                lastPosition.x = middle;
                lastBlock.localPosition = lastPosition;

                float rubbleHalfScale = deltaX / 2f;
                CreateRubble(
                    new Vector3(
                        isNegative ? lastPosition.x + stackBounds.x / 2 + rubbleHalfScale
                                   : lastPosition.x - stackBounds.x / 2 - rubbleHalfScale,
                        lastPosition.y,
                        lastPosition.z),
                    new Vector3(deltaX, 1, stackBounds.y)
                );
            }
            else
            {
                lastBlock.localPosition = prevBlockPosition + Vector3.up;
            }
        }
        else
        {
            float deltaZ = prevBlockPosition.z - lastPosition.z;
            bool isNegative = deltaZ < 0;
            deltaZ = Mathf.Abs(deltaZ);

            if (deltaZ > ErrorMargin)
            {
                stackBounds.y -= deltaZ;
                if (stackBounds.y <= 0) return GameOver();

                float middle = (prevBlockPosition.z + lastPosition.z) / 2;
                lastBlock.localScale = new Vector3(stackBounds.x, 1, stackBounds.y);
                lastPosition.z = middle;
                lastBlock.localPosition = lastPosition;

                float rubbleHalfScale = deltaZ / 2f;
                CreateRubble(
                    new Vector3(
                        lastPosition.x,
                        lastPosition.y,
                        isNegative ? lastPosition.z + stackBounds.y / 2 + rubbleHalfScale
                                   : lastPosition.z - stackBounds.y / 2 - rubbleHalfScale),
                    new Vector3(stackBounds.x, 1, deltaZ)
                );
            }
            else
            {
                lastBlock.localPosition = prevBlockPosition + Vector3.up;
            }
        }

        secondaryPosition = isMovingX ? lastBlock.localPosition.x : lastBlock.localPosition.z;

        score++;
        // 게임 중 UI 점수 갱신
        StackUIManager.Instance?.StackGameUI?.SetUI(score);

        return true;
    }

    bool GameOver()
    {
        isGameOver = true;

        // 점수 UI 갱신
        StackUIManager.Instance?.StackGameOverUI?.SetUI(score);
        StackUIManager.Instance?.ChangeState(StackUIState.Score);

        return false;
    }

    void CreateRubble(Vector3 pos, Vector3 scale)
    {
        GameObject rubble = Instantiate(lastBlock.gameObject);
        rubble.transform.parent = this.transform;
        rubble.transform.localPosition = pos;
        rubble.transform.localScale = scale;
        rubble.transform.localRotation = Quaternion.identity;
        rubble.AddComponent<Rigidbody>();
        rubble.name = "Rubble";
    }

    public void Restart()
    {
        int childCount = transform.childCount;
        for(int i = 0; i < childCount; i++)
            Destroy(transform.GetChild(i).gameObject);

        isGameOver = false;
        lastBlock = null;
        desiredPosition = Vector3.zero;
        stackBounds = new Vector3(BoundSize, BoundSize);
        stackCount = 0;
        score = 0;

        isMovingX = true;
        blockTransition = 0f;
        secondaryPosition = 0f;

        prevBlockPosition = Vector3.down;
        prevColor = GetRandomColor();
        nextColor = GetRandomColor();
        
        Spawn_Block();
    }
}
