using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

using Dalamud.DrunkenToad;
using Dalamud.DrunkenToad.Extensions;

namespace SillyChat
{
    /// <summary>
    /// Translate to Urianger.
    /// </summary>
    public class UriangerTranslator : BaseTranslator
    {
        private readonly Dictionary<string, string> mappedWords = new ();
        private readonly Dictionary<string, string> mappedPartialPhrases = new ();
        private readonly Dictionary<string, string> mappedFullPhrases = new ();

        /// <summary>
        /// Initializes a new instance of the <see cref="UriangerTranslator"/> class.
        /// </summary>
        /// <param name="plugin">SillyChat plugin.</param>
        public UriangerTranslator(ISillyChatPlugin plugin)
            : base(plugin)
        {
            this.SetWordMapping();
            this.SetFullPhraseMapping();
            this.SetPartialPhraseMapping();
        }

        /// <inheritdoc />
        public override string Translate(string input)
        {
            // full phrases
            foreach (var phrase in this.mappedFullPhrases)
            {
                if (Regex.IsMatch(input, phrase.Key))
                {
                    return phrase.Value;
                }
            }

            // partial phrases
            var outputText = input.ToLower();
            foreach (var phrase in this.mappedPartialPhrases)
            {
                outputText = outputText.Replace(phrase.Key, phrase.Value);
            }

            var words = outputText.Split(' ');
            var outputList = new List<string>();

            // words
            foreach (var word in words)
            {
                string currentWord = char.IsPunctuation(word.Last()) ? string.Concat(word.Take(word.Length - 1)) : word;

                if (this.mappedWords.ContainsKey(currentWord))
                {
                    currentWord = this.mappedWords[currentWord];
                    if (char.IsUpper(word.First()))
                    {
                        currentWord = new CultureInfo("en-US").TextInfo.ToTitleCase(currentWord);
                    }

                    if (char.IsPunctuation(word.Last()))
                    {
                        currentWord += word.Last();
                    }

                    outputList.Add(currentWord);
                }
                else
                {
                    outputList.Add(word);
                }
            }

            // merge & post fixes
            outputText = string.Join(" ", outputList);
            outputText = RunPostFixes(outputText);

            return outputText;
        }

        private static string RunPostFixes(string str)
        {
            str = str.EnsureEndsWithDot();
            str = str.CapitalizeFirst();
            str = str.Replace("  ", " ");
            str = str.Replace(" .", ".");
            str = str.Replace(" i ", " I ");
            str = str.Replace(" i' ", " I' ");
            str = str.Replace("?.", "?");
            str = str.Replace("??", "?");
            str = str.Replace("dont", "don't");
            return str;
        }

        private void SetFullPhraseMapping()
        {
            this.mappedFullPhrases.Add("^focus$", "We must needs remain vigilant.");
            this.mappedFullPhrases.Add("^how are you\\?$", "How fares the realm?");
            this.mappedFullPhrases.Add("^i'?ll come$", "I shall accompany thee on thy mission.");
            this.mappedFullPhrases.Add("^ready\\?$", "Thou art prepared, I presume?");
            this.mappedFullPhrases.Add("^ready[^?]*$", "'Twould seem so.");
            this.mappedFullPhrases.Add("^(you|u) (ok|okay)\\??$", "Art thou unwell?");
        }

        private void SetPartialPhraseMapping()
        {
            this.mappedPartialPhrases.Add("how are you", "how art thou?");
            this.mappedPartialPhrases.Add("hows it going", "how art thou?");
            this.mappedPartialPhrases.Add("how's it going", "how art thou?");
            this.mappedPartialPhrases.Add("take care", "May we all meet again ere long");
            this.mappedPartialPhrases.Add("good luck", "I wish you every success");
            this.mappedPartialPhrases.Add("i don't know", "I know not");
            this.mappedPartialPhrases.Add("i dont know", "I know not");
            this.mappedPartialPhrases.Add("whats up", "how art thou?");
            this.mappedPartialPhrases.Add("what's up", "how art thou?");
        }

        private void SetWordMapping()
        {
            this.mappedWords.Add("afk", "A moment to collect my thoughts, I prithee...");
            this.mappedWords.Add("again", "once more");
            this.mappedWords.Add("are", "art");
            this.mappedWords.Add("back", "returned");
            this.mappedWords.Add("bad", "dire");
            this.mappedWords.Add("ban", "exile");
            this.mappedWords.Add("before", "ere");
            this.mappedWords.Add("between", "'twixt");
            this.mappedWords.Add("brb", "I shall be but a few moments.");
            this.mappedWords.Add("btw", "Let us speak of another matter.");
            this.mappedWords.Add("bye", "farewell");
            this.mappedWords.Add("byebye", "farewell");
            this.mappedWords.Add("cmon", "I pray thee.");
            this.mappedWords.Add("come", "cameth");
            this.mappedWords.Add("cool", "excellent");
            this.mappedWords.Add("crazy", "chaos");
            this.mappedWords.Add("dearest", "dearest");
            this.mappedWords.Add("defeat", "vanquish");
            this.mappedWords.Add("did", "didst");
            this.mappedWords.Add("do", "doth");
            this.mappedWords.Add("does", "doth");
            this.mappedWords.Add("fast", "fleeting");
            this.mappedWords.Add("fun", "joy");
            this.mappedWords.Add("gg", "We art returned whole from thy grueling trial.");
            this.mappedWords.Add("go", "goest");
            this.mappedWords.Add("god", "the Twelve");
            this.mappedWords.Add("guy", "man");
            this.mappedWords.Add("guys", "men");
            this.mappedWords.Add("haha", "... hah!");
            this.mappedWords.Add("have", "hath");
            this.mappedWords.Add("hello", "hail");
            this.mappedWords.Add("help", "serve");
            this.mappedWords.Add("here", "hither");
            this.mappedWords.Add("hey", "hail");
            this.mappedWords.Add("hi", "hail");
            this.mappedWords.Add("idk", "I know not");
            this.mappedWords.Add("ikr?", "The meaning of these words now shines clear.");
            this.mappedWords.Add("im", "I am");
            this.mappedWords.Add("implying", "stating");
            this.mappedWords.Add("internet", "linkshell");
            this.mappedWords.Add("inv", "I pray thou wilt submit an invitation");
            this.mappedWords.Add("irl", "on the source");
            this.mappedWords.Add("it is", "'tis");
            this.mappedWords.Add("its", "'tis");
            this.mappedWords.Add("killed", "slain");
            this.mappedWords.Add("lf", "I pray thou wilt join our group");
            this.mappedWords.Add("lfg", "Might I impose on thee to allow entrance to thine party?");
            this.mappedWords.Add("lmao", "... hah!");
            this.mappedWords.Add("lmfao", "... hah!");
            this.mappedWords.Add("lol", "... hah!");
            this.mappedWords.Add("many", "numerous");
            this.mappedWords.Add("nah", "nay");
            this.mappedWords.Add("need", "requireth");
            this.mappedWords.Add("newbie", "neophyte");
            this.mappedWords.Add("no", "nay");
            this.mappedWords.Add("o/", "Greetings.");
            this.mappedWords.Add("ok", "The meaning of these words now shines clear.");
            this.mappedWords.Add("only", "merely");
            this.mappedWords.Add("oops", "Pray accept mine apologies");
            this.mappedWords.Add("perhaps", "mayhap");
            this.mappedWords.Add("please", "pray");
            this.mappedWords.Add("pls", "pray");
            this.mappedWords.Add("plz", "pray");
            this.mappedWords.Add("possess", "possesseth");
            this.mappedWords.Add("probaby", "perhaps");
            this.mappedWords.Add("require", "requireth");
            this.mappedWords.Add("rises", "riseth");
            this.mappedWords.Add("rn", "at the moment");
            this.mappedWords.Add("rofl", "... hah!");
            this.mappedWords.Add("sometimes", "On occasion");
            this.mappedWords.Add("soon", "ere long");
            this.mappedWords.Add("sorry", "Pray accept mine apologies");
            this.mappedWords.Add("stop", "restrain");
            this.mappedWords.Add("sucks", "seems undesirable");
            this.mappedWords.Add("sup", "How fares the realm?");
            this.mappedWords.Add("texted", "writ");
            this.mappedWords.Add("there", "yon");
            this.mappedWords.Add("to", "unto");
            this.mappedWords.Add("tomorrow", "morrow");
            this.mappedWords.Add("tyfp", "You have my thanks");
            this.mappedWords.Add("u", "thou");
            this.mappedWords.Add("wanna", "desire");
            this.mappedWords.Add("want", "desire");
            this.mappedWords.Add("weird", "strange");
            this.mappedWords.Add("whats", "what 'tis");
            this.mappedWords.Add("whoops", "Pray accept mine apologies");
            this.mappedWords.Add("will", "shall");
            this.mappedWords.Add("win", "prevail");
            this.mappedWords.Add("written", "writ");
            this.mappedWords.Add("xd", "...I jest.");
            this.mappedWords.Add("yea", "aye");
            this.mappedWords.Add("yep", "aye");
            this.mappedWords.Add("yes", "aye");
            this.mappedWords.Add("yesterday", "yester");
            this.mappedWords.Add("you", "thou");
            this.mappedWords.Add(";_;", "It is unfortunate.");
            this.mappedWords.Add(":d", "Words cannot well express my joy");
            this.mappedWords.Add("?", "What will you ask?");
            this.mappedWords.Add("<3", "I have more than words for you, my friend.");
        }
    }
}
