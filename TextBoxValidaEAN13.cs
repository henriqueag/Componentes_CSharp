using System;
using System.Windows.Forms;

namespace Componentes {
    class TextBoxValidaEAN13 : TextBox {

        protected override void OnKeyPress (KeyPressEventArgs e) {
            base.OnKeyPress(e);
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back) {
                e.Handled = true;
            }
        }

        protected override void OnEnter (EventArgs e) {
            base.OnEnter(e);
            MaxLength = 13;
        }

        protected override void OnLeave (EventArgs e) {
            base.OnLeave(e);
            if (!ValidaEAN13(Text)) {
                var resultado = MessageBox.Show("Código EAN13 inserido é inválido.\nDeseja corrigí-lo?", "Validação EAN13", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                if (resultado == DialogResult.Yes) {
                    Select();
                } else {
                    if (Text.Length > 0) {
                        Text = Convert.ToInt64(Text).ToString("0000000000000");
                    } else {
                        return;
                    }
                }
            }
        }

        private bool ValidaEAN13 (string ean13) {
            if (ean13.Length != 13) {
                return false;
            }
            int soma = 0;
            for (int i = 0; i < ean13.Length - 1; i++) {
                if (i % 2 == 0) {
                    soma += Convert.ToInt32(ean13.Substring(i, 1)) * 1;
                } else {
                    soma += Convert.ToInt32(ean13.Substring(i, 1)) * 3;
                }
            }
            int verificador = ((soma / 10 + 1) * 10 - soma) % 10 == 0 ? 0 : (soma / 10 + 1) * 10 - soma;
            return verificador.Equals(Convert.ToInt32(ean13.Substring(12)));
        }
    }
}
