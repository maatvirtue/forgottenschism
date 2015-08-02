package net.forgottenschism.gui.event;

public interface KeyEvent extends GuiEvent
{
	int getKeyCode();

	char getCharacter();

	boolean isKeyPressed();

	boolean isKeyReleased();
}
