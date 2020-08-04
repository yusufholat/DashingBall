using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldArea : MonoBehaviour
{
    void Start()
    {

        float cooldown = ItemManager.instance.getCooldown("Shield");
        StartCoroutine(endAnim(cooldown));
    }

    IEnumerator endAnim(float cooldown)
    {
        yield return new WaitForSeconds(cooldown + 1); // start anim offset
        GetComponent<Animator>().SetTrigger("endshieldanim");
        Destroy(gameObject, 2f); //finish animation offset 1+1
    }
}
