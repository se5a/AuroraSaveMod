	public void method_324(Button button_0, FlowLayoutPanel flowLayoutPanel_0)
	{
		try
		{
			string name = button_0.Name;
			Cursor.Current = Cursors.WaitCursor;
			this.int_98 = Convert.ToInt32(button_0.Tag);
			foreach (object obj in flowLayoutPanel_0.Controls)
			{
				Control control = (Control)obj;
				control.BackColor = Color.FromArgb(0, 0, 64);
				if (control.Name == name)
				{
					control.BackColor = Color.FromArgb(0, 0, 120);
				}
			}
			if (!this.bool_3)
			{
				this.method_325();
			}
			else
			{
				this.bool_1 = false;
				do
				{
					this.method_325();
                    SaveGameMethods.SaveFreq(this, decimal_0);
					if (this.bool_1)
					{
						break;
					}
					Application.DoEvents();
				}
				while (this.bool_3);
			}
			this.method_446();
		}
		catch (Exception exception_)
		{
			GClass202.smethod_68(exception_, 153);
		}
		finally
		{
			Cursor.Current = Cursors.Default;
		}
	}