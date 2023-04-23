using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using ET;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class ProtoBufEditor: EditorWindow
{
    [MenuItem("Tools/ProtoBufEditor")]
    static void ShowWin()
    {
        GetWindow<ProtoBufEditor>().Show();
    }

    private static string _dirPath;

    private List<string> _baseTypes;

    private List<ProtoBufFileInfo> _files;

    private Vector2 _scrollPos1;
    private Vector2 _scrollPos2;

    private ProtoBufFileInfo _selectInfo;
    private string _searchField;

    private void OnEnable()
    {
        this._baseTypes = new List<string>()
        {
            "int32",
            "int64",
            "sint32",
            "sint64",
            "string",
            "float",
            "double",
            "uint32",
            "uint64"
        };
        _dirPath = $"{Application.dataPath}/Config/Proto";
        this._files = new List<ProtoBufFileInfo>();
        foreach (var filePath in Directory.GetFiles(_dirPath))
        {
            if (filePath.EndsWith(".meta"))
                continue;
            this._files.Add(new ProtoBufFileInfo(Path.GetFileNameWithoutExtension(filePath), filePath));
        }

        foreach (ProtoBufFileInfo fileInfo in this._files)
        {
            foreach (ProtoBodyInfo info in fileInfo.ProtoBodyInfos)
            {
                CreateRecordList(info);
            }
        }

        this._selectInfo = this._files[0];
    }

    private void CreateRecordList(ProtoBodyInfo info)
    {
        info.ReorderableList = new ReorderableList(info.Fields,
            typeof (ProtoField), true, true, true, true);
        var fields = info.Fields;

        info.ReorderableList.drawElementCallback = (rect, i, isActive, isFocused) =>
        {
            GUI.Box(new Rect(rect.x, rect.y + 2, rect.width, rect.height - 4), "", "FrameBox");
            var field = fields[i];

            var curX = rect.x;
            var y = rect.y;
            field.Repeated = EditorGUI.Toggle(new Rect(curX + 40, y, 60, rect.height), field.Repeated);
            curX += 100;

            field.IsUnityType = EditorGUI.Toggle(new Rect(curX + 40, y, 60, rect.height), field.IsUnityType);
            curX += 100;

            if (field.IsUnityType)
            {
                field.Type = EditorGUI.TextField(new Rect(curX + 2, y + 20, 150 - 2, 20), field.Type);
            }
            else
            {
                var list = this.GetTypes(ProtoMsgType.None);
                list.AddRange(this._baseTypes);
                var fIndex = list.IndexOf(field.Type);
                fIndex = EditorGUI.Popup(new Rect(curX + 2, y + 20, 150 - 2, 20), fIndex, list.ToArray());
                field.Type = fIndex > -1? list[fIndex] : field.Type;
            }

            curX += 150;

            field.Name = EditorGUI.TextField(new Rect(curX + 2, y + 20, 100 - 2, 20), field.Name);
            curX += 100;

            field.Code = EditorGUI.IntField(new Rect(curX + 2, y + 20, 100 - 2, 20), field.Code);
            curX += 100;
            var style = new GUIStyle("ScriptText") { wordWrap = true };
            field.Note = EditorGUI.TextField(new Rect(curX + 2, y + 8, rect.width - curX - 2, rect.height - 16), field.Note, style);
        };
        info.ReorderableList.drawHeaderCallback = rect =>
        {
            var curX = rect.x;
            var style = new GUIStyle() { alignment = TextAnchor.MiddleCenter, fontStyle = FontStyle.Bold, fontSize = 14, richText = true };
            EditorGUI.LabelField(new Rect(curX, rect.y, 100, 20), "<color=white>Repeated</color>", style);
            curX += 100;
            EditorGUI.LabelField(new Rect(curX, rect.y, 100, 20), "<color=white>UnityStruct</color>", style);
            curX += 100;
            EditorGUI.LabelField(new Rect(curX, rect.y, 150, 20), "<color=white>Type</color>", style);
            curX += 150;
            EditorGUI.LabelField(new Rect(curX, rect.y, 100, 20), "<color=white>Name</color>", style);
            curX += 100;
            EditorGUI.LabelField(new Rect(curX, rect.y, 100, 20), "<color=white>Code</color>", style);
            curX += 100;
            EditorGUI.LabelField(new Rect(curX, rect.y, rect.width - curX, 20), "<color=white>注释</color>", style);
        };

        info.ReorderableList.elementHeight = 60;
    }

    private void OnGUI()
    {
        GUILayout.BeginArea(new Rect(0, 0, 200, this.position.height));
        this._scrollPos1 = EditorGUILayout.BeginScrollView(this._scrollPos1, "box");
        foreach (ProtoBufFileInfo info in this._files)
        {
            GUI.backgroundColor = this._selectInfo == info? Color.gray : Color.white;
            if (GUILayout.Button(info.FileName, "Tab onlyOne", GUILayout.Width(195), GUILayout.Height(40)))
            {
                this._selectInfo = info;
            }

            GUI.backgroundColor = Color.white;
        }

        EditorGUILayout.EndScrollView();
        GUILayout.EndArea();

        GUILayout.BeginArea(new Rect(205, 0, this.position.width - 205, 40), "", "box");
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("保存", "Tab onlyOne", GUILayout.Width(80), GUILayout.Height(20)))
        {
            this._selectInfo.Save();
            this.ShowNotification(new GUIContent("保存成功"));
        }

        if (GUILayout.Button("重置", "Tab onlyOne", GUILayout.Width(80), GUILayout.Height(20)))
        {
            if (EditorUtility.DisplayDialog("提示", "重置将丢失已编辑的数据，是否重置?", "是", "取消"))
            {
                for (int i = 0; i < this._files.Count; i++)
                {
                    if (this._selectInfo == this._files[i])
                    {
                        this._files[i] = new ProtoBufFileInfo(this._selectInfo.FileName, this._selectInfo.FilePath);
                        this._selectInfo = this._files[i];
                        break;
                    }
                }
            }
        }

        if (GUILayout.Button("新建", "Tab onlyOne", GUILayout.Width(80), GUILayout.Height(20)))
        {
            var info = new ProtoBodyInfo() { Name = $"__NewMessage{this._selectInfo.ProtoBodyInfos.Count}", };
            info.Fields.Add(new ProtoField() { Code = 90, Name = "RpcId", Type = "int32" });
            this._selectInfo.ProtoBodyInfos.Add(info);
            CreateRecordList(info);
            info.Foldout = true;
        }

        if (GUILayout.Button("编译", "Tab onlyOne", GUILayout.Width(80), GUILayout.Height(20)))
        {
            ToolsEditor.Proto2CS();
        }

        if (GUILayout.Button("打开", "Tab onlyOne", GUILayout.Width(80), GUILayout.Height(20)))
        {
            System.Diagnostics.Process.Start("notepad++.exe", this._selectInfo.FilePath);
        }

        this._searchField = EditorGUILayout.TextField(this._searchField, new GUIStyle("SearchTextField"));
        var searchString = this._searchField;
        var bodys = this._selectInfo.ProtoBodyInfos;
        var bodyList = new List<ProtoBodyInfo>();
        if (!string.IsNullOrEmpty(searchString))
        {
            foreach (var body in bodys)
            {
                if (body.Name.ToLower().Contains(searchString))
                {
                    bodyList.Add(body);
                }
            }
        }
        else
        {
            bodyList.AddRange(bodys);
        }

        GUILayout.EndHorizontal();
        GUILayout.EndArea();

        GUILayout.BeginArea(new Rect(205, 45, this.position.width - 205, this.position.height - 45));
        this._scrollPos2 = EditorGUILayout.BeginScrollView(this._scrollPos2);
        foreach (ProtoBodyInfo info in bodyList)
        {
            var style = new GUIStyle("HeaderButton") { alignment = TextAnchor.MiddleLeft };
            EditorGUILayout.BeginHorizontal();
            GUI.backgroundColor = info.Removed? Color.red : Color.white;
            if (GUILayout.Button(new GUIContent($"{(info.Foldout? '▼' : '▶')} {info.Name}", info.Note), style, GUILayout.Height(25)))
            {
                info.Foldout = !info.Foldout;
                if (info.Foldout)
                {
                    var list = this.GetTypes(ProtoMsgType.None);
                    list.AddRange(this._baseTypes);
                    foreach (var field in info.Fields)
                    {
                        var fIndex = list.IndexOf(field.Type);
                        field.IsUnityType = fIndex == -1;
                    }
                }
            }

            GUI.backgroundColor = Color.white;

            if (GUILayout.Button(new GUIContent("C", "克隆"), "HeaderButton", GUILayout.Width(25), GUILayout.Height(25)))
            {
                var json = JsonUtility.ToJson(info);
                var obj = JsonUtility.FromJson<ProtoBodyInfo>(json);
                CreateRecordList(obj);
                this._selectInfo.ProtoBodyInfos.Add(obj);
            }

            var content = new GUIContent(info.Removed? "←" : "X", info.Removed? "撤销删除" : "删除");
            if (GUILayout.Button(content, "HeaderButton", GUILayout.Width(25), GUILayout.Height(25)))
            {
                info.Removed = !info.Removed;
            }

            EditorGUILayout.EndHorizontal();
            if (info.Foldout)
            {
                EditorGUILayout.BeginVertical("U2D.createRect");
                info.Name = EditorGUILayout.TextField("Name", info.Name);
                info.MessageType = (ProtoMsgType)EditorGUILayout.EnumPopup("Type", info.MessageType);
                if (info.MessageType == ProtoMsgType.IRequest
                    || info.MessageType == ProtoMsgType.IActorRequest
                    || info.MessageType == ProtoMsgType.IActorLocationRequest)
                {
                    List<string> list;
                    if (info.MessageType == ProtoMsgType.IRequest)
                        list = this.GetTypes(ProtoMsgType.IResponse);
                    else if (info.MessageType == ProtoMsgType.IActorRequest)
                        list = this.GetTypes(ProtoMsgType.IActorResponse);
                    else
                        list = this.GetTypes(ProtoMsgType.IActorLocationResponse);
                    var index = list.IndexOf(info.ResponseType);
                    index = EditorGUILayout.Popup("ResponseType", index, list.ToArray());
                    if (index >= 0)
                    {
                        info.ResponseType = list[index];
                    }
                }

                #region 注释

                EditorGUILayout.BeginVertical("box");
                EditorGUILayout.LabelField("多行注释");
                if (info.Notes != null)
                {
                    info.MutiNotes.AddRange(info.Notes);
                    info.Notes = null;
                }

                if (info.MutiNotes != null)
                {
                    string text = "";
                    for (int i = 0; i < info.MutiNotes.Count; i++)
                    {
                        text += info.MutiNotes[i];
                        text += "\n";
                    }

                    text = text.Trim('\n');
                    var newText = EditorGUILayout.TextArea(text);
                    if (text != newText)
                    {
                        var ss = newText.Split('\n');
                        info.MutiNotes.Clear();
                        foreach (string s in ss)
                        {
                            info.MutiNotes.Add(s);
                        }
                    }
                }

                EditorGUILayout.EndVertical();

                #endregion

                #region 属性

                EditorGUILayout.BeginVertical("box");
                info.ReorderableList.DoLayoutList();
                EditorGUILayout.EndVertical();

                #endregion

                EditorGUILayout.EndVertical();
            }
        }

        EditorGUILayout.EndScrollView();
        GUILayout.EndArea();
    }

    public List<string> GetTypes(params ProtoMsgType[] types)
    {
        var res = new List<string>();
        foreach (ProtoBufFileInfo fileInfo in this._files)
        {
            foreach (var customType in fileInfo.ProtoBodyInfos)
            {
                foreach (ProtoMsgType type in types)
                {
                    if (customType.MessageType == type)
                    {
                        res.Add(customType.Name);
                        break;
                    }
                }
            }
        }

        return res;
    }

    public class ProtoBufFileInfo
    {
        public string FileName;
        public string FilePath;

        private static string[] HEADER = new[] { "syntax = \"proto3\";", "package ET;" };

        private List<ProtoBodyInfo> _customTypes;

        public List<ProtoBodyInfo> ProtoBodyInfos;

        private State _curState;
        private bool _isReadBody;
        private bool _isMainBody;
        private ProtoBodyInfo _curBody;
        private long _length;

        public List<string> GetTypes(params ProtoMsgType[] types)
        {
            var res = new List<string>();
            foreach (var customType in this._customTypes)
            {
                foreach (ProtoMsgType type in types)
                {
                    if (customType.MessageType == type)
                    {
                        res.Add(customType.Name);
                        break;
                    }
                }
            }

            return res;
        }

        public ProtoBufFileInfo(string name, string filePath)
        {
            this.ProtoBodyInfos = new List<ProtoBodyInfo>();
            this._customTypes = new List<ProtoBodyInfo>();

            this.FileName = name;
            this.FilePath = filePath;
            Parse(filePath);
        }

        private void Parse(string filePath)
        {
            this._length = File.ReadAllBytes(filePath).LongLength;
            var lines = File.ReadLines(filePath).ToArray();
            var index = 0;
            var notes = new Dictionary<int, string>();
            for (int j = 0; j < lines.Length; j++)
            {
                var line = lines[j].Trim();
                if (index < 3)
                {
                    index++;
                    continue;
                }

                if (!this._isReadBody)
                {
                    if (BeginRead(line))
                    {
                        this._isReadBody = true;
                        this._curBody = new ProtoBodyInfo();
                        this.ProtoBodyInfos.Add(this._curBody);
                    }
                }

                if (this._isReadBody)
                {
                    if (this._curState == State.ReadNote)
                    {
                        if (Regex.IsMatch(line, @"/// +<summary>"))
                        {
                        }
                        else if (Regex.IsMatch(line, @"/// +</summary>"))
                            this._curState = State.None;
                        else
                            this._curBody.MutiNotes.Add(line.Replace("///", "").Trim());
                    }
                    else
                    {
                        if (line.StartsWith("//"))
                        {
                            if (line.StartsWith("//ResponseType"))
                                this._curBody.ResponseType = line.Replace("//ResponseType", "").Trim();
                            else
                            {
                                if (!this._isMainBody)
                                    this._curBody.Notes.Add(line.Replace("//", "").Trim());
                                notes.Add(j, line.Replace("//", "").Trim());
                            }
                        }
                        else if (line.StartsWith("message"))
                        {
                            var ss = line.Replace("message", "").Split(new[] { "//" }, StringSplitOptions.RemoveEmptyEntries);
                            this._curBody.Name = ss[0].Trim();
                            if (!this._customTypes.Contains(this._curBody))
                                this._customTypes.Add(this._curBody);
                            if (ss.Length == 1)
                                this._curBody.MessageType = ProtoMsgType.None;
                            else
                            {
                                this._curBody.MessageType = (ProtoMsgType)Enum.Parse(typeof (ProtoMsgType), ss[1].Trim());
                            }
                        }
                        else if (line == "{")
                        {
                            this._isMainBody = true;
                        }
                        else if (line == "}")
                        {
                            this._isMainBody = false;
                            this._isReadBody = false;
                        }
                        else
                        {
                            if (string.IsNullOrEmpty(line)) continue;
                            var field = new ProtoField();
                            this._curBody.Fields.Add(field);

                            field.Repeated = line.Contains("repeated");
                            line = line.Replace("repeated", "").Replace(";", "").Trim();

                            var note = Regex.Match(line, @"//.*");
                            if (note.Success)
                            {
                                field.Note = note.Value.Replace("//", "").Trim();
                                line = line.Replace(note.Value, "").Trim();
                            }
                            else
                            {
                                if (string.IsNullOrEmpty(field.Note))
                                {
                                    if (notes.ContainsKey(j - 1))
                                        field.Note = notes[j - 1];
                                }
                            }

                            var ss = line.Split(' ');
                            var arr = new string[4];
                            var arri = 0;
                            for (int i = 0; i < ss.Length; i++)
                            {
                                if (!string.IsNullOrEmpty(ss[i]))
                                    arr[arri++] = ss[i];
                            }

                            field.Type = arr[0];
                            field.Name = arr[1];
                            field.Code = Convert.ToInt32(arr[3]);
                        }
                    }
                }
            }
        }

        public void Save()
        {
            var filePath = $"{_dirPath}/{this.FileName}.proto";
            var lines = this.ToLines();
            File.WriteAllLines(filePath, lines);
        }

        private List<string> ToLines()
        {
            List<string> lines = new List<string>();
            lines.AddRange(HEADER);
            lines.Add("");
            foreach (ProtoBodyInfo protoBodyInfo in this.ProtoBodyInfos)
            {
                if (protoBodyInfo.Removed)
                    continue;
                if (protoBodyInfo.MutiNotes.Count > 0)
                {
                    lines.Add("/// <summary>");
                    foreach (string mutiNote in protoBodyInfo.MutiNotes)
                    {
                        lines.Add($"/// {mutiNote}");
                    }

                    lines.Add("/// </summary>");
                }

                if (!string.IsNullOrEmpty(protoBodyInfo.ResponseType))
                    lines.Add($"//ResponseType {protoBodyInfo.ResponseType}");
                lines.Add(
                    $"message {protoBodyInfo.Name}{(protoBodyInfo.MessageType == ProtoMsgType.None? "" : $" // {protoBodyInfo.MessageType.ToString()}")}");
                lines.Add("{");
                foreach (ProtoField protoField in protoBodyInfo.Fields)
                {
                    lines.Add(
                        $"  {(protoField.Repeated? "repeated " : "")}{protoField.Type} {protoField.Name} = {protoField.Code}; {(string.IsNullOrEmpty(protoField.Note)? "" : $"// {protoField.Note}")}");
                }

                lines.Add("}");
                lines.Add("");
            }

            return lines;
        }

        private bool BeginRead(string line)
        {
            line = line.Trim();
            if (Regex.IsMatch(line, @"/// +<summary>"))
            {
                this._curState = State.ReadNote;
                return true;
            }

            if (line.StartsWith("//"))
                return true;

            if (line.StartsWith("message"))
                return true;
            return false;
        }

        private enum State
        {
            None,
            ReadNote,
        }
    }

    [Serializable]
    public class ProtoBodyInfo
    {
        public string Note
        {
            get
            {
                var sb = new StringBuilder();
                if (this.Notes?.Count > 0)
                {
                    foreach (string note in this.Notes)
                    {
                        sb.AppendLine(note);
                    }
                }
                else if (this.MutiNotes?.Count > 0)
                {
                    foreach (string mutiNote in this.MutiNotes)
                    {
                        sb.AppendLine(mutiNote);
                    }
                }

                return sb.ToString();
            }
        }

        public bool Foldout;
        public bool Removed;
        public ReorderableList ReorderableList;

        public string Name;
        public List<string> Notes = new List<string>();
        public List<string> MutiNotes = new List<string>();
        public List<ProtoField> Fields = new List<ProtoField>();
        public ProtoMsgType MessageType;
        public string ResponseType;
    }

    [Serializable]
    public class ProtoField
    {
        public string Type;
        public string Name;
        public int Code;
        public string Note;
        public bool Repeated;
        public bool IsUnityType;
    }

    public enum ProtoMsgType
    {
        None,
        IActorRequest,
        IActorResponse,
        IActorMessage,
        IActorLocationRequest,
        IActorLocationResponse,
        IActorLocationMessage,
        IRequest,
        IResponse,
        IMessage,
    }
}