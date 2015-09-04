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
	private Image cachedDefaultImage;

	public Picture()
	{
		this(null);
	}

	public Picture(Image image)
	{
		setImage(image);
	}

	@Override
	protected void renderControl(Graphics graphics)
	{
		if(image!=null)
			graphics.drawImage(image, 0, 0);
		else
		{
			Size2d controlSize = getSize();
			graphics.drawImage(getDefaultImage(controlSize.getWidth(), controlSize.getHeight()), 0, 0);
		}
	}

	@Override
	public void setSize(Size2d size)
	{
		super.setSize(size);
	}

	private Image getDefaultImage(int width, int height)
	{
		if(cachedDefaultImage!=null && cachedDefaultImage.getWidth()==width && cachedDefaultImage.getHeight()==height)
			return cachedDefaultImage;

		cachedDefaultImage = GraphicsGenerator.getSolidColorImage(width, height, Color.transparent);

		return cachedDefaultImage;
	}

	@Override
	public boolean isFocusable()
	{
		return false;
	}

	@Override
	public Size2d getPreferredSize()
	{
		if(image==null)
			return null;
		else
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
