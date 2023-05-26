
namespace ET
{
    [ComponentOf(typeof(Scene))]
    public class GameLogicComponent: Entity, IAwake, IDestroy
    {
        public object LogicHandle;
    }
}