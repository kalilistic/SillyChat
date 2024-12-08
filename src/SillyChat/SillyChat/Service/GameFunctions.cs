using Dalamud.Game.ClientState.Objects.Types;
using Dalamud.Game.Text.SeStringHandling;
using Dalamud.Hooking;
using Dalamud.Plugin.Services;
using Dalamud.Utility.Signatures;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using FFXIVClientStructs.FFXIV.Client.Game;
using FFXIVClientStructs.FFXIV.Client.Game.UI;
using FFXIVClientStructs.FFXIV.Client.System.Framework;
using FFXIVClientStructs.FFXIV.Client.System.String;
using FFXIVClientStructs.FFXIV.Client.UI.Agent;
using FFXIVClientStructs.FFXIV.Client.UI.Misc;
using FFXIVClientStructs.FFXIV.Component.GUI;
using XivCommon.Functions;
using Dalamud.Game.Addon.Lifecycle;
using FFXIVClientStructs.FFXIV.Client.UI;
using static FFXIVClientStructs.FFXIV.Client.UI.UIModule.Delegates;
using Dalamud.Game.Addon.Lifecycle.AddonArgTypes;

namespace SillyChat.SillyChat.Service
{
    public unsafe class GameFunctions
    {
        public SillyChatPlugin Plugin;

        public delegate void ChatBubbleEventDelegate(ref IGameObject @object, ref SeString text);

        [Signature("48 85 D2 0F 84 ?? ?? ?? ?? 48 89 6C 24 ?? 56 48 83 EC 30 8B 41 0C")]
        public ChatBubbleEventDelegate? OnChatBubbleDelegate = null;



        /// <summary>
        /// Options for displaying a BattleTalk window.
        /// </summary>
        public class BattleTalkOptions
        {
            /// <summary>
            /// Duration to display the window, in seconds.
            /// </summary>
            public float Duration { get; set; } = 5f;

            /// <summary>
            /// The style of the window.
            /// </summary>
            public BattleTalkStyle Style { get; set; } = BattleTalkStyle.Normal;
        }
    }
}
