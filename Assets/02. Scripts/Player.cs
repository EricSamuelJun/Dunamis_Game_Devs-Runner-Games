using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    public enum State { Stand, Run, Jump, Hit }
    public float startJumpPower;
    public float jumpPower;
    public bool isGround;
    public bool isJumpKey;
    public UnityEvent onHit;

    Rigidbody2D rigid;
    Animator anim;
    Sounder sound;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();        
        anim = GetComponent<Animator>();
        sound = GetComponent<Sounder>();
    }

    void Start() {
        sound.PlaySound(Sounder.Sfx.Reset);
    }

    // 1A. 기본 점프 
    void Update()
    {
        if (!GameManager.isLive)
            return;

        if ((Input.GetButtonDown("Jump") || Input.GetMouseButtonDown(0))&& isGround) { // 기본 점프 (숏 점프)
            rigid.AddForce(Vector2.up * startJumpPower, ForceMode2D.Impulse);
        }

        isJumpKey = Input.GetButton("Jump") || Input.GetMouseButtonDown(0);
    }
    
    // 1B. 롱 점프
    void FixedUpdate() {
        if (isJumpKey && !isGround) {
            jumpPower = Mathf.Lerp(jumpPower, 0, 0.1f);
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        }
    }

    //2. 착지 (물리 충돌 이벤트)
    void OnCollisionStay2D(Collision2D collision) {
        if (isGround != true){
            ChangeAnim(State.Run);
            sound.PlaySound(Sounder.Sfx.Land);
            jumpPower = 1;
        }
        isGround = true;
    }

    private void OnCollisionExit2D(Collision2D collision) {
        ChangeAnim(State.Jump);
        sound.PlaySound(Sounder.Sfx.Jump);
        isGround = false;
    }

    //3. 장애물 터치(트리거 충돌 이벤트)
    void OnTriggerEnter2D(Collider2D collision) {
        rigid.simulated = false;
        sound.PlaySound(Sounder.Sfx.Hit);
        ChangeAnim(State.Hit);
        onHit.Invoke();
    }

    //4. 애니메이션
    void ChangeAnim(State state)
    {
        anim.SetInteger("State", (int)state);
    }

}
