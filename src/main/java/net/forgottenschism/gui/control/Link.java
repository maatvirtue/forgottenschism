package net.forgottenschism.gui.control;

import net.forgottenschism.gui.Position2d;
import net.forgottenschism.gui.SelectionListener;
import net.forgottenschism.gui.theme.ColorTheme;
import net.forgottenschism.gui.theme.Theme;
import org.newdawn.slick.Color;
import org.newdawn.slick.GameContainer;
import org.newdawn.slick.Graphics;
import org.newdawn.slick.Input;

public class Link extends Label
{
	private static final ColorTheme COLOR_THEME = Theme.getDefaultTheme().getColorTheme();

	private SelectionListener selectionListener;

	public Link()
	{
		this("");
	}

	public Link(String text)
	{
		this(text, null);
	}

	public Link(String text, SelectionListener selectionListener)
	{
		super(text);

		this.selectionListener = selectionListener;
	}

	@Override
	public boolean canHaveFocus()
	{
		return true;
	}

	@Override
	protected void renderControl(GameContainer container, Graphics graphics)
	{
		Position2d position = getPosition();
		Color textColor;

		if(!isEnabled() || hasFocus())
			textColor = COLOR_THEME.getStatusColor(isEnabled(), hasFocus());
		else
			textColor = getTextColor();

		graphics.setFont(getFont());
		graphics.setColor(textColor);

		graphics.drawString(getText(), 0, 0);
	}

	@Override
	public void keyReleased(int key, char character)
	{
		if(key==Input.KEY_ENTER && selectionListener!=null)
			selectionListener.handleSelect(this);
	}

	public SelectionListener getSelectionListener()
	{
		return selectionListener;
	}

	public void setSelectionListener(SelectionListener selectionListener)
	{
		this.selectionListener = selectionListener;
	}
}
