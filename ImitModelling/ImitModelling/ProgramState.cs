using System;
using System.Windows.Forms;

namespace ImitModelling
{
	[Serializable]
	public class ProgramState
	{
		public enum MenuItemsNames
		{
			WallItemName,
			SpawnItemName,
			ExitItemName,
			CheckPointItemName,
			Size
		}
		public enum MouseHandlersNames
		{
			MouseDownName,
			MouseMoveName,
			MouseUpName,
			Size
		}
		protected bool[] MenuStates;
		[NonSerialized] protected MouseEventHandler[] MouseHandlers;
		public virtual void Reload(ImitationForm form)
		{
			MenuStates = null;
			MouseHandlers = null;
		}
		public ProgramState()
		{
		}
		public void LoadState(ImitationForm form)
		{
			form.LoadMenuEdit(MenuStates);
			form.LoadMouseHandlers(MouseHandlers);
		}
	}

	[Serializable]
	public class EditAllCellsState : ProgramState
	{
		public EditAllCellsState(ImitationForm form) : base()
		{
			Reload(form);
		}
		public override void Reload(ImitationForm form)
		{
			MenuStates = new bool[(int)MenuItemsNames.Size];
			MenuStates[(int)MenuItemsNames.WallItemName] = true;
			MenuStates[(int)MenuItemsNames.SpawnItemName] = true;
			MenuStates[(int)MenuItemsNames.ExitItemName] = true;
			MenuStates[(int)MenuItemsNames.CheckPointItemName] = false;
			MouseHandlers = new MouseEventHandler[(int)MouseHandlersNames.Size];
			MouseHandlers[(int)MouseHandlersNames.MouseDownName] = form.EditAll_PictureBox_MouseDown;
			MouseHandlers[(int)MouseHandlersNames.MouseMoveName] = form.EditAll_PictureBox_MouseMove;
			MouseHandlers[(int)MouseHandlersNames.MouseUpName] = form.EditAll_PictureBox_MouseUp;
		}
	}

	[Serializable]
	public class EditSpawnsState : ProgramState
	{
		public EditSpawnsState(ImitationForm form) : base()
		{
			Reload(form);
		}
		public override void Reload(ImitationForm form)
		{
			MenuStates = new bool[(int)MenuItemsNames.Size];
			MenuStates[(int)MenuItemsNames.WallItemName] = false;
			MenuStates[(int)MenuItemsNames.SpawnItemName] = false;
			MenuStates[(int)MenuItemsNames.ExitItemName] = false;
			MenuStates[(int)MenuItemsNames.CheckPointItemName] = false;
			MouseHandlers = new MouseEventHandler[(int)MouseHandlersNames.Size];
			MouseHandlers[(int)MouseHandlersNames.MouseDownName] = form.EditSpawn_PictureBox_MouseDown;
			MouseHandlers[(int)MouseHandlersNames.MouseMoveName] = form.EditSpawn_PictureBox_MouseMove;
			MouseHandlers[(int)MouseHandlersNames.MouseUpName] = form.EditSpawn_PictureBox_MouseUp;
		}
	}

	[Serializable]
	public class WorkingState : ProgramState
	{
		public WorkingState(ImitationForm form) : base()
		{
			Reload(form);
		}
		public override void Reload(ImitationForm form)
		{
			MenuStates = new bool[(int)MenuItemsNames.Size];
			MenuStates[(int)MenuItemsNames.WallItemName] = false;
			MenuStates[(int)MenuItemsNames.SpawnItemName] = false;
			MenuStates[(int)MenuItemsNames.ExitItemName] = false;
			MenuStates[(int)MenuItemsNames.CheckPointItemName] = false;
			MouseHandlers = new MouseEventHandler[(int)MouseHandlersNames.Size];
			MouseHandlers[(int)MouseHandlersNames.MouseDownName] = null;
			MouseHandlers[(int)MouseHandlersNames.MouseMoveName] = null;
			MouseHandlers[(int)MouseHandlersNames.MouseUpName] = null;
		}
	}

}