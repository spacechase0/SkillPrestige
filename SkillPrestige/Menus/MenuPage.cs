﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using SkillPrestige.InputHandling;
using SkillPrestige.Logging;
using SkillPrestige.Menus.Elements.Buttons;

namespace SkillPrestige.Menus
{
    internal abstract class MenuPage : IMenuPage
    {
        public int PageIndex { get; set; }

        private bool _controlMouseEventsRegistered;

        public IList<Button> Controls { get; set; } = new List<Button>();

        public IList<Action<SpriteBatch>> BeforeControlsDrawnBehindControlsDrawFunctions{ get; set; } = new List<Action<SpriteBatch>>();

        public IList<Action<SpriteBatch>> AfterControlsDrawnFunctions { get; set; } = new List<Action<SpriteBatch>>();

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var action in BeforeControlsDrawnBehindControlsDrawFunctions)
            {
                action.Invoke(spriteBatch);
            }
            foreach (var control in Controls)
            {
                control.Draw(spriteBatch);
            }
            foreach (var action in AfterControlsDrawnFunctions)
            {
                action.Invoke(spriteBatch);
            }
            DrawHoverText(spriteBatch);
            Mouse.DrawCursor(spriteBatch);
        }

        public void RegisterControls()
        {
            if (_controlMouseEventsRegistered) return;
            _controlMouseEventsRegistered = true;
            Logger.LogVerbose("Menu Page registering mouse events...");
            foreach (var control in Controls)
            {
                Mouse.MouseMoved += control.CheckForMouseHover;
                Mouse.MouseClicked += control.CheckForMouseClick;
            }
            Logger.LogVerbose("Menu Page mouse events registered.");
        }

        public void DeegisterControls()
        {
            if (!_controlMouseEventsRegistered) return;
            Logger.LogVerbose("Menu Page deregistering mouse events...");
            foreach (var control in Controls)
            {
                Mouse.MouseMoved -= control.CheckForMouseHover;
                Mouse.MouseClicked -= control.CheckForMouseClick;
            }
            _controlMouseEventsRegistered = false;
            Logger.LogVerbose("Menu Page mouse events deregistered.");
        }

        public void DrawHoverText(SpriteBatch spriteBatch)
        {
            foreach (var control in Controls)
            {
                control.DrawHoverText(spriteBatch);
            }
        }
    }
}
