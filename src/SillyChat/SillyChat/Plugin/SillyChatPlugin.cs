using System;
using System.Threading;
using System.Threading.Tasks;

using CheapLoc;
using Dalamud.Configuration;
using Dalamud.DrunkenToad;
using Dalamud.Game.ClientState.Objects.Types;
using Dalamud.Game.Command;
using Dalamud.Game.Gui;
using Dalamud.Game.Text;
using Dalamud.Game.Text.SeStringHandling;
using Dalamud.Game.Text.SeStringHandling.Payloads;
using Dalamud.IoC;
using Dalamud.Plugin;
using XivCommon;
using XivCommon.Functions;

namespace SillyChat
{
    /// <summary>
    /// SillyChat Plugin.
    /// </summary>
    public class SillyChatPlugin : ISillyChatPlugin, IDalamudPlugin
    {
        private XivCommonBase xivCommon = null!;
        private Localization localization = null!;

        /// <summary>
        /// Initializes a new instance of the <see cref="SillyChatPlugin"/> class.
        /// </summary>
        public SillyChatPlugin()
        {
            Task.Run(() =>
            {
                // setup common libs
                this.localization = new Localization(PluginInterface, CommandManager);
                const Hooks hooks = Hooks.Talk | Hooks.BattleTalk | Hooks.ChatBubbles;
                this.xivCommon = new XivCommonBase(hooks);

                // load config
                try
                {
                    this.Configuration = PluginInterface.GetPluginConfig() as PluginConfig ?? new PluginConfig();
                }
                catch (Exception ex)
                {
                    Logger.LogError("Failed to load config so creating new one.", ex);
                    this.Configuration = new PluginConfig();
                    this.SaveConfig();
                }

                // setup translator
                this.TranslationService = new TranslationService(this);
                this.HistoryService = new HistoryService(this);
                Chat.ChatMessage += this.ChatMessage;
                this.xivCommon.Functions.BattleTalk.OnBattleTalk += this.OnBattleTalk;
                this.xivCommon.Functions.Talk.OnTalk += this.OnTalk;

                this.xivCommon.Functions.ChatBubbles.OnChatBubble += this.OnChatBubble;

                // setup ui
                this.WindowManager = new WindowManager(this);

                // special handling for fresh install
                this.HandleFreshInstall();

                // toggle
                CommandManager.AddHandler("/silly", new CommandInfo(this.TogglePlugin)
                {
                    HelpMessage = Loc.Localize("ToggleCommandHelp", "Toggle SillyChat."),
                    ShowInHelp = true,
                });
            });
        }

        /// <summary>
        /// Gets pluginInterface.
        /// </summary>
        [PluginService]
        [RequiredVersion("1.0")]
        public static DalamudPluginInterface PluginInterface { get; private set; } = null!;

        /// <summary>
        /// Gets chat gui.
        /// </summary>
        [PluginService]
        [RequiredVersion("1.0")]
        public static ChatGui Chat { get; private set; } = null!;

        /// <summary>
        /// Gets command manager.
        /// </summary>
        [PluginService]
        [RequiredVersion("1.0")]
        public static CommandManager CommandManager { get; private set; } = null!;

        /// <inheritdoc />
        public string Name => "SillyChat";

        /// <summary>
        /// Gets translationService.
        /// </summary>
        public TranslationService TranslationService { get; private set; } = null!;

        /// <summary>
        /// Gets historyService.
        /// </summary>
        public HistoryService HistoryService { get; private set; } = null!;

        /// <summary>
        /// Gets or sets plugin name.
        /// </summary>
        public string PluginName { get; set; } = null!;

        /// <inheritdoc/>
        public SillyChatConfig Configuration { get; set; } = null!;

        /// <summary>
        /// Gets or sets window manager.
        /// </summary>
        public WindowManager WindowManager { get; set; } = null!;

        /// <inheritdoc/>
        public void SaveConfig()
        {
            PluginInterface.SavePluginConfig((IPluginConfiguration)this.Configuration);
        }

        /// <summary>
        /// Dispose sillyChat plugin.
        /// </summary>
        public void Dispose()
        {
            try
            {
                this.HistoryService.Dispose();
                this.WindowManager.Dispose();

                // plugin service
                CommandManager.RemoveHandler("/silly");
                Chat.ChatMessage -= this.ChatMessage;

                // xiv common
                this.xivCommon.Functions.ChatBubbles.OnChatBubble -= this.OnChatBubble;
                this.xivCommon.Functions.BattleTalk.OnBattleTalk -= this.OnBattleTalk;
                this.xivCommon.Functions.Talk.OnTalk -= this.OnTalk;
                this.xivCommon.Dispose();

                // localization
                this.localization.Dispose();
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Failed to dispose plugin properly.");
            }
        }

        private void TogglePlugin(string command, string args)
        {
            this.Configuration.Enabled ^= true;
            this.SaveConfig();
        }

        private void ChatMessage(XivChatType type, uint senderId, ref SeString sender, ref SeString message, ref bool isHandled)
        {
            if (!this.Configuration.Enabled)
            {
                return;
            }

            if (isHandled)
            {
                return;
            }

            var chatType = (uint)type & ~(~0 << 7);
            if (!Enum.IsDefined(typeof(SupportedChatType), chatType))
            {
                return;
            }

            this.Translate(message);
        }

        private void OnTalk(ref SeString name, ref SeString text, ref TalkStyle style)
        {
            if (!this.Configuration.Enabled)
            {
                return;
            }

            this.Translate(text);
        }

        private void OnBattleTalk(ref SeString sender, ref SeString message, ref BattleTalkOptions options, ref bool isHandled)
        {
            if (!this.Configuration.Enabled)
            {
                return;
            }

            if (isHandled)
            {
                return;
            }

            this.Translate(message);
        }

        private void OnChatBubble(ref GameObject gameObject, ref SeString text)
        {
            if (!this.Configuration.Enabled)
            {
                return;
            }

            this.Translate(text);
        }

        private void Translate(SeString message)
        {
            try
            {
                foreach (var payload in message.Payloads)
                {
                    if (payload is TextPayload textPayload)
                    {
                        var input = textPayload.Text;
                        if (string.IsNullOrEmpty(input) || input.Contains('\uE0BB'))
                        {
                            continue;
                        }

                        var output = this.TranslationService.Translate(input);
                        if (!input.Equals(output))
                        {
                            textPayload.Text = output;
                            Logger.LogDebug($"{input}|{output}");
                            this.HistoryService.AddTranslation(new Translation(input, output));
                        }
                    }
                }
            }
            catch
            {
                Logger.LogDebug($"Failed to process message: {message}.");
            }
        }

        private void HandleFreshInstall()
        {
            if (!this.Configuration.FreshInstall)
            {
                return;
            }

            Chat.PluginPrintNotice(Loc.Localize("InstallThankYou", "Thank you for installing SillyChat!"));
            Thread.Sleep(500);
            Chat.PluginPrintNotice(
                Loc.Localize("Instructions", "You can use /silly to toggle the plugin, /sillyconfig to view settings, and /sillyhistory to see previous translations."));
            this.Configuration.FreshInstall = false;
            this.SaveConfig();
            this.WindowManager.ConfigWindow!.IsOpen = true;
        }
    }
}
