namespace _Project.Core.Placeholders {
    public class TextPlaceholder {
        #region Properties

        string Text { get; }

        string Prefix { get; }

        string Suffix { get; }

        #endregion

        #region Constructor

        public TextPlaceholder(string text, string prefix = "", string suffix = "") {
            Text = text;
            Prefix = prefix;
            Suffix = suffix;
        }

        #endregion

        public string GetText() => Prefix + Text + Suffix;
    }
}