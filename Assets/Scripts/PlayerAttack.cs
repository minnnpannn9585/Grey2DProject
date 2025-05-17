using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    //public Animator animator; // 引用动画控制器
    private int attackState = 0; // 当前攻击状态
    private float lastAttackTime = 0f; // 上一次攻击时间
    
    void Update()
    {
        // 检查是否需要重置连招
        if (Time.time - lastAttackTime > 2f)
        {
            attackState = 0; // 重置攻击状态
            StopAttack();
        }

        // 检测攻击输入
        if (Input.GetKeyDown(KeyCode.J))
        {
            lastAttackTime = Time.time; // 更新最后一次攻击时间

            if (attackState == 0)
            {
                // 扇形攻击
                //animator.SetTrigger("FanAttack");
                attackState = 1;
                FanShapeAttack();
            }
            else if (attackState == 1)
            {
                // 穿刺攻击
                //animator.SetTrigger("PierceAttack");
                attackState = 2;
                LineShapeAttack();
            }
            else if (attackState == 2)
            {
                // 回到扇形攻击
                //animator.SetTrigger("FanAttack");
                attackState = 1;
                FanShapeAttack();
            }
        }
    }

    void StopAttack()
    {
        
    }

    void FanShapeAttack()
    {
        
    }

    void LineShapeAttack()
    {
        
    }
}