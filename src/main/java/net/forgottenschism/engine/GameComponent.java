package net.forgottenschism.engine;

import org.newdawn.slick.GameContainer;
import org.newdawn.slick.Graphics;

public interface GameComponent
{
    void update(GameContainer container, int delta);
    void render(GameContainer container, Graphics graphics);
}
