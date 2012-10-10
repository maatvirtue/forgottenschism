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

        public CharManage()
        {
            lbl_charMng = new Label("Character Management");
            lbl_charMng.Color = Color.Gold;
            lbl_charMng.Position = new Vector2(50, 30);

            charPic = new PictureBox();
            charPic.Image = Graphic.Content.Instance.Images.characters.healer;
            charPic.Size = new Vector2(384, 384);
            charPic.Position = new Vector2(0, 60);

            cm.add(lbl_charMng);
            cm.add(charPic);

            lbl_name = new Label("Mr. Derpington");
            lbl_name.Position = new Vector2(400, 50);
            cm.add(lbl_name);

            lbl_class = new Label("Fighter");
            lbl_class.Position = new Vector2(400, 80);
            cm.add(lbl_class);

            lbl_level = new Label("Level:");
            lbl_level.Position = new Vector2(400, 110);
            cm.add(lbl_level);

            lbl_clevel = new Label("10");
            lbl_clevel.Color = Color.White;
            lbl_clevel.Position = new Vector2(460, 110);
            cm.add(lbl_clevel);

            lbl_exp = new Label("Exp:");
            lbl_exp.Position = new Vector2(520, 110);
            cm.add(lbl_exp);

            lbl_cexp = new Label("0");
            lbl_cexp.Color = Color.White;
            lbl_cexp.Position = new Vector2(565, 110);
            cm.add(lbl_cexp);

            lbl_hp = new Label("HP:");
            lbl_hp.Position = new Vector2(400, 140);
            cm.add(lbl_hp);

            lbl_currHp = new Label("15");
            lbl_currHp.Color = Color.White;
            lbl_currHp.Position = new Vector2(520, 140);
            cm.add(lbl_currHp);

            lbl_hpSlash = new Label("/");
            lbl_hpSlash.Position = new Vector2(550, 140);
            cm.add(lbl_hpSlash);

            lbl_maxHp = new Label("20");
            lbl_maxHp.Color = Color.White;
            lbl_maxHp.Position = new Vector2(565, 140);
            cm.add(lbl_maxHp);

            lbl_mana = new Label("Mana:");
            lbl_mana.Position = new Vector2(400, 170);
            cm.add(lbl_mana);

            lbl_currMana = new Label("2");
            lbl_currMana.Color = Color.White;
            lbl_currMana.Position = new Vector2(520, 170);
            cm.add(lbl_currMana);

            lbl_manaSlash = new Label("/");
            lbl_manaSlash.Position = new Vector2(550, 170);
            cm.add(lbl_manaSlash);

            lbl_maxMana = new Label("5");
            lbl_maxMana.Color = Color.White;
            lbl_maxMana.Position = new Vector2(565, 170);
            cm.add(lbl_maxMana);

            lbl_state = new Label("State:");
            lbl_state.Position = new Vector2(400, 200);
            cm.add(lbl_state);

            lbl_cstate = new Label("Normal");
            lbl_cstate.Color = Color.White;
            lbl_cstate.Position = new Vector2(520, 200);
            cm.add(lbl_cstate);

            lbl_force = new Label("Strength:");
            lbl_force.Position = new Vector2(400, 230);
            cm.add(lbl_force);

            lbl_cforce = new Label("10");
            lbl_cforce.Color = Color.White;
            lbl_cforce.Position = new Vector2(520, 230);
            cm.add(lbl_cforce);

            lbl_dex = new Label("Dexterity:");
            lbl_dex.Position = new Vector2(400, 260);
            cm.add(lbl_dex);

            lbl_cdex = new Label("5");
            lbl_cdex.Color = Color.White;
            lbl_cdex.Position = new Vector2(520, 260);
            cm.add(lbl_cdex);

            lbl_intel = new Label("Intelligence:");
            lbl_intel.Position = new Vector2(400, 290);
            cm.add(lbl_intel);

            lbl_cintel = new Label("1");
            lbl_cintel.Color = Color.White;
            lbl_cintel.Position = new Vector2(520, 290);
            cm.add(lbl_cintel);

            lbl_sag = new Label("Wisdom:");
            lbl_sag.Position = new Vector2(400, 320);
            cm.add(lbl_sag);

            lbl_csag = new Label("3");
            lbl_csag.Color = Color.White;
            lbl_csag.Position = new Vector2(520, 320);
            cm.add(lbl_csag);

            lbl_spd = new Label("Speed:");
            lbl_spd.Position = new Vector2(400, 350);
            cm.add(lbl_spd);

            lbl_cspd = new Label("4");
            lbl_cspd.Color = Color.White;
            lbl_cspd.Position = new Vector2(520, 350);
            cm.add(lbl_cspd);

            lbl_con = new Label("Constitution:");
            lbl_con.Position = new Vector2(400, 380);
            cm.add(lbl_con);

            lbl_ccon = new Label("12");
            lbl_ccon.Color = Color.White;
            lbl_ccon.Position = new Vector2(520, 380);
            cm.add(lbl_ccon);

            lbl_move = new Label("Movement:");
            lbl_move.Position = new Vector2(400, 410);
            cm.add(lbl_move);

            lbl_cmove = new Label("5");
            lbl_cmove.Color = Color.White;
            lbl_cmove.Position = new Vector2(520, 410);
            cm.add(lbl_cmove);
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
