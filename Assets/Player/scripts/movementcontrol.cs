using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public interface Istate
{
    void OnEnter(movementcontrol movementcontrol);
    void OnStay();
    void OnExit();
}

public class Static : Istate //静止情况
{
    public Static(movementcontrol movementcontrol1)//构造函数
    {
        OnEnter(movementcontrol1);
    }
    private movementcontrol movementcontroller;
    public void OnEnter(movementcontrol movementcontrol)
    {
        
        movementcontroller = movementcontrol;
        movementcontroller.myrigidbody.velocity = new Vector2(0, movementcontroller.myrigidbody.velocity.y);
        //设置动画为静止时的动画放在这      默认即是静止
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
public class Running : Istate //跑步情况
{   public Running(movementcontrol movementcontrol)
    {
        movementcontroller = movementcontrol;
    }
    private movementcontrol movementcontroller;
    public void OnEnter(movementcontrol movementcontrol)
    {

        movementcontroller = movementcontrol;
        movementcontroller.anim.SetFloat("running", 0.2f);
        //设置动画为跑动时的动画放在这
    }
    
    public void OnExit()
    {
        movementcontroller.anim.SetFloat("running", 0.0f);;//跑动动画退出
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
        //设置动画为攀爬时的动画放在这
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
    Climbing//添加新状态第二步，新状态加到枚举类中
}
//添加新状态第一步：创建一个状态类，实现Istate接口，并要有构造函数

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
    public int playerStates;//角色状态：0：常态 1：可攀爬态
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
        state.Add(StateType.Climbing, _climbing);//第三步，注册键值对
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

    }//跳跃
    public void Die()
    {
        DieUI.GetComponent<Restart>().Paused();
    }
}