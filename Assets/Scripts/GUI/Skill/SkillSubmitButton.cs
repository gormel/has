using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using Assets.Scripts.Core.Skills;

public class SkillSubmitButton : MonoBehaviour, IPointerDownHandler
{
    public SkillSelectionPanel Panel;
    public Skill Skill;

    public void OnPointerDown(PointerEventData eventData)
    {
        Panel.SubmitSkill(Skill);
    }
}
