using System;
using System.Threading;
using System.Threading.Tasks;

using CheapLoc;
using Dalamud.Configuration;
using Dalamud.Game.ClientState.Objects.Types;
using Dalamud.Game.Command;
using Dalamud.Game.Text;
using Dalamud.Game.Text.SeStringHandling;
using Dalamud.Game.Text.SeStringHandling.Payloads;
using Dalamud.IoC;
using Dalamud.Plugin;
using Dalamud.Plugin.Services;
using Dalamud.Utility.Signatures;
using SillyChat.Localization;
using SillyChat.SillyChat.Service;

namespace SillyChat
{
    /// <summary>
    /// SillyChat Plugin.
    /// </summary>
    public class SillyChatPlugin : ISillyChatPlugin, IDalamudPlugin
    {
        private LegacyLoc localization = null!;
        private HooksService Hooks = null!;

        /// <summary>
        /// Initializes a new instance of the <see cref="SillyChatPlugin"/> class.
        /// </summary>
        public SillyChatPlugin()
        {
            Task.Run(() =>
            {
                // setup common libs
                this.localization = new LegacyLoc(PluginInterface, CommandManager);
                this.Hooks = new HooksService();

                // load config
                try
                {
                    this.Configuration = PluginInterface.GetPluginConfig() as PluginConfig ?? new PluginConfig();
                }
                catch (Exception ex)
                {
                    PluginLog.Error("Failed to load config so creating new one.", ex);
                    this.Configuration = new PluginConfig();
                    this.SaveConfig();
                }

                // setup translator
                this.TranslationService = new TranslationService(this);
                this.HistoryService = new HistoryService(this);
                Chat.ChatMessage += this.ChatMessage;

                this.Hooks.OnBattleTalkDelegate += this.OnBattleTalk;
                this.Hooks.OnTalkDelegate += this.OnTalk;
                this.Hooks.OnChatBubbleDelegate += this.OnChatBubble;

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
                Hooks.OnChatBubbleDelegate -= this.OnChatBubble;
                Hooks.OnBattleTalkDelegate -= this.OnBattleTalk;
                Hooks.OnTalkDelegate -= this.OnTalk;
                this.Hooks.Dispose();

                // localization
                this.localization.Dispose();
            }
            catch (Exception ex)
            {
                PluginLog.Error(ex, "Failed to dispose plugin properly.");
            }
        }

        /// <summary>
        /// Gets pluginInterface.
        /// </summary>
        [PluginService]
        public static IDalamudPluginInterface PluginInterface { get; private set; } = null!;

        /// <summary>
        /// Gets chat gui.
        /// </summary>
        [PluginService]
        public static IChatGui Chat { get; private set; } = null!;

        /// <summary>
        /// Gets chat gui.
        /// </summary>
        [PluginService]
        public static IFramework Framework { get; private set; } = null!;

        /// <summary>
        /// Gets command manager.
        /// </summary>
        [PluginService]
        public static ICommandManager CommandManager { get; private set; } = null!;

        /// <summary>
        /// Gets plugin log.
        /// </summary>
        [PluginService]
        public static IPluginLog PluginLog { get; private set; } = null!;

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

        private void TogglePlugin(string command, string args)
        {
            this.Configuration.Enabled ^= true;
            this.SaveConfig();
        }

        private void ChatMessage(Dalamud.Game.Text.XivChatType type, int timestamp, ref SeString sender, ref SeString message, ref bool isHandled)
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

        private void OnTalk(ref SeString name, ref SeString text, ref XivCommon.Functions.TalkStyle style)
        {
            if (!this.Configuration.Enabled)
            {
                return;
            }

            this.Translate(text);
        }

        private void OnBattleTalk(ref SeString sender, ref SeString message, ref XivCommon.Functions.BattleTalkOptions options, ref bool isHandled)
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

        private void OnChatBubble(ref IGameObject gameObject, ref SeString text)
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
                            PluginLog.Debug($"{input}|{output}");
                            this.HistoryService.AddTranslation(new Translation(input, output));
                        }
                    }
                }
            }
            catch
            {
                PluginLog.Debug($"Failed to process message: {message}.");
            }
        }

        private void HandleFreshInstall()
        {
            if (!this.Configuration.FreshInstall)
            {
                return;
            }

            Chat.Print(Loc.Localize("InstallThankYou", "Thank you for installing SillyChat!"));
            Thread.Sleep(500);
            Chat.Print(
                Loc.Localize("Instructions", "You can use /silly to toggle the plugin, /sillyconfig to view settings, and /sillyhistory to see previous translations."));
            this.Configuration.FreshInstall = false;
            this.SaveConfig();
            this.WindowManager.ConfigWindow!.IsOpen = true;
        }
    }
}
