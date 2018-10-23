/*
 * Translated default messages for the jQuery validation plugin.
 * Locale: RO (Romanian, limba romn)
 */
$.extend( $.validator.messages, {
	required: "Acest cmp este obligatoriu.",
	remote: "Te rugm s completezi acest cmp.",
	email: "Te rugm s introduci o adres de email valid",
	url: "Te rugm sa introduci o adres URL valid.",
	date: "Te rugm s introduci o dat corect.",
	dateISO: "Te rugm s introduci o dat (ISO) corect.",
	number: "Te rugm s introduci un numr ntreg valid.",
	digits: "Te rugm s introduci doar cifre.",
	creditcard: "Te rugm s introduci un numar de carte de credit valid.",
	equalTo: "Te rugm s reintroduci valoarea.",
	extension: "Te rugm s introduci o valoare cu o extensie valid.",
	maxlength: $.validator.format( "Te rugm s nu introduci mai mult de {0} caractere." ),
	minlength: $.validator.format( "Te rugm s introduci cel puin {0} caractere." ),
	rangelength: $.validator.format( "Te rugm s introduci o valoare ntre {0} i {1} caractere." ),
	range: $.validator.format( "Te rugm s introduci o valoare ntre {0} i {1}." ),
	max: $.validator.format( "Te rugm s introduci o valoare egal sau mai mic dect {0}." ),
	min: $.validator.format( "Te rugm s introduci o valoare egal sau mai mare dect {0}." )
} );
