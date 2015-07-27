package net.forgottenschism.gui.impl;

import net.forgottenschism.gui.Control;
import net.forgottenschism.gui.FocusManager;
import org.newdawn.slick.GameContainer;
import org.newdawn.slick.Graphics;
import org.newdawn.slick.Input;

import java.util.LinkedList;
import java.util.List;

public class FocusManagerImpl implements FocusManager
{
	private List<Control> controls;
	private Integer selectionIndex;
	private boolean enabled;

	public FocusManagerImpl()
	{
		controls = new LinkedList<>();
		selectionIndex = null;
		enabled = true;
	}

	@Override
	public void addControl(Control control)
	{
		controls.add(control);

		if(selectionIndex==null && control.isEnabled() && control.canHaveFocus())
		{
			selectionIndex = controls.size()-1;
			control.setFocus(true);
		}
	}

	@Override
	public void removeControl(Control control)
	{
		controls.remove(control);

		if(controls.isEmpty())
			selectionIndex = null;
	}

	@Override
	public void keyReleased(int key, char character)
	{
		if(!enabled || controls.isEmpty())
			return;

		if(key==Input.KEY_DOWN)
		{
			focusNext();
			return;
		}

		if(key==Input.KEY_UP)
		{
			focusPrevious();
			return;
		}

		for(Control control : controls)
		{
			if(control.hasFocus())
				control.keyReleased(key, character);
		}
	}

	@Override
	public void keyPressed(int key, char character)
	{
		if(!enabled || controls.isEmpty())
			return;

		for(Control control : controls)
		{
			if(control.hasFocus())
				control.keyPressed(key, character);
		}
	}

	@Override
	public void update(GameContainer container, int delta)
	{
		if(!enabled || controls.isEmpty())
			return;

		for(Control control : controls)
		{
			if(control.isEnabled())
				control.update(container, delta);
		}
	}

	@Override
	public void render(GameContainer container, Graphics graphics)
	{
		for(Control control : controls)
		{
			if(control.isVisible())
				control.render(container, graphics);
		}
	}

	public void focusFirst()
	{
		if(controls.isEmpty())
			return;

		if(controls.size()==1)
		{
			Control control = controls.get(0);

			if(control.isEnabled() && control.canHaveFocus())
			{
				selectionIndex = 0;
				controls.get(selectionIndex).setFocus(true);
			}
		}
		else
		{
			selectionIndex = controls.size()-1;
			focusNext();
		}
	}

	public void focusNext()
	{
		if(controls.size()<2)
			return;

		Control oldFocusedControl = controls.get(selectionIndex);
		Integer newFocusedIndex = findNextFocusCapableControl(selectionIndex);

		if(newFocusedIndex!=null)
		{
			Control newFocusedControl = controls.get(newFocusedIndex);

			oldFocusedControl.setFocus(false);
			newFocusedControl.setFocus(true);

			selectionIndex = newFocusedIndex;
		}
	}

	public void focusPrevious()
	{
		if(controls.size()<2)
			return;

		Control oldFocusedControl = controls.get(selectionIndex);
		Integer newFocusedIndex = findPreviousFocusCapableControl(selectionIndex);

		if(newFocusedIndex!=null)
		{
			Control newFocusedControl = controls.get(newFocusedIndex);

			oldFocusedControl.setFocus(false);
			newFocusedControl.setFocus(true);

			selectionIndex = newFocusedIndex;
		}
	}

	private Integer findPreviousFocusCapableControl(int startIndex)
	{
		if(controls.isEmpty())
			return null;

		if(controls.size()==1)
		{
			Control control = controls.get(0);

			if(control.isEnabled() && control.canHaveFocus())
				return 0;
			else
				return null;
		}

		Control control;
		int index = startIndex-1;

		if(index<0)
			index = controls.size()-1;

		while(index!=startIndex)
		{
			control = controls.get(index);

			if(control.isEnabled() && control.canHaveFocus())
				return index;

			index--;

			if(index<0)
				index = controls.size()-1;
		}

		Control defaultFocusControl = controls.get(startIndex);

		if(defaultFocusControl.isEnabled() && defaultFocusControl.canHaveFocus())
			return startIndex;
		else
			return null;
	}

	private Integer findNextFocusCapableControl(int startIndex)
	{
		if(controls.isEmpty())
			return null;

		if(controls.size()==1)
		{
			Control control = controls.get(0);

			if(control.isEnabled() && control.canHaveFocus())
				return 0;
			else
				return null;
		}

		Control control;
		int index = startIndex+1;

		if(index>controls.size())
			index = 0;

		while(index!=startIndex)
		{
			control = controls.get(index);

			if(control.isEnabled() && control.canHaveFocus())
				return index;

			index++;

			if(index>controls.size())
				index = 0;
		}

		Control defaultFocusControl = controls.get(startIndex);

		if(defaultFocusControl.isEnabled() && defaultFocusControl.canHaveFocus())
			return startIndex;
		else
			return null;
	}
}
