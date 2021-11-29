using System;
using System.Windows.Forms;

namespace Componentes {
    class MaskTextBoxCPF : MaskedTextBox {


        protected override void OnEnter (EventArgs e) {
            base.OnEnter(e);
            if (ValidaSeTemNumero()) {
                Select();
            } else {
                Mask = string.Empty;
            }
        }

        protected override void OnKeyDown (KeyEventArgs e) {
            base.OnKeyDown(e);
            //Culture = new System.Globalization.CultureInfo("en-US");
            Mask = AplicaMascara();
        }

        protected override void OnLeave (EventArgs e) {
            base.OnLeave(e);
            if (ValidaSeTemNumero()) {
                if (!ValidaCPF(Text)) {
                    MessageBox.Show("O CPF inserido é inválido, digite novamente.", "Validação do CPF", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    Select();
                } else {
                    return;
                }
            }
            Mask = AplicaMascara();
        }

        private bool ValidaSeTemNumero () {
            return Text.Contains("1") || Text.Contains("2") || Text.Contains("3") || Text.Contains("4") || Text.Contains("5") || Text.Contains("6") || Text.Contains("7") || Text.Contains("8") || Text.Contains("9");
        }

        private bool ValidaCPF (string cpf) {
            cpf = cpf.Replace(".", "").Replace("-", "");

            switch (cpf) {
                case "11111111111": return false;
                case "22222222222": return false;
                case "33333333333": return false;
                case "44444444444": return false;
                case "55555555555": return false;
                case "66666666666": return false;
                case "77777777777": return false;
                case "88888888888": return false;
                case "99999999999": return false;
            }
            if (cpf.Length != 11) {
                return false;
            }

            int soma1 = 0;
            int cont = 10;
            for (int i = 0; i < cpf.Length; i++) {
                soma1 += Convert.ToInt32(cpf.Substring(i, 1)) * cont;
                cont--;
                if (cont == 1) break;
            }
            int digitoVerificador1 = 11 - (soma1 % 11) >= 10 ? 0 : 11 - (soma1 % 11);

            int soma2 = 0;
            cont = 11;
            for (int i = 0; i < cpf.Length; i++) {
                soma2 += Convert.ToInt32(cpf.Substring(i, 1)) * cont;
                cont--;
                if (cont == 1) break;
            }
            int digitoVerificador2 = 11 - (soma2 % 11) >= 10 ? 0 : 11 - (soma2 % 11);

            return string.Concat(digitoVerificador1, digitoVerificador2).Equals(cpf.Substring(9, 2));
        }

        private string AplicaMascara () {
            Culture = new System.Globalization.CultureInfo("en-US");
            return "000.000.000-00";
        }
    }
}
