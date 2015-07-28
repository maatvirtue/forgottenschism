package net.forgottenschism.gui.control;

import net.forgottenschism.gui.Position2d;
import net.forgottenschism.gui.Size2d;
import net.forgottenschism.gui.impl.AbstractControl;
import net.forgottenschism.gui.theme.ColorTheme;
import net.forgottenschism.gui.theme.ColorThemeElement;
import net.forgottenschism.gui.theme.Theme;
import org.newdawn.slick.*;

import javax.swing.*;

public class Textbox extends AbstractControl
{
	private static final ColorTheme COLOR_THEME = Theme.getDefaultTheme().getColorTheme();

	private char[] text;
	private int cursorPosition;
	private int capacity;
	private int characterWidth;
	private int characterHeight;
	private Color textColor;
	private Font font;

	public Textbox(int capacity)
	{
		this(capacity, Theme.getDefaultTheme().getDefaultFont());
	}

	public Textbox(int capacity, Font font)
	{
		this(capacity, font, COLOR_THEME.getColor(ColorThemeElement.LABEL_NORMAL));
	}

	public Textbox(int capacity, Font font, Color textColor)
	{
		if(capacity<1)
			throw new IllegalArgumentException("capacity has to be greater than 0");

		this.capacity = capacity;
		this.font = font;
		this.textColor = textColor;
		cursorPosition = 0;
		characterWidth = font.getWidth("W");
		characterHeight = font.getHeight("W");

		text = new char[this.capacity];

		for(int i = 0; i<this.capacity; i++)
			text[i] = ' ';

		setSize(new Size2d((capacity*characterWidth)+(6*2), characterHeight+(6*2)));
	}

	@Override
	public boolean canHaveFocus()
	{
		return true;
	}

	@Override
	public void render(GameContainer container, Graphics graphics)
	{
		Color borderColor = COLOR_THEME.getStatusColor(isEnabled(), hasFocus());
		Color backgroundColor = COLOR_THEME.getColor(ColorThemeElement.TEXTBOX_BACKGROUND_COLOR);

		renderBorder(graphics, borderColor);
		renderBackground(graphics, backgroundColor);
		renderCursor(graphics);
		renderText(graphics);
	}

	private void renderBorder(Graphics graphics, Color borderColor)
	{
		Position2d position = getPosition();
		Size2d size = getSize();

		graphics.setColor(borderColor);
		graphics.fillRect(position.getX(), position.getY(), size.getWidth(), size.getHeight());
	}

	private void renderBackground(Graphics graphics, Color backgroundColor)
	{
		Position2d position = getPosition();
		Size2d size = getSize();

		graphics.setColor(backgroundColor);
		graphics.fillRect(position.getX()+2, position.getY()+2, size.getWidth()-4, size.getHeight()-4);
	}

	private void renderCursor(Graphics graphics)
	{
		Position2d position = getPosition();
		Color cursorColor = textColor;

		graphics.setColor(cursorColor);
		graphics.fillRect((position.getX()+6+cursorPosition*characterWidth),
				(position.getY()+5+characterHeight+2),
				characterWidth, 2);
	}

	private void renderText(Graphics graphics)
	{
		Position2d position = getPosition();

		graphics.setColor(textColor);
		graphics.setFont(font);
		graphics.drawString(new String(text), position.getX()+6, position.getY()+6);
	}

	@Override
	public void keyReleased(int key, char character)
	{
		if(key==Input.KEY_LEFT && cursorPosition>0)
			cursorPosition--;
		else if(key==Input.KEY_RIGHT && cursorPosition<capacity-1)
			cursorPosition++;
		else if(key==Input.KEY_BACK)
		{
			if(text[cursorPosition]!=' ')
				text[cursorPosition] = ' ';
			else
			{
				if(cursorPosition!=0)
					cursorPosition--;

				text[cursorPosition] = ' ';
			}
		}
		else
		{
			text[cursorPosition] = character;

			if(cursorPosition<capacity-1)
				cursorPosition++;
		}
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
