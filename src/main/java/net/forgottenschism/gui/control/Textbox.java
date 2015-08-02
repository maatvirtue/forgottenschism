package net.forgottenschism.gui.control;

import net.forgottenschism.gui.bean.Position2d;
import net.forgottenschism.gui.bean.Size2d;
import net.forgottenschism.gui.event.KeyEvent;
import net.forgottenschism.gui.impl.AbstractControl;
import net.forgottenschism.gui.theme.ColorTheme;
import net.forgottenschism.gui.theme.ColorThemeElement;
import net.forgottenschism.gui.theme.Theme;
import org.newdawn.slick.*;

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

		setSize(getPreferredSize());
	}

	@Override
	public boolean isFocusable()
	{
		return true;
	}

	@Override
	public Size2d getPreferredSize()
	{
		return new Size2d((capacity*characterWidth)+(6*2), characterHeight+(6*2));
	}

	@Override
	protected void renderControl(Graphics graphics)
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

		graphics.fillRect(0, 0, size.getWidth(), size.getHeight());
	}

	private void renderBackground(Graphics graphics, Color backgroundColor)
	{
		Position2d position = getPosition();
		Size2d size = getSize();

		graphics.setColor(backgroundColor);

		graphics.fillRect(2, 2, size.getWidth()-4, size.getHeight()-4);
	}

	private void renderCursor(Graphics graphics)
	{
		Color cursorColor = textColor;

		graphics.setColor(cursorColor);

		graphics.fillRect((6+cursorPosition*characterWidth),
				(5+characterHeight+2),
				characterWidth, 2);
	}

	private void renderText(Graphics graphics)
	{
		graphics.setColor(textColor);
		graphics.setFont(font);

		graphics.drawString(new String(text), 6, 6);
	}

	@Override
	public void keyReleased(KeyEvent keyEvent)
	{
		if(keyEvent.getKeyCode()==Input.KEY_LEFT && cursorPosition>0)
			cursorPosition--;
		else if(keyEvent.getKeyCode()==Input.KEY_RIGHT && cursorPosition<capacity-1)
			cursorPosition++;
		else if(keyEvent.getKeyCode()==Input.KEY_BACK)
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
			text[cursorPosition] = keyEvent.getCharacter();

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
