package net.forgottenschism.gui.control;

import org.newdawn.slick.Color;
import org.newdawn.slick.Font;
import org.newdawn.slick.Graphics;
import org.newdawn.slick.Input;
import org.newdawn.slick.geom.Rectangle;

import net.forgottenschism.gui.SelectionListener;
import net.forgottenschism.gui.bean.Size2d;
import net.forgottenschism.gui.event.KeyEvent;
import net.forgottenschism.gui.impl.AbstractControl;
import net.forgottenschism.gui.theme.ColorTheme;
import net.forgottenschism.gui.theme.ColorThemeElement;
import net.forgottenschism.gui.theme.Theme;

public class Spinner extends AbstractControl
{
	private static final ColorTheme COLOR_THEME = Theme.getDefaultTheme().getColorTheme();
	
	private Font font;
	private Color textColor;
	private Color bgColor;
	
	private String[] textList;
	private int selection;
	private boolean selecting;
	private SelectionListener selectionListener;

	public Spinner()
	{
		this(new String[]{""});
	}
	
	public Spinner(String[] textList)
	{
		this(textList, null);
	}
	
	public Spinner(String[] textList, SelectionListener selectionListener)
	{
		this.textList = textList;
		selection = 0;
		selecting = false;
		font = Theme.getDefaultTheme().getDefaultFont();
		textColor = COLOR_THEME.getColor(ColorThemeElement.LABEL_NORMAL);
		bgColor = COLOR_THEME.getColor(ColorThemeElement.SPINNER_BACKGROUND_COLOR);
		this.selectionListener = selectionListener;
		
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
		int strWidth = 0;
		for(String str : textList)
		{
			if(font.getWidth(str) > strWidth)
				strWidth = font.getWidth(str);
		}
		
		return new Size2d(strWidth+font.getWidth("<  >"), font.getHeight("W"));
	}

	@Override
	protected void renderControl(Graphics graphics) 
	{
		// clear previous value
		graphics.setColor(bgColor);
		graphics.fill(new Rectangle(0, 0, getPreferredSize().getWidth(), getPreferredSize().getHeight()));
		
		Color textColor;
		if(!isEnabled() || hasFocus())
			textColor = COLOR_THEME.getStatusColor(isEnabled(), hasFocus());
		else
			textColor = getTextColor();

		graphics.setFont(getFont());
		graphics.setColor(textColor);
		
		if(isSelecting() && hasFocus())
			graphics.drawString("< "+getSelection()+" >", 0, 0);
		else
			graphics.drawString(getSelection(), 0, 0);
	}

	public String getSelection()
	{
        return textList[selection];
    }

    public void select(int selection)
    {
    	if(selection<0 || selection > textList.length-1)
			throw new IllegalArgumentException("selection is out of bounds!");
    	
        this.selection = selection;
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
	
	@Override
	public void keyReleased(KeyEvent keyEvent)
	{
		if(keyEvent.getKeyCode()==Input.KEY_ENTER)
		{
			if(isSelecting())
			{
				setSelecting(false);
				selectionListener.handleSelect(this);
			}
			else
				setSelecting(true);
		}
		else if(keyEvent.getKeyCode()==Input.KEY_LEFT)
		{
			if(!isSelecting())
				setSelecting(true);
				
			if(selection == 0)
				selection = textList.length-1;
			else
				selection--;
				
			select(selection);
		}
		else if(keyEvent.getKeyCode()==Input.KEY_RIGHT)
		{
			if(!isSelecting())
				setSelecting(true);
			
			if(selection == textList.length-1)
				selection = 0;
			else
				selection++;
				
			select(selection);
		}
	}
	
	@Override
	public void keyPressed(KeyEvent keyEvent)
	{
		
	}

	private boolean isSelecting() 
	{
		return selecting;
	}
	
	private void setSelecting(boolean selecting)
	{
		this.selecting = selecting;
	}
	
}
