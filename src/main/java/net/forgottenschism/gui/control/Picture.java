package net.forgottenschism.gui.control;

import net.forgottenschism.engine.GraphicsGenerator;
import net.forgottenschism.gui.bean.Size2d;
import net.forgottenschism.gui.impl.AbstractControl;
import org.newdawn.slick.Color;
import org.newdawn.slick.Graphics;
import org.newdawn.slick.Image;

public class Picture extends AbstractControl
{
	private Image image;

	public Picture()
	{
		this(null);
	}

	public Picture(Image image)
	{
		this.image = image;
	}

	@Override
	protected void renderControl(Graphics graphics)
	{
		if(image!=null)
			graphics.drawImage(image, 0, 0);
		else
		{
			Size2d controlSize = getSize();
			Image blackImage = GraphicsGenerator.getSolidColorImage(controlSize.getWidth(),
					controlSize.getHeight(), Color.black);
			graphics.drawImage(blackImage, 0, 0);
		}
	}

	@Override
	public boolean isFocusable()
	{
		return false;
	}

	@Override
	public Size2d getPreferredSize()
	{
		return new Size2d(image.getWidth(), image.getHeight());
	}

	public Image getImage()
	{
		return image;
	}

	public void setImage(Image image)
	{
		this.image = image;
	}
}
