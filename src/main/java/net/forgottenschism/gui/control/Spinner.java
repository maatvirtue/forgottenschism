package net.forgottenschism.gui.control;

import net.forgottenschism.gui.event.ValueChangeListener;
import net.forgottenschism.world.Terrain;
import org.newdawn.slick.Color;
import org.newdawn.slick.Font;
import org.newdawn.slick.Graphics;
import org.newdawn.slick.Input;
import org.newdawn.slick.geom.Rectangle;

import net.forgottenschism.gui.bean.Size2d;
import net.forgottenschism.gui.event.KeyEvent;
import net.forgottenschism.gui.impl.AbstractControl;
import net.forgottenschism.gui.theme.ColorTheme;
import net.forgottenschism.gui.theme.ColorThemeElement;
import net.forgottenschism.gui.theme.Theme;

import java.util.Collection;
import java.util.LinkedList;
import java.util.List;

public class Spinner<OptionType> extends AbstractControl
{
	private static final ColorTheme DEFAULT_COLOR_THEME = Theme.getDefaultTheme().getColorTheme();

	private Font font;
	private Color backgroundColor;
	private List<OptionType> options;
	private Integer selectionIndex;
	private ValueChangeListener<OptionType> valueChangeListener;

	public Spinner()
	{
		this(new LinkedList<>());
	}

	public Spinner(List<OptionType> options)
	{
		this(options, null);
	}

	public Spinner(List<OptionType> options, ValueChangeListener<OptionType> valueChangeListener)
	{
		selectionIndex = null;
		font = Theme.getDefaultTheme().getDefaultFont();
		backgroundColor = DEFAULT_COLOR_THEME.getColor(ColorThemeElement.SPINNER_BACKGROUND_COLOR);
		this.valueChangeListener = valueChangeListener;
		this.options = new LinkedList<>();
		this.options.addAll(options);

		setSize(getPreferredSize());
	}

	public void addAll(Collection<OptionType> options)
	{
		this.options.addAll(options);

		if(selectionIndex==null)
			select(0);
	}

	public void add(OptionType option)
	{
		options.add(option);

		if(selectionIndex==null)
			select(0);
	}

	@Override
	public boolean isFocusable()
	{
		return true;
	}

	@Override
	public Size2d getPreferredSize()
	{
		int widestOptionText = 0;
		int optionTextWidth;
		String optionText;

		for(OptionType optionValue : options)
		{
			optionText = optionValue.toString();
			optionTextWidth = font.getWidth(optionText);

			if(optionTextWidth>widestOptionText)
				widestOptionText = optionTextWidth;
		}

		return new Size2d(widestOptionText+font.getWidth("<  >"), font.getHeight("W"));
	}

	@Override
	protected void renderControl(Graphics graphics)
	{
		Size2d controlSize = getSize();

		graphics.setColor(backgroundColor);
		graphics.fill(new Rectangle(0, 0, controlSize.getWidth(), controlSize.getHeight()));

		Color textColor = DEFAULT_COLOR_THEME.getStatusColor(isEnabled(), hasFocus());

		graphics.setFont(getFont());
		graphics.setColor(textColor);

		if(hasFocus())
			graphics.drawString("< "+getSelectionText()+" >", 0, 0);
		else
			graphics.drawString(getSelectionText(), 0, 0);
	}

	public OptionType getSelection()
	{
		if(selectionIndex==null)
			return null;

		return options.get(selectionIndex);
	}

	public String getSelectionText()
	{
		if(getSelection()==null)
			return null;
		else
			return getSelection().toString();
	}

	public void select(int index)
	{
		if(index<0 || index>options.size()-1)
			throw new IllegalArgumentException("selection is out of bounds!");

		this.selectionIndex = index;

		if(valueChangeListener!=null)
			valueChangeListener.valueChanged(this, getSelection());
	}

	public void select(OptionType option)
	{
		int optionIndex = options.indexOf(option);

		if(optionIndex==-1)
			throw new IllegalArgumentException("No such option: "+option);

		select(optionIndex);
	}

	@Override
	public void keyReleased(KeyEvent keyEvent)
	{
		if(keyEvent.getKeyCode()==Input.KEY_LEFT)
			selectPreviousOption();
		else if(keyEvent.getKeyCode()==Input.KEY_RIGHT)
			selectNextOption();
	}

	public void selectPreviousOption()
	{
		if(selectionIndex==null)
			return;

		if(selectionIndex==0)
			select(options.size()-1);
		else
			select(selectionIndex-1);
	}

	public void selectNextOption()
	{
		if(selectionIndex==null)
			return;

		if(selectionIndex==options.size()-1)
			select(0);
		else
			select(selectionIndex+1);
	}

	public Font getFont()
	{
		return font;
	}

	public void setFont(Font font)
	{
		this.font = font;
	}

	public ValueChangeListener<OptionType> getValueChangeListener()
	{
		return valueChangeListener;
	}

	public void setValueChangeListener(ValueChangeListener<OptionType> valueChangeListener)
	{
		this.valueChangeListener = valueChangeListener;
	}
}
