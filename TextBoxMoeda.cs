using System;
using System.Windows.Forms;

namespace Componentes {
    public class TextBoxMoeda : TextBox {

        protected override void OnEnter (EventArgs e) {
            base.OnEnter(e);
            MaxLength = 20;
        }

        protected override void OnTextChanged (EventArgs e) {
            base.OnTextChanged(e);
            string numTemp;
            double Value;

            try {
                numTemp = Text.Replace(",", "").Replace(".", "");
                if (numTemp.Equals("")) {
                    numTemp = "";
                }
                numTemp = numTemp.PadLeft(3, '0');
                if (numTemp.Length > 3 & numTemp.Substring(0, 1) == "0")
                    numTemp = numTemp.Substring(1, numTemp.Length - 1);

                Value = Convert.ToDouble(numTemp) / 100;
                Text = string.Format("{0:N}", Value);
                SelectionStart = Text.Length;
            } catch (Exception ex) {
                MessageBox.Show(ex.Message, "Entrada de valor", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        protected override void OnKeyPress (KeyPressEventArgs e) {
            base.OnKeyPress(e);
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back) {
                e.Handled = true;
            }
        }
    }
}
