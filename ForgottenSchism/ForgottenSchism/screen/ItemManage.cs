using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

using ForgottenSchism.control;
using ForgottenSchism.engine;
using ForgottenSchism.world;

namespace ForgottenSchism.screen
{
    class ItemManage: Screen
    {
        Label lbl_title;
        PictureBox pb_item;

        Label lbl_name;

        Label lbl_type;

        Label lbl_cost;
        Label lbl_ccost;

        Label lbl_force;
        Label lbl_cforce;

        Label lbl_dex;
        Label lbl_cdex;

        Label lbl_intel;
        Label lbl_cintel;

        Label lbl_sag;
        Label lbl_csag;

        Label lbl_spd;
        Label lbl_cspd;

        Label lbl_con;
        Label lbl_ccon;

        Label lbl_esc;
        Label lbl_escAction;

        Item item;

        public ItemManage(Item i)
        {
            MainWindow.BackgroundImage = Content.Graphics.Instance.Images.background.bg_bigMenu;

            item=i;

            lbl_title = new Label("Item View");
            lbl_title.LabelFun=ColorTheme.LabelColorTheme.LabelFunction.TITLE;
            lbl_title.Position = new Vector2(50, 30);

            pb_item = new PictureBox();
            pb_item.Image = Graphic.getSprite(new Archer("void"));
            pb_item.Size = new Vector2(384, 384);
            pb_item.Position = new Vector2(0, 30);

            MainWindow.add(lbl_title);
            MainWindow.add(pb_item);

            lbl_name = new Label(item.Name);
            lbl_name.LabelFun=ColorTheme.LabelColorTheme.LabelFunction.BOLD;
            lbl_name.Position = new Vector2(340, 80);
            MainWindow.add(lbl_name);

            lbl_type = new Label(item.Type.ToString());
            lbl_type.LabelFun=ColorTheme.LabelColorTheme.LabelFunction.NORM;
            lbl_type.Position = new Vector2(340, 110);
            MainWindow.add(lbl_type);

            lbl_cost = new Label("Cost/Value:");
            lbl_cost.LabelFun = ColorTheme.LabelColorTheme.LabelFunction.BOLD;
            lbl_cost.Position = new Vector2(340, 140);
            MainWindow.add(lbl_cost);

            lbl_ccost = new Label(item.Cost.ToString());
            lbl_ccost.LabelFun=ColorTheme.LabelColorTheme.LabelFunction.NORM;
            lbl_ccost.Position = new Vector2(450, 140);
            MainWindow.add(lbl_ccost);

            lbl_force = new Label("Strength:");
            lbl_force.LabelFun = ColorTheme.LabelColorTheme.LabelFunction.BOLD;
            lbl_force.Position = new Vector2(340, 260);
            MainWindow.add(lbl_force);

            lbl_cforce = new Label(item.Modifications.str.ToString());
            lbl_cforce.LabelFun=ColorTheme.LabelColorTheme.LabelFunction.NORM;
            lbl_cforce.Position = new Vector2(460, 260);
            MainWindow.add(lbl_cforce);

            lbl_dex = new Label("Dexterity:");
            lbl_dex.LabelFun = ColorTheme.LabelColorTheme.LabelFunction.BOLD;
            lbl_dex.Position = new Vector2(340, 290);
            MainWindow.add(lbl_dex);

            lbl_cdex = new Label(item.Modifications.dex.ToString());
            lbl_cdex.LabelFun=ColorTheme.LabelColorTheme.LabelFunction.NORM;
            lbl_cdex.Position = new Vector2(460, 290);
            MainWindow.add(lbl_cdex);

            lbl_intel = new Label("Intelligence:");
            lbl_intel.LabelFun = ColorTheme.LabelColorTheme.LabelFunction.BOLD;
            lbl_intel.Position = new Vector2(340, 320);
            MainWindow.add(lbl_intel);

            lbl_cintel = new Label(item.Modifications.intel.ToString());
            lbl_cintel.LabelFun=ColorTheme.LabelColorTheme.LabelFunction.NORM;
            lbl_cintel.Position = new Vector2(460, 320);
            MainWindow.add(lbl_cintel);

            lbl_sag = new Label("Wisdom:");
            lbl_sag.LabelFun = ColorTheme.LabelColorTheme.LabelFunction.BOLD;
            lbl_sag.Position = new Vector2(550, 260);
            MainWindow.add(lbl_sag);

            lbl_csag = new Label(item.Modifications.wis.ToString());
            lbl_csag.LabelFun=ColorTheme.LabelColorTheme.LabelFunction.NORM;
            lbl_csag.Position = new Vector2(670, 260);
            MainWindow.add(lbl_csag);

            lbl_spd = new Label("Speed:");
            lbl_spd.LabelFun = ColorTheme.LabelColorTheme.LabelFunction.BOLD;
            lbl_spd.Position = new Vector2(550, 290);
            MainWindow.add(lbl_spd);

            lbl_cspd = new Label(item.Modifications.spd.ToString());
            lbl_cspd.LabelFun=ColorTheme.LabelColorTheme.LabelFunction.NORM;
            lbl_cspd.Position = new Vector2(670, 290);
            MainWindow.add(lbl_cspd);

            lbl_con = new Label("Constitution:");
            lbl_con.LabelFun = ColorTheme.LabelColorTheme.LabelFunction.BOLD;
            lbl_con.Position = new Vector2(550, 320);
            MainWindow.add(lbl_con);

            lbl_ccon = new Label(item.Modifications.con.ToString());
            lbl_ccon.LabelFun=ColorTheme.LabelColorTheme.LabelFunction.NORM;
            lbl_ccon.Position = new Vector2(670, 320);
            MainWindow.add(lbl_ccon);

            lbl_esc = new Label("ESC");
            lbl_esc.LabelFun = ColorTheme.LabelColorTheme.LabelFunction.CONTROL;
            lbl_esc.Position = new Vector2(50, 440);
            MainWindow.add(lbl_esc);

            lbl_escAction = new Label("Go Back");
            lbl_escAction.LabelFun = ColorTheme.LabelColorTheme.LabelFunction.NORM;
            lbl_escAction.Position = new Vector2(100, 440);
            MainWindow.add(lbl_escAction);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (InputHandler.keyReleased(Keys.Escape))
            {
                StateManager.Instance.goBack();
            }
        }
    }
}
