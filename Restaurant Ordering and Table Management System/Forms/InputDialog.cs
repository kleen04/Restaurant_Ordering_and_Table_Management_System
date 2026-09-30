using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Restaurant_Ordering_and_Management_System.Helper;

namespace Restaurant_Ordering_and_Management_System.Forms
{
    /// <summary>Describes one input row in an <see cref="InputDialog"/>.</summary>
    public class DialogField
    {
        public string Label { get; private set; }
        public string Value { get; private set; }
        public string[] Choices { get; private set; }
        public bool IsDate { get; private set; }
        public DateTime DateValue { get; private set; }

        public static DialogField Text(string label, string value = "")
        {
            return new DialogField { Label = label, Value = value ?? "" };
        }

        public static DialogField Choice(string label, string value, params string[] choices)
        {
            return new DialogField { Label = label, Value = value, Choices = choices };
        }

        public static DialogField Date(string label, DateTime value)
        {
            return new DialogField { Label = label, IsDate = true, DateValue = value };
        }
    }

    /// <summary>
    /// One reusable, code-built Add/Edit dialog used by the Tables, Staff and
    /// Inventory forms (instead of three near-identical designer forms — DRY).
    /// The caller passes a validator; while it returns a message the dialog
    /// stays open and shows that message.
    /// </summary>
    public class InputDialog : Form
    {
        private readonly List<Control> _inputs = new List<Control>();
        private readonly Func<string[], string> _validator;

        public string[] Values { get; private set; }

        /// <summary>Shows the dialog. Returns the entered values (dates as yyyy-MM-dd), or null if cancelled.</summary>
        public static string[] Prompt(IWin32Window owner, string title, IList<DialogField> fields,
                                      Func<string[], string> validator)
        {
            using (InputDialog dialog = new InputDialog(title, fields, validator))
            {
                return dialog.ShowDialog(owner) == DialogResult.OK ? dialog.Values : null;
            }
        }

        private InputDialog(string title, IList<DialogField> fields, Func<string[], string> validator)
        {
            _validator = validator;

            Text = title;
            Font = new Font("Segoe UI", 9.75F);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            StartPosition = FormStartPosition.CenterParent;
            MaximizeBox = false;
            MinimizeBox = false;
            ShowInTaskbar = false;

            const int left = 20;
            const int labelWidth = 130;
            const int inputWidth = 240;
            const int rowHeight = 36;
            int y = 20;

            foreach (DialogField field in fields)
            {
                Label label = new Label
                {
                    Text = field.Label + ":",
                    Left = left,
                    Top = y + 3,
                    Width = labelWidth,
                    AutoSize = false
                };
                Controls.Add(label);

                Control input;
                if (field.IsDate)
                {
                    input = new DateTimePicker
                    {
                        Format = DateTimePickerFormat.Short,
                        Value = field.DateValue
                    };
                }
                else if (field.Choices != null)
                {
                    ComboBox combo = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList };
                    combo.Items.AddRange(field.Choices);
                    int index = Array.IndexOf(field.Choices, field.Value);
                    combo.SelectedIndex = index >= 0 ? index : 0;
                    input = combo;
                }
                else
                {
                    input = new TextBox { Text = field.Value };
                }

                input.Left = left + labelWidth;
                input.Top = y;
                input.Width = inputWidth;
                Controls.Add(input);
                _inputs.Add(input);

                y += rowHeight;
            }

            Button okButton = new Button { Text = "Save", Width = 90, Height = 30, Left = left + labelWidth + inputWidth - 190, Top = y + 10 };
            Button cancelButton = new Button { Text = "Cancel", Width = 90, Height = 30, Left = left + labelWidth + inputWidth - 90, Top = y + 10, DialogResult = DialogResult.Cancel };
            okButton.Click += OkButton_Click;
            Controls.Add(okButton);
            Controls.Add(cancelButton);

            AcceptButton = okButton;
            CancelButton = cancelButton;
            ClientSize = new Size(left + labelWidth + inputWidth + left, y + 60);
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            string[] values = ReadValues();

            string error = _validator == null ? null : _validator(values);
            if (error != null)
            {
                MessageHelper.ShowWarning(error, "Please check your input");
                return;
            }

            Values = values;
            DialogResult = DialogResult.OK;
        }

        private string[] ReadValues()
        {
            string[] values = new string[_inputs.Count];

            for (int i = 0; i < _inputs.Count; i++)
            {
                Control input = _inputs[i];
                DateTimePicker picker = input as DateTimePicker;
                ComboBox combo = input as ComboBox;

                if (picker != null)
                {
                    values[i] = picker.Value.ToString("yyyy-MM-dd");
                }
                else if (combo != null)
                {
                    values[i] = combo.SelectedItem == null ? "" : combo.SelectedItem.ToString();
                }
                else
                {
                    values[i] = input.Text.Trim();
                }
            }

            return values;
        }
    }
}
