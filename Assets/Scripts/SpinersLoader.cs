using UnityEngine;

public class SpinersLoader : MonoBehaviour
{
    private const string _PATH = "Spiners/spiner";
    public static GameObject LoadSpiner(int charaNum)
    {
        return (GameObject)Resources.Load($"{_PATH}_{charaNum}");
    }
}
