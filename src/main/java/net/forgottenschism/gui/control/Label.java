package net.forgottenschism.gui.control;

import net.forgottenschism.gui.bean.Position2d;
import net.forgottenschism.gui.bean.Size2d;
import net.forgottenschism.gui.impl.AbstractControl;
import net.forgottenschism.gui.theme.ColorThemeElement;
import net.forgottenschism.gui.theme.Theme;
import org.newdawn.slick.Color;
import org.newdawn.slick.Font;
import org.newdawn.slick.Graphics;

public class Label extends AbstractControl
{
    private String text;
	private Font font;
	private Color textColor;

    public Label()
    {
        this("");
    }

    public Label(String text)
    {
        this.text = text;
		font = Theme.getDefaultTheme().getDefaultFont();
		textColor = Theme.getDefaultTheme().getColorTheme().getColor(ColorThemeElement.LABEL_NORMAL);

		setSize(getPreferredSize());
	}

    @Override
	public boolean isFocusable()
	{
        return false;
    }

	@Override
	public Size2d getPreferredSize()
	{
		return new Size2d(font.getWidth(text), font.getHeight(text));
	}

	@Override
	protected void renderControl(Graphics graphics)
	{
        Position2d position = getPosition();

		graphics.setFont(font);
		graphics.setColor(textColor);

		graphics.drawString(text, 0, 0);
	}

	@Override
	public String toString()
	{
		return this.getClass().getSimpleName()+": \""+text+"\"";
	}

	public String getText()
	{
        return text;
    }

    public void setText(String text)
    {
        this.text = text;
    }

	public Font getFont()
	{
		return font;
	}

	public void setFont(Font font)
	{
		this.font = font;
	}

	public Color getTextColor()
	{
		return textColor;
	}

	public void setTextColor(Color textColor)
	{
		this.textColor = textColor;
	}
}
