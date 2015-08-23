package net.forgottenschism.gamescreen;

import org.newdawn.slick.Color;

import net.forgottenschism.gui.Control;
import net.forgottenschism.gui.SelectionListener;
import net.forgottenschism.gui.bean.GraphicalUnit;
import net.forgottenschism.gui.control.Label;
import net.forgottenschism.gui.control.Link;
import net.forgottenschism.gui.control.Spinner;
import net.forgottenschism.gui.control.Textbox;
import net.forgottenschism.gui.impl.AbstractScreen;
import net.forgottenschism.gui.layout.RelativeLayout;
import net.forgottenschism.gui.layout.RelativeLayoutParameters;
import net.forgottenschism.gui.layout.RowLayout;
import net.forgottenschism.gui.layout.TableLayout;

public class CreateNewGameScreen extends AbstractScreen
{
	public CreateNewGameScreen()
	{
		setUpGui();
	}

	private void setUpGui() 
	{
		getMainWindow().setLayout(new RelativeLayout());
		
		TableLayout tableLayout = new TableLayout();
		RelativeLayoutParameters tableLayoutPosition = new RelativeLayoutParameters();
		tableLayoutPosition.horizontallyCentered();
		tableLayoutPosition.setTopPosition(10, GraphicalUnit.PERCENT);
		tableLayout.setLayoutParameters(tableLayoutPosition);
		addControl(tableLayout);
		
		RelativeLayoutParameters centered = new RelativeLayoutParameters();
		centered.horizontallyCentered();
		centered.verticalyCentered();
		
		RelativeLayoutParameters rightAligned = new RelativeLayoutParameters();
		rightAligned.verticalyCentered();
		rightAligned.setRightPosition(0, GraphicalUnit.PIXEL);
		
		RelativeLayoutParameters leftAligned = new RelativeLayoutParameters();
		leftAligned.verticalyCentered();
		leftAligned.setLeftPosition(0, GraphicalUnit.PIXEL);
				
		Label screenTitle = new Label("Create New Game");
		screenTitle.setTextColor(Color.yellow);
		screenTitle.setLayoutParameters(centered);
		
		Label lblName = new Label("Name: ");
		lblName.setLayoutParameters(rightAligned);
		
		Textbox txtName = new Textbox(8);
		txtName.setLayoutParameters(leftAligned);
		
		Label lblDifficulty = new Label("Difficulty: ");
		lblDifficulty.setLayoutParameters(rightAligned);
		
		Spinner spnrDifficulty = new Spinner(new String[]{"Pathetic", "Casual", "Hardcore", "Masochist"});
		spnrDifficulty.setLayoutParameters(leftAligned);
		
		Link linkStart = new Link("START", new SelectionListener()
		{
			@Override
			public void handleSelect(Control control) 
			{
				// start new game...
			}
		});
		linkStart.setLayoutParameters(centered);
		
		Control[][] tableContent = new Control[][]
		{
			new Control[] {screenTitle},
			new Control[] {lblName, txtName},
			new Control[] {lblDifficulty, spnrDifficulty},
			new Control[] {linkStart}
		};
		
		RowLayout row;
		for(Control[] rowContent : tableContent)
		{
			row = new RowLayout();

			for(Control control : rowContent)
				row.addControl(control);

			tableLayout.addControl(row);
		}
	}
}
