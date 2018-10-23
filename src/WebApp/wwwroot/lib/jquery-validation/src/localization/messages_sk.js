/*
 * Translated default messages for the jQuery validation plugin.
 * Locale: SK (Slovak; slovenina, slovensk jazyk)
 */
$.extend( $.validator.messages, {
	required: "Povinn zada.",
	maxlength: $.validator.format( "Maximlne {0} znakov." ),
	minlength: $.validator.format( "Minimlne {0} znakov." ),
	rangelength: $.validator.format( "Minimlne {0} a maximlne {1} znakov." ),
	email: "E-mailov adresa mus by platn.",
	url: "URL mus by platn.",
	date: "Mus by dtum.",
	number: "Mus by slo.",
	digits: "Me obsahova iba slice.",
	equalTo: "Dve hodnoty sa musia rovna.",
	range: $.validator.format( "Mus by medzi {0} a {1}." ),
	max: $.validator.format( "Neme by viac ako {0}." ),
	min: $.validator.format( "Neme by menej ako {0}." ),
	creditcard: "slo platobnej karty mus by platn."
} );
