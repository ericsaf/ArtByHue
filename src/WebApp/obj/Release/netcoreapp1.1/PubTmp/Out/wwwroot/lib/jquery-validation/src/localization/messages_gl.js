/*
 * Translated default messages for the jQuery validation plugin.
 * Locale: GL (Galician; Galego)
 */
( function( $ ) {
	$.extend( $.validator.messages, {
		required: "Este campo  obrigatorio.",
		remote: "Por favor, cubre este campo.",
		email: "Por favor, escribe unha direccin de correo vlida.",
		url: "Por favor, escribe unha URL vlida.",
		date: "Por favor, escribe unha data vlida.",
		dateISO: "Por favor, escribe unha data (ISO) vlida.",
		number: "Por favor, escribe un nmero vlido.",
		digits: "Por favor, escribe s dxitos.",
		creditcard: "Por favor, escribe un nmero de tarxeta vlido.",
		equalTo: "Por favor, escribe o mesmo valor de novo.",
		extension: "Por favor, escribe un valor cunha extensin aceptada.",
		maxlength: $.validator.format( "Por favor, non escribas mis de {0} caracteres." ),
		minlength: $.validator.format( "Por favor, non escribas menos de {0} caracteres." ),
		rangelength: $.validator.format( "Por favor, escribe un valor entre {0} e {1} caracteres." ),
		range: $.validator.format( "Por favor, escribe un valor entre {0} e {1}." ),
		max: $.validator.format( "Por favor, escribe un valor menor ou igual a {0}." ),
		min: $.validator.format( "Por favor, escribe un valor maior ou igual a {0}." ),
		nifES: "Por favor, escribe un NIF vlido.",
		nieES: "Por favor, escribe un NIE vlido.",
		cifES: "Por favor, escribe un CIF vlido."
	} );
}( jQuery ) );
