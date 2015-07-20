package net.forgottenschism.engine;

import org.newdawn.slick.Color;
import org.newdawn.slick.Graphics;
import org.newdawn.slick.Image;
import org.newdawn.slick.SlickException;

public class GraphicsGenerator
{
    private static GraphicsGenerator instance;

    public static GraphicsGenerator getInstance()
    {
        if(instance==null)
            instance=new GraphicsGenerator();

        return instance;
    }

    public static Image getSolidColorImage(int width, int height, Color color)
    {
        Image image;
        Graphics graphics;

        try
        {
            image = new Image(width, height);
            graphics = image.getGraphics();

        }
        catch (SlickException exception)
        {
            throw new RuntimeException("Error creating image", exception);
        }

        graphics.setColor(color);
        graphics.fillRect(0, 0, width, height);
        graphics.flush();

        return image;
    }
}
