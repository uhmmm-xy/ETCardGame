using MahjongAlgorithm;

namespace ET
{
    [FriendOf(typeof (GameLogicComponent))]
    public static class GameLogicComponentSystem
    {
        public class GameLogicComponentAwakeSystem: AwakeSystem<GameLogicComponent>
        {
            protected override void Awake(GameLogicComponent self)
            {
                Scene scene = self.DomainScene();
                string gameName = scene.Name.IndexOf('_') > 0? scene.Name.Substring(0, scene.Name.IndexOf('_')) : scene.Name;
                GameConfig config = GameConfigCategory.Instance.GetByName(gameName);
                long now = TimeHelper.ServerNow();
                switch (config.Id)
                {
                    case GameType.HangZhouMahjong:
                        self.LogicHandle = Mahjong.GetAlgorithm("Resources/TingTable.json",
                            "Resources/LaiTable.json",
                            "Resources/HuTable.json");
                        break;
                }

                Log.Console($"逻辑表载入时长：{TimeHelper.ServerNow() - now}ms");
            }
        }

        public static object GetLogicHandle(this GameLogicComponent self)
        {
            return self.LogicHandle;
        }
    }
}