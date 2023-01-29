
using UnityEngine;

public class DamageSP : CharPower
{
    private float modifier;
    public override void OnStatsPowerClicked()
    {
        hero.SetDamageMultiplier(2);
    }
}
