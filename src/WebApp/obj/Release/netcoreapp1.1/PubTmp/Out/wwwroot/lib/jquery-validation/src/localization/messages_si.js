/*
 * Translated default messages for the jQuery validation plugin.
 * Locale: SI (Slovenian)
 */
$.extend( $.validator.messages, {
	required: "To polje je obvezno.",
	remote: "Vpis v tem polju ni v pravi obliki.",
	email: "Prosimo, vnesite pravi email naslov.",
	url: "Prosimo, vnesite pravi URL.",
	date: "Prosimo, vnesite pravi datum.",
	dateISO: "Prosimo, vnesite pravi datum (ISO).",
	number: "Prosimo, vnesite pravo tevilko.",
	digits: "Prosimo, vnesite samo tevilke.",
	creditcard: "Prosimo, vnesite pravo tevilko kreditne kartice.",
	equalTo: "Prosimo, ponovno vnesite enako vsebino.",
	extension: "Prosimo, vnesite vsebino z pravo konnico.",
	maxlength: $.validator.format( "Prosimo, da ne vnaate ve kot {0} znakov." ),
	minlength: $.validator.format( "Prosimo, vnesite vsaj {0} znakov." ),
	rangelength: $.validator.format( "Prosimo, vnesite od {0} do {1} znakov." ),
	range: $.validator.format( "Prosimo, vnesite vrednost med {0} in {1}." ),
	max: $.validator.format( "Prosimo, vnesite vrednost manjo ali enako {0}." ),
	min: $.validator.format( "Prosimo, vnesite vrednost vejo ali enako {0}." )
} );
