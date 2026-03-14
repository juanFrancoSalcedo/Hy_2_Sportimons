using B_Extensions;
using B_Extensions.SceneLoader;

public class ButtonQuickApplication: BaseButtonAttendant
{
    private void Start() => buttonComponent.onClick.AddListener(()=>SceneLoader.Instance.Quit());
}
