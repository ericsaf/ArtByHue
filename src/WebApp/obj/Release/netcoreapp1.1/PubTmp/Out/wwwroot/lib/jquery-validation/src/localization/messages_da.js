/*
 * Translated default messages for the jQuery validation plugin.
 * Locale: DA (Danish; dansk)
 */
$.extend( $.validator.messages, {
	required: "Dette felt er pkrvet.",
	maxlength: $.validator.format( "Indtast hjst {0} tegn." ),
	minlength: $.validator.format( "Indtast mindst {0} tegn." ),
	rangelength: $.validator.format( "Indtast mindst {0} og hjst {1} tegn." ),
	email: "Indtast en gyldig email-adresse.",
	url: "Indtast en gyldig URL.",
	date: "Indtast en gyldig dato.",
	number: "Indtast et tal.",
	digits: "Indtast kun cifre.",
	equalTo: "Indtast den samme vrdi igen.",
	range: $.validator.format( "Angiv en vrdi mellem {0} og {1}." ),
	max: $.validator.format( "Angiv en vrdi der hjst er {0}." ),
	min: $.validator.format( "Angiv en vrdi der mindst er {0}." ),
	creditcard: "Indtast et gyldigt kreditkortnummer."
} );
