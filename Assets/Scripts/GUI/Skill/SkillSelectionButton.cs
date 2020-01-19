using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class SkillSelectionButton : MonoBehaviour, IPointerDownHandler
{
    public Root Root;
    public SkillSelectionPanel Panel;
    public int SkillSlot;

    public void OnPointerDown(PointerEventData eventData)
    {
        StartCoroutine(OnClick());
    }

    private IEnumerator OnClick()
    {
        yield return StartCoroutine(Panel.SelectSkill());
        if (Panel.SelectedSkill != null)
            Root.SelectedSkills[SkillSlot] = Panel.SelectedSkill;
    }
}
