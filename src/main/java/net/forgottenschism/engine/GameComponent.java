package net.forgottenschism.engine;

import org.newdawn.slick.GameContainer;
import org.newdawn.slick.Graphics;

public interface GameComponent
{
	void init(GameContainer gameContainer);

	void update(int delta);

	void render(Graphics graphics);
}
