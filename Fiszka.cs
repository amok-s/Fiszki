using System.ComponentModel;
using System.Transactions;

namespace Fiszka
{
    [Serializable]
    public class Fiszka
    {
        public string NativePhrase { get; set; }
        public string NativePhraseExample { get; set; }
        public string TranslatedPhrase { get; set; }
        public string TranslatedPhraseExample { get; set; }
        public double MemoScore
        {
            get
            {
                double score = 0;
                foreach (double item in allScores)
                {
                    score += item;
                }
                score = score / allScores.Count;
                return score;
            }
        }

        public List<double> allScores = new List<double>();

        public Fiszka(string nativePhrase, string nativePhraseExample, string translatedPhrase, string translatedPhraseExample)
        {
            this.NativePhrase = nativePhrase;
            this.NativePhraseExample = nativePhraseExample;
            this.TranslatedPhrase = translatedPhrase;
            this.TranslatedPhraseExample = translatedPhraseExample;
        }


        
        public string ShowFiszka()
        {
            var report = new System.Text.StringBuilder();

            report.AppendLine(NativePhrase.ToUpper());
            report.AppendLine($"{NativePhraseExample}\r\n");
            report.AppendLine(TranslatedPhrase.ToUpper());
            report.AppendLine($"{TranslatedPhraseExample}");
            return report.ToString();
        }
     }




    public class Komenda
    {
        public string nazwaKomendy { get; set; }
        public string definicjaKomendy { get; set; }

        public Komenda(string nazwaKomendy, string definicjaKomendy)
        {
            this.nazwaKomendy = nazwaKomendy;
            this.definicjaKomendy = definicjaKomendy;
        }
        public List<Komenda> listaKomend = new List<Komenda>();


    }


}
