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
            public enum LabelFunction { NORM, BOLD, TITLE, CUSTOM };

            public static LabelColorTheme Default = new LabelColorTheme();

            /// <summary>
            /// Bold Label Color
            /// </summary>
            Color bold;

            /// <summary>
            /// Title Label Color
            /// </summary>
            Color title;

            /// <summary>
            /// Normal Label Color
            /// </summary>
            Color norm;

            /// <summary>
            /// initialize with defaults color theme
            /// </summary>
            public LabelColorTheme()
            {
                bold = Color.Blue;
                title = Color.Yellow;
                norm = Color.White;
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="fbold">Bold Label Color</param>
            /// <param name="ftitle">Title Label Color</param>
            /// /// <param name="fnorm">Normal Label Color</param>
            public LabelColorTheme(Color fbold, Color ftitle, Color fnorm)
            {
                bold = fbold;
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
                else if (fun == LabelFunction.BOLD)
                    return bold;
                else if (fun == LabelFunction.TITLE)
                    return title;
                else
                    return norm;
            }
        }

        /// <summary>
        /// LabelColorTheme for this ColorTheme
        /// </summary>
        LabelColorTheme lblct;

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
            eu = Color.White;
            ds = Color.Orange;
            du = Color.Gray;

            lblct = LabelColorTheme.Default;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fes">Enable and Selected Color</param>
        /// <param name="feu">Enables and Unselected Color</param>
        /// <param name="fds">Disabled and Selected Color</param>
        /// <param name="fdu">Disabled and Unselected Color</param>
        public ColorTheme(Color fes, Color feu, Color fds, Color fdu, LabelColorTheme flblct)
        {
            es = fes;
            eu = feu;
            ds = fds;
            du = fdu;

            lblct = flblct;
        }

        public LabelColorTheme LabelCT
        {
            get { return lblct; }
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
