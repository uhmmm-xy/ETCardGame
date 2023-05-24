using System.Collections.Generic;
using System.Net.Sockets;
using MongoDB.Bson;

namespace ET.Client
{
    public static class SceneFactory
    {
        public static async ETTask<Scene> CreateClientScene(int zone, string name)
        {
            await ETTask.CompletedTask;
            
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("channel_id", "1");
            param.Add("type", "phone");
            param.Add("phone", "15202302991");
            param.Add("code", "123456");
            
            string ret = await HttpHelper.SendPost("http://192.168.50.243/api/login/login", param);

            Dictionary<string,string> json = JsonHelper.FromJson<Dictionary<string, string>>(ret);
            
            Log.Info(json.ToJson());
            Log.Info(ret);
            Scene clientScene = EntitySceneFactory.CreateScene(zone, SceneType.Client, name, ClientSceneManagerComponent.Instance);
            clientScene.AddComponent<CurrentScenesComponent>();
            clientScene.AddComponent<ObjectWait>();
            clientScene.AddComponent<PlayerComponent>();

            EventSystem.Instance.Publish(clientScene, new EventType.AfterCreateClientScene());
            return clientScene;
        }

        public static Scene CreateCurrentScene(long id, int zone, string name, CurrentScenesComponent currentScenesComponent)
        {
            Scene currentScene = EntitySceneFactory.CreateScene(id, IdGenerater.Instance.GenerateInstanceId(), zone, SceneType.Current, name,
                currentScenesComponent);
            currentScenesComponent.Scene = currentScene;

            EventSystem.Instance.Publish(currentScene, new EventType.AfterCreateCurrentScene());
            return currentScene;
        }
    }
}