using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;

public class MIPlayer : IPlayer
{
    private void Awake()
    {
        HP = 99999;
        type = Type.Player;
    }

    public SpriteRenderer spriteRenderer;

    public Rigidbody2D r2D;

    public Vector2 lastNZSpeed = new();
    public int power;

    public Vector2 nextVector;
    public bool needDo;
    public bool canDo;

    private Vector2 beganV2;
    private Vector2 endV2;

    public Vector2 reboundUnitVector
    {
        get
        {

            float x = lastNZSpeed.x * 100000;
            float y = lastNZSpeed.y * 100000;

            float z = MathF.Sqrt(x * x + y * y);

            x /= z;
            y /= z;

            return new(x * -1, y );
        }
    }

    public override void WhenTriggerEnter(Type type)
    {
        if (type == Type.Player) return;
        if (type == Type.land)
        {
            r2D.velocity = Vector2.zero;
            return;
        }
        r2D.velocity = Vector2.zero;
        r2D.AddForce(new Vector2(reboundUnitVector.x * power * 0.8f , reboundUnitVector.y * power * 0.8f));
    }

    void OnTriggerStay2D(Collider2D other)
    {
        IPlayer ip = other.gameObject.GetComponent<IPlayer>();

        if (ip == null) return;

        if (ip.type == Type.land)
        {
            r2D.velocity = Vector2.zero;
            canDo = true;
            if (!needDo) return;
            needDo = false;
            r2D.AddForce(nextVector);
            Debug.Log("Fire!");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        IPlayer ip = other.gameObject.GetComponent<IPlayer>();

        if (ip == null) return;

        if (ip.type == Type.land)
        {
            canDo = false;
        }
    }

    public virtual void IUpdateDo() { }

    private Vector2 tp;

    public override void UpdateDo()
    {
        goto mstart;

    m1:
        spriteRenderer.gameObject.SetActive(false);

        Vector2 tmp = new(endV2.x - beganV2.x, endV2.y - beganV2.y);

        if (tmp == Vector2.zero) goto e;

        float x = tmp.x;
        float y = tmp.y;
        float z = MathF.Sqrt(x * x + y * y);

        nextVector = new(x / z * -1f * power * 1f, y / z * -1f * power * 1f);
        needDo = true;

        goto e;

    m2:

        if (canDo)
        {
            Vector2 _tmp = new(tp.x - beganV2.x, tp.y - beganV2.y);

            float _x = _tmp.x ;
            float _y = _tmp.y ;
            float _z = MathF.Sqrt(_x * _x + _y * _y);

            float _p = 0;

            if (_x < 0) _p = 1;

            spriteRenderer.gameObject.SetActive(true);
            spriteRenderer.transform.eulerAngles = new Vector3(0,0,(180 * _p - Mathf.Asin(_y/_z) * Mathf.Rad2Deg * Mathf.Pow(-1,_p) + 90) * -1f + 180f);
        }

        goto e;

    mstart:
        if (r2D.velocity != Vector2.zero) lastNZSpeed = r2D.velocity;

        if (Input.GetMouseButtonDown(0))
        {
            beganV2 = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            endV2 = Input.mousePosition;

            if (canDo) goto m1;
        }
        else if (Input.GetMouseButton(0))
        {
            tp = Input.mousePosition;
            goto m2;
        }

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                beganV2 = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                endV2 = touch.position;

                if (canDo) goto m1;
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                tp = touch.position;
                goto m2;
            }
        }
    e:
        IUpdateDo();
    }
}
