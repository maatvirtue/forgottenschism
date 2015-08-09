package net.forgottenschism.gui.layout;

import net.forgottenschism.gui.Control;
import net.forgottenschism.gui.bean.Position2d;
import net.forgottenschism.gui.bean.Size2d;
import net.forgottenschism.util.GenericUtil;
import org.apache.commons.lang3.math.NumberUtils;

import java.util.List;

public class RowLayout extends AbstractLayout
{
	private int[] columnsWidth;

	public RowLayout()
	{
		//Do nothing
	}

	public RowLayout(List<Control> children)
	{
		for(Control control : children)
			addControl(control);
	}

	@Override
	protected void layout()
	{
		List<Control> children = getChildren();

		if(children==null || children.isEmpty())
			return;

		if(columnsWidth==null)
			throw new IllegalStateException("Columns width not calculated by parent layout. (make sure parent layout is a TableLayout)");

		if(columnsWidth.length!=children.size())
			throw new IllegalStateException("Invalid columns width. (make sure parent layout is a TableLayout)");

		int layoutHeight = getSize().getHeight();
		int positionX = 0;
		Control control;
		Position2d cellOffset;
		Size2d cellSize;

		for(int i = 0; i<children.size(); i++)
		{
			control = children.get(i);

			cellOffset = new Position2d(positionX, 0);
			cellSize = new Size2d(columnsWidth[i], layoutHeight);

			layoutRelatively(cellOffset, cellSize, control);

//			control.setPosition(cellOffset);
//			control.setSize(cellSize);

			positionX += columnsWidth[i];
		}
	}

	@Override
	public Size2d getPreferredSize()
	{
		List<Control> children = getChildren();

		if(children==null || children.isEmpty())
			return new Size2d(0, 0);

		int maxHeight = 0;
		int width = 0;
		Size2d controlPreferredSize;

		for(Control control : children)
		{
			controlPreferredSize = control.getPreferredSize();

			if(controlPreferredSize!=null)
			{
				width += controlPreferredSize.getWidth();
				maxHeight = NumberUtils.max(maxHeight, controlPreferredSize.getHeight());
			}
		}

		return new Size2d(width, maxHeight);
	}

	public int[] getColumnsWidth()
	{
		return columnsWidth;
	}

	public void setColumnsWidth(int[] columnsWidth)
	{
		this.columnsWidth = columnsWidth;
	}
}
