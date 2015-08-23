package net.forgottenschism.gui.layout;

import net.forgottenschism.gui.Control;
import net.forgottenschism.gui.bean.Orientation2d;
import net.forgottenschism.gui.bean.Position2d;
import net.forgottenschism.gui.bean.Size2d;
import net.forgottenschism.util.GenericUtil;

import org.apache.commons.lang3.math.NumberUtils;

import java.util.*;

public class TableLayout extends AbstractLayout
{
	private static class Cell
	{
		private Control control;

		public Cell()
		{
			//Do nothing
		}

		public Cell(Control control)
		{
			this.control = control;
		}

		public Control getControl()
		{
			return control;
		}

		public void setControl(Control control)
		{
			this.control = control;
		}
	}

	private static class Table
	{
		private Map<Position2d, Cell> cellsMap;
		private int numberOfColumns;
		private int numberOfRows;

		public Table()
		{
			cellsMap = new HashMap<>();
			numberOfColumns = 0;
			numberOfRows = 0;
		}

		public boolean isEmpty()
		{
			return cellsMap.isEmpty();
		}

		public void putCell(Position2d position, Cell cell)
		{
			cellsMap.put(position, cell);

			numberOfColumns = NumberUtils.max(numberOfColumns, position.getX()+1);
			numberOfRows = NumberUtils.max(numberOfRows, position.getY()+1);
		}

		public Cell getCell(Position2d position)
		{
			return cellsMap.get(position);
		}

		public int getNumberOfCell(Orientation2d orientation)
		{
			if(orientation==Orientation2d.HORIZONTAL)
				return numberOfColumns;
			else
				return numberOfRows;
		}
	}

	@Override
	public void addControl(Control control)
	{
		if(!(control instanceof RowLayout))
			throw new IllegalArgumentException("control is not a RowLayout (you can only add RowLayout children to TableLayout)");

		super.addControl(control);
	}

	@Override
	protected void layout()
	{
		List<Control> children = getChildren();

		if(children==null || children.isEmpty())
			return;

		int[] columnsWidth = getLayedoutDivisionsLength(Orientation2d.HORIZONTAL);
		int[] rowsHeight = getLayedoutDivisionsLength(Orientation2d.VERTICAL);

		layout(columnsWidth, rowsHeight);
	}

	private int[] getLayedoutDivisionsLength(Orientation2d orientation)
	{
		Size2d layoutSize = getSize();
		Table table = getTable();

		int numberOfDivisions = table.getNumberOfCell(orientation);
		int[] layedoutDivisionsLengths;
		int idealDivisionSize = layoutSize.getValueByOrientation(orientation)/numberOfDivisions;
		int[] divisionsPreferredLengths = getDivisionsPreferredLength(table, orientation);
		int totalPreferredLength = GenericUtil.sum(divisionsPreferredLengths);

		int pixelLengthToDistribute = layoutSize.getValueByOrientation(orientation)-totalPreferredLength;

		layedoutDivisionsLengths = Arrays.copyOf(divisionsPreferredLengths, numberOfDivisions);

		if(pixelLengthToDistribute==0)
			return layedoutDivisionsLengths;

		List<Integer> indexesOfDivisionsToReadjust = getIndexOfDivisionsToReadjust(divisionsPreferredLengths,
				idealDivisionSize, pixelLengthToDistribute);

		adjustDivisionsLength(layedoutDivisionsLengths, indexesOfDivisionsToReadjust,
				pixelLengthToDistribute);

		return layedoutDivisionsLengths;
	}

	private static void adjustDivisionsLength(int[] divisionsLengths, List<Integer> indexesOfDivisionsToReadjust,
											  int lengthToDistribute)
	{
		Map<Integer, Float> weightOfDivisionsToReadjust = getWeightOfDivisions(divisionsLengths,
				indexesOfDivisionsToReadjust);

		float weight;
		int pixelToAdjust;

		for(int divisionIndex : indexesOfDivisionsToReadjust)
		{
			weight = weightOfDivisionsToReadjust.get(divisionIndex);
			pixelToAdjust = (int) (weight*lengthToDistribute);

			divisionsLengths[divisionIndex] += pixelToAdjust;

			lengthToDistribute -= pixelToAdjust;
		}

		int lastDivisionIndex = indexesOfDivisionsToReadjust.get(indexesOfDivisionsToReadjust.size()-1);

		if(lengthToDistribute!=0)
			divisionsLengths[lastDivisionIndex] += lengthToDistribute;
	}

	private static Map<Integer, Float> getWeightOfDivisions(int[] divisionsLengths, List<Integer> divisionsIndexes)
	{
		Map<Integer, Float> weightOfDivisions = new HashMap<>(divisionsIndexes.size());
		int totalDivisionsLength = getTotalLength(divisionsLengths, divisionsIndexes);
		int divisionLength;
		float divisionWeight;

		for(int divisionIndex : divisionsIndexes)
		{
			divisionLength = divisionsLengths[divisionIndex];
			divisionWeight = (float) divisionLength/(float) totalDivisionsLength;

			weightOfDivisions.put(divisionIndex, divisionWeight);
		}

		return weightOfDivisions;
	}

	private static int getTotalLength(int[] divisionsLengths, List<Integer> divisionsIndexes)
	{
		int totalLength = 0;

		for(int divisionIndex : divisionsIndexes)
		{
			totalLength += divisionsLengths[divisionIndex];
		}

		return totalLength;
	}

	private static List<Integer> getIndexOfDivisionsToReadjust(int[] divisionsPreferredLength, int idealDivisionSize,
														int pixelLengthToDistribute)
	{
		List<Integer> indexOfDivisionsToReadjust = new LinkedList<>();

		for(int i = 0; i<divisionsPreferredLength.length; i++)
		{
			if((pixelLengthToDistribute>0 && divisionsPreferredLength[i]<idealDivisionSize) ||
					(pixelLengthToDistribute<0 && divisionsPreferredLength[i]>idealDivisionSize))
				indexOfDivisionsToReadjust.add(i);
		}

		return indexOfDivisionsToReadjust;
	}

	private void layout(int[] columnsWidth, int[] rowsHeight)
	{
		Size2d layoutSize = getSize();
		List<Control> children = getChildren();

		RowLayout row;
		int positionY = 0;

		for(int i = 0; i<children.size(); i++)
		{
			row = (RowLayout) children.get(i);

			row.setColumnsWidth(columnsWidth);
			row.setPosition(new Position2d(0, positionY));
			row.setSize(new Size2d(layoutSize.getWidth(), rowsHeight[i]));

			positionY += rowsHeight[i];
		}
	}

	private Table getTable()
	{
		Table table = new Table();

		List<Control> children = getChildren();

		if(children==null || children.isEmpty())
			return table;

		RowLayout row;
		List<Control> rowCells;

		for(int y = 0; y<children.size(); y++)
		{
			row = (RowLayout) children.get(y);
			rowCells = row.getChildren();

			for(int x = 0; x<rowCells.size(); x++)
				table.putCell(new Position2d(x, y), new Cell(rowCells.get(x)));
		}

		return table;
	}

	private static int[] getDivisionsPreferredLength(Table table, Orientation2d orientation)
	{
		Orientation2d otherOrientation = Orientation2d.getOtherOrientation(orientation);
		int[] divisionsMaxLength = new int[table.getNumberOfCell(orientation)];
		Arrays.fill(divisionsMaxLength, 0);

		if(table.isEmpty())
			return divisionsMaxLength;

		Position2d position = new Position2d();
		Cell cell;
		Size2d cellPreferredSize;

		for(int e = 0; e<table.getNumberOfCell(otherOrientation); e++)
			for(int i = 0; i<table.getNumberOfCell(orientation); i++)
			{
				position.setValueByOrientation(orientation, i);
				position.setValueByOrientation(otherOrientation, e);

				cell = table.getCell(position);

				if(cell!=null)
				{
					cellPreferredSize = cell.getControl().getPreferredSize();

					if(cellPreferredSize!=null)
						divisionsMaxLength[i] = NumberUtils.max(divisionsMaxLength[i],
								cellPreferredSize.getValueByOrientation(orientation));
				}
			}

		return divisionsMaxLength;
	}

	@Override
	public Size2d getPreferredSize()
	{
		List<Control> children = getChildren();

		if(children==null || children.isEmpty())
			return new Size2d(0, 0);

		int height = 0;
		int maxWidth = 0;
		RowLayout row;

		for(Control control : children)
		{
			row = (RowLayout) control;

			maxWidth = NumberUtils.max(maxWidth, row.getPreferredSize().getWidth());
			height += row.getPreferredSize().getHeight();
		}

		return new Size2d(maxWidth, height);
	}
}
