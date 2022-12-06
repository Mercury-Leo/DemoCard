using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace _Project.AppUI.Components.Scripts{

	[RequireComponent(typeof(Selectable))]
	public abstract class UIObject : MonoBehaviour, ISelectHandler, ICancelHandler{
		public void OnSelect(BaseEventData eventData) {
			throw new System.NotImplementedException();
		}

		public void OnCancel(BaseEventData eventData) {
			throw new System.NotImplementedException();
		}
	}
}