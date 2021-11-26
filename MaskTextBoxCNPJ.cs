using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Componentes {
    class MaskTextBoxCNPJ : MaskedTextBox {

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

            Mask = AplicaMascara();
        }

        protected override void OnLeave (EventArgs e) {
            base.OnLeave(e);
            if (ValidaSeTemNumero()) {
                if (!ValidaCNPJ(Text)) {
                    MessageBox.Show("O CNPJ inserido é inválido, digite novamente.", "Validação do CNPJ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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

        public static bool ValidaCNPJ (string cnpj) {

            cnpj = cnpj.Replace(".", "").Replace("/", "").Replace("-", "");

            switch (cnpj) {
                case "11111111111111": return false;
                case "22222222222222": return false;
                case "33333333333333": return false;
                case "44444444444444": return false;
                case "55555555555555": return false;
                case "66666666666666": return false;
                case "77777777777777": return false;
                case "88888888888888": return false;
                case "99999999999999": return false;
            }

            if (cnpj.Length < 14) {
                return false;
            }

            int soma1 = 0;
            int soma2 = 0;
            List<int> list = new List<int>();
            // Nesse loop é criado um list com os numeros do CNPJ passado por parametro
            for (int i = 0; i < cnpj.Length; i++) {
                list.Add(Convert.ToInt32(cnpj.Substring(i, 1)));
            }
            // agora é feito o somatório acumulado do produto das 4 posições iniciais do list pela variável cont em decremento
            // ficando list[0] * 5, list[0] * 4, list[0] * 3, list[0] * 2
            int cont = 5;
            for (int i = 0; i < 4; i++) {
                soma1 += list[i] * cont;
                cont--;
            }
            // agora é feito o somatório acumulado do produto da posição 5 até a ultima posição da lista pela variável cont em decremento
            // ficando list[0] * 9, list[0] * 8, list[0] * 7, list[0] * 6...
            // quando cont é 2 o loop é parado com o comando break
            cont = 9;
            for (int i = 4; i < list.Count; i++) {
                soma1 += list[i] * cont;
                cont--;
                if (cont == 1) break;
            }
            // aqui encontramos o primeiro dígito verificador
            int primeiroVerificador = soma1 % 11 < 2 ? 0 : 11 - soma1 % 11;

            // essa parte apenas refaz o procedimentos explicados acima, porém com a diferença que agora cont vale 6
            cont = 6;
            for (int i = 0; i < 5; i++) {
                soma2 += list[i] * cont;
                cont--;
            }
            cont = 9;
            for (int i = 5; i < list.Count; i++) {
                soma2 += list[i] * cont;
                cont--;
                if (cont == 1) break;
            }
            int segundoVerificador = soma2 % 11 < 2 ? 0 : 11 - soma2 % 11;

            return string.Concat(primeiroVerificador, segundoVerificador).Equals(cnpj.Substring(12, 2));
        }

        private string AplicaMascara () {
            Culture = new System.Globalization.CultureInfo("en-US");
            return "00.000.000/0000-00";
        }
    }
}
