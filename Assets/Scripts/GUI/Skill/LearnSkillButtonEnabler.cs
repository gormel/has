using Assets.Scripts.Core;
using UnityEngine;

public class LearnSkillButtonEnabler : MonoBehaviour
{
    public Root Root;
    public LearnSkillButton Button;

    void Update()
    {
        Button.gameObject.SetActive(Root.PlayerView.Model<Player>().SkillPoints > 0);
    }
}