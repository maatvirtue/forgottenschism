using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using ForgottenSchism.control;
using ForgottenSchism.engine;
using ForgottenSchism.world;

namespace ForgottenSchism.screen
{
    class Options : Screen
    {
        Label lbl_options;

        Label lbl_volume;
        Label lbl_soundEffects;

        Select sel_volume;
        Select sel_soundEffects;

        Link lnk_goBack;

        public Options()
        {
            MainWindow.BackgroundImage = Content.Graphics.Instance.Images.background.bg_menuless;

            lbl_options = new Label("Options");
            lbl_options.LabelFun = ColorTheme.LabelColorTheme.LabelFunction.TITLE;
            lbl_options.Position = new Vector2(50, 50);
            MainWindow.add(lbl_options);

            lbl_volume = new Label("Music Volume:");
            lbl_volume.Position = new Vector2(260, 180);
            MainWindow.add(lbl_volume);

            lbl_soundEffects = new Label("Sound Effects:");
            lbl_soundEffects.Position = new Vector2(260, 240);
            MainWindow.add(lbl_soundEffects);

            sel_volume = new Select();
            sel_volume.Position = new Vector2(402, 180);
            for (int i = 0; i <= 10; i++)
                sel_volume.add(i.ToString());
            sel_volume.selectionChanged = volumeChange;
            sel_volume.Selection = (int)MediaPlayer.Volume * 10;
            MainWindow.add(sel_volume);

            sel_soundEffects = new Select();
            sel_soundEffects.Position = new Vector2(400, 240);
            sel_soundEffects.add("On");
            sel_soundEffects.add("Off");
            sel_soundEffects.selectionChanged = sfxChange;
            MainWindow.add(sel_soundEffects);

            lnk_goBack = new Link("Go Back");
            lnk_goBack.Position = new Vector2(325, 300);
            lnk_goBack.selected = goBack;
            MainWindow.add(lnk_goBack);
        }

        private void volumeChange(object o, EventArgs e)
        {
            MediaPlayer.Volume = float.Parse(sel_volume.SelectedValue) / 10;
        }

        private void sfxChange(object o, EventArgs e)
        {
            //TODO turn sound effects on/off (when we'll have some :P)
        }

        private void goBack(object o, EventArgs e)
        {
            StateManager.Instance.goBack();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (InputHandler.keyReleased(Keys.Escape))
                StateManager.Instance.goBack();
        }
    }
}
