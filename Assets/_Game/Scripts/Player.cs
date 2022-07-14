using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody Rb;
    public Transform PlayerTrans;
    public Transform StackRootTrans;
    private Vector3 moveDir;
    private Vector2 mouseDownPos;
    private Vector2 mouseCurrentPos;
    private bool isMove;
    private float moveSpeed = 1f;
    private Quaternion rotation;
    public Quaternion BrickRotation;
    public Animator anim;
    [SerializeField]
    private float inputThreshhold = 150f * 150f;
    private Vector2 screenMoveDir;
    public Stack<GameObject> BrickStack = new Stack<GameObject>();
    private Quaternion StackRootLocalRotation;
    private bool isInputLock;

    private void Start()
    {
        StackRootLocalRotation = StackRootTrans.localRotation;
    }
#if UNITY_EDITOR
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            StartCoroutine(Fall());
        }
    }
#endif
    private void FixedUpdate()
    {
        Move();
    }
    // private void Move()
    // {
    //     GetMoveDir();
    //     GetCharacterRotation();
    //     GetBrickRotation();
    //     if (isMove && !isInputLock)
    //     {
    //         Rb.velocity = moveDir * moveSpeed;
    //         Rb.MoveRotation(rotation);
    //     }
    // }
    private void Move(){
        GetMoveDir();
        GetCharacterRotation();
        GetBrickRotation();
        if (isMove && !isInputLock)
        {
            PlayerTrans.position = Vector3.MoveTowards(PlayerTrans.position, PlayerTrans.position + moveDir, Time.deltaTime * moveSpeed);
            PlayerTrans.rotation = rotation;
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
            if (screenMoveDir.sqrMagnitude > inputThreshhold)
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
            Vector2 tmp = mouseCurrentPos - mouseDownPos;
            moveDir = new Vector3(tmp.x, 0, tmp.y).normalized;
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

    IEnumerator Fall()
    {
        isInputLock = true;
        anim.SetTrigger(ConstValues.PLAYER_ANIM_FALL);
        yield return new WaitForSeconds(5.3f); //NOTE: ~ time of fall plus kipup animation
        isInputLock = false;
    }
}
