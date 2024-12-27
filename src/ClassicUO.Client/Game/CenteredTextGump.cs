using ClassicUO.Game.UI.Gumps;
using ClassicUO.Game.UI.Controls;
using ClassicUO.Game.Managers;
using ClassicUO.Assets;
using ClassicUO.Utility;
using System.Collections.Generic;
using ClassicUO;

public class CenteredTextGump : Gump
{
    private readonly uint _closeTime;

    public CenteredTextGump(string text, ushort hue) : base(0, 0)
    {
        CanCloseWithRightClick = true; // Permette di chiudere con il tasto destro

        
        const int maxLineWidth = 250; // Larghezza massima di una riga in pixel
        string[] wrappedText = WrapText(text, maxLineWidth, 5); // Font 5

        int lineHeight = 20; // Altezza di ogni riga
        int totalHeight = wrappedText.Length * lineHeight + 20;

        // Sfondo dinamico per adattarsi al testo
        Add(new AlphaBlendControl
        {
            X = 0,
            Y = 0,
            Width = 270,  // Larghezza fissa
            Height = totalHeight, // dipendente dal testo
            Hue = 0x038E,
            Alpha = 0.5f
        });

        
        for (int i = 0; i < wrappedText.Length; i++) 
        {
            Add(new Label(wrappedText[i], true, 46, font: 5)
            {                
                X = 10, 
                Y = 10 + i * lineHeight 
            });
        }

        // Gump a centro finestra
        X = (Client.Game.Window.ClientBounds.Width - 300) / 2;
        Y = (Client.Game.Window.ClientBounds.Height - totalHeight) / 2;

        // tempo di esposizione
        _closeTime = Time.Ticks + 7000;
    }

    public override void Update()
    {
        base.Update();

        // Chiude il gump
        if (Time.Ticks >= _closeTime)
        {
            Dispose();
        }
    }

    private string[] WrapText(string text, int maxLineWidth, byte font)
    {
        var result = new List<string>();
        string[] words = text.Split(' ');
        string currentLine = string.Empty;

        foreach (string word in words)
        {
            string testLine = string.IsNullOrEmpty(currentLine) ? word : $"{currentLine} {word}";
            int width = FontsLoader.Instance.GetWidthUnicode(font, testLine);

            if (width > maxLineWidth)
            {
                if (!string.IsNullOrEmpty(currentLine))
                {
                    result.Add(currentLine);
                }
                currentLine = word;
            }
            else
            {
                currentLine = testLine;
            }
        }

        if (!string.IsNullOrEmpty(currentLine))
        {
            result.Add(currentLine);
        }

        return result.ToArray();
    }

}
