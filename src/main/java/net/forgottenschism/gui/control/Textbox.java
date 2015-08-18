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
	private int cursorPositionX;
	private int cursorPositionY;
	private int capacity;
	private int numLines;
	private int characterWidth;
	private int characterHeight;
	private Color textColor;
	private Font font;
	private boolean multiLine;

	public Textbox(int capacity)
	{
		this(capacity, 1, Theme.getDefaultTheme().getDefaultFont());
	}

	public Textbox(int capacity, int numLines)
	{
		this(capacity, numLines, Theme.getDefaultTheme().getDefaultFont());
	}

	public Textbox(int capacity, int numLines, Font font)
	{
		this(capacity, numLines, font, COLOR_THEME.getColor(ColorThemeElement.LABEL_NORMAL));
	}

	public Textbox(int capacity, int numLines, Font font, Color textColor)
	{
		if(capacity<1)
			throw new IllegalArgumentException("capacity must be greater than 0");
		if(numLines<1)
			throw new IllegalArgumentException("numLines must be greater than 0");

		this.capacity = capacity;
		this.numLines = numLines;
		this.multiLine = (numLines>1);
		this.font = font;
		this.textColor = textColor;
		cursorPositionX = 0;
		cursorPositionY = 0;
		characterWidth = font.getWidth("W");
		characterHeight = font.getHeight("W");

		text = new char[this.capacity*this.numLines];

		for(int i = 0; i<(this.capacity*this.numLines); i++)
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
		return new Size2d((capacity*characterWidth)+(6*2), (numLines*(characterHeight+6))+6);
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
		Size2d size = getSize();

		graphics.setColor(borderColor);

		graphics.fillRect(0, 0, size.getWidth(), size.getHeight());
	}

	private void renderBackground(Graphics graphics, Color backgroundColor)
	{
		Size2d size = getSize();

		graphics.setColor(backgroundColor);

		graphics.fillRect(2, 2, size.getWidth()-4, size.getHeight()-4);
	}

	private void renderCursor(Graphics graphics)
	{
		Color cursorColor = textColor;

		graphics.setColor(cursorColor);

		graphics.fillRect((6+cursorPositionX*characterWidth),
				(((cursorPositionY+1)*(5+characterHeight))+2),
				characterWidth, 2);
	}

	private void renderText(Graphics graphics)
	{
		graphics.setColor(textColor);
		graphics.setFont(font);

		for(int i = 0; i<numLines; i++)
		{
			graphics.drawString(new String(text, (i*capacity), capacity), 6, 6+(i*(characterHeight+6)));
		}
	}

	@Override
	public void keyReleased(KeyEvent keyEvent)
	{
		if(keyEvent.getKeyCode()==Input.KEY_ENTER)
		{
			if(isMultiLine() && cursorPositionY<numLines-1)
			{
				cursorPositionX = 0;
				cursorPositionY++;
			}
		}
		else if(keyEvent.getKeyCode()==Input.KEY_LEFT)
		{
			if(cursorPositionX>0)
				cursorPositionX--;
			else if(cursorPositionX==0 && isMultiLine() && cursorPositionY>0)
			{
				cursorPositionX = capacity-1;
				cursorPositionY--;
			}
		}
		else if(keyEvent.getKeyCode()==Input.KEY_RIGHT)
		{
			if(cursorPositionX<capacity-1)
				cursorPositionX++;
			else if(isMultiLine() && cursorPositionY<numLines-1)
			{
				cursorPositionX = 0;
				cursorPositionY++;
			}
		}
		else if(keyEvent.getKeyCode()==Input.KEY_BACK)
		{
			int targetCharPos = cursorPositionX+(cursorPositionY*capacity);
			if(text[targetCharPos]!=' ')
				text[targetCharPos] = ' ';
			else
			{
				if(cursorPositionX!=0)
					cursorPositionX--;
				else if(isMultiLine() && cursorPositionY>0)
				{
					cursorPositionX = capacity-1;
					cursorPositionY--;
				}

				targetCharPos = targetCharPos==0 ? targetCharPos : targetCharPos-1;
				text[targetCharPos] = ' ';
			}
		}
		else
		{

			text[cursorPositionX+(cursorPositionY*capacity)] = keyEvent.getCharacter();

			if(cursorPositionX<capacity-1)
				cursorPositionX++;
			else if(cursorPositionX==capacity-1 && isMultiLine() && cursorPositionY<numLines-1)
			{
				cursorPositionX = 0;
				cursorPositionY++;
			}
		}
	}

	@Override
	public String toString()
	{
		return this.getClass().getSimpleName()+": \""+text+"\"";
	}

	public Color getTextColor()
	{
		return textColor;
	}

	public void setTextColor(Color textColor)
	{
		this.textColor = textColor;
	}

	public boolean isMultiLine()
	{
		return multiLine;
	}

	public void setMultiLine(boolean multiLine)
	{
		this.multiLine = multiLine;
	}
}
