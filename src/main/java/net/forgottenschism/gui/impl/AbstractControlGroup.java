package net.forgottenschism.gui.impl;

import net.forgottenschism.gui.Control;
import net.forgottenschism.gui.ControlGroup;

import java.util.LinkedList;
import java.util.List;

public abstract class AbstractControlGroup extends AbstractControl implements ControlGroup
{
	private List<Control> controls;

	public AbstractControlGroup()
	{
		controls = new LinkedList<>();
	}

	@Override
	public void addControl(Control control)
	{
		controls.add(control);
	}

	@Override
	public void removeControl(Control control)
	{
		controls.remove(control);
	}

	@Override
	public void removeAllChildren()
	{
		controls.clear();
	}

	@Override
	public List<Control> getChildren()
	{
		return controls;
	}
}
