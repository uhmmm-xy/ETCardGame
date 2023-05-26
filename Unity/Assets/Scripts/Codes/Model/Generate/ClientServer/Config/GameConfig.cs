using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
using ProtoBuf;

namespace ET
{
    [ProtoContract]
    [Config]
    public partial class GameConfigCategory : ConfigSingleton<GameConfigCategory>, IMerge
    {
        [ProtoIgnore]
        [BsonIgnore]
        private Dictionary<int, GameConfig> dict = new Dictionary<int, GameConfig>();
        
        private Dictionary<string, GameConfig> GamebyName = new Dictionary<string, GameConfig>();
		
        [BsonElement]
        [ProtoMember(1)]
        private List<GameConfig> list = new List<GameConfig>();
		
        public void Merge(object o)
        {
            GameConfigCategory s = o as GameConfigCategory;
            this.list.AddRange(s.list);
        }
		
		[ProtoAfterDeserialization]        
        public void ProtoEndInit()
        {
            foreach (GameConfig config in list)
            {
                config.AfterEndInit();
                this.dict.Add(config.Id, config);
            }
            this.list.Clear();
            
            this.AfterEndInit();
        }
		
        public GameConfig Get(int id)
        {
            this.dict.TryGetValue(id, out GameConfig item);

            if (item == null)
            {
                throw new Exception($"配置找不到，配置表名: {nameof (GameConfig)}，配置id: {id}");
            }

            return item;
        }
		
        public bool Contain(int id)
        {
            return this.dict.ContainsKey(id);
        }

        public Dictionary<int, GameConfig> GetAll()
        {
            return this.dict;
        }

        public GameConfig GetOne()
        {
            if (this.dict == null || this.dict.Count <= 0)
            {
                return null;
            }
            return this.dict.Values.GetEnumerator().Current;
        }
        
        public GameConfig GetByName(string name)
        {
	        return this.GamebyName[name];
        }

        public override void AfterEndInit()
        {
	        foreach (GameConfig config in this.GetAll().Values)
	        {
		        this.GamebyName.Add(config.Name, config);
	        }
        }
    }

    [ProtoContract]
	public partial class GameConfig: ProtoObject, IConfig
	{
		/// <summary>Id</summary>
		[ProtoMember(1)]
		public int Id { get; set; }
		/// <summary>游戏类型</summary>
		[ProtoMember(2)]
		public int GameType { get; set; }
		/// <summary>游戏名</summary>
		[ProtoMember(3)]
		public string Name { get; set; }
		/// <summary>牌类型</summary>
		[ProtoMember(4)]
		public int[] GameCardType { get; set; }
		/// <summary>牌数量</summary>
		[ProtoMember(5)]
		public int Count { get; set; }

	}
}
