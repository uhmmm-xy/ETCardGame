using System.Collections.Generic;
using System.Linq;
using ET.EventType;
using MongoDB.Bson;
using UnityEngine;

namespace ET.Client
{
    [Event(SceneType.Client)]
    public class GamerOperate_Finsh: AEvent<GamerOperate>
    {
        protected override async ETTask Run(Scene scene, GamerOperate a)
        {
            scene.GetComponent<UIComponent>().GetDlgLogic<DlgGameRoom>().View.E_OpatePlanImage.SetVisible(true);

            scene.GetComponent<UIComponent>().GetDlgLogic<DlgGameRoom>().View.EButton_ChiButton.SetVisible(false);
            scene.GetComponent<UIComponent>().GetDlgLogic<DlgGameRoom>().View.EButton_HuButton.SetVisible(false);
            scene.GetComponent<UIComponent>().GetDlgLogic<DlgGameRoom>().View.EButton_GangButton.SetVisible(false);
            scene.GetComponent<UIComponent>().GetDlgLogic<DlgGameRoom>().View.EButton_PengButton.SetVisible(false);
            scene.GetComponent<UIComponent>().GetDlgLogic<DlgGameRoom>().View.EButton_PassButton.SetVisible(true);
            scene.GetComponent<UIComponent>().GetDlgLogic<DlgGameRoom>().View.E_SelectCardsImage.SetVisible(false);

            scene.GetComponent<UIComponent>().GetDlgLogic<DlgGameRoom>().View.EButton_SelectOneImage.SetVisible(false);
            scene.GetComponent<UIComponent>().GetDlgLogic<DlgGameRoom>().View.EButton_SelectTwoImage.SetVisible(false);
            scene.GetComponent<UIComponent>().GetDlgLogic<DlgGameRoom>().View.EButton_SelectThreeImage.SetVisible(false);

            List<int> operates = new List<int>();
            GetOperateList(operates, a.Type);
            Log.Info($"Gamer Operate List is {operates.ToJson()} Score:{a.Type}");

            foreach (int operate in operates)
            {
                switch (operate)
                {
                    case OperateType.MahjongChi:
                        scene.GetComponent<UIComponent>().GetDlgLogic<DlgGameRoom>().View.EButton_ChiButton.SetVisible(true);
                        break;
                    case OperateType.MahjongPeng:
                        scene.GetComponent<UIComponent>().GetDlgLogic<DlgGameRoom>().View.EButton_PengButton.SetVisible(true);
                        break;
                    case OperateType.MahjongGang:
                        scene.GetComponent<UIComponent>().GetDlgLogic<DlgGameRoom>().View.EButton_GangButton.SetVisible(true);
                        break;
                    case OperateType.MahjongHu:
                        scene.GetComponent<UIComponent>().GetDlgLogic<DlgGameRoom>().View.EButton_HuButton.SetVisible(true);
                        break;
                }
            }

            await ETTask.CompletedTask;
        }

        private void GetOperateList(List<int> operates, int type)
        {
            while (true)
            {
                switch (type)
                {
                    case >= OperateType.MahjongHu:
                        operates.Add(OperateType.MahjongHu);
                        type -= OperateType.MahjongHu;
                        continue;
                    case >= OperateType.MahjongGang:
                        operates.Add(OperateType.MahjongGang);
                        type -= OperateType.MahjongGang;
                        continue;
                    case >= OperateType.MahjongPeng:
                        operates.Add(OperateType.MahjongPeng);
                        type -= OperateType.MahjongPeng;
                        continue;
                    case >= OperateType.MahjongChi:
                        operates.Add(OperateType.MahjongChi);
                        type -= OperateType.MahjongChi;
                        continue;
                    case OperateType.MahjongNone:
                    default:
                        return;
                }
            }
        }
    }
}