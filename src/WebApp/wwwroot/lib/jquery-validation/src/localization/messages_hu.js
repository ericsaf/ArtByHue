/*
 * Translated default messages for the jQuery validation plugin.
 * Locale: HU (Hungarian; Magyar)
 */
$.extend( $.validator.messages, {
	required: "Ktelez megadni.",
	maxlength: $.validator.format( "Legfeljebb {0} karakter hossz legyen." ),
	minlength: $.validator.format( "Legalbb {0} karakter hossz legyen." ),
	rangelength: $.validator.format( "Legalbb {0} s legfeljebb {1} karakter hossz legyen." ),
	email: "rvnyes e-mail cmnek kell lennie.",
	url: "rvnyes URL-nek kell lennie.",
	date: "Dtumnak kell lennie.",
	number: "Szmnak kell lennie.",
	digits: "Csak szmjegyek lehetnek.",
	equalTo: "Meg kell egyeznie a kt rtknek.",
	range: $.validator.format( "{0} s {1} kz kell esnie." ),
	max: $.validator.format( "Nem lehet nagyobb, mint {0}." ),
	min: $.validator.format( "Nem lehet kisebb, mint {0}." ),
	creditcard: "rvnyes hitelkrtyaszmnak kell lennie.",
	remote: "Krem javtsa ki ezt a mezt.",
	dateISO: "Krem rjon be egy rvnyes dtumot (ISO)."
} );
