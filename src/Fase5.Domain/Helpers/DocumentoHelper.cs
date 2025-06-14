using System.Text.RegularExpressions;

namespace Fase5.Domain.Helpers;

public static class DocumentoHelper
{
    // CPF — algoritmo oficial + formatação opcional (000.000.000-00 ou 00000000000)
    public static bool CpfValido(string? cpf)
    {
        if (string.IsNullOrWhiteSpace(cpf)) return false;

        cpf = Regex.Replace(cpf, "[^0-9]", "");
        if (cpf.Length != 11 || new string(cpf[0], 11) == cpf) return false;

        int[] multiplicadores1 = { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
        int[] multiplicadores2 = { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

        string tempCpf = cpf[..9];
        int soma = 0;

        for (int i = 0; i < 9; i++)
            soma += int.Parse(tempCpf[i].ToString()) * multiplicadores1[i];

        int resto = soma % 11;
        int digito1 = resto < 2 ? 0 : 11 - resto;
        tempCpf += digito1;

        soma = 0;
        for (int i = 0; i < 10; i++)
            soma += int.Parse(tempCpf[i].ToString()) * multiplicadores2[i];

        resto = soma % 11;
        int digito2 = resto < 2 ? 0 : 11 - resto;

        return cpf.EndsWith($"{digito1}{digito2}");
    }

    // CRM – regra simplificada: 2-3 letras + 4-6 dígitos (ex.: “SP123456”)
    public static bool CrmValido(string? crm)
        => !string.IsNullOrWhiteSpace(crm)
        && Regex.IsMatch(crm, @"^[A-Z]{2,3}[0-9]{4,6}$", RegexOptions.IgnoreCase);
}