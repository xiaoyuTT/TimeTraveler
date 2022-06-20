using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public interface Istate
{
    void OnEnter(movementcontrol movementcontrol);
    void OnStay();
    void OnExit();
}

public class Static : Istate //��ֹ���
{
    public Static(movementcontrol movementcontrol1)//���캯��
    {
        OnEnter(movementcontrol1);
    }
    private movementcontrol movementcontroller;
    public void OnEnter(movementcontrol movementcontrol)
    {
        
        movementcontroller = movementcontrol;
        movementcontroller.myrigidbody.velocity = new Vector2(0, movementcontroller.myrigidbody.velocity.y);
        //���ö���Ϊ��ֹʱ�Ķ���������      Ĭ�ϼ��Ǿ�ֹ
    }

    public void OnExit()
    {
        
    }

    public void OnStay()
    {
        if (Input.GetKeyDown(KeyCode.D)||Input.GetKeyDown(KeyCode.A))
            movementcontroller.ChangeState(StateType.Running);
        if (movementcontroller.canClimb && Input.GetKeyDown(KeyCode.E))
            movementcontroller.ChangeState(StateType.Climbing);
        //if(Input.GetKeyDown(KeyCode.Space))

    }
}
public class Running : Istate //�ܲ����
{   public Running(movementcontrol movementcontrol)
    {
        movementcontroller = movementcontrol;
    }
    private movementcontrol movementcontroller;
    public void OnEnter(movementcontrol movementcontrol)
    {

        movementcontroller = movementcontrol;
        movementcontroller.anim.SetFloat("running", 0.2f);
        //���ö���Ϊ�ܶ�ʱ�Ķ���������
    }
    
    public void OnExit()
    {
        movementcontroller.anim.SetFloat("running", 0.0f);;//�ܶ������˳�
    }

    public void OnStay()
    {
        if (movementcontroller.canClimb && Input.GetKey(KeyCode.E))
            movementcontroller.ChangeState(StateType.Climbing);
        if (Input.GetKey(KeyCode.A))
        {
            movementcontroller.gameObject.transform.localScale = new Vector3(-movementcontroller.xlocalscale, movementcontroller.gameObject.transform.localScale.y, movementcontroller.gameObject.transform.localScale.z);
            movementcontroller.myrigidbody.velocity = new Vector2(-movementcontroller.movespeed, movementcontroller.myrigidbody.velocity.y);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            movementcontroller.gameObject.transform.localScale = new Vector3(movementcontroller.xlocalscale, movementcontroller.gameObject.transform.localScale.y, movementcontroller.gameObject.transform.localScale.z);
            movementcontroller.myrigidbody.velocity = new Vector2(movementcontroller.movespeed, movementcontroller.myrigidbody.velocity.y);
        }
         
        else
        {
            movementcontroller.ChangeState(StateType.Static);
        }
    }
}
public class Climbing : Istate
{
    private movementcontrol movementcontroller;
    public Climbing(movementcontrol movementcontrol)
    {
        movementcontroller = movementcontrol;
    }
    public void OnEnter(movementcontrol movementcontrol)
    {
        movementcontroller = movementcontrol;
        movementcontroller.myrigidbody.gravityScale = 0;
        //���ö���Ϊ����ʱ�Ķ���������
    }

    public void OnExit()
    {
        movementcontroller.myrigidbody.gravityScale = movementcontroller.GravityScale;
    }

    public void OnStay()
    {   if (!movementcontroller.canClimb)
        {
            movementcontroller.ChangeState(StateType.Static);
        }
        if (Input.GetKey(KeyCode.W))
        {
            movementcontroller.myrigidbody.velocity = Vector2.up * movementcontroller.climbspeed;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            movementcontroller.myrigidbody.velocity = Vector2.down * movementcontroller.climbspeed;
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            movementcontroller.ChangeState(StateType.Static);
        }
        //else if (Input.GetKey(KeyCode.E))
        //{
        //    movementcontroller.ChangeState(StateType.Static);
        //}
        else
        {
            movementcontroller.myrigidbody.velocity = Vector2.zero;
        }
    }
}
public enum StateType
{
    Static,
    Running,
    Climbing//�����״̬�ڶ�������״̬�ӵ�ö������
}
//�����״̬��һ��������һ��״̬�࣬ʵ��Istate�ӿڣ���Ҫ�й��캯��

public class movementcontrol : MonoBehaviour
{
   
    public float xlocalscale;
    public bool canClimb=false;
    public Istate currentstate;
    public GameObject DieUI;
    public Dictionary<StateType, Istate> state;
    public void ChangeState(StateType type)
    {if (currentstate != state.GetValueOrDefault(type))
        {
            currentstate.OnExit();
            state.TryGetValue(type, out currentstate);
            currentstate.OnEnter(this);
        }
    }
    public int playerStates;//��ɫ״̬��0����̬ 1��������̬
    public float climbspeed;
    public GameObject ThisObjects;
    public float speed = 3;
    public float jumphight = 5;
    public Rigidbody2D rb2D;
    public Collider2D obstacle;
    public Animator anim;
    public bool footonground;
    public void Setfootonground(bool n)
    {
        footonground = n;
    }
    public Rigidbody2D myrigidbody;
    public float movespeed = 3;
    public float GravityScale;
    private Static _static;
    private Running _running;
    private Climbing _climbing;

    private void Awake()
    {
        _static = new Static(this);
        _running = new Running(this);
        _climbing = new Climbing(this);

        myrigidbody = GetComponent<Rigidbody2D>();
        state = new Dictionary<StateType, Istate>();
        state.Add(StateType.Static, _static);
        state.Add(StateType.Running, _running);
        state.Add(StateType.Climbing, _climbing);//��������ע���ֵ��
        state.TryGetValue(StateType.Static, out currentstate);
    }
    void Start()
    {
        xlocalscale = -gameObject.transform.localScale.x;
        
        GravityScale = myrigidbody.gravityScale;        
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        jump();
        currentstate.OnStay();
       // Debug.Log(currentstate.ToString());
    }
    
    private void jump()
    {
        //Debug.Log(footonground);
        if (footonground && Input.GetKey(KeyCode.Space))
        {
            myrigidbody.velocity = new Vector2(myrigidbody.velocity.x, jumphight);
            anim.SetBool("jumping", true);
        }

    }//��Ծ
    public void Die()
    {
        DieUI.GetComponent<Restart>().Paused();
    }
}