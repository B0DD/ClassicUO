using ClassicUO.Assets;
using ClassicUO.Configuration;
using ClassicUO.Game.Managers;
using ClassicUO.Game.UI.Controls;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace ClassicUO.Game.UI.Gumps
{
    public class CommandsGump : Gump
    {
        public CommandsGump() : base(0, 0)
        {
            X = 300;
            Y = 200;
            Width = 400;
            Height = 500;
            CanCloseWithRightClick = true;
            CanMove = true;

            BorderControl bc = new BorderControl(0, 0, Width, Height, 36);
            bc.T_Left = 39925;
            bc.H_Border = 39926;
            bc.T_Right = 39927;
            bc.V_Border = 39928;
            bc.V_Right_Border = 39930;
            bc.B_Left = 39931;
            bc.B_Right = 39933;
            bc.H_Bottom_Border = 39932;

            Add(new GumpPicTiled(39929) { X = bc.BorderSize, Y = bc.BorderSize, Width = Width - (bc.BorderSize * 2), Height = Height - (bc.BorderSize * 2) });

            Add(bc);

            TextBox t;
            Add(t = new TextBox("Last Hera Commands", TrueTypeLoader.EMBEDDED_FONT, 28, Width, Color.Gold, FontStashSharp.RichText.TextHorizontalAlignment.Center) { Y = 5 });

            ScrollArea scroll = new ScrollArea(10, 10 + t.Height, Width - 20, Height - t.Height - 40, true) { ScrollbarBehaviour = ScrollbarBehaviour.ShowAlways };

            Add(new AlphaBlendControl(0.45f) { Width = scroll.Width, Height = scroll.Height, X = scroll.X, Y = scroll.Y });
            
            ShowCommands(scroll);

            Add(scroll);
        }

        private void ShowCommands(ScrollArea scroll)
        {
            int y = 0;
            foreach (var command in _commandDescriptions.Keys) 
            {
                
                TextBox commandText = new TextBox(command, TrueTypeLoader.EMBEDDED_FONT, 18, scroll.Width, Color.White)
                {
                    X = 5,
                    Y = y,
                    AcceptMouseInput = false
                };

                
                string description = GetCommandDescription(command);
                
                HitBox tooltipHitBox = new HitBox(5, y, commandText.Width, commandText.Height, description);
                
                scroll.Add(commandText);
                scroll.Add(tooltipHitBox);

                y += commandText.Height + 10;
            }
        }

        // Dizionario Comandi LAST HERA: vanno aggiunti solo qui
        private static readonly Dictionary<string, string> _commandDescriptions = new Dictionary<string, string>
        {
            {".guildmenu", "Guild menu/creation"},
            {".gs text", "Guild chat"},
            {".GSL", "Guild list"},
            {".GSS", "Guild status"},
            {".raisehood", "Equip/unequip guld robe hood"},
            {"bank check xxxxx", "Bank check with xxxx gold coins"},
            {"Die and give me the Power of the Dragons", "Fight with Lord of Dragons"},
            {"Heal Me", "On wisps summons"},
            {"recruit", "Mercenary NPC"},
            {"destination-escort", "Traveler NPC"},
            {"travel", "Travel vendor for open a gate"},
            {"Tell me your secret ritual guardian", "Fight with a Guardian of Ritual"},
            {"i want to know the sex of my animal", "Breeder vendor for know animals sex"},
            {"identify", "Mage Vendor for identify items"},
            {"buy , sale , auction", "For Auction NPC"}
        };

        //Pesca dal dizionario, se non trova da la scritta di descrizione mancante
        private string GetCommandDescription(string commandName)
        {
            return _commandDescriptions.TryGetValue(commandName, out string description) ? description : "Missing Description";
        }        

    }
}
