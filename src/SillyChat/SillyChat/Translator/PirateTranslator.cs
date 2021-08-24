using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace SillyChat
{
    /// <summary>
    /// Translate to turkey.
    /// </summary>
    public class PirateTranslator : BaseTranslator
    {
        private readonly Dictionary<string, string> mappedWords = new ();

        /// <summary>
        /// Initializes a new instance of the <see cref="PirateTranslator"/> class.
        /// </summary>
        /// <param name="plugin">SillyChat plugin.</param>
        public PirateTranslator(ISillyChatPlugin plugin)
            : base(plugin)
        {
            this.SetWordMapping();
        }

        /// <inheritdoc />
        public override string Translate(string input)
        {
            var words = input.Split(' ');
            var output = new List<string>();
            foreach (var word in words)
            {
                string currentWord = char.IsPunctuation(word.Last()) ? string.Concat(word.Take(word.Length - 1)) : word;
                currentWord = currentWord.ToLower();

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

                    output.Add(currentWord);
                }
                else
                {
                    output.Add(word);
                }
            }

            return string.Join(" ", output);
        }

        private void SetWordMapping()
        {
            this.mappedWords.Add("address", "port o' call");
            this.mappedWords.Add("admin", "helm");
            this.mappedWords.Add("am", "be");
            this.mappedWords.Add("an", "a");
            this.mappedWords.Add("and", "n'");
            this.mappedWords.Add("are", "be");
            this.mappedWords.Add("award", "prize");
            this.mappedWords.Add("bathroom", "head");
            this.mappedWords.Add("beer", "grog");
            this.mappedWords.Add("before", "afore");
            this.mappedWords.Add("belief", "creed");
            this.mappedWords.Add("between", "betwixt");
            this.mappedWords.Add("big", "vast");
            this.mappedWords.Add("bill", "coin");
            this.mappedWords.Add("bills", "coins");
            this.mappedWords.Add("boss", "admiral");
            this.mappedWords.Add("bourbon", "rum");
            this.mappedWords.Add("box", "barrel");
            this.mappedWords.Add("boy", "lad");
            this.mappedWords.Add("buddy", "mate");
            this.mappedWords.Add("business", "company");
            this.mappedWords.Add("businesses", "companies");
            this.mappedWords.Add("calling", "callin'");
            this.mappedWords.Add("canada", "Great North");
            this.mappedWords.Add("cash", "coin");
            this.mappedWords.Add("cat", "parrot");
            this.mappedWords.Add("cheat", "hornswaggle");
            this.mappedWords.Add("comes", "hails");
            this.mappedWords.Add("comments", "yer words");
            this.mappedWords.Add("cool", "shipshape");
            this.mappedWords.Add("country", "land");
            this.mappedWords.Add("dashboard", "shanty");
            this.mappedWords.Add("dead", "in Davy Jones's Locker");
            this.mappedWords.Add("disconnect", "keelhaul");
            this.mappedWords.Add("do", "d'");
            this.mappedWords.Add("dog", "parrot");
            this.mappedWords.Add("dollar", "doubloon");
            this.mappedWords.Add("dollars", "doubloons");
            this.mappedWords.Add("dude", "pirate");
            this.mappedWords.Add("employee", "crew");
            this.mappedWords.Add("everyone", "all hands");
            this.mappedWords.Add("eye", "eye-patch");
            this.mappedWords.Add("family", "kin");
            this.mappedWords.Add("fee", "debt");
            this.mappedWords.Add("female", "wench");
            this.mappedWords.Add("females", "wenches");
            this.mappedWords.Add("food", "grub");
            this.mappedWords.Add("for", "fer");
            this.mappedWords.Add("friend", "mate");
            this.mappedWords.Add("friends", "crew");
            this.mappedWords.Add("fuck", "shiver me timbers");
            this.mappedWords.Add("gin", "rum");
            this.mappedWords.Add("girl", "lass");
            this.mappedWords.Add("girls", "lassies");
            this.mappedWords.Add("go", "sail");
            this.mappedWords.Add("good", "jolly good");
            this.mappedWords.Add("grave", "Davy Jones's Locker");
            this.mappedWords.Add("group", "maties");
            this.mappedWords.Add("gun", "bluderbuss");
            this.mappedWords.Add("haha", "yo ho");
            this.mappedWords.Add("hahaha", "yo ho ho");
            this.mappedWords.Add("hahahaha", "yo ho ho ho");
            this.mappedWords.Add("hand", "hook");
            this.mappedWords.Add("happy", "grog-filled");
            this.mappedWords.Add("hello", "ahoy");
            this.mappedWords.Add("hey", "ahoy");
            this.mappedWords.Add("hi", "ahoy");
            this.mappedWords.Add("hotel", "fleebag inn");
            this.mappedWords.Add("i", "me");
            this.mappedWords.Add("i'm", "i be");
            this.mappedWords.Add("internet", "series o' tubes");
            this.mappedWords.Add("invalid", "sunk");
            this.mappedWords.Add("is", "be");
            this.mappedWords.Add("island", "isle");
            this.mappedWords.Add("isn't", "be not");
            this.mappedWords.Add("it's", "'tis");
            this.mappedWords.Add("jail", "brig");
            this.mappedWords.Add("kill", "keelhaul");
            this.mappedWords.Add("king", "king");
            this.mappedWords.Add("ladies", "lasses");
            this.mappedWords.Add("lady", "lass");
            this.mappedWords.Add("lawyer", "scurvy land lubber");
            this.mappedWords.Add("left", "port");
            this.mappedWords.Add("leg", "peg");
            this.mappedWords.Add("logout", "walk the plank");
            this.mappedWords.Add("lol", "blimey");
            this.mappedWords.Add("male", "pirate");
            this.mappedWords.Add("man", "pirate");
            this.mappedWords.Add("manager", "admiral");
            this.mappedWords.Add("money", "doubloons");
            this.mappedWords.Add("month", "moon");
            this.mappedWords.Add("my", "me");
            this.mappedWords.Add("never", "nary");
            this.mappedWords.Add("no", "nay");
            this.mappedWords.Add("not", "nay");
            this.mappedWords.Add("of", "o'");
            this.mappedWords.Add("old", "barnacle-covered");
            this.mappedWords.Add("omg", "shiver me timbers");
            this.mappedWords.Add("over", "o'er");
            this.mappedWords.Add("page", "parchment");
            this.mappedWords.Add("people", "scallywags");
            this.mappedWords.Add("person", "scurvy dog");
            this.mappedWords.Add("posted", "tacked to the yardarm");
            this.mappedWords.Add("president", "king");
            this.mappedWords.Add("prison", "brig");
            this.mappedWords.Add("quickly", "smartly");
            this.mappedWords.Add("really", "verily");
            this.mappedWords.Add("relative", "kin");
            this.mappedWords.Add("relatives", "kin");
            this.mappedWords.Add("religion", "creed");
            this.mappedWords.Add("restaurant", "galley");
            this.mappedWords.Add("right", "starboard");
            this.mappedWords.Add("rotf", "rollin' on the decks");
            this.mappedWords.Add("say", "cry");
            this.mappedWords.Add("seconds", "ticks o' tha clock");
            this.mappedWords.Add("shipping", "cargo");
            this.mappedWords.Add("shit", "shiver me timbers");
            this.mappedWords.Add("small", "puny");
            this.mappedWords.Add("snack", "grub");
            this.mappedWords.Add("soldier", "sailor");
            this.mappedWords.Add("sorry", "yarr");
            this.mappedWords.Add("spouse", "ball 'n' chain");
            this.mappedWords.Add("state", "land");
            this.mappedWords.Add("supervisor", "Cap'n");
            this.mappedWords.Add("that's", "that be");
            this.mappedWords.Add("the", "thar");
            this.mappedWords.Add("thief", "swoggler");
            this.mappedWords.Add("them", "'em");
            this.mappedWords.Add("this", "dis");
            this.mappedWords.Add("to", "t'");
            this.mappedWords.Add("together", "t'gether");
            this.mappedWords.Add("treasure", "booty");
            this.mappedWords.Add("vodka", "rum");
            this.mappedWords.Add("was", "be");
            this.mappedWords.Add("water", "grog");
            this.mappedWords.Add("we", "our jolly crew");
            this.mappedWords.Add("we're", "we's");
            this.mappedWords.Add("whiskey", "rum");
            this.mappedWords.Add("whisky", "rum");
            this.mappedWords.Add("wine", "grog");
            this.mappedWords.Add("with", "wit'");
            this.mappedWords.Add("work", "duty");
            this.mappedWords.Add("yah", "aye");
            this.mappedWords.Add("yeah", "aye");
            this.mappedWords.Add("yes", "aye");
            this.mappedWords.Add("you", "ye");
            this.mappedWords.Add("you're", "you be");
            this.mappedWords.Add("you've", "ye");
            this.mappedWords.Add("your", "yer");
            this.mappedWords.Add("yup", "aye");
        }
    }
}
