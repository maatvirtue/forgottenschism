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
    class EquipmentManage : Screen
    {
        Label lbl_title;
        Label lbl_name;
        Label lbl_cname;
        Label lbl_eq;
        Menu menu_eq;
        Menu menu_uitem;
        Label lbl_uitem;
        Label lbl_enter;
        Label lbl_enterGive;
        Label lbl_v;
        Label lbl_vView;
        
        Character c;
        Unit unit;

        List<Item> uitemls;

        bool inMenuEq;

        int selEq;

        public EquipmentManage(Character fc, Unit u)
        {
            selEq = 0;
            c = fc;
            unit = u;

            uitemls = new List<Item>();

            lbl_name = new Label("Character Name");
            lbl_name.LabelFun = ColorTheme.LabelColorTheme.LabelFunction.BOLD;
            lbl_name.Position = new Vector2(50, 50);
            MainWindow.add(lbl_name);

            lbl_cname = new Label(c.Name);
            lbl_cname.LabelFun = ColorTheme.LabelColorTheme.LabelFunction.NORM;
            lbl_cname.Position = new Vector2(150, 50);
            MainWindow.add(lbl_cname);

            lbl_title = new Label("Character Equipment");
            lbl_title.LabelFun = ColorTheme.LabelColorTheme.LabelFunction.BOLD;
            lbl_title.Position = new Vector2(250, 100);
            MainWindow.add(lbl_title);

            lbl_eq = new Label("Equipment");
            lbl_eq.LabelFun = ColorTheme.LabelColorTheme.LabelFunction.TITLE;
            lbl_eq.Position = new Vector2(90, 130);
            MainWindow.add(lbl_eq);

            menu_eq = new Menu(3);
            menu_eq.Position = new Vector2(70, 150);
            MainWindow.add(menu_eq);

            lbl_uitem = new Label("Unit's Item");
            lbl_uitem.LabelFun = ColorTheme.LabelColorTheme.LabelFunction.TITLE;
            lbl_uitem.Position = new Vector2(450, 130);
            MainWindow.add(lbl_uitem);

            menu_uitem = new Menu(10);
            menu_uitem.Position = new Vector2(430, 160);
            MainWindow.add(menu_uitem);

            lbl_enter = new Label("ENTER");
            lbl_enter.LabelFun = ColorTheme.LabelColorTheme.LabelFunction.BOLD;
            lbl_enter.Position = new Vector2(50, 440);
            MainWindow.add(lbl_enter);

            lbl_enterGive = new Label("Equip Item");
            lbl_enterGive.LabelFun = ColorTheme.LabelColorTheme.LabelFunction.NORM;
            lbl_enterGive.Position = new Vector2(130, 440);
            MainWindow.add(lbl_enterGive);

            lbl_v = new Label("V");
            lbl_v.LabelFun = ColorTheme.LabelColorTheme.LabelFunction.BOLD;
            lbl_v.Position = new Vector2(100, 470);
            MainWindow.add(lbl_v);

            lbl_vView = new Label("View Item");
            lbl_vView.LabelFun = ColorTheme.LabelColorTheme.LabelFunction.NORM;
            lbl_vView.Position = new Vector2(130, 470);
            MainWindow.add(lbl_vView);

            update_menuUItem();
            update_menuEq();

            inMenuEq = true;

            menu_uitem.TabStop = false;
            menu_uitem.unfocusLink();

            update_lblView();

            if (unit == null)
            {
                lbl_enter.Visible = false;
                lbl_enterGive.Visible = false;
            }
        }

        private void update_lblView()
        {
            lbl_v.Visible = (selectedItem() != null);
            lbl_vView.Visible = (selectedItem() != null);
        }

        /// <summary>
        /// updates menu_UItem
        /// </summary>
        private void update_menuUItem()
        {
            if(unit==null)
                return;

            uitemls.Clear();
            menu_uitem.clear();

            foreach (Item i in unit.Inventory.Items)
                if (i.Type == selectedType())
                {
                    menu_uitem.add(new Link(i.Name));
                    uitemls.Add(i);
                }

            if (inMenuEq)
                menu_uitem.unfocusLink();
        }

        /// <summary>
        /// Return the type of the selected equipment
        /// </summary>
        /// <returns>type of the selected equipment</returns>
        private Item.Item_Type selectedType()
        {
            int i = menu_eq.Selected;

            if (i == 0)
                return Item.Item_Type.WEAPON;
            else if(i==1)
                return Item.Item_Type.ARMOR;
            else
                return Item.Item_Type.ACCESORY;
        }

        /// <summary>
        /// updates menu_Eq
        /// </summary>
        private void update_menuEq()
        {
            menu_eq.clear();

            if (c.equipment.weapon == null)
                menu_eq.add(new Link("nothing equiped"));
            else
                menu_eq.add(new Link(c.equipment.weapon.Name));

            if (c.equipment.armor == null)
                menu_eq.add(new Link("nothing equiped"));
            else
                menu_eq.add(new Link(c.equipment.armor.Name));

            if (c.equipment.accesory == null)
                menu_eq.add(new Link("nothing equiped"));
            else
                menu_eq.add(new Link(c.equipment.accesory.Name));
        }

        /// <summary>
        /// Gets Selected Item (on the equipment menu)
        /// </summary>
        /// <returns>Selected Item or null</returns>
        private Item selectedItemEq()
        {
            int i = menu_eq.Selected;

            if (i == 0)
                return c.equipment.weapon;
            else if (i == 1)
                return c.equipment.armor;
            else
                return c.equipment.accesory;
        }

        /// <summary>
        /// Gets Selected Item (on the unit items menu)
        /// </summary>
        /// <returns>Selected Item or null</returns>
        private Item selectedItemUItem()
        {
            if (uitemls.Count == 0)
                return null;

            return uitemls[menu_uitem.Selected];
        }

        /// <summary>
        /// Gets Selected Item (on the selected menu)
        /// </summary>
        /// <returns>Selected Item or null</returns>
        private Item selectedItem()
        {
            if (inMenuEq)
            {
                int i = menu_eq.Selected;

                if (i == 0)
                    return c.equipment.weapon;
                else if (i == 1)
                    return c.equipment.armor;
                else
                    return c.equipment.accesory;
            }
            else
            {
                if (uitemls.Count == 0)
                    return null;

                return uitemls[menu_uitem.Selected];
            }
        }

        /// <summary>
        /// Is an item selected (in the equipment menu only)
        /// </summary>
        /// <returns>Is an item selected</returns>
        private bool isItemSelected()
        {
            int i = menu_eq.Selected;

            if (i == 0)
                return c.equipment.weapon != null;
            else if (i == 1)
                return c.equipment.armor != null;
            else
                return c.equipment.accesory != null;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (selEq != menu_eq.Selected)
            {
                selEq = menu_eq.Selected;

                update_lblView();
                update_menuUItem();
            }

            if (InputHandler.keyReleased(Keys.Escape))
            {
                if (inMenuEq)
                    StateManager.Instance.goBack();
                else
                {
                    lbl_v.Visible = isItemSelected();
                    lbl_vView.Visible = isItemSelected();

                    menu_eq.TabStop = true;
                    menu_uitem.TabStop = false;
                    menu_eq.HasFocus = true;
                    menu_uitem.HasFocus = false;

                    menu_uitem.unfocusLink();

                    inMenuEq = true;
                }
            }

            if (InputHandler.keyReleased(Keys.V) && lbl_v.Visible)
            {
                StateManager.Instance.goForward(new ItemManage(selectedItem()));
            }

            if (InputHandler.keyReleased(Keys.Enter)&&lbl_enter.Visible)
            {
                if (inMenuEq)
                {
                    //the user just selected an item

                    if (menu_uitem.Count > 0)
                    {
                        lbl_v.Visible = false;
                        lbl_vView.Visible = false;

                        menu_eq.TabStop = false;
                        menu_uitem.TabStop = true;
                        menu_eq.HasFocus = false;
                        menu_uitem.HasFocus = true;

                        menu_uitem.refocusLink();

                        inMenuEq = false;
                        
                        update_lblView();
                    }
                }
                else
                {
                    if (isItemSelected())
                        unit.Inventory.Items.Add(selectedItemEq());

                    c.equip(selectedItemUItem());
                    unit.Inventory.Items.Remove(selectedItemUItem());

                    lbl_v.Visible = true;
                    lbl_vView.Visible = true;

                    menu_eq.TabStop = true;
                    menu_uitem.TabStop = false;
                    menu_eq.HasFocus = true;
                    menu_uitem.HasFocus = false;

                    menu_uitem.unfocusLink();

                    update_menuEq();
                    update_menuUItem();

                    inMenuEq = true;

                    update_lblView();
                }
            }
        }
    }
}
