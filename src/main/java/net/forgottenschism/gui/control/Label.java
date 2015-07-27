package net.forgottenschism.gui.control;

import net.forgottenschism.gui.Position2d;
import net.forgottenschism.gui.impl.AbstractControl;
import org.newdawn.slick.GameContainer;
import org.newdawn.slick.Graphics;

public class Label extends AbstractControl
{
    private String text;

    public Label()
    {
        this("");
    }

    public Label(String text)
    {
        this.text = text;
    }

    @Override
    public boolean canHaveFocus()
    {
        return false;
    }

    @Override
    public void render(GameContainer container, Graphics graphics)
    {
        Position2d position = getPosition();

        graphics.drawString(text, position.getX(), position.getY());
    }

    public String getText()
    {
        return text;
    }

    public void setText(String text)
    {
        this.text = text;
    }
}
