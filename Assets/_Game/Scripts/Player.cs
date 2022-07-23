using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody Rb;
    public Transform PlayerTrans;
    public Collider Col;
    public Transform StackRootTrans;
    private Vector3 moveDir;
    private Vector2 mouseDownPos;
    private Vector2 mouseCurrentPos;
    private bool isMove;
    private float moveSpeed = 1f;
    protected Quaternion rotation;
    public Quaternion BrickRotation;
    public Animator anim;
    [SerializeField]
    private float inputThreshold = 150f * 150f; //NOTE: 150 pixel threshold compare to sqrMag Vector3
    private Vector2 screenMoveDir;
    public Stack<GameObject> BrickStack = new Stack<GameObject>();
    protected Quaternion StackRootLocalRotation;
    private bool isInputLock;
    public PrefabManager.ObjectType BrickTag;
    public PrefabManager.ObjectType StairTag;
    public LevelManager.Level_Stage LevelStage;

    protected void Start()
    {
        StackRootLocalRotation = StackRootTrans.localRotation;
    }
    protected virtual void FixedUpdate()
    {
        Move();
    }
    private void Move()
    {
        GetMoveDir();
        GetCharacterRotation();
        GetBrickRotation();
        if (isMove && !isInputLock)
        {
            Rb.constraints = ConstValues.RB_PLAYER_MOVE_CONSTRAINTS;
            PlayerTrans.position = Vector3.MoveTowards(PlayerTrans.position, PlayerTrans.position + moveDir, Time.deltaTime * moveSpeed);
            PlayerTrans.rotation = rotation;
        }
        else
        {
            Rb.constraints = ConstValues.RB_PLAYER_STAY_CONSTRAINTS;
        }
    }
    private void GetMoveDir()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mouseDownPos = Input.mousePosition;
        }

        if (Input.GetMouseButton(0))
        {
            mouseCurrentPos = Input.mousePosition;
            screenMoveDir = mouseCurrentPos - mouseDownPos;
            if (screenMoveDir.sqrMagnitude > inputThreshold)
            {
                isMove = true;
                anim.SetFloat(ConstValues.PLAYER_ANIM_VELOCITY, 1);
            }
            else
            {
                isMove = false;
                anim.SetFloat(ConstValues.PLAYER_ANIM_VELOCITY, 0);
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            isMove = false;
            anim.SetFloat(ConstValues.PLAYER_ANIM_VELOCITY, 0);
        }

        if (isMove)
        {
            moveDir = new Vector3(screenMoveDir.x, 0, screenMoveDir.y).normalized;
        }
    }
    private void GetCharacterRotation()
    {
        float tmp = Mathf.Atan2(moveDir.x, moveDir.z) * Mathf.Rad2Deg;
        rotation = Quaternion.Lerp(rotation, Quaternion.Euler(0, tmp, 0), Time.deltaTime * 5);
    }

    private void GetBrickRotation()
    {
        BrickRotation = rotation * StackRootLocalRotation;
    }
    public bool MinusBrick()
    {
        if (BrickStack.Count > 0)
        {
            GameObject obj = BrickStack.Peek();
            BrickStack.Pop();
            StackRootTrans.position -= StackRootTrans.up * Brick.brickHeight;
            PrefabManager.Instance.PushToPool(BrickTag, obj);

            //NOTE: spawn new brick if a brick turn into bridge brick
            LevelManager.Instance.SpawnObject(LevelManager.Instance.spawnLocations[LevelStage], BrickTag);

            return true;
        }
        return false;
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag(ConstValues.TAG_PLAYER))
        {
            Player _player = other.collider.GetComponent<Player>();

            if (_player != null)
            {
                HitPlayer(_player);
            }
        }
    }
    private void HitPlayer(Player _player)
    {
        int tmp = BrickStack.Count - _player.BrickStack.Count;


        if (tmp == 0) return;
        if (tmp < 0 && !IsOnBridge())
        {
            TriggerFall();
            DropBrick();
        }

    }
    protected virtual void TriggerFall()
    {
        StartCoroutine(Fall()); 
    }
    private void DropBrick()
    {
        int tmp = BrickStack.Count;

        for (int i = 0; i < tmp; i++)
        {
            GameObject obj = BrickStack.Peek();
            Transform objTrans = obj.transform;

            BrickStack.Pop();
            StackRootTrans.position -= StackRootTrans.up * Brick.brickHeight;

            //NOTE: spawn new brick if a brick is turn into grayBrick
            LevelManager.Instance.SpawnObject(LevelManager.Instance.spawnLocations[LevelStage], BrickTag);

            PrefabManager.Instance.PopFromPool(PrefabManager.ObjectType.GrayBrick, objTrans.position, objTrans.rotation);
            PrefabManager.Instance.PushToPool(BrickTag, obj);
        }
    }
    private bool IsOnBridge()
    {
        Debug.DrawRay(PlayerTrans.position + Vector3.up, Vector3.down, Color.red, 10f);

        if (Physics.Raycast(PlayerTrans.position + Vector3.up, Vector3.down, 10f, ConstValues.LAYER_MASK_GROUND))
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    private IEnumerator Fall()
    {
        isInputLock = true;
        Col.enabled = false;
        anim.SetTrigger(ConstValues.PLAYER_ANIM_FALL);
        yield return new WaitForSeconds(ConstValues.VALUE_TIME_OF_FALL_ANIM); //NOTE: ~ time of fall plus kipup animation
        isInputLock = false;
        Col.enabled = true;
    }
}
