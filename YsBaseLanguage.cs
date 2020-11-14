/***
*   Company:Frozen Wolf Sudio
*	Title："LockstepFrame" Lockstep框架项目
*		主题：XXX
*	Description：
*		功能：XXX
*	Date：2019
*	Version：0.1版本
*	Author：xxx
*	Modify Recoder：
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FightClub
{
	public abstract class YsBaseLanguage : MonoBehaviour 
    {
        public abstract void UpdateText(string str);
        public virtual Text  TargeText { get;} 
        public virtual Dropdown TargeDropdown { get;} 
    }

}
