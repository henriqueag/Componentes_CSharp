using System;
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
            // Remove as pontuações
            cnpj = cnpj.Replace(".", "").Replace("/", "").Replace("-", "");

            // Verifica se a escrita veio de alguma dessas formas e se o tamanho é diferente de 14
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

            if (cnpj.Length != 14) {
                return false;
            }

            int soma1 = 0;
            int cont = 5;
            // Percorre a string e em cada iteração faz a conversão para int da posição da string referente ao i em uma posição a frente
            // Decrementa a variável cont
            // A cada iteração faz a soma acumulada na variável soma1
            for (int i = 0; i < 4; i++) {
                soma1 += Convert.ToInt32(cnpj.Substring(i, 1)) * cont;
                cont--;
            }
            // Aqui é o mesmo procedimento de cima, porém nessa parte o cont começa com 9 e quando chega em 1 para o loop for
            cont = 9;
            for (int i = 4; i < cnpj.Length; i++) {
                soma1 += Convert.ToInt32(cnpj.Substring(i, 1)) * cont;
                cont--;
                if (cont == 1) break;
            }
            // aqui encontramos o primeiro dígito verificador
            int primeiroVerificador = soma1 % 11 < 2 ? 0 : 11 - soma1 % 11;

            // essa parte apenas refaz o procedimentos explicados acima, porém com a diferença que agora cont vale 6
            int soma2 = 0;
            cont = 6;
            for (int i = 0; i < 5; i++) {
                soma2 += Convert.ToInt32(cnpj.Substring(i, 1)) * cont;
                cont--;
            }
            cont = 9;
            for (int i = 5; i < cnpj.Length; i++) {
                soma2 += Convert.ToInt32(cnpj.Substring(i, 1)) * cont;
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
