using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
using ProtoBuf;

namespace ET
{
    [ProtoContract]
    [Config]
    public partial class CardConfigCategory : ConfigSingleton<CardConfigCategory>, IMerge
    {
        [ProtoIgnore]
        [BsonIgnore]
        private Dictionary<int, CardConfig> dict = new Dictionary<int, CardConfig>();
		
        [BsonElement]
        [ProtoMember(1)]
        private List<CardConfig> list = new List<CardConfig>();
		
        public void Merge(object o)
        {
            CardConfigCategory s = o as CardConfigCategory;
            this.list.AddRange(s.list);
        }
		
		[ProtoAfterDeserialization]        
        public void ProtoEndInit()
        {
            foreach (CardConfig config in list)
            {
                config.AfterEndInit();
                this.dict.Add(config.Id, config);
            }
            this.list.Clear();
            
            this.AfterEndInit();
        }
		
        public CardConfig Get(int id)
        {
            this.dict.TryGetValue(id, out CardConfig item);

            if (item == null)
            {
                throw new Exception($"配置找不到，配置表名: {nameof (CardConfig)}，配置id: {id}");
            }

            return item;
        }
		
        public bool Contain(int id)
        {
            return this.dict.ContainsKey(id);
        }

        public Dictionary<int, CardConfig> GetAll()
        {
            return this.dict;
        }

        public CardConfig GetOne()
        {
            if (this.dict == null || this.dict.Count <= 0)
            {
                return null;
            }
            return this.dict.Values.GetEnumerator().Current;
        }
    }

    [ProtoContract]
	public partial class CardConfig: ProtoObject, IConfig
	{
		/// <summary>Id</summary>
		[ProtoMember(1)]
		public int Id { get; set; }
		/// <summary>最小值</summary>
		[ProtoMember(2)]
		public int MinValue { get; set; }
		/// <summary>最大值</summary>
		[ProtoMember(3)]
		public int MaxValue { get; set; }
		/// <summary>描述</summary>
		[ProtoMember(4)]
		public string Name { get; set; }
		/// <summary>牌类型</summary>
		[ProtoMember(5)]
		public string[] ValueName { get; set; }
		/// <summary>牌数量</summary>
		[ProtoMember(6)]
		public int Count { get; set; }

	}
}
