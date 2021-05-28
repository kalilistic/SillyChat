using System;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

using CheapLoc;
using Dalamud.Game.ClientState.Actors.Types;
using Dalamud.Game.Command;
using Dalamud.Game.Text;
using Dalamud.Game.Text.SeStringHandling;
using Dalamud.Game.Text.SeStringHandling.Payloads;
using Dalamud.Plugin;
using DalamudPluginCommon;
using XivCommon;
using XivCommon.Functions;

namespace SillyChat
{
    /// <summary>
    /// SillyChat Plugin.
    /// </summary>
    public class SillyChatPlugin : PluginBase, ISillyChatPlugin
    {
        private DalamudPluginInterface pluginInterface = null!;
        private XivCommonBase common = null!;

        /// <summary>
        /// Initializes a new instance of the <see cref="SillyChatPlugin"/> class.
        /// </summary>
        /// <param name="pluginName">Plugin name.</param>
        /// <param name="pluginInterface">Plugin interface.</param>
        public SillyChatPlugin(string pluginName, DalamudPluginInterface pluginInterface)
            : base(pluginName, pluginInterface, Assembly.GetExecutingAssembly())
        {
            Task.Run(() =>
            {
                this.PluginName = pluginName;
                this.pluginInterface = pluginInterface;

                // load config
                try
                {
                    this.Configuration = this.LoadConfig() as PluginConfig ?? new PluginConfig();
                }
                catch (Exception ex)
                {
                    Logger.LogError("Failed to load config so creating new one.", ex);
                    this.Configuration = new PluginConfig();
                    this.SaveConfig();
                }

                // setup common
                const Hooks hooks = Hooks.Talk | Hooks.BattleTalk | Hooks.ChatBubbles;
                this.common = new XivCommonBase(pluginInterface, hooks);

                // setup translator
                this.TranslationService = new TranslationService(this);
                this.HistoryService = new HistoryService(this);
                pluginInterface.Framework.Gui.Chat.OnChatMessage += this.OnChatMessage;
                this.common.Functions.BattleTalk.OnBattleTalk += this.OnBattleTalk;
                this.common.Functions.Talk.OnTalk += this.OnTalk;
                this.common.Functions.ChatBubbles.OnChatBubble += this.OnChatBubble;

                // setup ui
                this.WindowManager = new WindowManager(this, pluginInterface);

                // special handling for fresh install
                this.HandleFreshInstall();

                // toggle
                this.PluginInterface.CommandManager.AddHandler("/silly", new CommandInfo(this.TogglePlugin)
                {
                    HelpMessage = Loc.Localize("ToggleCommandHelp", "Toggle SillyChat."),
                    ShowInHelp = true,
                });
            });
        }

        /// <summary>
        /// Gets translationService.
        /// </summary>
        public TranslationService TranslationService { get; private set; } = null!;

        /// <summary>
        /// Gets historyService.
        /// </summary>
        public HistoryService HistoryService { get; private set; } = null!;

        /// <inheritdoc/>
        public new string PluginName { get; set; } = null!;

        /// <inheritdoc/>
        public SillyChatConfig Configuration { get; set; } = null!;

        /// <summary>
        /// Gets or sets window manager.
        /// </summary>
        public WindowManager WindowManager { get; set; } = null!;

        /// <inheritdoc/>
        public void SaveConfig()
        {
            this.SaveConfig(this.Configuration);
        }

        /// <summary>
        /// Dispose sillyChat plugin.
        /// </summary>
        public new void Dispose()
        {
            this.HistoryService.Dispose();
            this.common.Dispose();
            base.Dispose();
            this.WindowManager.Dispose();
            this.common.Functions.BattleTalk.OnBattleTalk -= this.OnBattleTalk;
            this.common.Functions.Talk.OnTalk -= this.OnTalk;
            this.pluginInterface.Framework.Gui.Chat.OnChatMessage -= this.OnChatMessage;
            this.common.Functions.ChatBubbles.OnChatBubble -= this.OnChatBubble;
            this.pluginInterface.CommandManager.RemoveHandler("/silly");
            this.pluginInterface.Dispose();
        }

        private void TogglePlugin(string command, string args)
        {
            this.Configuration.Enabled ^= true;
            this.SaveConfig();
        }

        private void OnChatMessage(XivChatType type, uint senderId, ref SeString sender, ref SeString message, ref bool isHandled)
        {
            if (!this.Configuration.Enabled) return;
            if (isHandled) return;
            var chatType = (uint)type & ~(~0 << 7);
            if (!Enum.IsDefined(typeof(SupportedChatType), chatType)) return;
            this.Translate(message);
        }

        private void OnTalk(ref SeString name, ref SeString text, ref TalkStyle style)
        {
            if (!this.Configuration.Enabled) return;
            this.Translate(text);
        }

        private void OnBattleTalk(ref SeString sender, ref SeString message, ref BattleTalkOptions options, ref bool isHandled)
        {
            if (!this.Configuration.Enabled) return;
            if (isHandled) return;
            this.Translate(message);
        }

        private void OnChatBubble(ref Actor actor, ref SeString text)
        {
            if (!this.Configuration.Enabled) return;
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
                        if (string.IsNullOrEmpty(input) || input.Contains('\uE0BB')) continue;
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

            this.Chat.PrintNotice(Loc.Localize("InstallThankYou", "Thank you for installing SillyChat!"));
            Thread.Sleep(500);
            this.Chat.PrintNotice(
                Loc.Localize("Instructions", "You can use /silly to toggle the plugin, /sillyconfig to view settings, and /sillyhistory to see previous translations."));
            this.Configuration.FreshInstall = false;
            this.SaveConfig();
            this.WindowManager.ConfigWindow!.IsOpen = true;
        }
    }
}
