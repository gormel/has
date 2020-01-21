using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Core.Skills;
using Assets.Scripts.Core;
using Assets.Scripts.GUI.Skill;
using Assets.Scripts.View.Skills;

public class SkillSelectionPanel : MonoBehaviour
{
    public GameObject ButtonsRoot;
    public GameObject SubmitButtonPrefab;
    public Root Root;
    public SkillIcons IconDatabase;
    public SkillView SelectedSkill { get; private set; }

    private bool mSkillSubmited;

    public IEnumerator SelectSkill(IEnumerable<Skill> skills)
    {
        while (ButtonsRoot.transform.childCount > 0)
        {
            var go = ButtonsRoot.transform.GetChild(0).gameObject;
            go.transform.SetParent(null);
            Destroy(go);
        }

        foreach (var skill in skills)
        {
            var buttonInst = Instantiate(SubmitButtonPrefab);
            buttonInst.transform.SetParent(ButtonsRoot.transform);
            var submitButton = buttonInst.GetComponent<SkillSubmitButton>();
            submitButton.IconTarget.sprite = IconDatabase.GetIcon(skill);
            submitButton.Panel = this;
            submitButton.Skill = Root.AllSkills.FirstOrDefault(view => view.Model<Skill>() == skill);
        }

        SelectedSkill = null;
        mSkillSubmited = false;
        gameObject.SetActive(true);
        yield return new WaitUntil(() => mSkillSubmited);
        gameObject.SetActive(false);
    }

    public void SubmitSkill(SkillView skill)
    {
        SelectedSkill = skill;
        mSkillSubmited = true;
    }
}
