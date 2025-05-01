using UnityEngine;

namespace Entity
{
    public class PlayerResourceController : ResourceController
    {
        UIManager uiManager;
        
        protected override void Awake()
        {
            base.Awake(); // 부모의 Awake 먼저 실행

            uiManager = FindObjectOfType<UIManager>();
            
        }

        protected override void Start()
        {
            base.Start(); 
            if (uiManager != null)
            {
                this.RemoveHealthChangeEvent(uiManager.ChangePlayerHP);
                this.AddHealthChangeEvent(uiManager.ChangePlayerHP);
            }
            else
            {
                Debug.LogWarning("UIManager를 찾을 수 없습니다.");
            }
        }
    }
}