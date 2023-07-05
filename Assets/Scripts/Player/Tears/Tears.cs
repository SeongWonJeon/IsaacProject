using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

class Tears : BaseTears
{

    protected override void Awake()
    {
        base.Awake();
        
    }
    protected override void OnEnable()
    {
        base.OnEnable();
    }

    public override void SetStartPos(Vector3 pos)
    {
        base.SetStartPos(pos);
    }
    protected override void Disappear()
    {
        base.Disappear();
        

    }
    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
    }

}
