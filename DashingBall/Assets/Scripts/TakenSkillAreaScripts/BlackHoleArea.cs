using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHoleArea : MonoBehaviour
{
    void Start()
    {
        float cooldown = ItemManager.instance.getCooldown("BlackHole");
        StartCoroutine(endAnim(cooldown));
    }

    IEnumerator endAnim( float cooldown)
    {
        yield return new WaitForSeconds(cooldown + 2f); // start anim offset
        GetComponent<Animator>().SetTrigger("endblackhole");
        Destroy(gameObject, 2f); //finish animation offset 1+1
    }

}
