using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Timers;

using Dalamud.Logging;

namespace SillyChat
{
    /// <summary>
    /// Manage history of translations.
    /// </summary>
    public class HistoryService
    {
        private readonly ISillyChatPlugin plugin;
        private readonly Timer processTranslationsTimer;

        /// <summary>
        /// Initializes a new instance of the <see cref="HistoryService"/> class.
        /// </summary>
        /// <param name="plugin">SillyChat plugin.</param>
        public HistoryService(ISillyChatPlugin plugin)
        {
            this.plugin = plugin;
            this.Translations = new ConcurrentQueue<Translation>();
            this.TranslationsDisplay = new List<Translation>();
            this.processTranslationsTimer = new Timer
            {
                Interval = plugin.Configuration.ProcessTranslationInterval,
                Enabled = true,
            };
            this.processTranslationsTimer.Elapsed += this.ProcessTranslationsTimerOnElapsed;
            this.processTranslationsTimer.Start();
        }

        /// <summary>
        /// Gets a value indicating whether gets indicator if history is being processed.
        /// </summary>
        public bool IsProcessing { get; private set; }

        /// <summary>
        /// Gets list of current historical records for display.
        /// </summary>
        public List<Translation> TranslationsDisplay { get; private set; }

        /// <summary>
        /// Gets queue for historical translations.
        /// </summary>
        public ConcurrentQueue<Translation> Translations { get; }

        /// <summary>
        /// Add translation to queue.
        /// </summary>
        /// <param name="translation">translation.</param>
        public void AddTranslation(Translation translation)
        {
            this.Translations.Enqueue(translation);
            if (!this.IsProcessing)
            {
                this.TranslationsDisplay.Add(translation);
            }
        }

        /// <summary>
        /// Dispose history service.
        /// </summary>
        public void Dispose()
        {
            this.processTranslationsTimer.Stop();
            this.processTranslationsTimer.Elapsed -= this.ProcessTranslationsTimerOnElapsed;
        }

        private void ProcessTranslationsTimerOnElapsed(object? sender, ElapsedEventArgs e)
        {
            try
            {
                this.IsProcessing = true;
                while (this.Translations.Count > this.plugin.Configuration.TranslationHistoryMax)
                {
                    this.Translations.TryDequeue(out _);
                }

                this.TranslationsDisplay = this.Translations.ToList();
                this.IsProcessing = false;
            }
            catch (Exception ex)
            {
                SillyChatPlugin.PluginLog.Error(ex, "Failed to process translations.");
                this.IsProcessing = false;
            }
        }
    }
}
