using UnityEngine;

public class InputProvider : SingletonMonoBehaviour<InputProvider>, IInput
{
    public bool GetFire1Down()
    {
#if UNITY_EDITOR
        return Input.GetMouseButtonDown(0);
#else
        // TODO:ここにコントローラー処理を描く
#endif
    }

    public bool GetFire1()
    {
#if UNITY_EDITOR
        return Input.GetMouseButton(0);
#else
        // TODO:ここにコントローラー処理を描く
#endif
    }

    public bool GetFire1Up()
    {
#if UNITY_EDITOR
        return Input.GetMouseButtonUp(0);
#else
        // TODO:ここにコントローラー処理を描く
#endif
    }
}
