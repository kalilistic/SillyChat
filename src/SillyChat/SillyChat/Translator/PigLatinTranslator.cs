using System.Linq;
using System.Text;

namespace SillyChat
{
    /// <summary>
    /// Translate to pig latin.
    /// </summary>
    public class PigLatinTranslator : BaseTranslator
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PigLatinTranslator"/> class.
        /// </summary>
        /// <param name="plugin">SillyChat plugin.</param>
        public PigLatinTranslator(ISillyChatPlugin plugin)
            : base(plugin)
        {
        }

        /// <inheritdoc />
        public override string Translate(string input)
        {
            StringBuilder output = new ();
            StringBuilder word = new ();
            var wordBuilding = false;
            foreach (var c in input)
            {
                if (!char.IsLetter(c))
                {
                    if (wordBuilding)
                    {
                        output.Append(ConvertWord(word.ToString()));
                        word.Clear();
                        wordBuilding = false;
                    }

                    output.Append(c);
                }
                else
                {
                    word.Append(c);
                    wordBuilding = true;
                }
            }

            if (wordBuilding) output.Append(ConvertWord(word.ToString()));
            return output.ToString();
        }

        private static string ConvertWord(string word)
        {
            StringBuilder output = new ();
            char[] cluster = new char[5];
            var buildingCluster = true;
            var isCapital = char.IsUpper(word[0]);

            for (var i = 0; i < word.Length; i++)
            {
                var c = word[i];

                if (buildingCluster)
                {
                    if (!IsVowel(c))
                    {
                        int j;
                        for (j = 0; j < cluster.Length - 2; j++)
                        {
                            if (i + j >= word.Length)
                            {
                                foreach (var l in cluster)
                                {
                                    if (l != '\0') output.Append(l);
                                }

                                output.Append("ay");
                                return output.ToString();
                            }

                            if (IsVowel(word[i + j]))
                            {
                                break;
                            }

                            cluster[j] = word[i + j];
                        }

                        i += j - 1;
                        cluster[j] = 'a';
                        cluster[j + 1] = 'y';
                    }
                    else
                    {
                        output.Append(c);
                        cluster = new[] { 'w', 'a', 'y' };
                    }

                    buildingCluster = false;
                }
                else
                {
                    if (isCapital)
                    {
                        isCapital = false;
                        output.Append(char.ToUpper(c));
                    }
                    else
                    {
                        output.Append(c);
                    }
                }
            }

            foreach (var l in cluster)
            {
                if (l != '\0') output.Append(char.ToLower(l));
            }

            return output.ToString();
        }

        private static bool IsVowel(char c)
        {
            const string vowels = "aeiou";
            return vowels.Contains(char.ToLower(c));
        }
    }
}
