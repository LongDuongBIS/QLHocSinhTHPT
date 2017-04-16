using System;
using System.Windows.Forms;

namespace QuanLyTruongCap3.Components
{
    public class CalendarColumn : DataGridViewColumn
    {
        public CalendarColumn() : base(new CalendarCell())
        {
        }

        public CalendarColumn(DataGridViewCell cellTemplate) : base(cellTemplate)
        {
        }

        public override DataGridViewCell CellTemplate
        {
            get
            {
                return base.CellTemplate;
            }
            set
            {
                if (value != null && !value.GetType().IsAssignableFrom(typeof(CalendarCell)))
                    throw new InvalidCastException("Must be a CalendarCell");
                base.CellTemplate = value;
            }
        }
    }

    public class CalendarCell : DataGridViewTextBoxCell
    {
        public CalendarCell()
        {
            this.Style.Format = "dd/MM/yyyy";
        }

        public override object DefaultNewRowValue
        {
            get { return DateTime.Now; }
        }

        public override Type EditType
        {
            get { return typeof(CalendarEditingControl); }
        }

        public override Type ValueType
        {
            get { return typeof(DateTime); }
        }

        public override void InitializeEditingControl(int rowIndex, object initialFormattedValue, DataGridViewCellStyle dataGridViewCellStyle)
        {
            base.InitializeEditingControl(rowIndex, initialFormattedValue, dataGridViewCellStyle);
            var ctl = DataGridView.EditingControl as CalendarEditingControl;
            ctl.Value = (DateTime)this.Value;
        }
    }

    public class CalendarEditingControl : DateTimePicker, IDataGridViewEditingControl
    {

        public CalendarEditingControl()
        {
            this.Format = DateTimePickerFormat.Short;
        }

        public DataGridView EditingControlDataGridView { get; set; }

        public object EditingControlFormattedValue
        {
            get
            {
                return this.Value.ToShortDateString();
            }
            set
            {
                if ((value != null) && (value is string))
                    this.Value = DateTime.Parse((string)value);
            }
        }

        public int EditingControlRowIndex { get; set; }

        public bool EditingControlValueChanged { get; set; }

        public Cursor EditingPanelCursor
        {
            get { return base.Cursor; }
        }

        public bool RepositionEditingControlOnValueChange
        {
            get { return false; }
        }

        public void ApplyCellStyleToEditingControl(DataGridViewCellStyle dataGridViewCellStyle)
        {
            this.Font = dataGridViewCellStyle.Font;
            this.CalendarForeColor = dataGridViewCellStyle.ForeColor;
            this.CalendarMonthBackground = dataGridViewCellStyle.BackColor;
        }

        public bool EditingControlWantsInputKey(Keys key, bool dataGridViewWantsInputKey)
        {
            switch (key & Keys.KeyCode)
            {
                case Keys.Left:
                case Keys.Up:
                case Keys.Down:
                case Keys.Right:
                case Keys.Home:
                case Keys.End:
                case Keys.PageDown:
                case Keys.PageUp:
                    return true;

                default:
                    return false;
            }
        }

        public object GetEditingControlFormattedValue(DataGridViewDataErrorContexts context)
        {
            return EditingControlFormattedValue;
        }

        public void PrepareEditingControlForEdit(bool selectAll)
        {
            //No preparation needs to be done.
        }

        protected override void OnValueChanged(EventArgs eventargs)
        {
            EditingControlValueChanged = true;
            this.EditingControlDataGridView.NotifyCurrentCellDirty(true);
            base.OnValueChanged(eventargs);
        }
    }
}