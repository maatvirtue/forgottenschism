using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using ForgottenSchism.control;
using ForgottenSchism.engine;
using ForgottenSchism.world;

namespace ForgottenSchism.screen
{
    public class CharManage : Screen
    {
        Character c;

        Label lbl_charMng;
        PictureBox charPic;

        Label lbl_name;

        Label lbl_class;

        Label lbl_level;
        Label lbl_clevel;

        Label lbl_exp;
        Label lbl_cexp;

        Label lbl_hp;
        Label lbl_currHp;
        Label lbl_hpSlash;
        Label lbl_maxHp;

        Label lbl_mana;
        Label lbl_currMana;
        Label lbl_manaSlash;
        Label lbl_maxMana;

        Label lbl_state;
        Label lbl_cstate;

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

        Label lbl_move;
        Label lbl_cmove;

        Label lbl_org;
        Label lbl_corg;

        Label lbl_e;
        Label lbl_eAction;

        Label lbl_esc;
        Label lbl_escAction;

        Unit unit;

        public CharManage(Character selectedChar, Unit u)
        {
            MainWindow.BackgroundImage = Content.Graphics.Instance.Images.background.bg_bigMenu;

            c = selectedChar;
            unit = u;

            lbl_charMng = new Label("Character Management");
            lbl_charMng.Color = Color.Gold;
            lbl_charMng.Position = new Vector2(50, 30);

            charPic = new PictureBox();
            charPic.Image = Graphic.getSprite(c);
            charPic.Size = new Vector2(384, 384);
            charPic.Position = new Vector2(0, 30);

            MainWindow.add(lbl_charMng);
            MainWindow.add(charPic);

            lbl_name = new Label(c.Name);
            lbl_name.Position = new Vector2(340, 80);
            MainWindow.add(lbl_name);

            lbl_class = new Label(c.Type.ToString());
            lbl_class.Position = new Vector2(340, 110);
            MainWindow.add(lbl_class);

            lbl_level = new Label("Level:");
            lbl_level.Position = new Vector2(340, 140);
            MainWindow.add(lbl_level);

            lbl_clevel = new Label(c.Lvl.ToString());
            lbl_clevel.Position = new Vector2(400, 140);
            MainWindow.add(lbl_clevel);

            lbl_exp = new Label("Exp:");
            lbl_exp.Position = new Vector2(460, 140);
            MainWindow.add(lbl_exp);

            lbl_cexp = new Label(c.Exp.ToString());
            lbl_cexp.Position = new Vector2(505, 140);
            MainWindow.add(lbl_cexp);

            lbl_hp = new Label("HP:");
            lbl_hp.Position = new Vector2(340, 170);
            MainWindow.add(lbl_hp);

            lbl_currHp = new Label(c.stats.hp.ToString());
            lbl_currHp.Position = new Vector2(460, 170);
            MainWindow.add(lbl_currHp);

            lbl_hpSlash = new Label("/");
            lbl_hpSlash.Position = new Vector2(500, 170);
            MainWindow.add(lbl_hpSlash);

            lbl_maxHp = new Label(c.stats.maxHp.ToString());
            lbl_maxHp.Position = new Vector2(515, 170);
            MainWindow.add(lbl_maxHp);

            lbl_mana = new Label("Mana:");
            lbl_mana.Position = new Vector2(340, 200);
            MainWindow.add(lbl_mana);

            lbl_currMana = new Label(c.stats.mana.ToString());
            lbl_currMana.Position = new Vector2(460, 200);
            MainWindow.add(lbl_currMana);

            lbl_manaSlash = new Label("/");
            lbl_manaSlash.Position = new Vector2(500, 200);
            MainWindow.add(lbl_manaSlash);

            lbl_maxMana = new Label(c.stats.maxMana.ToString());
            lbl_maxMana.Position = new Vector2(515, 200);
            MainWindow.add(lbl_maxMana);

            lbl_state = new Label("State:");
            lbl_state.Position = new Vector2(340, 230);
            MainWindow.add(lbl_state);

            lbl_cstate = new Label(c.stats.state.ToString());            
            lbl_cstate.Position = new Vector2(460, 230);
            MainWindow.add(lbl_cstate);

            lbl_force = new Label("Strength:");
            lbl_force.Position = new Vector2(340, 260);
            MainWindow.add(lbl_force);

            lbl_cforce = new Label(c.stats.traits.str.ToString());
            lbl_cforce.Position = new Vector2(460, 260);
            MainWindow.add(lbl_cforce);

            lbl_dex = new Label("Dexterity:");
            lbl_dex.Position = new Vector2(340, 290);
            MainWindow.add(lbl_dex);

            lbl_cdex = new Label(c.stats.traits.dex.ToString());
            lbl_cdex.Position = new Vector2(460, 290);
            MainWindow.add(lbl_cdex);

            lbl_intel = new Label("Intelligence:");
            lbl_intel.Position = new Vector2(340, 320);
            MainWindow.add(lbl_intel);

            lbl_cintel = new Label(c.stats.traits.intel.ToString());
            lbl_cintel.Position = new Vector2(460, 320);
            MainWindow.add(lbl_cintel);

            lbl_sag = new Label("Wisdom:");
            lbl_sag.Position = new Vector2(550, 260);
            MainWindow.add(lbl_sag);

            lbl_csag = new Label(c.stats.traits.wis.ToString());
            lbl_csag.Position = new Vector2(670, 260);
            MainWindow.add(lbl_csag);

            lbl_spd = new Label("Speed:");
            lbl_spd.Position = new Vector2(550, 290);
            MainWindow.add(lbl_spd);

            lbl_cspd = new Label(c.stats.traits.spd.ToString());
            lbl_cspd.Position = new Vector2(670, 290);
            MainWindow.add(lbl_cspd);

            lbl_con = new Label("Constitution:");
            lbl_con.Position = new Vector2(550, 320);
            MainWindow.add(lbl_con);

            lbl_ccon = new Label(c.stats.traits.con.ToString());
            lbl_ccon.Position = new Vector2(670, 320);
            MainWindow.add(lbl_ccon);

            lbl_move = new Label("Movement:");
            lbl_move.Position = new Vector2(550, 350);
            MainWindow.add(lbl_move);

            lbl_cmove = new Label(c.stats.movement.ToString());
            lbl_cmove.Position = new Vector2(670, 350);
            MainWindow.add(lbl_cmove);

            lbl_org = new Label("Organization:");
            lbl_org.Position = new Vector2(340, 350);
            MainWindow.add(lbl_org);

            lbl_corg = new Label(c.Organization);
            lbl_corg.Position = new Vector2(460, 350);
            MainWindow.add(lbl_corg);

            lbl_esc = new Label("ESC");
            lbl_esc.LabelFun = ColorTheme.LabelColorTheme.LabelFunction.BOLD;
            lbl_esc.Position = new Vector2(50, 440);
            MainWindow.add(lbl_esc);

            lbl_escAction = new Label("Go Back");
            lbl_escAction.LabelFun = ColorTheme.LabelColorTheme.LabelFunction.NORM;
            lbl_escAction.Position = new Vector2(100, 440);
            MainWindow.add(lbl_escAction);

            lbl_e = new Label("E");
            lbl_e.Position = new Vector2(50, 470);
            lbl_e.LabelFun = ColorTheme.LabelColorTheme.LabelFunction.BOLD;
            MainWindow.add(lbl_e);

            lbl_eAction = new Label("Equipment");
            lbl_eAction.Position = new Vector2(80, 470);
            lbl_eAction.LabelFun = ColorTheme.LabelColorTheme.LabelFunction.NORM;
            MainWindow.add(lbl_eAction);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (InputHandler.keyReleased(Keys.Escape))
                StateManager.Instance.goBack();

            if (InputHandler.keyReleased(Keys.E))
            {
                StateManager.Instance.goForward(new EquipmentManage(c, unit));
            }
        }
    }
}
