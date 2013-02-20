using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

namespace ForgottenSchism.control
{
    /// <summary>
    /// color theme for display
    /// </summary>
    public class ColorTheme
    {
        public static ColorTheme Default = new ColorTheme();

        public class LabelColorTheme
        {
            public enum LabelFunction { NORM, CONTROL, TITLE, WARNING, BOLD, CUSTOM };

            public static LabelColorTheme Default = new LabelColorTheme();

            /// <summary>
            /// Control Label Color
            /// </summary>
            Color control;

            /// <summary>
            /// Title Label Color
            /// </summary>
            Color title;

            /// <summary>
            /// Normal Label Color
            /// </summary>
            Color norm;

            /// <summary>
            /// Warning Label Color
            /// </summary>
            Color warning;

            /// <summary>
            /// Bold Label Color
            /// </summary>
            Color bold;

            /// <summary>
            /// initialize with defaults color theme
            /// </summary>
            public LabelColorTheme()
            {
                control = Color.Blue;
                title = Color.OrangeRed;
                norm = Color.Black;
                bold = new Color(91, 26, 0);
                warning = Color.Red;
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="fbold">Bold Label Color</param>
            /// <param name="ftitle">Title Label Color</param>
            /// /// <param name="fnorm">Normal Label Color</param>
            public LabelColorTheme(Color fbold, Color ftitle, Color fnorm)
            {
                control = fbold;
                title = ftitle;
                norm = fnorm;
            }

            /// <summary>
            /// gets the Color of the label according to the current LabelColorTheme
            /// </summary>
            /// <param name="fun">Label's Function</param>
            /// <returns>Color of the label according to the current LabelColorTheme</returns>
            public Color get(LabelFunction fun)
            {
                if (fun == LabelFunction.NORM)
                    return norm;
                else if (fun == LabelFunction.CONTROL)
                    return control;
                else if (fun == LabelFunction.TITLE)
                    return title;
                else if (fun == LabelFunction.BOLD)
                    return bold;
                else if (fun == LabelFunction.WARNING)
                    return warning;
                else
                    return norm;
            }
        }

        /// <summary>
        /// LabelColorTheme for this ColorTheme
        /// </summary>
        LabelColorTheme lblct;

        /// <summary>
        /// Color when an action is performed
        /// </summary>
        Color action;

        /// <summary>
        /// Enabled and Selected Color
        /// </summary>
        Color es;

        /// <summary>
        /// Enabled and Unselected Color
        /// </summary>
        Color eu;

        /// <summary>
        /// Disabled and Selected Color
        /// </summary>
        Color ds;

        /// <summary>
        /// Disabled and Unselected Color
        /// </summary>
        Color du;

        /// <summary>
        /// initializes with default colot theme
        /// </summary>
        public ColorTheme()
        {
            es = Color.DarkRed;
            eu = new Color(50, 50, 50);
            ds = Color.Orange;
            du = Color.Gray;
            action = Color.Yellow;

            lblct = LabelColorTheme.Default;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fes">Enable and Selected Color</param>
        /// <param name="feu">Enables and Unselected Color</param>
        /// <param name="fds">Disabled and Selected Color</param>
        /// <param name="fdu">Disabled and Unselected Color</param>
        /// <param name="flblct">Label Color Themer</param>
        /// <param name="faction">Color when an action is performed</param>
        public ColorTheme(Color fes, Color feu, Color fds, Color fdu, LabelColorTheme flblct, Color faction)
        {
            es = fes;
            eu = feu;
            ds = fds;
            du = fdu;
            action = faction;

            lblct = flblct;
        }

        /// <summary>
        /// Label Color Theme
        /// </summary>
        public LabelColorTheme LabelCT
        {
            get { return lblct; }
        }


        /// <summary>
        /// Color when an action is performed
        /// </summary>
        public Color ActionColor
        {
            get { return action; }
        }

        /// <summary>
        /// gets Color according to enabled / selected
        /// </summary>
        /// <param name="enabled">is the display element enabled</param>
        /// <param name="selected">id the display element selected</param>
        /// <returns>Color in this theme according to enabled / selected</returns>
        public Color getColor(bool enabled, bool selected)
        {
            if (enabled)
                if (selected)
                    return es;
                else
                    return eu;
            else
                if (selected)
                    return ds;
                else
                    return du;
        }
    }
}
