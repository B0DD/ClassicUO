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

using System.Linq;
using System;
using ClassicUO.Configuration;
using ClassicUO.Game.Data;
using ClassicUO.Game.GameObjects;
using ClassicUO.Game.Managers;
using ClassicUO.Game.UI.Controls;
using ClassicUO.Assets;
using ClassicUO.Resources;
using System.Collections.Generic;
using ClassicUO.Utility;

namespace ClassicUO.Game.UI.Gumps.CharCreation
{
    internal class CreateCharTradeGump : Gump
    {
        private readonly HSliderBar[] _attributeSliders;
        private readonly PlayerMobile _character;
        private readonly Combobox[] _skillsCombobox;
        private readonly HSliderBar[] _skillSliders;
        private readonly List<SkillEntry> _skillList;



        public CreateCharTradeGump(PlayerMobile character, ProfessionInfo profession) : base(0, 0)
        {
            _character = character;

            foreach (Skill skill in _character.Skills)
            {
                skill.ValueFixed = 0;
                skill.BaseFixed = 0;
                skill.CapFixed = 0;
                skill.Lock = Lock.Locked;
            }

            Add
            (
                new ResizePic(2600) { X = 100, Y = 80, Width = 470, Height = 372 }
            );

            // center menu with fancy top
            // public GumpPic(AControl parent, int x, int y, int gumpID, int hue)
            Add(new GumpPic(291, 42, 0x0589, 0));
            Add(new GumpPic(214, 58, 0x058B, 0));
            Add(new GumpPic(300, 51, 0x15A9, 0));

            bool isAsianLang = string.Compare(Settings.GlobalSettings.Language, "CHT", StringComparison.InvariantCultureIgnoreCase) == 0 || 
                string.Compare(Settings.GlobalSettings.Language, "KOR", StringComparison.InvariantCultureIgnoreCase) == 0 ||
                string.Compare(Settings.GlobalSettings.Language, "JPN", StringComparison.InvariantCultureIgnoreCase) == 0;

            bool unicode = isAsianLang;
            byte font = (byte)(isAsianLang ? 1 : 2);
            ushort hue = (ushort)(isAsianLang ? 0xFFFF : 0x0386);

            // title text
            //TextLabelAscii(AControl parent, int x, int y, int font, int hue, string text, int width = 400)
           
            Add(new Label("Choose Your Skills", false, 0x0386, font: 1) { X = 148, Y = 112 });

            _skillList = SkillsLoader.Instance.SortedSkills;
            var skillNames = _skillList.Select(s => s.Name).ToArray();

                      
            _skillsCombobox = new Combobox[10]; // 4 a 90, 2 a 70, 4 a 50

            // 4 abilità a 90
            Add(new Label($"Set 4 Skills at 90.0:", false, 0x0386, font: 1) { X = 148, Y = 132 });
            Add(_skillsCombobox[0] = new Combobox(148, 150, 182, skillNames, -1, 200, false, "Select Skill"));
            Add(_skillsCombobox[1] = new Combobox(350, 150, 182, skillNames, -1, 200, false, "Select Skill"));
            Add(_skillsCombobox[2] = new Combobox(148, 180, 182, skillNames, -1, 200, false, "Select Skill"));
            Add(_skillsCombobox[3] = new Combobox(350, 180, 182, skillNames, -1, 200, false, "Select Skill"));

            // 2 abilità a 70
            Add(new Label($"Set 4 Skills at 70.0:", false, 0x0386, font: 1) { X = 148, Y = 220 });
            Add(_skillsCombobox[4] = new Combobox(148, 240, 182, skillNames, -1, 200, false, "Select Skill"));
            Add(_skillsCombobox[5] = new Combobox(350, 240, 182, skillNames, -1, 200, false, "Select Skill"));

            // 4 abilità a 50
            Add(new Label($"Set 4 Skills at 50.0:", false, 0x0386, font: 1) { X = 148, Y = 280 });
            Add(_skillsCombobox[6] = new Combobox(148, 300, 182, skillNames, -1, 200, false, "Select Skill"));
            Add(_skillsCombobox[7] = new Combobox(350, 300, 182, skillNames, -1, 200, false, "Select Skill"));
            Add(_skillsCombobox[8] = new Combobox(148, 330, 182, skillNames, -1, 200, false, "Select Skill"));
            Add(_skillsCombobox[9] = new Combobox(350, 330, 182, skillNames, -1, 200, false, "Select Skill"));


            Add
            (
                new Button((int) Buttons.Prev, 0x15A1, 0x15A3, 0x15A2)
                {
                    X = 15, Y = 445, ButtonAction = ButtonAction.Activate
                }
            );

            Add
            (
                new Button((int) Buttons.Next, 0x15A4, 0x15A6, 0x15A5)
                {
                    X = 610, Y = 445, ButtonAction = ButtonAction.Activate
                }
            );
           
           
        }

        public override void OnButtonClick(int buttonID)
        {
            CharCreationGump charCreationGump = UIManager.GetGump<CharCreationGump>();

            switch ((Buttons) buttonID)
            {
                case Buttons.Prev:
                    charCreationGump.StepBack();

                    break;

                case Buttons.Next:
                    if (ValidateValues())
                    {
                        // Imposta le abilità selezionate con i valori fissi
                        for (int i = 0; i < 4; i++)  // 4 abilità a 90
                        {
                            if (_skillsCombobox[i].SelectedIndex != -1)
                            {
                                Skill skill = _character.Skills[_skillList[_skillsCombobox[i].SelectedIndex].Index];
                                skill.ValueFixed =(ushort)900;  // 90.0 in valore fisso
                                skill.Lock = Lock.Locked;
                            }
                        }

                        for (int i = 4; i < 6; i++)  // 2 abilità a 70
                        {
                            if (_skillsCombobox[i].SelectedIndex != -1)
                            {
                                Skill skill = _character.Skills[_skillList[_skillsCombobox[i].SelectedIndex].Index];
                                skill.ValueFixed = (ushort)700;  // 70.0 in valore fisso
                                skill.Lock = Lock.Locked;
                            }
                        }

                        for (int i = 6; i < 10; i++)  // 4 abilità a 50
                        {
                            if (_skillsCombobox[i].SelectedIndex != -1)
                            {
                                Skill skill = _character.Skills[_skillList[_skillsCombobox[i].SelectedIndex].Index];
                                skill.ValueFixed = (ushort)500;  // 50.0 in valore fisso
                                skill.Lock = Lock.Locked;
                            }
                        }

                        // Passa alla pagina successiva
                        charCreationGump.SetLastPage();
                    }
                    break;
            }

            base.OnButtonClick(buttonID);
        }

        private bool ValidateValues()
        {
            // Verifica che tutte le abilità siano selezionate
            if (_skillsCombobox.All(s => s.SelectedIndex >= 0))
            {
                // Assicurati che non ci siano abilità duplicate
                int duplicated = _skillsCombobox.GroupBy(o => o.SelectedIndex).Count(o => o.Count() > 1);

                if (duplicated > 0)
                {
                    UIManager.GetGump<CharCreationGump>()?.ShowMessage("You must select unique skills.");
                    return false;
                }

                return true;
            }
            else
            {
                UIManager.GetGump<CharCreationGump>()?.ShowMessage("You must select all skills.");
                return false;
            }
        }

        private enum Buttons
        {
            Prev,
            Next
        }
    }
}