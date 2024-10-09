#region license

// Copyright (c) 2021, andreakarasho
// All rights reserved.
// 
// Redistribution and use in source and binary forms, with or without
// modification, are permitted provided that the following conditions are met:
// 1. Redistributions of source code must retain the above copyright
//    notice, this list of conditions and the following disclaimer.
// 2. Redistributions in binary form must reproduce the above copyright
//    notice, this list of conditions and the following disclaimer in the
//    documentation and/or other materials provided with the distribution.
// 3. All advertising materials mentioning features or use of this software
//    must display the following acknowledgement:
//    This product includes software developed by andreakarasho - https://github.com/andreakarasho
// 4. Neither the name of the copyright holder nor the
//    names of its contributors may be used to endorse or promote products
//    derived from this software without specific prior written permission.
// 
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS ''AS IS'' AND ANY
// EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
// WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
// DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER BE LIABLE FOR ANY
// DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
// (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
// LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND
// ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
// (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
// SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.

#endregion

using System;
using ClassicUO.Configuration;
using System.Collections.Generic;
using ClassicUO.Game.Managers;
using ClassicUO.Game.UI.Controls;
using ClassicUO.Input;
using ClassicUO.Assets;
using System.IO;
using System.Diagnostics;
using ClassicUO.Game.UI.Gumps.CharCreation;


namespace ClassicUO.Game.UI.Gumps.CharCreation
{
    internal class CreateCharProfessionGump : Gump
    {
        private ChosenClass _selectedClass;

        private enum ChosenClass
        {
            Warrior = 1,
            Warmage = 2,
            Mage = 3,
            Ranger = 4,
            Archer = 5,
            Cleric = 6,
            Assassin = 7,
            Warlock = 8
        }

        public void SelectProfession(ProfessionInfo info)
        {
            if (info.Type == ProfessionLoader.PROF_TYPE.CATEGORY && ProfessionLoader.Instance.Professions.TryGetValue(info, out List<ProfessionInfo> list) && list != null)
            {
                Parent.Add(new CreateCharProfessionGump(info));
                Parent.Remove(this);
            }
            else
            {
                CharCreationGump charCreationGump = UIManager.GetGump<CharCreationGump>();

                charCreationGump?.SetProfession(info);
            }
        }

        public CreateCharProfessionGump(ProfessionInfo parent = null) : base(0, 0)
        {
            
            Add(new ResizePic(2600) 
                {
                    X = 100,
                    Y = 80,
                    Width = 470,
                    Height = 372 
                }
            );
            Add(new Label("Choose a Class for Your Character", false, 0x0386, font: 1) { X = 158, Y = 132 });

            // BTN 1: Warrior
            Add(
                new Button(1, 0x15C9, 0x15CA, 0x15C9)  
                {
                    X = 145,  
                    Y = 170, 
                    ButtonAction = ButtonAction.Activate
                }
            );
            Add(
                new Label("Warrior", false, 0x0386, font: 1)
                {
                    X = 147,  
                    Y = 240,  
                }
            );

            // BTN 2: Warmage
            Add(
                new Button(2, 0x15D1, 0X15D2, 0x15D1)  
                {
                    X = 235,  
                    Y = 170, 
                    ButtonAction = ButtonAction.Activate
                }
            );
            Add(
                new Label("Warmage", false, 0x0386, font: 1)
                {
                    X = 235,  
                    Y = 240,  
                }
            );

            // BTN 3: Mage
            Add(
                new Button(3, 0x15C1, 0x15C2, 0x15C1)  
                {
                    X = 325,  
                    Y = 170,  
                    ButtonAction = ButtonAction.Activate
                }
            );
            Add(
                new Label("Mage", false, 0x0386, font: 1)
                {
                    X = 335,  
                    Y = 240,  
                }
            );

            // BTN 4: Ranger
            Add(
                new Button(4, 0x15C7, 0x15C8, 0x15C7)  
                {
                    X = 415,  
                    Y = 170, 
                    ButtonAction = ButtonAction.Activate
                }
            );
            Add(
                new Label("Ranger", false, 0x0386, font: 1)
                {
                    X = 425, 
                    Y = 240,  
                }
            );

            // BTN 5: Archer
            Add(
                new Button(5, 0x15AF, 0x15B0, 0x15AF)  
                {
                    X = 145,
                    Y = 290,  
                    ButtonAction = ButtonAction.Activate
                }
            );
            Add(
                new Label("Archer", false, 0x0386, font: 1)
                {
                    X = 152,  
                    Y = 360,  
                }
            );

            // BTN 6: Cleric
            Add(
                new Button(6, 0x15BB, 0x15BC, 0x15BB) 
                {
                    X = 235, 
                    Y = 290, 
                    ButtonAction = ButtonAction.Activate
                }
            );
            Add(
                new Label("Cleric", false, 0x0386, font: 1)
                {
                    X = 245,  
                    Y = 360, 
                }
            );

            // BTN 7: Assassin
            Add(
                new Button(7, 0x15B9, 0x15BA, 0x15B9) 
                {
                    X = 325, 
                    Y = 290,  
                    ButtonAction = ButtonAction.Activate
                }
            );
            Add(
                new Label("Assassin", false, 0x0386, font: 1)
                {
                    X = 327,  
                    Y = 360,  
                }
            );

            // BTN 8: Warlock
            Add(
                new Button(8, 0x15CD, 0x15CE, 0x15CD)  
                {
                    X = 415,
                    Y = 290, 
                    ButtonAction = ButtonAction.Activate
                }
            );
            Add(
                new Label("Warlock", false, 0x0386, font: 1)
                {
                    X = 417,  
                    Y = 360,  
                }
            );


            Add(new Button((int)Buttons.Prev, 0x15A1, 0x15A3, 0x15A2) { X = 15, Y = 445, ButtonAction = ButtonAction.Activate });            
        }

        public override void OnButtonClick(int buttonID)
        {
            switch ((Buttons)buttonID)
            {
                case Buttons.Prev:

                    {                        
                        Parent.Remove(this);
                        CharCreationGump charCreationGump = UIManager.GetGump<CharCreationGump>();
                        charCreationGump?.StepBack();                        
                        break;
                    }
                
            }
            if (buttonID >= 1 && buttonID <= 8)
            {
                // classe scelta
                _selectedClass = (ChosenClass)buttonID;
                Debug.WriteLine($"Classe scelta: {_selectedClass}");

                // salva classe scelta su *.txt
                SaveChosenClass();

                CharCreationGump charCreationGump = UIManager.GetGump<CharCreationGump>();

                if (charCreationGump != null)
                {
                    // Passa alla pagina successiva
                    charCreationGump.SetAttributes(true);  
                }
            }
            else
            {
                base.OnButtonClick(buttonID);
            }
        }

        private void SaveChosenClass()
        {
            using (StreamWriter writer = new StreamWriter("chosen_class.txt"))
            {
                writer.WriteLine((int)_selectedClass);
            }
        }

        private enum Buttons
        {
            Prev     
        }
    }

    internal class ProfessionInfoGump : Control
    {
        private readonly ProfessionInfo _info;

        public ProfessionInfoGump(ProfessionInfo info)
        {
            _info = info;

            ClilocLoader localization = ClilocLoader.Instance;

            ResizePic background = new ResizePic(3000)
            {
                Width = 175,
                Height = 34
            };

            background.SetTooltip(localization.GetString(info.Description), 250);

            Add(background);

            Add
            (
                new Label(localization.GetString(info.Localization), true, 0x00, font: 1)
                {
                    X = 7,
                    Y = 8
                }
            );

            Add(new GumpPic(121, -12, info.Graphic, 0));
        }

        public Action<ProfessionInfo> Selected;

        protected override void OnMouseUp(int x, int y, MouseButtonType button)
        {
            base.OnMouseUp(x, y, button);

            if (button == MouseButtonType.Left)
            {
                Selected?.Invoke(_info);
            }
        }
    }
}

