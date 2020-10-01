using UnityEngine.Events;

public interface IHaveDependant
{
    UnityAction onAwoke { get; set;}
}
