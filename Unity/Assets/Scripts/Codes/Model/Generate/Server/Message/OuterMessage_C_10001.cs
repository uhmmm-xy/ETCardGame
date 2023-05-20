using ET;
using ProtoBuf;
using System.Collections.Generic;
namespace ET
{
	[Message(OuterMessage.HttpGetRouterResponse)]
	[ProtoContract]
	public partial class HttpGetRouterResponse: ProtoObject
	{
		[ProtoMember(1)]
		public List<string> Realms { get; set; }

		[ProtoMember(2)]
		public List<string> Routers { get; set; }

		[ProtoMember(3)]
		public List<string> Logins { get; set; }

	}

	[Message(OuterMessage.RouterSync)]
	[ProtoContract]
	public partial class RouterSync: ProtoObject
	{
		[ProtoMember(1)]
		public uint ConnectId { get; set; }

		[ProtoMember(2)]
		public string Address { get; set; }

	}

	[ResponseType(nameof(M2C_TestResponse))]
	[Message(OuterMessage.C2M_TestRequest)]
	[ProtoContract]
	public partial class C2M_TestRequest: ProtoObject, IActorLocationRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public string request { get; set; }

	}

	[Message(OuterMessage.M2C_TestResponse)]
	[ProtoContract]
	public partial class M2C_TestResponse: ProtoObject, IActorLocationResponse
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int Error { get; set; }

		[ProtoMember(3)]
		public string Message { get; set; }

		[ProtoMember(4)]
		public string response { get; set; }

	}

	[ResponseType(nameof(Actor_TransferResponse))]
	[Message(OuterMessage.Actor_TransferRequest)]
	[ProtoContract]
	public partial class Actor_TransferRequest: ProtoObject, IActorLocationRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int MapIndex { get; set; }

	}

	[Message(OuterMessage.Actor_TransferResponse)]
	[ProtoContract]
	public partial class Actor_TransferResponse: ProtoObject, IActorLocationResponse
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int Error { get; set; }

		[ProtoMember(3)]
		public string Message { get; set; }

	}

	[ResponseType(nameof(G2C_EnterMap))]
	[Message(OuterMessage.C2G_EnterMap)]
	[ProtoContract]
	public partial class C2G_EnterMap: ProtoObject, IRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

	}

	[Message(OuterMessage.G2C_EnterMap)]
	[ProtoContract]
	public partial class G2C_EnterMap: ProtoObject, IResponse
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int Error { get; set; }

		[ProtoMember(3)]
		public string Message { get; set; }

// 自己unitId
		[ProtoMember(4)]
		public long MyId { get; set; }

	}

	[Message(OuterMessage.MoveInfo)]
	[ProtoContract]
	public partial class MoveInfo: ProtoObject
	{
		[ProtoMember(1)]
		public List<Unity.Mathematics.float3> Points { get; set; }

		[ProtoMember(2)]
		public Unity.Mathematics.quaternion Rotation { get; set; }

		[ProtoMember(3)]
		public int TurnSpeed { get; set; }

	}

	[Message(OuterMessage.UnitInfo)]
	[ProtoContract]
	public partial class UnitInfo: ProtoObject
	{
		[ProtoMember(1)]
		public long UnitId { get; set; }

		[ProtoMember(2)]
		public int ConfigId { get; set; }

		[ProtoMember(3)]
		public int Type { get; set; }

		[ProtoMember(4)]
		public Unity.Mathematics.float3 Position { get; set; }

		[ProtoMember(5)]
		public Unity.Mathematics.float3 Forward { get; set; }

		[MongoDB.Bson.Serialization.Attributes.BsonDictionaryOptions(MongoDB.Bson.Serialization.Options.DictionaryRepresentation.ArrayOfArrays)]
		[ProtoMember(6)]
		public Dictionary<int, long> KV { get; set; }
		[ProtoMember(7)]
		public MoveInfo MoveInfo { get; set; }

	}

	[Message(OuterMessage.M2C_CreateUnits)]
	[ProtoContract]
	public partial class M2C_CreateUnits: ProtoObject, IActorLocationMessage
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public List<UnitInfo> Units { get; set; }

	}

	[Message(OuterMessage.M2C_CreateMyUnit)]
	[ProtoContract]
	public partial class M2C_CreateMyUnit: ProtoObject, IActorLocationMessage
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public UnitInfo Unit { get; set; }

	}

	[Message(OuterMessage.M2C_StartSceneChange)]
	[ProtoContract]
	public partial class M2C_StartSceneChange: ProtoObject, IActorLocationMessage
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public long SceneInstanceId { get; set; }

		[ProtoMember(3)]
		public string SceneName { get; set; }

	}

	[Message(OuterMessage.M2C_RemoveUnits)]
	[ProtoContract]
	public partial class M2C_RemoveUnits: ProtoObject, IActorLocationMessage
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public List<long> Units { get; set; }

	}

	[Message(OuterMessage.C2M_PathfindingResult)]
	[ProtoContract]
	public partial class C2M_PathfindingResult: ProtoObject, IActorLocationMessage
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public Unity.Mathematics.float3 Position { get; set; }

	}

	[Message(OuterMessage.C2M_Stop)]
	[ProtoContract]
	public partial class C2M_Stop: ProtoObject, IActorLocationMessage
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

	}

	[Message(OuterMessage.M2C_PathfindingResult)]
	[ProtoContract]
	public partial class M2C_PathfindingResult: ProtoObject, IActorLocationMessage
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public long Id { get; set; }

		[ProtoMember(3)]
		public Unity.Mathematics.float3 Position { get; set; }

		[ProtoMember(4)]
		public List<Unity.Mathematics.float3> Points { get; set; }

	}

	[Message(OuterMessage.M2C_Stop)]
	[ProtoContract]
	public partial class M2C_Stop: ProtoObject, IActorLocationMessage
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int Error { get; set; }

		[ProtoMember(3)]
		public long Id { get; set; }

		[ProtoMember(4)]
		public Unity.Mathematics.float3 Position { get; set; }

		[ProtoMember(5)]
		public Unity.Mathematics.quaternion Rotation { get; set; }

	}

	[ResponseType(nameof(G2C_Ping))]
	[Message(OuterMessage.C2G_Ping)]
	[ProtoContract]
	public partial class C2G_Ping: ProtoObject, IRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

	}

	[Message(OuterMessage.G2C_Ping)]
	[ProtoContract]
	public partial class G2C_Ping: ProtoObject, IResponse
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int Error { get; set; }

		[ProtoMember(3)]
		public string Message { get; set; }

		[ProtoMember(4)]
		public long Time { get; set; }

	}

	[Message(OuterMessage.G2C_Test)]
	[ProtoContract]
	public partial class G2C_Test: ProtoObject, IMessage
	{
	}

	[ResponseType(nameof(M2C_Reload))]
	[Message(OuterMessage.C2M_Reload)]
	[ProtoContract]
	public partial class C2M_Reload: ProtoObject, IRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public string Account { get; set; }

		[ProtoMember(3)]
		public string Password { get; set; }

	}

	[Message(OuterMessage.M2C_Reload)]
	[ProtoContract]
	public partial class M2C_Reload: ProtoObject, IResponse
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int Error { get; set; }

		[ProtoMember(3)]
		public string Message { get; set; }

	}

	[ResponseType(nameof(R2C_Login))]
	[Message(OuterMessage.C2R_Login)]
	[ProtoContract]
	public partial class C2R_Login: ProtoObject, IRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public string Account { get; set; }

		[ProtoMember(3)]
		public string Password { get; set; }

	}

	[Message(OuterMessage.R2C_Login)]
	[ProtoContract]
	public partial class R2C_Login: ProtoObject, IResponse
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int Error { get; set; }

		[ProtoMember(3)]
		public string Message { get; set; }

		[ProtoMember(4)]
		public string Address { get; set; }

		[ProtoMember(5)]
		public long Key { get; set; }

		[ProtoMember(6)]
		public long GateId { get; set; }

	}

	[ResponseType(nameof(G2C_LoginGate))]
	[Message(OuterMessage.C2G_LoginGate)]
	[ProtoContract]
	public partial class C2G_LoginGate: ProtoObject, IRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public long Key { get; set; }

		[ProtoMember(3)]
		public long GateId { get; set; }

	}

	[Message(OuterMessage.G2C_LoginGate)]
	[ProtoContract]
	public partial class G2C_LoginGate: ProtoObject, IResponse
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int Error { get; set; }

		[ProtoMember(3)]
		public string Message { get; set; }

		[ProtoMember(4)]
		public long PlayerId { get; set; }

	}

	[Message(OuterMessage.G2C_TestHotfixMessage)]
	[ProtoContract]
	public partial class G2C_TestHotfixMessage: ProtoObject, IMessage
	{
		[ProtoMember(1)]
		public string Info { get; set; }

	}

	[ResponseType(nameof(M2C_TestRobotCase))]
	[Message(OuterMessage.C2M_TestRobotCase)]
	[ProtoContract]
	public partial class C2M_TestRobotCase: ProtoObject, IActorLocationRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int N { get; set; }

	}

	[Message(OuterMessage.M2C_TestRobotCase)]
	[ProtoContract]
	public partial class M2C_TestRobotCase: ProtoObject, IActorLocationResponse
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int Error { get; set; }

		[ProtoMember(3)]
		public string Message { get; set; }

		[ProtoMember(4)]
		public int N { get; set; }

	}

	[Message(OuterMessage.C2M_TestRobotCase2)]
	[ProtoContract]
	public partial class C2M_TestRobotCase2: ProtoObject, IActorLocationMessage
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int N { get; set; }

	}

	[Message(OuterMessage.M2C_TestRobotCase2)]
	[ProtoContract]
	public partial class M2C_TestRobotCase2: ProtoObject, IActorLocationMessage
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int N { get; set; }

	}

	[ResponseType(nameof(M2C_TransferMap))]
	[Message(OuterMessage.C2M_TransferMap)]
	[ProtoContract]
	public partial class C2M_TransferMap: ProtoObject, IActorLocationRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

	}

	[Message(OuterMessage.M2C_TransferMap)]
	[ProtoContract]
	public partial class M2C_TransferMap: ProtoObject, IActorLocationResponse
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int Error { get; set; }

		[ProtoMember(3)]
		public string Message { get; set; }

	}

	[ResponseType(nameof(G2C_Benchmark))]
	[Message(OuterMessage.C2G_Benchmark)]
	[ProtoContract]
	public partial class C2G_Benchmark: ProtoObject, IRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

	}

	[Message(OuterMessage.G2C_Benchmark)]
	[ProtoContract]
	public partial class G2C_Benchmark: ProtoObject, IResponse
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int Error { get; set; }

		[ProtoMember(3)]
		public string Message { get; set; }

	}

//房卡麻将消息开始
	[ResponseType(nameof(L2C_LoginAccount))]
	[Message(OuterMessage.C2L_LoginAccount)]
	[ProtoContract]
	public partial class C2L_LoginAccount: ProtoObject, IRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public string Token { get; set; }

	}

	[Message(OuterMessage.L2C_LoginAccount)]
	[ProtoContract]
	public partial class L2C_LoginAccount: ProtoObject, IResponse
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int Error { get; set; }

		[ProtoMember(3)]
		public string Message { get; set; }

		[ProtoMember(4)]
		public string GateIPAddress { get; set; }

		[ProtoMember(5)]
		public string Token { get; set; }

	}

	[Message(OuterMessage.A2C_Disconnent)]
	[ProtoContract]
	public partial class A2C_Disconnent: ProtoObject, IActorMessage
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

	}

	[Message(OuterMessage.UserInfo)]
	[ProtoContract]
	public partial class UserInfo: ProtoObject
	{
		[ProtoMember(1)]
		public int PlayerId { get; set; }

		[ProtoMember(2)]
		public int Jewel { get; set; }

		[ProtoMember(3)]
		public int Glod { get; set; }

		[ProtoMember(4)]
		public int Gender { get; set; }

		[ProtoMember(5)]
		public string HeaderImg { get; set; }

		[ProtoMember(6)]
		public int Status { get; set; }

		[ProtoMember(7)]
		public int RoomNumber { get; set; }

	}

	[ResponseType(nameof(G2C_AuthToken))]
	[Message(OuterMessage.C2G_AuthToken)]
	[ProtoContract]
	public partial class C2G_AuthToken: ProtoObject, IRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public string Token { get; set; }

	}

	[Message(OuterMessage.G2C_AuthToken)]
	[ProtoContract]
	public partial class G2C_AuthToken: ProtoObject, IResponse
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int Error { get; set; }

		[ProtoMember(3)]
		public string Message { get; set; }

		[ProtoMember(4)]
		public UserInfo Info { get; set; }

	}

	[ResponseType(nameof(G2C_CreatedRoom))]
	[Message(OuterMessage.C2G_CreatedRoom)]
	[ProtoContract]
	public partial class C2G_CreatedRoom: ProtoObject, IRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int GameId { get; set; }

	}

	[Message(OuterMessage.G2C_CreatedRoom)]
	[ProtoContract]
	public partial class G2C_CreatedRoom: ProtoObject, IResponse
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int Error { get; set; }

		[ProtoMember(3)]
		public string Message { get; set; }

		[ProtoMember(4)]
		public int RoomId { get; set; }

	}

	[ResponseType(nameof(G2C_EnterRoom))]
	[Message(OuterMessage.C2G_EnterRoom)]
	[ProtoContract]
	public partial class C2G_EnterRoom: ProtoObject, IRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int RoomId { get; set; }

		[ProtoMember(3)]
		public int GameId { get; set; }

	}

	[Message(OuterMessage.G2C_EnterRoom)]
	[ProtoContract]
	public partial class G2C_EnterRoom: ProtoObject, IResponse
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int Error { get; set; }

		[ProtoMember(3)]
		public string Message { get; set; }

		[ProtoMember(4)]
		public int RoomId { get; set; }

	}

	[Message(OuterMessage.CardInfo)]
	[ProtoContract]
	public partial class CardInfo: ProtoObject
	{
		[ProtoMember(1)]
		public int Type { get; set; }

		[ProtoMember(2)]
		public int Value { get; set; }

	}

	[Message(OuterMessage.GamerInfo)]
	[ProtoContract]
	public partial class GamerInfo: ProtoObject
	{
		[ProtoMember(1)]
		public int PlayerId { get; set; }

		[ProtoMember(2)]
		public List<CardInfo> OutCards { get; set; }

		[ProtoMember(3)]
		public string Hander { get; set; }

		[ProtoMember(4)]
		public string Name { get; set; }

		[ProtoMember(5)]
		public List<CardInfo> OpenDeal { get; set; }

		[ProtoMember(6)]
		public int Score { get; set; }

		[ProtoMember(7)]
		public int Status { get; set; }

		[ProtoMember(8)]
		public List<CardInfo> HandCards { get; set; }

	}

	[Message(OuterMessage.RoundInfo)]
	[ProtoContract]
	public partial class RoundInfo: ProtoObject
	{
		[ProtoMember(1)]
		public int StartIndex { get; set; }

		[ProtoMember(2)]
		public List<int> Score { get; set; }

		[ProtoMember(3)]
		public int PlayerIndex { get; set; }

		[ProtoMember(4)]
		public int Status { get; set; }

	}

	[Message(OuterMessage.RoomInfo)]
	[ProtoContract]
	public partial class RoomInfo: ProtoObject
	{
		[ProtoMember(1)]
		public int RoomId { get; set; }

		[ProtoMember(2)]
		public List<GamerInfo> Players { get; set; }

		[ProtoMember(3)]
		public List<RoundInfo> Rounds { get; set; }

		[ProtoMember(4)]
		public int Status { get; set; }

	}

	[ResponseType(nameof(M2C_RoomInfo))]
	[Message(OuterMessage.C2M_RoomInfo)]
	[ProtoContract]
	public partial class C2M_RoomInfo: ProtoObject, IActorLocationRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int RoomId { get; set; }

	}

	[Message(OuterMessage.M2C_RoomInfo)]
	[ProtoContract]
	public partial class M2C_RoomInfo: ProtoObject, IActorLocationResponse
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int Error { get; set; }

		[ProtoMember(3)]
		public string Message { get; set; }

		[ProtoMember(4)]
		public RoomInfo Info { get; set; }

	}

	[Message(OuterMessage.G2M_UpdateRoom)]
	[ProtoContract]
	public partial class G2M_UpdateRoom: ProtoObject, IActorLocationMessage
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

	}

	[ResponseType(nameof(M2C_GamerReady))]
	[Message(OuterMessage.C2M_GamerReady)]
	[ProtoContract]
	public partial class C2M_GamerReady: ProtoObject, IActorLocationRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

	}

	[Message(OuterMessage.M2C_GamerReady)]
	[ProtoContract]
	public partial class M2C_GamerReady: ProtoObject, IActorLocationResponse
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int Error { get; set; }

		[ProtoMember(3)]
		public string Message { get; set; }

	}

	public static class OuterMessage
	{
		 public const ushort HttpGetRouterResponse = 10002;
		 public const ushort RouterSync = 10003;
		 public const ushort C2M_TestRequest = 10004;
		 public const ushort M2C_TestResponse = 10005;
		 public const ushort Actor_TransferRequest = 10006;
		 public const ushort Actor_TransferResponse = 10007;
		 public const ushort C2G_EnterMap = 10008;
		 public const ushort G2C_EnterMap = 10009;
		 public const ushort MoveInfo = 10010;
		 public const ushort UnitInfo = 10011;
		 public const ushort M2C_CreateUnits = 10012;
		 public const ushort M2C_CreateMyUnit = 10013;
		 public const ushort M2C_StartSceneChange = 10014;
		 public const ushort M2C_RemoveUnits = 10015;
		 public const ushort C2M_PathfindingResult = 10016;
		 public const ushort C2M_Stop = 10017;
		 public const ushort M2C_PathfindingResult = 10018;
		 public const ushort M2C_Stop = 10019;
		 public const ushort C2G_Ping = 10020;
		 public const ushort G2C_Ping = 10021;
		 public const ushort G2C_Test = 10022;
		 public const ushort C2M_Reload = 10023;
		 public const ushort M2C_Reload = 10024;
		 public const ushort C2R_Login = 10025;
		 public const ushort R2C_Login = 10026;
		 public const ushort C2G_LoginGate = 10027;
		 public const ushort G2C_LoginGate = 10028;
		 public const ushort G2C_TestHotfixMessage = 10029;
		 public const ushort C2M_TestRobotCase = 10030;
		 public const ushort M2C_TestRobotCase = 10031;
		 public const ushort C2M_TestRobotCase2 = 10032;
		 public const ushort M2C_TestRobotCase2 = 10033;
		 public const ushort C2M_TransferMap = 10034;
		 public const ushort M2C_TransferMap = 10035;
		 public const ushort C2G_Benchmark = 10036;
		 public const ushort G2C_Benchmark = 10037;
		 public const ushort C2L_LoginAccount = 10038;
		 public const ushort L2C_LoginAccount = 10039;
		 public const ushort A2C_Disconnent = 10040;
		 public const ushort UserInfo = 10041;
		 public const ushort C2G_AuthToken = 10042;
		 public const ushort G2C_AuthToken = 10043;
		 public const ushort C2G_CreatedRoom = 10044;
		 public const ushort G2C_CreatedRoom = 10045;
		 public const ushort C2G_EnterRoom = 10046;
		 public const ushort G2C_EnterRoom = 10047;
		 public const ushort CardInfo = 10048;
		 public const ushort GamerInfo = 10049;
		 public const ushort RoundInfo = 10050;
		 public const ushort RoomInfo = 10051;
		 public const ushort C2M_RoomInfo = 10052;
		 public const ushort M2C_RoomInfo = 10053;
		 public const ushort G2M_UpdateRoom = 10054;
		 public const ushort C2M_GamerReady = 10055;
		 public const ushort M2C_GamerReady = 10056;
	}
}
