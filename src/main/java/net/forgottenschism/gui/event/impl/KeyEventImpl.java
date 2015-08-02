package net.forgottenschism.gui.event.impl;

import net.forgottenschism.gui.event.KeyEvent;

public class KeyEventImpl implements KeyEvent
{
	private int keyCode;
	private char character;
	private boolean keyPressed;

	public KeyEventImpl(int keyCode, char character, boolean keyPressed)
	{
		this.keyCode = keyCode;
		this.character = character;
		this.keyPressed = keyPressed;
	}

	@Override
	public int getKeyCode()
	{
		return keyCode;
	}

	@Override
	public char getCharacter()
	{
		return character;
	}

	@Override
	public boolean isKeyPressed()
	{
		return keyPressed;
	}

	@Override
	public boolean isKeyReleased()
	{
		return !keyPressed;
	}
}
