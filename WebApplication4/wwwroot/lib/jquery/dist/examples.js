$(function () {
    $('.Cliente.Cpf').mask('000.000.000-00');
    $('#Cliente_Cpf').mask('000.000.000-00');
    $('Cpf').mask('000.000.000-00');
    $('#Cpf').mask('000.000.000-00');
    $('#date').mask('00/00/0000');
    $('#dt_date').mask('00/00/0000');
    $('#Data').mask('00/00/0000', { placeholder: "__/__/____" });
    $('.Data').mask('00/00/0000', { placeholder: "__/__/____" });
    $('#dt_vencimento').mask('00/00/0000');
    $('#time').mask('00:00:00');
    $('#hora_enca').mask('00:00');
    $('.date_time').mask('00/00/0000 00:00:00');
    $('#cep').mask('00000-000', { placeholder: "_____-___" });
    $('#a_cep').mask('00000-000', { placeholder: "_____-___" });
    $('#b_cep').mask('00000-000', { placeholder: "_____-___" });
    $('#c_cep').mask('00000-000', { placeholder: "_____-___" });
    $('#d_cep').mask('00000-000', { placeholder: "_____-___" });
    $('#e_cep').mask('00000-000', { placeholder: "_____-___" });
    $('#f_cep').mask('00000-000', { placeholder: "_____-___" });
    $('.phone').mask('0000-0000');
    $('.phone_with_ddd').mask('(00) 0000-0000');
    $('.phone_us').mask('(000) 000-0000');
    $('.mixed').mask('AAA 000-S0S');
    $('.ip_address').mask('099.099.099.099');
    $('.percent').mask('##0,00%', { reverse: true });
    $('.clear-if-not-match').mask("00/00/0000", { clearIfNotMatch: true });
    $('#dt_publicado').mask("00/00/0000", { placeholder: "__/__/____" });
    $('#nasci_dep').mask("00/00/0000", { placeholder: "__/__/____" });
    $('#datanasc').mask("00/00/0000", { placeholder: "__/__/____" });
    $('#a_datanasc').mask("00/00/0000", { placeholder: "__/__/____" });
    $('#d_nascimento').mask("00/00/0000", { placeholder: "__/__/____" });
    $('#dt_nascimento').mask("00/00/0000", { placeholder: "__/__/____" });
    $('#conv_vencto').mask("00/00/0000", { placeholder: "__/__/____" });
    $('#a_modificado').mask("00/00/0000", { placeholder: "__/__/____" });

    $('#c_vencimento').mask("00/00/0000", { placeholder: "__/__/____" });
    $('#c_faturamento').mask("00/00/0000", { placeholder: "__/__/____" });

    $('#fone').mask("(99) 9999-9999", { placeholder: "(__) _____-____" });
    $('#celular').mask("(99) 9999-9999", { placeholder: "(__) _____-____" });
    $('#d_fone_res').mask("(99) 9999-9999", { placeholder: "(__) _____-____" });
    $('#d_fone_com').mask("(99) 9999-9999", { placeholder: "(__) _____-____" });
    $('#d_celular').mask("(99) 99999-9999", { placeholder: "(__) _____-____" });
    $('#d_whatsapp').mask("(99) 99999-9999", { placeholder: "(__) _____-____" });
    $('#c_fone_res').mask("(99) 9999-9999", { placeholder: "(__) _____-____" });
    $('#b_fone_res').mask("(99) 9999-9999", { placeholder: "(__) _____-____" });
    $('#b_fone_col').mask("(99) 9999-9999", { placeholder: "(__) _____-____" });
    $('#b_celular').mask("(99) 99999-9999", { placeholder: "(__) _____-____" });
    $('#b_fone1').mask("(99) 9999-9999", { placeholder: "(__) _____-____" });
    $('#b_fone2').mask("(99) 9999-9999", { placeholder: "(__) _____-____" });

    $('#a_telefone').mask("(99) 9999-9999", { placeholder: "(__) _____-____" });
    $('#a_telefone1').mask("(99) 9999-9999", { placeholder: "(__) _____-____" });
    $('#a_telefone2').mask("(99) 9999-9999", { placeholder: "(__) _____-____" });
    $('#a_whatsapp').mask("(99) 9999-9999", { placeholder: "(__) _____-____" });
    $('#a_celular').mask("(99) 9999-9999", { placeholder: "(__) _____-____" });

    $('#cnpj').mask("99.999.999/9999-99", { placeholder: "__.___.___/____-__" });
    $('#a_cnpj').mask("99.999.999/9999-99", { placeholder: "__.___.___/____-__" });
    $('#b_cnpj').mask("99.999.999/9999-99", { placeholder: "__.___.___/____-__" });
    $('#c_cnpj').mask("99.999.999/9999-99", { placeholder: "__.___.___/____-__" });
    $('#d_cnpj').mask("99.999.999/9999-99", { placeholder: "__.___.___/____-__" });
    $('#f_cnpj').mask("99.999.999/9999-99", { placeholder: "__.___.___/____-__" });
    $('#g_cnpj').mask("99.999.999/9999-99", { placeholder: "__.___.___/____-__" });

    $('#c_fone_col1').mask("(99) 9999-9999", { placeholder: "(__) _____-____" });
    $('#c_fone_col2').mask("(99) 9999-9999", { placeholder: "(__) _____-____" });

    $('#c_celular1').mask("(99) 9999-9999", { placeholder: "(__) _____-____" });
    $('#c_celular2').mask("(99) 9999-9999", { placeholder: "(__) _____-____" });
    $('#c_recados').mask("(99) 9999-9999", { placeholder: "(__) _____-____" });
    $('#c_comercial1').mask("(99) 9999-9999", { placeholder: "(__) _____-____" });
    $('#c_comercial2').mask("(99) 9999-9999", { placeholder: "(__) _____-____" });
    $('.fallback').mask("00r00r0000", {
        translation: {
            'r': {
                pattern: /[\/]/,
                fallback: '/'
            },
            placeholder: "__/__/____"
        }
    });

    $('.selectonfocus').mask("00/00/0000", { selectOnFocus: true });

    $('.cep_with_callback').mask('00000-000', {
        onComplete: function (cep) {
            console.log('Mask is done!:', cep);
        },
        onKeyPress: function (cep, event, currentField, options) {
            console.log('An key was pressed!:', cep, ' event: ', event, 'currentField: ', currentField.attr('class'), ' options: ', options);
        },
        onInvalid: function (val, e, field, invalid, options) {
            var error = invalid[0];
            console.log("Digit: ", error.v, " is invalid for the position: ", error.p, ". We expect something like: ", error.e);
        }
    });

    $('.crazy_cep').mask('00000-000', {
        onKeyPress: function (cep, e, field, options) {
            var masks = ['00000-000', '0-00-00-00'];
            mask = (cep.length > 7) ? masks[1] : masks[0];
            $('.crazy_cep').mask(mask, options);
        }
    });

    $('.cnpj').mask('00.000.000/0000-00', { reverse: true });
    $('#cnpj').mask('00.000.000/0000-00', { reverse: true });
    $('.money').mask('#.##0,00', { reverse: true });
    $('#Valor_principal').mask('##.###.##0,00', { reverse: true });
    $('#Valor_atualizado').mask('##.###.##0,00', { reverse: true });
    $('#Valor').mask('##.###.##0,00', { reverse: true });
    
    var SPMaskBehavior = function (val) {
        return val.replace(/\D/g, '').length === 11 ? '(00) 00000-0000' : '(00) 0000-00009';
    },
        spOptions = {
            onKeyPress: function (val, e, field, options) {
                field.mask(SPMaskBehavior.apply({}, arguments), options);
            }
        };

    $('.sp_celphones').mask(SPMaskBehavior, spOptions);

    $(".bt-mask-it").click(function () {
        $(".mask-on-div").mask("000.000.000-00");
        $(".mask-on-div").fadeOut(500).fadeIn(500)
    })

    $('pre').each(function (i, e) { hljs.highlightBlock(e) });
});


