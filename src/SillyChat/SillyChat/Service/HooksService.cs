using Dalamud.Game.ClientState.Objects.Types;
using Dalamud.Game.Text.SeStringHandling;
using Dalamud.Utility.Signatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XivCommon.Functions;

namespace SillyChat.SillyChat.Service
{
    public class HooksService
    {

        public delegate void ChatBubbleEventDelegate(ref IGameObject @object, ref SeString text);

        public delegate void BattleTalkEventDelegate(ref SeString sender, ref SeString message, ref BattleTalkOptions options, ref bool isHandled);

        public delegate void TalkEventDelegate(ref SeString name, ref SeString text, ref XivCommon.Functions.TalkStyle style);

        [Signature("48 85 D2 0F 84 ?? ?? ?? ?? 48 89 6C 24 ?? 56 48 83 EC 30 8B 41 0C")]
        public ChatBubbleEventDelegate? OnChatBubbleDelegate = null;

        [Signature("E8 ?? ?? ?? ?? 41 88 84 2C")]
        public BattleTalkEventDelegate? OnBattleTalkDelegate = null;

        [Signature("E8 ?? ?? ?? ?? 41 88 84 2C")]
        public TalkEventDelegate? OnTalkDelegate = null;

        public HooksService()
        {
        }

        public void Dispose()
        {

        }
    }
}
