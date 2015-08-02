package net.forgottenschism.gui.impl;

import net.forgottenschism.gui.GuiComponent;
import net.forgottenschism.gui.bean.Position2d;
import net.forgottenschism.gui.bean.Size2d;
import org.newdawn.slick.GameContainer;

public abstract class AbstractGuiComponent implements GuiComponent
{
	private boolean hasFocus;
	private Position2d position;
	private Size2d size;
	private GameContainer gameContainer;

	public AbstractGuiComponent()
	{
		hasFocus = false;
		position = new Position2d(0, 0);
		size = new Size2d(0, 0);
	}

	@Override
	public void init(GameContainer gameContainer)
	{
		this.gameContainer = gameContainer;
	}

	protected GameContainer getGameContainer()
	{
		return gameContainer;
	}

	@Override
	public boolean hasFocus()
	{
		return hasFocus;
	}

	@Override
	public void setFocus(boolean focus)
	{
		hasFocus = focus;
	}

	@Override
	public Position2d getPosition()
	{
		return position;
	}

	@Override
	public void setPosition(Position2d position)
	{
		this.position = position;
	}

	@Override
	public Size2d getSize()
	{
		return size;
	}

	@Override
	public void setSize(Size2d size)
	{
		this.size = size;
	}
}
