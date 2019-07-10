using System;
using System.Drawing;
using System.Windows.Forms;
using TotalSmartCoding.Views.Commons;

namespace TotalSmartCoding
{
    public class CustomMsgBox
    {
        public static DialogResult Show(IWin32Window owner, string text)
        {
            return CustomMsgBox.Show(owner, text, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
        }

        public static DialogResult Show(IWin32Window owner, string text, string caption, MessageBoxButtons buttons)
        {
            return CustomMsgBox.Show(owner, text, caption, buttons, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
        }

        public static DialogResult Show(IWin32Window owner, string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            return CustomMsgBox.Show(owner, text, caption, buttons, icon, MessageBoxDefaultButton.Button1);
        }

        public static DialogResult Show(IWin32Window owner, string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton)
        {
            CustomMessageBox customMessageBox = new CustomMessageBox(text, caption, buttons, icon, defaultButton);
            DialogResult dialogResult = customMessageBox.ShowDialog(owner);

            customMessageBox.Dispose();
            return dialogResult;
        }

    }

    class CustomInputBox
    {
        public static DialogResult Show(string title, string promptText, bool password, ref string value)
        {
            Form form = new Form();
            Label label = new Label();
            TextBox textBox = new TextBox();
            Button buttonOk = new Button();
            Button buttonCancel = new Button();

            form.Font = new System.Drawing.Font("Calibri", 10.2F);
            form.Text = title;
            label.Text = promptText;
            textBox.Text = value;
            if (password) textBox.PasswordChar = '*';

            buttonOk.Text = "OK";
            buttonCancel.Text = "Cancel";
            buttonOk.DialogResult = DialogResult.OK;
            buttonCancel.DialogResult = DialogResult.Cancel;

            label.SetBounds(39, 20, 372, 13);
            textBox.SetBounds(42, 36, 372, 32);
            buttonOk.SetBounds(228, 72, 75, 32);
            buttonCancel.SetBounds(309, 72, 75, 32);

            label.AutoSize = true;
            textBox.Anchor = textBox.Anchor | AnchorStyles.Right;
            buttonOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

            form.ClientSize = new Size(439, 112);
            form.Controls.AddRange(new Control[] { label, textBox, buttonOk, buttonCancel });
            form.ClientSize = new Size(Math.Max(300, label.Right + 10), form.ClientSize.Height + label.Height - 5);
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.MinimizeBox = false;
            form.MaximizeBox = false;
            form.AcceptButton = buttonOk;
            form.CancelButton = buttonCancel;

            textBox.Top = textBox.Top + label.Height - 12;

            DialogResult dialogResult = form.ShowDialog();
            value = textBox.Text;
            value = value.Trim();

            form.Dispose();
            return dialogResult;
        }
    }
}
