/*
 * Translated default messages for the jQuery validation plugin.
 * Locale: TR (Turkish; Trke)
 */
$.extend( $.validator.messages, {
	required: "Bu alann doldurulmas zorunludur.",
	remote: "Ltfen bu alan dzeltin.",
	email: "Ltfen geerli bir e-posta adresi giriniz.",
	url: "Ltfen geerli bir web adresi (URL) giriniz.",
	date: "Ltfen geerli bir tarih giriniz.",
	dateISO: "Ltfen geerli bir tarih giriniz(ISO formatnda)",
	number: "Ltfen geerli bir say giriniz.",
	digits: "Ltfen sadece saysal karakterler giriniz.",
	creditcard: "Ltfen geerli bir kredi kart giriniz.",
	equalTo: "Ltfen ayn deeri tekrar giriniz.",
	extension: "Ltfen geerli uzantya sahip bir deer giriniz.",
	maxlength: $.validator.format( "Ltfen en fazla {0} karakter uzunluunda bir deer giriniz." ),
	minlength: $.validator.format( "Ltfen en az {0} karakter uzunluunda bir deer giriniz." ),
	rangelength: $.validator.format( "Ltfen en az {0} ve en fazla {1} uzunluunda bir deer giriniz." ),
	range: $.validator.format( "Ltfen {0} ile {1} arasnda bir deer giriniz." ),
	max: $.validator.format( "Ltfen {0} deerine eit ya da daha kk bir deer giriniz." ),
	min: $.validator.format( "Ltfen {0} deerine eit ya da daha byk bir deer giriniz." ),
	require_from_group: "Ltfen bu alanlarn en az {0} tanesini doldurunuz."
} );
